using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sec2_Group1_HappyJumpingFrog
{
    public partial class UserControlPause : UserControl
    {
        // 1. ประกาศตัวแปรระดับคลาส
        private bool _soundOn = true;
        private bool _musicOn = true;

        // 2. ประกาศ Events สำหรับส่งสัญญาณกลับไปที่ FormGame
        public event EventHandler ContinueClicked;
        public event EventHandler MenuClicked;
        public event EventHandler SoundToggleClicked;
        public event EventHandler MusicToggleClicked;

        // 3. Constructor แบบไม่มีพารามิเตอร์ (จำเป็นสำหรับ WinForms Designer)
        public UserControlPause()
        {
            InitializeComponent();
        }

        // 4. Constructor แบบรับค่าสถานะเสียงมาจากหน้าเกมหลัก
        public UserControlPause(bool soundOn, bool musicOn)
        {
            InitializeComponent();

            _soundOn = soundOn;
            _musicOn = musicOn;

            // อัปเดตข้อความปุ่มให้ตรงกับสถานะจริงที่ส่งมา
            if (btnSound != null) btnSound.Text = _soundOn ? "Sound On" : "Sound Off";
            if (btnMusic != null) btnMusic.Text = _musicOn ? "Music On" : "Music Off";
        }

        // ปุ่ม Continue (เล่นต่อ)
        private void btnContinue_Click(object sender, EventArgs e)
        {
            ContinueClicked?.Invoke(this, EventArgs.Empty);
        }

        // ปุ่ม Menu (กลับหน้าหลัก)
        private void btnMenu_Click(object sender, EventArgs e)
        {
            MenuClicked?.Invoke(this, EventArgs.Empty);
        }

        // ปุ่ม Sound On/Off (สลับข้อความและสถานะ)
        private void btnSound_Click(object sender, EventArgs e)
        {
            _soundOn = !_soundOn;
            if (btnSound != null) btnSound.Text = _soundOn ? "Sound On" : "Sound Off";
            SoundToggleClicked?.Invoke(this, EventArgs.Empty);
        }

        // ปุ่ม Music On/Off (สลับข้อความและสถานะ)
        private void btnMusic_Click(object sender, EventArgs e)
        {
            _musicOn = !_musicOn;
            if (btnMusic != null) btnMusic.Text = _musicOn ? "Music On" : "Music Off";
            MusicToggleClicked?.Invoke(this, EventArgs.Empty);
        }

        // ปุ่มกากบาทมุมขวาบน (X)
        private void btnClose_Click(object sender, EventArgs e)
        {
            ContinueClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
