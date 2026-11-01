using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sec2_Group1_HappyJumpingFrog
{
    public static class SoundManager
    {
        public static bool SfxOn = true;

        // ---------- เพลงพื้นหลัง ----------
        private static WaveOutEvent _bgmOut;
        private static LoopStream _bgmLoop;

        public static void PlayBgm()
        {
            if (_bgmOut != null) return;   // กำลังเล่นอยู่แล้ว

            try
            {
                var reader = new WaveFileReader(Properties.Resources.bgm);
                _bgmLoop = new LoopStream(reader);
                _bgmOut = new WaveOutEvent { Volume = 0.5f };   // ปรับความดัง 0.0 - 1.0
                _bgmOut.Init(_bgmLoop);
                _bgmOut.Play();
            }
            catch
            {
                StopBgm();
            }
        }

        public static void StopBgm()
        {
            _bgmOut?.Stop();
            _bgmOut?.Dispose();
            _bgmLoop?.Dispose();
            _bgmOut = null;
            _bgmLoop = null;
        }

        // ---------- เสียงเอฟเฟกต์ (โหลดไว้ในหน่วยความจำ + mixer) ----------
        private static MixingSampleProvider _mixer;
        private static WaveOutEvent _sfxOut;
        private static readonly Dictionary<string, CachedSound> _cache = new Dictionary<string, CachedSound>();
        private static bool _warned = false;

        // เรียกตอนเริ่มเกมเพื่อโหลดเสียงไว้ล่วงหน้า (กันกระตุกครั้งแรก)
        public static void InitSfx(params string[] names)
        {
            try
            {
                EnsureMixer();
                foreach (string n in names) GetSound(n);
            }
            catch (Exception ex)
            {
                Warn("เตรียมเสียงไม่ได้: " + ex.Message);
            }
        }

        private static void EnsureMixer()
        {
            if (_sfxOut != null) return;

            _mixer = new MixingSampleProvider(CachedSound.Format) { ReadFully = true };
            _sfxOut = new WaveOutEvent { DesiredLatency = 60, NumberOfBuffers = 2 };
            _sfxOut.Init(_mixer);
            _sfxOut.Play();
        }

        private static CachedSound GetSound(string name)
        {
            if (_cache.TryGetValue(name, out CachedSound cached)) return cached;

            string dir = Path.Combine(AppContext.BaseDirectory, "Sounds");
            if (!Directory.Exists(dir))
            {
                Warn("ไม่พบโฟลเดอร์:\n" + dir);
                return null;
            }

            string path = Directory.GetFiles(dir, name + ".*").FirstOrDefault();
            if (path == null)
            {
                Warn("ไม่พบไฟล์ชื่อ '" + name + "' ใน:\n" + dir);
                return null;
            }

            var sound = new CachedSound(path);
            _cache[name] = sound;
            return sound;
        }

        public static void PlaySfx(string name)
        {
            if (!SfxOn) return;

            try
            {
                EnsureMixer();
                CachedSound sound = GetSound(name);
                if (sound == null) return;

                _mixer.AddMixerInput(new CachedSoundSampleProvider(sound));
            }
            catch (Exception ex)
            {
                Warn("เล่นเสียงไม่ได้: " + ex.Message);
            }
        }

        // แจ้งเตือนครั้งเดียวพอ ไม่ให้เด้งทุกครั้งที่กระโดด
        private static void Warn(string msg)
        {
            if (_warned) return;
            _warned = true;
            System.Windows.Forms.MessageBox.Show(msg, "SoundManager");
        }
    }

    // เก็บข้อมูลเสียงทั้งไฟล์ไว้ในหน่วยความจำ (แปลงเป็น 44100 Hz สเตอริโอ)
    public class CachedSound
    {
        public static readonly WaveFormat Format = WaveFormat.CreateIeeeFloatWaveFormat(44100, 2);

        public float[] AudioData { get; }

        public CachedSound(string path)
        {
            using (var reader = new AudioFileReader(path))
            {
                ISampleProvider sp = reader;

                if (sp.WaveFormat.SampleRate != Format.SampleRate)
                    sp = new WdlResamplingSampleProvider(sp, Format.SampleRate);

                if (sp.WaveFormat.Channels == 1)
                    sp = new MonoToStereoSampleProvider(sp);

                var data = new List<float>();
                var buffer = new float[Format.SampleRate * Format.Channels];
                int read;
                while ((read = sp.Read(buffer, 0, buffer.Length)) > 0)
                    data.AddRange(buffer.Take(read));

                AudioData = data.ToArray();
            }
        }
    }

    // เล่นเสียงที่แคชไว้ 1 ครั้ง (สร้างใหม่ทุกครั้งที่สั่งเล่น)
    public class CachedSoundSampleProvider : ISampleProvider
    {
        private readonly CachedSound _sound;
        private long _position;

        public CachedSoundSampleProvider(CachedSound sound) { _sound = sound; }

        public WaveFormat WaveFormat => CachedSound.Format;

        public int Read(float[] buffer, int offset, int count)
        {
            long available = _sound.AudioData.Length - _position;
            int toCopy = (int)Math.Min(available, count);
            Array.Copy(_sound.AudioData, _position, buffer, offset, toCopy);
            _position += toCopy;
            return toCopy;
        }
    }

    // ทำให้เพลงเล่นวนซ้ำ
    public class LoopStream : WaveStream
    {
        private readonly WaveStream _source;

        public LoopStream(WaveStream source) { _source = source; }

        public override WaveFormat WaveFormat => _source.WaveFormat;
        public override long Length => _source.Length;
        public override long Position
        {
            get => _source.Position;
            set => _source.Position = value;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int total = 0;
            while (total < count)
            {
                int read = _source.Read(buffer, offset + total, count - total);
                if (read == 0)
                {
                    if (_source.Position == 0) break;   // ไฟล์ว่าง กันลูปไม่รู้จบ
                    _source.Position = 0;
                }
                total += read;
            }
            return total;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _source.Dispose();
            base.Dispose(disposing);
        }
    }
}