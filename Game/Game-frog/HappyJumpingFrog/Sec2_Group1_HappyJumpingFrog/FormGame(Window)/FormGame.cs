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
        // ประกาศตัวแปรระดับคลาส (เพื่อให้ทุกฟังก์ชันเข้าถึงได้)
        // ==========================================
        private River river;                    // ตัวแปรสำหรับจัดการแม่น้ำและปลา/มอนสเตอร์ในน้ำ
        private PlayerFrog player;              // ตัวแปรผู้เล่น (กบ)
        private const int WorldHeight = 650;   // กำหนดความสูงของพื้นที่เกม (ค่าคงที่)
        private const int PanelWidth = 160;    // ความกว้างของแถบเมนูด้านขวา
        private const int ButtonSize = 64;     // ขนาดของปุ่มกดบนแผงด้านขวา

        private float _scale = 1f;             // สเกลอัตราส่วนย่อ/ขยายหน้าจอ
        private int _fieldWidth = 750;         // ความกว้างของพื้นที่เล่นเกม (ปรับตามขนาดหน้าต่าง)

        private PlayerFrog _frog;              // ตัวละครกบที่ใช้งานจริง
        private Image _background;             // รูปภาพพื้นหลังเกม
        private Road _road;                    // วัตถุจัดการถนนและศัตรูบนถนน

        // ข้อมูลสถานะเกมบนแถบด้านขวา
        private int _level = 1;                // ด่านปัจจุบัน
        private int _score = 0;                // คะแนน
        private int _goal = 0;                 // เป้าหมาย
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

            DoubleBuffered = true;               // เปิดระบบลดการกะพริบของหน้าจอขณะวาด กราฟิก
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

            // *** สร้างวัตถุแม่น้ำตรงนี้ เพื่อให้พร้อมใช้งานทันทีและปลาไม่เป็น null ***
            river = new River(_fieldWidth);

            // ตั้งค่าเวลาในการอัปเดตเกม (เฟรมเรต)
            timerGame.Interval = 16;             // ประมาณ 60 FPS (1000ms / 60 ≈ 16ms)
            timerGame.Start();                   // เริ่มให้ตัวนับเวลาทำงาน
        }

        // คำนวณขอบเขตพื้นที่เล่นเกมเมื่อมีการปรับขนาดหน้าต่าง
        private void UpdateLayout()
        {
            if (ClientSize.Height <= 0) return;   // ป้องกันข้อผิดพลาดตอนย่อหน้าต่างลง Taskbar

            _scale = ClientSize.Height / (float)WorldHeight;
            float worldWidth = ClientSize.Width / _scale;
            _fieldWidth = Math.Max(300, (int)(worldWidth - PanelWidth));

            if (_road != null) _road.SetAreaWidth(_fieldWidth);
            if (_frog != null && _frog.X > _fieldWidth - _frog.Width)
                _frog.X = _fieldWidth - _frog.Width;
        }

        // ทำงานอัตโนมัติเมื่อผู้ใช้ขยายหรือย่อหน้าต่าง
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateLayout();                      // คำนวณตำแหน่งใหม่
            Invalidate();                        // สั่งให้วาดหน้าจอใหม่
        }

        // ==========================================
        // ฟังก์ชันวาดภาพทุกอย่างลงบนหน้าจอ (OnPaint)
        // ==========================================
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias; // ปรับให้ภาพลบรอยหยักเพื่อความเรียบเนียน
            g.ScaleTransform(_scale, _scale);          // ปรับขนาดกราฟิกตามสเกลหน้าจอ

            // 1. วาดรูปพื้นหลังก่อนเสมอ (อยู่ชั้นล่างสุด)
            g.DrawImage(_background, 0, 0, _fieldWidth, WorldHeight);

            // 2. *** วาดปลา/มอนสเตอร์ในแม่น้ำทับบนรูปพื้นหลัง ***
            if (river != null)
            {
                river.Draw(g); // เรียกเมธอด Draw ของ class River เพื่อวาดปลาทุกตัวในลิสต์
            }

            // 3. วาดถนนและศัตรูบนถนน
            _road.Draw(g);

            // 4. วาดตัวละครกบของผู้เล่น
            _frog.Draw(g);

            // 5. วาดเมนูแผงควบคุมด้านขวา
            DrawSidePanel(g);

            // 6. แสดงข้อความ PAUSED หากเกมถูกหยุดชั่วคราว
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
                g.FillRectangle(bg, panel); // วาดพื้นหลังแผงด้านขวาแบบไล่เฉดสี
            }
            using (Pen border = new Pen(Color.Goldenrod, 6))
            {
                g.DrawRectangle(border, _fieldWidth + 3, 3, PanelWidth - 6, WorldHeight - 6); // วาดกรอบทอง
            }

            // แสดงข้อความข้อมูลในเกม
            float cx = _fieldWidth + PanelWidth / 2f;
            DrawCenteredText(g, "Level:", _titleFont, cx, 20);
            DrawCenteredText(g, _level + "/3", _valueFont, cx, 55);
            DrawCenteredText(g, "Score:", _titleFont, cx, 100);
            DrawCenteredText(g, _score.ToString(), _valueFont, cx, 135);
            DrawCenteredText(g, "Goal:", _titleFont, cx, 180);
            DrawCenteredText(g, _goal + "/5", _valueFont, cx, 215);
            DrawCenteredText(g, "Lives:", _titleFont, cx, 260);
            DrawCenteredText(g, _frog.Lives.ToString(), _valueFont, cx, 295);

            // วาดปุ่มกดต่างๆ
            DrawButton(g, ButtonRect(0), _paused ? ">" : "II", true); // ปุ่ม Pause / Play
            DrawButton(g, ButtonRect(1), "SFX", _soundOn);            // ปุ่มเสียงเอฟเฟกต์
            DrawButton(g, ButtonRect(2), "BGM", _musicOn);             // ปุ่มเพลงประกอบ
        }

        // คำนวณตำแหน่งพิกัดของปุ่มกดบนแผงขวา
        private RectangleF ButtonRect(int index)
        {
            float x = _fieldWidth + (PanelWidth - ButtonSize) / 2f;
            return new RectangleF(x, 360 + index * 80, ButtonSize, ButtonSize);
        }

        // วาดรูปปุ่มทรงกลม
        private void DrawButton(Graphics g, RectangleF r, string text, bool active)
        {
            using (SolidBrush fill = new SolidBrush(Color.FromArgb(110, 190, 50)))
            using (Pen edge = new Pen(Color.FromArgb(50, 110, 20), 4))
            {
                g.FillEllipse(fill, r);   // วาดสีข้างในปุ่ม
                g.DrawEllipse(edge, r);   // วาดเส้นขอบปุ่ม
            }
            DrawCenteredText(g, text, _smallFont, r.X + r.Width / 2f, r.Y + r.Height / 2f - 9);

            // ถ้าสถานะเป็นปิด ให้ขีดเส้นสีแดงทับปุ่ม
            if (!active)
            {
                using (Pen slash = new Pen(Color.Red, 5))
                {
                    g.DrawLine(slash, r.Left + 10, r.Bottom - 10, r.Right - 10, r.Top + 10);
                }
            }
        }

        // ฟังก์ชันช่วยจัดข้อความให้อยู่ตรงกลางตำแหน่งที่กำหนด
        private void DrawCenteredText(Graphics g, string text, Font font, float centerX, float y)
        {
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                g.DrawString(text, font, Brushes.Black, centerX + 1.5f, y + 1.5f, sf); // วาดเงาข้อความ
                g.DrawString(text, font, Brushes.White, centerX, y, sf);               // วาดตัวหนังสือจริง
            }
        }

        // ดักจับการคลิกเมาส์บนปุ่มเมนู
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            PointF p = new PointF(e.X / _scale, e.Y / _scale); // แปลงพิกัดเมาส์ตามสเกล

            if (ButtonRect(0).Contains(p)) TogglePause();      // คลิกปุ่ม Pause
            else if (ButtonRect(1).Contains(p)) _soundOn = !_soundOn; // คลิกเปิด/ปิด SFX
            else if (ButtonRect(2).Contains(p)) _musicOn = !_musicOn; // คลิกเปิด/ปิด BGM

            Invalidate(); // สั่งรีเฟรชหน้าจอ
        }

        // สลับสถานะหยุดเกม / เล่นต่อ
        private void TogglePause()
        {
            _paused = !_paused;
            timerGame.Enabled = !_paused; // ปิด/เปิด ตัวนับเวลา
        }

        // ดักจับการกดปุ่มบนคีย์บอร์ด (เช่น ปุ่มลูกศรบังคับกบ หรือปุ่ม P)
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.P) // กด P เพื่อ Pause
            {
                TogglePause();
                Invalidate();
                return true;
            }

            // บังคับกบด้วยปุ่มลูกศร ขึ้น ลง ซ้าย ขวา
            if (keyData == Keys.Up || keyData == Keys.Down ||
                keyData == Keys.Left || keyData == Keys.Right)
            {
                if (!_paused)
                {
                    _frog.Hop(keyData, _fieldWidth, WorldHeight); // สั่งกบกระโดด
                    Invalidate(); // รีเฟรชภาพ
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // ==========================================
        // ฟังก์ชัน Timer ทำงานตามรอบเวลา (อัปเดตตำแหน่งและการเคลื่อนที่)
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
            int riverTop = 100;     // ขอบบนแม่น้ำ (ปรับตามรูปจริง)
            int riverBottom = 250; // ขอบล่างแม่น้ำ (ปรับตามรูปจริง)

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

            // 3. เช็กสถานะ Game Over
            if (_frog.IsGameOver)
            {
                timerGame.Stop();
                MessageBox.Show("Game Over");
                Close();
            }

            Invalidate();
        }

        // Event Handler เมื่อ Form โหลดเสร็จ
        private void FormGame_Load(object sender, EventArgs e)
        {
            // (ย้ายโค้ดสร้าง river ไปไว้ใน FormGame() แล้ว)
        }
    }
}