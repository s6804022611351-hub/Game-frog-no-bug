using HappyJumpingFrog;
using Sec2_Group1_HappyJumpingFrog.Class;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sec2_Group1_HappyJumpingFrog
{
    public partial class FormGame : Form
    {
        // ==========================================
        // 1. ประกาศตัวแปรระดับคลาส
        // ==========================================
        private List<TargetLotus> _lotuses = new List<TargetLotus>(); // ลิสต์เก็บดอกบัวของด่านย่อยปัจจุบัน
        private River river;                    // ตัวแปรสำหรับจัดการแม่น้ำและปลา
        private PlayerFrog _frog;              // ตัวละครกบของผู้เล่น
        private Road _road;                    // วัตถุจัดการถนนและศัตรูบนถนน
        private Image _background;             // รูปภาพพื้นหลังเกม
        private Image _lotusImage;             // เก็บรูปดอกบัวไว้ใช้ซ้ำ

        // ค่าคงที่ขนาดหน้าจอและพื้นที่เล่นเกม
        private const int WorldHeight = 650;   // ความสูงพื้นที่เกม
        private const int PanelWidth = 160;    // ความกว้างแผงเมนูด้านขวา
        private const int ButtonSize = 64;     // ขนาดปุ่มกดบนแผงขวา

        // ค่าคงที่ของถนนและแม่น้ำ
        private const float RoadTop = 358;     // ขอบบนถนน
        private const float LaneHeight = 62;   // ความสูงต่อเลน
        private const int RiverFirstRow = 5;   // แถวแม่น้ำแถวแรก
        private const int RiverLastRow = 7;    // แถวแม่น้ำแถวสุดท้าย

        // จุดเริ่มต้นของกบ
        private const float StartX = 350;
        private const float StartY = 565;

        // [ด่านย่อย] ขนาดและตำแหน่งแนวตั้งของดอกบัว
        private const float LotusWidth = 110f;
        private const float LotusHeight = 70f;
        private const float LotusCenterY = 50f;   // ตรงกับกึ่งกลางแถวบนสุดที่กบกระโดดไปถึง
        private const int SubLevelCount = 5;      // [ด่านย่อย] จำนวนด่านย่อยต่อด่านหลัก

        // เวลาของด่านหลักแต่ละด่าน (วินาที): ด่าน 1 = 1500, ด่าน 2 = 1200, ด่าน 3 = 900
        private static readonly int[] LevelSeconds = { 1500, 1200, 900 };

        private float _scale = 1f;             // สเกลอัตราส่วนย่อ/ขยาย
        private int _fieldWidth = 750;         // ความกว้างพื้นที่สนามเล่น

        // นาฬิกาจับเวลาถอยหลัง + ตัววัดเวลาจริงระหว่างเฟรม
        private GameTimer _gameTimer;
        private readonly Stopwatch _stopwatch = Stopwatch.StartNew();
        private double _lastElapsed = 0;

        // สถานะเกมและคะแนน
        private int _level = 1;                // ด่านหลักปัจจุบัน (สูงสุด 3)
        private int _subLevel = 1;             // [ด่านย่อย] ด่านย่อยปัจจุบัน (1-5)
        private int _score = 0;                // คะแนน
        private int _goal = 0;                 // จำนวนดอกบัวที่เก็บได้ในด่านย่อยนี้
        private int _goalTarget = 5;           // [ด่านย่อย] จำนวนดอกบัวที่ต้องเก็บ (5,4,3,2,1)
        private bool _paused = false;          // สถานะหยุดเกม
        private bool _soundOn = true;          // สถานะเสียง SFX
        private bool _musicOn = true;          // สถานะเพลง BGM

        // หน่วงเวลาการเดิน/กระโดด 0.25 วินาที (250 ms)
        private DateTime _lastHopTime = DateTime.MinValue;
        private const double HopCooldownMs = 250;

        // แบบฟอนต์แสดงผล
        private readonly Font _titleFont = new Font("Segoe UI", 18, FontStyle.Bold);
        private readonly Font _valueFont = new Font("Segoe UI", 15, FontStyle.Bold);
        private readonly Font _smallFont = new Font("Segoe UI", 10, FontStyle.Bold);

        // ==========================================
        // 2. Constructor
        // ==========================================
        public FormGame()
        {
            InitializeComponent();

            DoubleBuffered = true;               // ลดการกะพริบของหน้าจอ
            ResizeRedraw = true;                 // วาดใหม่เมื่อปรับขนาดหน้าต่าง
            KeyPreview = true;                   // ดักจับคีย์บอร์ดที่ Form ก่อน
            ClientSize = new Size(750 + PanelWidth, WorldHeight);
            UpdateLayout();

            // โหลดรูปภาพและสร้างวัตถุเกม
            _background = Properties.Resources.background;
            _lotusImage = Properties.Resources.lotus;
            _frog = new PlayerFrog(Properties.Resources.frog, StartX, StartY);

            _road = new Road(RoadTop, LaneHeight, 3,
                new Image[] { Properties.Resources.enemy1, Properties.Resources.enemy2 },
                2f, _fieldWidth);

            river = new River(_fieldWidth);

            // สร้างนาฬิกาของด่านปัจจุบัน
            _gameTimer = new GameTimer(LevelSeconds[_level - 1]);

            // [ด่านย่อย] สร้างดอกบัวของด่านย่อยแรก (5 ใบ)
            BuildLotuses();

            timerGame.Interval = 16; // ประมาณ 60 FPS
            _lastElapsed = _stopwatch.Elapsed.TotalSeconds;
            timerGame.Start();
        }

        // [ด่านย่อย] สร้างดอกบัวใหม่ตามด่านย่อย: ด่านย่อย 1..5 -> 5,4,3,2,1 ใบ
        private void BuildLotuses()
        {
            _lotuses.Clear();
            _goal = 0;
            _goalTarget = SubLevelCount + 1 - _subLevel;

            for (int i = 0; i < _goalTarget; i++)
                _lotuses.Add(new TargetLotus(_lotusImage, 0, 0, LotusWidth, LotusHeight));

            PositionLotuses();
        }

        // [ด่านย่อย] จัดตำแหน่งดอกบัวให้อยู่กลางสนามเสมอ (แบ่งสนามเป็นช่องเท่าๆ กัน แล้ววางกลางช่อง)
        private void PositionLotuses()
        {
            int n = _lotuses.Count;
            for (int i = 0; i < n; i++)
            {
                TargetLotus lotus = _lotuses[i];
                lotus.X = (i + 0.5f) * _fieldWidth / n - lotus.Width / 2f;
                lotus.Y = LotusCenterY - lotus.Height / 2f;
            }
        }

        private void UpdateLayout()
        {
            if (ClientSize.Height <= 0) return;

            _scale = ClientSize.Height / (float)WorldHeight;
            float worldWidth = ClientSize.Width / _scale;
            _fieldWidth = Math.Max(300, (int)(worldWidth - PanelWidth));

            if (_road != null) _road.SetAreaWidth(_fieldWidth);
            if (river != null) river.SetAreaWidth(_fieldWidth);
            if (_frog != null && _frog.X > _fieldWidth - _frog.Width)
                _frog.X = _fieldWidth - _frog.Width;

            PositionLotuses();   // [ด่านย่อย] จัดดอกบัวให้อยู่กลางสนามใหม่ทุกครั้งที่ขนาดเปลี่ยน
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateLayout();
            Invalidate();
        }

        // ==========================================
        // 3. การแสดงผลกราฟิก (OnPaint) วาดอย่างเดียว ไม่มีตรรกะเกม
        // ==========================================
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.ScaleTransform(_scale, _scale);

            // 1. พื้นหลัง
            g.DrawImage(_background, 0, 0, _fieldWidth, WorldHeight);

            // 2. ดอกบัว (ยังไม่เก็บ = รูปดอกบัว, เก็บแล้ว = ให้ TargetLotus วาดสถานะเก็บแล้ว)
            foreach (var lotus in _lotuses)
            {
                if (!lotus.IsCollected)
                    g.DrawImage(_lotusImage, lotus.X, lotus.Y, lotus.Width, lotus.Height);
                else
                    lotus.Draw(g);
            }

            // 3. แม่น้ำและปลา
            if (river != null) river.Draw(g);

            // 4. ถนนและศัตรู
            _road.Draw(g);

            // 5. ตัวละครกบ
            _frog.Draw(g);

            // 6. แผงเมนูด้านขวา
            DrawSidePanel(g);

            // 7. ข้อความ PAUSED
            if (_paused)
                DrawCenteredText(g, "PAUSED", _titleFont, _fieldWidth / 2f, WorldHeight / 2f - 15);
        }

        private void DrawSidePanel(Graphics g)
        {
            RectangleF panel = new RectangleF(_fieldWidth, 0, PanelWidth, WorldHeight);
            using (LinearGradientBrush bg = new LinearGradientBrush(
                panel, Color.FromArgb(150, 200, 240), Color.FromArgb(90, 150, 210), 90f))
            {
                g.FillRectangle(bg, panel);
            }
            using (Pen border = new Pen(Color.Goldenrod, 6))
            {
                g.DrawRectangle(border, _fieldWidth + 3, 3, PanelWidth - 6, WorldHeight - 6);
            }

            float cx = _fieldWidth + PanelWidth / 2f;
            DrawCenteredText(g, "Level:", _titleFont, cx, 15);
            DrawCenteredText(g, _level + "/3", _valueFont, cx, 48);
            // [ด่านย่อย] แสดงด่านย่อยปัจจุบัน
            DrawCenteredText(g, "Stage " + _subLevel + "/" + SubLevelCount, _smallFont, cx, 72);
            DrawCenteredText(g, "Score:", _titleFont, cx, 90);
            DrawCenteredText(g, _score.ToString(), _valueFont, cx, 123);
            DrawCenteredText(g, "Goal:", _titleFont, cx, 165);
            DrawCenteredText(g, _goal + "/" + _goalTarget, _valueFont, cx, 198);   // [ด่านย่อย]
            DrawCenteredText(g, "Lives:", _titleFont, cx, 240);
            DrawCenteredText(g, _frog.Lives.ToString(), _valueFont, cx, 273);
            DrawCenteredText(g, "Time:", _titleFont, cx, 315);
            DrawCenteredText(g, _gameTimer.TimeText, _valueFont, cx, 348);

            DrawButton(g, ButtonRect(0), _paused ? ">" : "II", true);
            DrawButton(g, ButtonRect(1), "SFX", _soundOn);
            DrawButton(g, ButtonRect(2), "BGM", _musicOn);
        }

        private RectangleF ButtonRect(int index)
        {
            float x = _fieldWidth + (PanelWidth - ButtonSize) / 2f;
            return new RectangleF(x, 410 + index * 75, ButtonSize, ButtonSize);
        }

        private void DrawButton(Graphics g, RectangleF r, string text, bool active)
        {
            using (SolidBrush fill = new SolidBrush(Color.FromArgb(110, 190, 50)))
            using (Pen edge = new Pen(Color.FromArgb(50, 110, 20), 4))
            {
                g.FillEllipse(fill, r);
                g.DrawEllipse(edge, r);
            }
            DrawCenteredText(g, text, _smallFont, r.X + r.Width / 2f, r.Y + r.Height / 2f - 9);

            if (!active)
            {
                using (Pen slash = new Pen(Color.Red, 5))
                {
                    g.DrawLine(slash, r.Left + 10, r.Bottom - 10, r.Right - 10, r.Top + 10);
                }
            }
        }

        private void DrawCenteredText(Graphics g, string text, Font font, float centerX, float y)
        {
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                g.DrawString(text, font, Brushes.Black, centerX + 1.5f, y + 1.5f, sf);
                g.DrawString(text, font, Brushes.White, centerX, y, sf);
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            PointF p = new PointF(e.X / _scale, e.Y / _scale);

            if (ButtonRect(0).Contains(p)) TogglePause();
            else if (ButtonRect(1).Contains(p)) _soundOn = !_soundOn;
            else if (ButtonRect(2).Contains(p)) _musicOn = !_musicOn;

            Invalidate();
        }

        private void TogglePause()
        {
            _paused = !_paused;
            timerGame.Enabled = !_paused;

            // ตอนเล่นต่อ เริ่มนับใหม่ เพื่อไม่ให้นับเวลาที่หยุดเกมอยู่
            if (!_paused)
                _lastElapsed = _stopwatch.Elapsed.TotalSeconds;
        }

        private FriendMonster FindFishUnderFrog()
        {
            if (river == null) return null;

            float fx = _frog.X + _frog.Width / 2f;
            float fy = _frog.Y + _frog.Height / 2f;

            foreach (FriendMonster fish in river.Friends)
            {
                float cy = fish.Y + fish.Height / 2f;
                if (Math.Abs(cy - fy) < 20 && fx >= fish.X && fx <= fish.X + fish.Width)
                    return fish;
            }
            return null;
        }

        // ==========================================
        // 4. Game Loop (timerGame_Tick)
        // ==========================================
        private void timerGame_Tick(object sender, EventArgs e)
        {
            if (river != null) river.UpdateFriends();
            _frog.Update();
            _road.Update();

            // 1. ตรวจจับชนศัตรู
            foreach (EnemyMonster enemy in _road.Enemies)
            {
                if (_frog.Intersects(enemy))
                {
                    _frog.Die();
                    break;
                }
            }

            // 2. ตรวจจับตกน้ำ / ขี่ปลา
            int row = _frog.CurrentRow;
            if (row >= RiverFirstRow && row <= RiverLastRow)
            {
                FriendMonster fish = FindFishUnderFrog();
                if (fish != null)
                    _frog.X += fish.SwimSpeed;
                else
                    _frog.Die();
            }

            if (_frog.X < -_frog.Width / 2f || _frog.X > _fieldWidth - _frog.Width / 2f)
                _frog.Die();

            // 3. ตรวจจับกบกระโดดไปลงดอกบัว
            foreach (var lotus in _lotuses)
            {
                if (!_frog.Intersects(lotus)) continue;

                if (!lotus.IsCollected)
                {
                    // ยังไม่เคยเก็บ -> ได้คะแนน และวาร์ปกบกลับจุดเริ่มต้น
                    lotus.IsCollected = true;
                    _goal++;
                    _score += 1;

                    _frog.X = StartX;
                    _frog.Y = StartY;

                    // [ด่านย่อย] เก็บครบตามจำนวนของด่านย่อยนี้แล้ว
                    if (_goal >= _goalTarget)
                    {
                        HandleSubLevelClear();
                        return;   // ต้อง return เพราะลิสต์ดอกบัวถูกสร้างใหม่แล้ว
                    }
                }
                else
                {
                    // เคยเก็บไปแล้ว แต่กบกระโดดมาลงจุดเดิม -> ตาย
                    _frog.Die();
                }
                break;
            }

            // 4. นับเวลาถอยหลังตามเวลาจริงที่ผ่านไป
            double now = _stopwatch.Elapsed.TotalSeconds;
            _gameTimer.Tick(now - _lastElapsed);
            _lastElapsed = now;

            // 5. เช็ก Game Over (หัวใจหมด หรือ เวลาหมด)
            if (_frog.IsGameOver || _gameTimer.IsTimeUp)
            {
                timerGame.Stop();
                FormGameOver gameOverForm = new FormGameOver(_score);
                this.Hide();
                gameOverForm.ShowDialog(this);
                this.Close();
                return;
            }

            Invalidate();
        }

        // ==========================================
        // 5. ระบบเก็บดอกบัวทางลัด (Z, X, C, V, B) ปุ่มทดสอบ ลบก่อนส่งงาน
        // ==========================================
        private void CollectLotusByIndex(int index)
        {
            if (index >= 0 && index < _lotuses.Count)
            {
                TargetLotus lotus = _lotuses[index];

                if (!lotus.IsCollected)
                {
                    lotus.IsCollected = true;
                    _goal++;
                    _score += 1;

                    _frog.X = StartX;
                    _frog.Y = StartY;

                    Invalidate();

                    if (_goal >= _goalTarget)
                    {
                        HandleSubLevelClear();
                    }
                }
            }
        }

        // ==========================================
        // 6. ระบบจัดการด่านย่อยและด่านหลัก
        // ==========================================

        // [ด่านย่อย] เก็บดอกบัวครบในด่านย่อยนี้แล้ว
        private void HandleSubLevelClear()
        {
            if (_subLevel < SubLevelCount)
            {
                // ขึ้นด่านย่อยถัดไปทันที ดอกบัวลดลงทีละ 1 ใบ (เวลายังเดินต่อ ไม่รีเซ็ต)
                _subLevel++;
                BuildLotuses();

                _frog.X = StartX;
                _frog.Y = StartY;

                Invalidate();
                return;
            }

            // ผ่านครบ 5 ด่านย่อย -> ผ่านด่านหลัก
            HandleLevelClear();
        }

        private void HandleLevelClear()
        {
            timerGame.Stop();

            if (_level >= 3)
            {
                MessageBox.Show(
                    "🎉 Congratulations! คุณผ่านครบทั้ง 3 ด่านเรียบร้อยแล้ว!",
                    "You Win!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                Close();
                return;
            }

            DialogResult result = MessageBox.Show(
                $"🎉 คุณผ่านด่านที่ {_level} เรียบร้อยแล้ว!\n\nต้องการเล่นด่านที่ {_level + 1} ต่อหรือไม่?",
                "Level Cleared",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information
            );

            if (result == DialogResult.Yes)
            {
                StartNextLevel();
            }
            else
            {
                Close();
            }
        }

        private void StartNextLevel()
        {
            _level++;
            _subLevel = 1;      // [ด่านย่อย] เริ่มด่านย่อยแรกของด่านหลักใหม่
            _paused = false;

            BuildLotuses();     // [ด่านย่อย] กลับมาเริ่มที่ 5 ใบ

            _frog.X = StartX;
            _frog.Y = StartY;

            // ขึ้นด่านหลักใหม่ ตั้งเวลาตามด่านนั้น (ด่าน 2 = 1200, ด่าน 3 = 900)
            _gameTimer.Reset(LevelSeconds[_level - 1]);
            _lastElapsed = _stopwatch.Elapsed.TotalSeconds;

            if (timerGame.Interval > 6)
            {
                timerGame.Interval -= 3;
            }

            this.Text = $"Happy Jumping Frog - Level {_level}";

            this.Activate();
            this.Focus();
            this.ActiveControl = null;

            timerGame.Start();
            Invalidate();
        }

        // ==========================================
        // 7. ปุ่มควบคุม (WASD, ปุ่มลูกศร, P, Z-B)
        // ==========================================
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.P)
            {
                TogglePause();
                Invalidate();
                return true;
            }

            if (!_paused)
            {
                if (keyData == Keys.Z) { CollectLotusByIndex(0); return true; }
                if (keyData == Keys.X) { CollectLotusByIndex(1); return true; }
                if (keyData == Keys.C) { CollectLotusByIndex(2); return true; }
                if (keyData == Keys.V) { CollectLotusByIndex(3); return true; }
                if (keyData == Keys.B) { CollectLotusByIndex(4); return true; }
            }

            Keys moveKey = keyData;
            if (keyData == Keys.W) moveKey = Keys.Up;
            else if (keyData == Keys.S) moveKey = Keys.Down;
            else if (keyData == Keys.A) moveKey = Keys.Left;
            else if (keyData == Keys.D) moveKey = Keys.Right;

            if (moveKey == Keys.Up || moveKey == Keys.Down ||
                moveKey == Keys.Left || moveKey == Keys.Right)
            {
                if (!_paused)
                {
                    // ผ่านไปอย่างน้อย 0.25 วินาทีจากการเดินครั้งล่าสุดหรือยัง
                    if ((DateTime.Now - _lastHopTime).TotalMilliseconds >= HopCooldownMs)
                    {
                        _frog.Hop(moveKey, _fieldWidth, WorldHeight);
                        _lastHopTime = DateTime.Now;
                        Invalidate();
                    }
                }
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void FormGame_Load(object sender, EventArgs e)
        {
        }
    }
}