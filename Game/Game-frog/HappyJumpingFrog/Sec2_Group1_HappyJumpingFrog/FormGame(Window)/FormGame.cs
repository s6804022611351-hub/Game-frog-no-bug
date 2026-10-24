using HappyJumpingFrog;
using Sec2_Group1_HappyJumpingFrog.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Numerics;
using System.Text;
using System.Windows.Forms;

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

        private float _scale = 1f;             // สเกลอัตราส่วนย่อ/ขยายหน้าจอ
        private int _fieldWidth = 750;         // ความกว้างของพื้นที่เล่นเกม

        private PlayerFrog _frog;              // ตัวละครกบที่ใช้งานจริง
        private Image _background;             // รูปภาพพื้นหลังเกม
        private Road _road;                    // วัตถุจัดการถนนและศัตรูบนถนน

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
            _frog = new PlayerFrog(Properties.Resources.frog, 350, 600); // สร้างกบที่พิกัดเริ่มต้น
            _road = new Road(7, 3,
                new Image[] { Properties.Resources.enemy1, Properties.Resources.enemy2 },
                2f, _fieldWidth); // สร้างถนนและศัตรูวิ่งบนถนน

            // สร้างวัตถุแม่น้ำ
            river = new River(_fieldWidth);

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

        // วาดแผงเมนูด้านขวา (คะแนน, ปุ่มควบคุม)
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
            DrawCenteredText(g, "Level:", _titleFont, cx, 20);
            DrawCenteredText(g, _level + "/3", _valueFont, cx, 55);
            DrawCenteredText(g, "Score:", _titleFont, cx, 100);
            DrawCenteredText(g, _score.ToString(), _valueFont, cx, 135);
            DrawCenteredText(g, "Goal:", _titleFont, cx, 180);
            DrawCenteredText(g, _goal + "/5", _valueFont, cx, 215);
            DrawCenteredText(g, "Lives:", _titleFont, cx, 260);
            DrawCenteredText(g, _frog.Lives.ToString(), _valueFont, cx, 295);

            DrawButton(g, ButtonRect(0), _paused ? ">" : "II", true);
            DrawButton(g, ButtonRect(1), "SFX", _soundOn);
            DrawButton(g, ButtonRect(2), "BGM", _musicOn);
        }

        private RectangleF ButtonRect(int index)
        {
            float x = _fieldWidth + (PanelWidth - ButtonSize) / 2f;
            return new RectangleF(x, 360 + index * 80, ButtonSize, ButtonSize);
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

            // 2. ตรวจจับกบตกแม่น้ำ / เหยียบปลา
            int riverTop = 100;    // ขอบบนแม่น้ำ
            int riverBottom = 250; // ขอบล่างแม่น้ำ

            if (_frog.Y >= riverTop && _frog.Y <= riverBottom)
            {
                bool isOnFish = false;

                if (river != null)
                {
                    foreach (FriendMonster fish in river.Friends)
                    {
                        if (_frog.Intersects(fish))
                        {
                            isOnFish = true;
                            _frog.X += fish.SwimSpeed; // กบลอยตามปลา
                            break;
                        }
                    }
                }

                if (!isOnFish)
                {
                    _frog.Die(); // ตกน้ำตาย
                }
            }

            // 3. *** [เพิ่มระบบ] ตรวจจับกบเดินไปเก็บดอกบัว ***
            foreach (var lotus in _lotuses)
            {
                // ถ้าดอกบัวยังไม่ถูกเก็บ และตัวกบกระโดดไปชน/ทับดอกบัว
                if (!lotus.IsCollected && _frog.Intersects(lotus))
                {
                    lotus.IsCollected = true; // ซ่อนดอกบัวดอกนี้ทันที
                    _goal++;                  // เพิ่มจำนวนที่เก็บได้บนแถบขวา (Goal: x/5)
                    _score += 100;            // บวกคะแนนเพิ่ม 100 คะแนน

                    // วาร์ปกบกลับไปเริ่มใหม่ที่จุดเริ่มต้นด้านล่าง
                    _frog.X = 350;
                    _frog.Y = 600;

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

            // 4. เช็กสถานะ Game Over (พลังชีวิตหมด)
            if (_frog.IsGameOver)
            {
                timerGame.Stop();
                MessageBox.Show("Game Over");
                Close();
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
                    _frog.X = 350;
                    _frog.Y = 600;

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

        // 📍 วาง ProcessCmdKey ต่อท้ายลงมาได้เลย
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

            // 2. กด Z, X, C, V, B เพื่อวาร์ปเก็บดอกบัวดอกที่ 1-5
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