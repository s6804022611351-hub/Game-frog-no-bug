using HappyJumpingFrog;
using Sec2_Group1_HappyJumpingFrog.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Numerics;
using System.Text;
using System.Windows.Forms;
using static Sec2_Group1_HappyJumpingFrog.Class.GameTImer;

namespace Sec2_Group1_HappyJumpingFrog
{
    public partial class FormGame : Form
    {
        // ==========================================
        // ประกาศตัวแปรระดับคลาส
        // ==========================================
        private List<TargetLotus> _lotuses = new List<TargetLotus>(); // ลิสต์เก็บดอกบัวทั้ง 5 ดอก
        private River river;                    // ตัวแปรสำหรับจัดการแม่น้ำและปลา
        private PlayerFrog player;              // ตัวแปรผู้เล่น (กบ)
        private const int WorldHeight = 650;   // กำหนดความสูงของพื้นที่เกม
        private const int PanelWidth = 160;    // ความกว้างของแถบเมนูด้านขวา
        private const int ButtonSize = 64;     // ขนาดของปุ่มกดบนแผงด้านขวา

        // ค่าคงที่ของถนน (ปรับให้ตรงกับรูปพื้นหลัง)
        private const float RoadTop = 358;     // ขอบบนของถนน
        private const float LaneHeight = 62;   // ความสูงต่อเลน

        // แถวของแม่น้ำ (ตรงกับตาราง RowCenters ใน PlayerFrog)
        private const int RiverFirstRow = 5;   // แถวแม่น้ำแถวแรก
        private const int RiverLastRow = 7;    // แถวแม่น้ำแถวสุดท้าย

        // จุดเริ่มต้นของกบ
        private const float StartX = 350;
        private const float StartY = 565;

        // [เวลา] เวลาของด่านหลักแต่ละด่าน (วินาที): ด่าน 1 = 1500, ด่าน 2 = 1200, ด่าน 3 = 900
        private static readonly int[] LevelSeconds = { 1500, 1200, 900 };

        private float _scale = 1f;             // สเกลอัตราส่วนย่อ/ขยายหน้าจอ
        private int _fieldWidth = 750;         // ความกว้างของพื้นที่เล่นเกม

        private PlayerFrog _frog;              // ตัวละครกบที่ใช้งานจริง
        private Image _background;             // รูปภาพพื้นหลังเกม
        private Road _road;                    // วัตถุจัดการถนนและศัตรูบนถนน

        // [เวลา] นาฬิกาจับเวลาถอยหลัง + ตัววัดเวลาจริงระหว่างเฟรม
        private GameTimer _gameTimer;
        private readonly Stopwatch _stopwatch = Stopwatch.StartNew();
        private double _lastElapsed = 0;

        // ข้อมูลสถานะเกมบนแถบด้านขวา
        private int _level = 1;                // ด่านปัจจุบัน
        private int _score = 0;                // คะแนน
        private int _goal = 0;                 // เป้าหมาย (จำนวนดอกบัวที่เก็บได้)
        private bool _paused = false;          // สถานะหยุดเกมชั่วคราว
        private bool _soundOn = true;          // สถานะเปิด/ปิดเสียงเอฟเฟกต์
        private bool _musicOn = true;          // สถานะเปิด/ปิดเพลงประกอบ

        // แบบฟอนต์สำหรับแสดงข้อความในเกม
        private readonly Font _titleFont = new Font("Segoe UI", 18, FontStyle.Bold);
        private readonly Font _valueFont = new Font("Segoe UI", 15, FontStyle.Bold);
        private readonly Font _smallFont = new Font("Segoe UI", 10, FontStyle.Bold);

        // ==========================================
        // Constructor (ทำงานเมื่อเริ่มสร้าง Form ขึ้นมา)
        // ==========================================
        public FormGame()
        {
            InitializeComponent();

            DoubleBuffered = true;               // เปิดระบบลดการกะพริบของหน้าจอขณะวาดกราฟิก
            ResizeRedraw = true;                 // สั่งให้วาดหน้าจอใหม่ทันทีที่มีการปรับขนาดหน้าต่าง
            KeyPreview = true;                   // ให้ Form ดักจับการกดคีย์บอร์ดก่อน Control อื่น
            ClientSize = new Size(750 + PanelWidth, WorldHeight); // กำหนดขนาดหน้าต่างเกม
            UpdateLayout();                      // คำนวณสเกลพื้นที่การเล่น

            // โหลดรูปภาพและสร้างวัตถุเกมต่าง ๆ
            _background = Properties.Resources.background; // ดึงรูปพื้นหลัง
            _frog = new PlayerFrog(Properties.Resources.frog, StartX, StartY);

            // สร้างถนนและศัตรูโดยระบุขอบบนถนนกับความสูงเลน
            _road = new Road(RoadTop, LaneHeight, 3,
                new Image[] { Properties.Resources.enemy1, Properties.Resources.enemy2 },
                2f, _fieldWidth);

            // สร้างวัตถุแม่น้ำ
            river = new River(_fieldWidth);

            // [เวลา] สร้างนาฬิกาด่านที่ 1 = 1500 วินาที
            _gameTimer = new GameTimer(LevelSeconds[_level - 1]);

            // *** สร้างดอกบัว 5 ดอก วางกระจายบนพื้นหญ้าด้านบน ***
            Image lotusImg = Properties.Resources.lotus;
            float lotusWidth = 110f;   // 📍 ขนาดความกว้างดอกบัว (ปรับเพิ่ม/ลดขนาดได้ที่นี่)
            float lotusHeight = 70f;  // 📍 ขนาดความสูงดอกบัว
            float padding = 25f;      // 📍 ระยะเว้นขอบซ้าย-ขวา

            // คำนวณระยะห่างเพื่อกระจายดอกบัวให้เต็มพื้นที่สนามเล่น
            float availableWidth = _fieldWidth - (padding * 2) - lotusWidth;
            float step = availableWidth / 4f; // แบ่งช่วงระยะห่าง 4 ช่องเท่า ๆ กัน

            _lotuses.Clear();
            for (int i = 0; i < 5; i++)
            {
                float x = padding + (i * step);
                float y = 10; // พิกัด Y บนพื้นหญ้าด้านบนสุด
                _lotuses.Add(new TargetLotus(lotusImg, x, y, lotusWidth, lotusHeight));
            }

            // ตั้งค่าเวลาในการอัปเดตเกม (เฟรมเรต)
            timerGame.Interval = 16;             // ประมาณ 60 FPS
            _lastElapsed = _stopwatch.Elapsed.TotalSeconds;  // [เวลา] เริ่มนับจากตอนนี้
            timerGame.Start();                   // เริ่มตัวนับเวลา
        }

        // คำนวณขอบเขตพื้นที่เล่นเกมเมื่อมีการปรับขนาดหน้าต่าง
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
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateLayout();
            Invalidate();
        }

        // ==========================================
        // ฟังก์ชันวาดภาพทุกอย่างลงบนหน้าจอ (OnPaint)
        // ==========================================
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias; // ปรับภาพให้ลบรอยหยัก
            g.ScaleTransform(_scale, _scale);          // ปรับขนาดตามสเกลหน้าจอ

            // 1. วาดรูปพื้นหลังก่อนเสมอ
            g.DrawImage(_background, 0, 0, _fieldWidth, WorldHeight);

            // 2. *** วาดดอกบัว 5 ดอกบนหญ้าด้านบน ***
            foreach (var lotus in _lotuses)
            {
                lotus.Draw(g);
            }

            // 3. วาดปลาและแม่น้ำ
            if (river != null)
            {
                river.Draw(g);
            }

            // 4. วาดถนนและศัตรูบนถนน
            _road.Draw(g);

            // 5. วาดตัวละครกบของผู้เล่น
            _frog.Draw(g);

            // 6. วาดเมนูแผงควบคุมด้านขวา
            DrawSidePanel(g);

            // 7. แสดงข้อความ PAUSED หากเกมถูกหยุดชั่วคราว
            if (_paused)
                DrawCenteredText(g, "PAUSED", _titleFont, _fieldWidth / 2f, WorldHeight / 2f - 15);
        }

        // วาดแผงเมนูด้านขวา (คะแนน, เวลา, ปุ่มควบคุม)
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

            // [เวลา] จัดตำแหน่งใหม่ให้มีช่อง Time เพิ่มอีก 1 ช่อง
            float cx = _fieldWidth + PanelWidth / 2f;
            DrawCenteredText(g, "Level:", _titleFont, cx, 15);
            DrawCenteredText(g, _level + "/3", _valueFont, cx, 48);
            DrawCenteredText(g, "Score:", _titleFont, cx, 90);
            DrawCenteredText(g, _score.ToString(), _valueFont, cx, 123);
            DrawCenteredText(g, "Goal:", _titleFont, cx, 165);
            DrawCenteredText(g, _goal + "/5", _valueFont, cx, 198);
            DrawCenteredText(g, "Lives:", _titleFont, cx, 240);
            DrawCenteredText(g, _frog.Lives.ToString(), _valueFont, cx, 273);
            DrawCenteredText(g, "Time:", _titleFont, cx, 315);
            DrawCenteredText(g, _gameTimer.TimeText, _valueFont, cx, 348);

            DrawButton(g, ButtonRect(0), _paused ? ">" : "II", true);
            DrawButton(g, ButtonRect(1), "SFX", _soundOn);
            DrawButton(g, ButtonRect(2), "BGM", _musicOn);
        }

        // [เวลา] ขยับปุ่มลงมาเริ่มที่ y = 410 และห่างกัน 75
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

            // [เวลา] ตอนเล่นต่อ ให้เริ่มนับใหม่ เพื่อไม่ให้นับเวลาที่หยุดเกมอยู่
            if (!_paused)
                _lastElapsed = _stopwatch.Elapsed.TotalSeconds;
        }

        // หาปลาที่อยู่เลนเดียวกับกบ และอยู่ใต้กึ่งกลางตัวกบพอดี
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
        // ฟังก์ชัน Timer ทำงานตามรอบเวลา (อัปเดตสถานะเกม)
        // ==========================================
        private void timerGame_Tick(object sender, EventArgs e)
        {
            if (river != null) river.UpdateFriends();
            _frog.Update();
            _road.Update();

            // 1. ตรวจจับการชนกับศัตรูบนถนน
            foreach (EnemyMonster enemy in _road.Enemies)
            {
                if (_frog.Intersects(enemy))
                {
                    _frog.Die();
                    break;
                }
            }

            // 2. ตรวจจับกบตกแม่น้ำ / ขี่ปลา
            int row = _frog.CurrentRow;
            if (row >= RiverFirstRow && row <= RiverLastRow)
            {
                FriendMonster fish = FindFishUnderFrog();
                if (fish != null)
                    _frog.X += fish.SwimSpeed;   // กบลอยตามปลา
                else
                    _frog.Die();                 // ตกน้ำ
            }

            // ถูกปลาพัดออกนอกจอ
            if (_frog.X < -_frog.Width / 2f || _frog.X > _fieldWidth - _frog.Width / 2f)
                _frog.Die();

            // 3. *** [เพิ่มระบบ] ตรวจจับกบเดินไปเก็บดอกบัว ***
            foreach (var lotus in _lotuses)
            {
                // ถ้าดอกบัวยังไม่ถูกเก็บ และตัวกบกระโดดไปชน/ทับดอกบัว
                if (!lotus.IsCollected && _frog.Intersects(lotus))
                {
                    lotus.IsCollected = true; // ซ่อนดอกบัวดอกนี้ทันที
                    _goal++;                  // เพิ่มจำนวนที่เก็บได้บนแถบขวา (Goal: x/5)
                    _score += 1;              // บวกคะแนนเพิ่ม 1 คะแนน

                    // วาร์ปกบกลับไปเริ่มใหม่ที่จุดเริ่มต้นด้านล่าง
                    _frog.X = StartX;
                    _frog.Y = StartY;

                    // หากเก็บครบ 5 ดอก -> แสดงความยินดีและจบด่าน
                    if (_goal >= 5)
                    {
                        timerGame.Stop();
                        MessageBox.Show("You Win! คุณผ่านด่านเรียบร้อยแล้ว!", "Congratulations");
                        Close();
                    }
                    break;
                }
            }

            // 4. [เวลา] นับเวลาถอยหลังตามเวลาจริงที่ผ่านไป
            double now = _stopwatch.Elapsed.TotalSeconds;
            _gameTimer.Tick(now - _lastElapsed);
            _lastElapsed = now;

            // หมดเวลา -> กบตาย หัวใจลด 1 ดวง แล้วเริ่มนับเวลาของด่านใหม่
            if (_gameTimer.IsTimeUp)
            {
                _frog.Die();
                _gameTimer.Restart();
            }

            // 5. เช็กสถานะ Game Over (พลังชีวิตหมด)
            if (_frog.IsGameOver)
            {
                timerGame.Stop();

                // สร้างหน้า Game Over พร้อมส่งคะแนน _score ไปแสดงผล
                FormGameOver gameOverForm = new FormGameOver(_score);

                // ซ่อนหน้าเกมไว้ก่อนเพื่อให้หน้าต่าง Game Over ค้างอยู่
                this.Hide();

                // เปิดหน้า Game Over แบบ Dialog
                gameOverForm.ShowDialog(this);

                // ปิดฟอร์มเกมถาวรเมื่อปิดหน้า Game Over แล้ว
                this.Close();
            }

            Invalidate(); // สั่งรีเฟรชหน้าจอ
        }

        private void CollectLotusByIndex(int index)
        {
            if (index >= 0 && index < _lotuses.Count)
            {
                TargetLotus lotus = _lotuses[index];

                if (!lotus.IsCollected)
                {
                    lotus.IsCollected = true;
                    _goal++;
                    _score += 100;

                    // วาร์ปกบกลับจุดเริ่มต้นด้านล่าง
                    _frog.X = StartX;
                    _frog.Y = StartY;

                    Invalidate();

                    if (_goal >= 5)
                    {
                        timerGame.Stop();
                        MessageBox.Show("You Win! คุณผ่านด่านเรียบร้อยแล้ว!", "Congratulations");
                        Close();
                    }
                }
            }
        }

        // ==========================================
        // ดักจับการกดคีย์บอร์ด (P, Z, X, C, V, B, ปุ่มลูกศร และ WASD)
        // ==========================================
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // 1. กด P เพื่อ Pause
            if (keyData == Keys.P)
            {
                TogglePause();
                Invalidate();
                return true;
            }

            // 2. กด Z, X, C, V, B เพื่อวาร์ปเก็บดอกบัวดอกที่ 1-5 (ปุ่มทดสอบ ลบก่อนส่งงาน)
            if (!_paused)
            {
                if (keyData == Keys.Z) { CollectLotusByIndex(0); return true; }
                if (keyData == Keys.X) { CollectLotusByIndex(1); return true; }
                if (keyData == Keys.C) { CollectLotusByIndex(2); return true; }
                if (keyData == Keys.V) { CollectLotusByIndex(3); return true; }
                if (keyData == Keys.B) { CollectLotusByIndex(4); return true; }
            }

            // 3. แปลงปุ่ม WASD ให้เป็นปุ่มลูกศรทิศทาง
            Keys moveKey = keyData;
            if (keyData == Keys.W) moveKey = Keys.Up;
            else if (keyData == Keys.S) moveKey = Keys.Down;
            else if (keyData == Keys.A) moveKey = Keys.Left;
            else if (keyData == Keys.D) moveKey = Keys.Right;

            // 4. สั่งกบกระโดด (รองรับทั้ง ปุ่มลูกศร และ WASD)
            if (moveKey == Keys.Up || moveKey == Keys.Down ||
                moveKey == Keys.Left || moveKey == Keys.Right)
            {
                if (!_paused)
                {
                    _frog.Hop(moveKey, _fieldWidth, WorldHeight);
                    Invalidate();
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