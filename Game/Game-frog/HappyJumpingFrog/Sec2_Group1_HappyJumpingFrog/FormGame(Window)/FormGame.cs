using HappyJumpingFrog;
using Sec2_Group1_HappyJumpingFrog.Class;
using System;
using System.Collections.Generic;
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
        private List<TargetLotus> _lotuses = new List<TargetLotus>(); // ลิสต์เก็บดอกบัวทั้ง 5 ดอก
        private River river;                    // ตัวแปรสำหรับจัดการแม่น้ำและปลา
        private PlayerFrog _frog;              // ตัวละครกบของผู้เล่น
        private Road _road;                    // วัตถุจัดการถนนและศัตรูบนถนน
        private Image _background;             // รูปภาพพื้นหลังเกม
        private Items _heartItem;              // ไอเท็มหัวใจ

        private int _lives = 5;
        private const float HeartWidth = 70f;  // ความกว้างไอเท็มหัวใจ
        private const float HeartHeight = 40f; // ความสูงไอเท็มหัวใจ

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

        private float _scale = 1f;             // สเกลอัตราส่วนย่อ/ขยาย
        private int _fieldWidth = 750;         // ความกว้างพื้นที่สนามเล่น

        // สถานะเกมและคะแนน
        private int _level = 1;                // ด่านปัจจุบัน (สูงสุด 3)
        private int _score = 0;                // คะแนน
        private int _goal = 0;                 // จำนวนดอกบัวที่เก็บได้ (เป้าหมาย 5)
        private bool _paused = false;          // สถานะหยุดเกม
        private bool _soundOn = true;          // สถานะเสียง SFX
        private bool _musicOn = true;          // สถานะเพลง BGM

        // 📍 หน่วงเวลาการเดิน/กระโดด (100 ms = 0.1 วินาที)
        private DateTime _lastHopTime = DateTime.MinValue;
        private const double HopCooldownMs = 100;

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
            _frog = new PlayerFrog(Properties.Resources.frog, StartX, StartY);

            _road = new Road(RoadTop, LaneHeight, 3,
                new Image[] { Properties.Resources.enemy1, Properties.Resources.enemy2 },
                2f, _fieldWidth);

            river = new River(_fieldWidth);

            // สร้างไอเท็มหัวใจและสุ่มตำแหน่งบนแมพ
            _heartItem = new Items(Properties.Resources.heart, 0, 0, HeartWidth, HeartHeight);
            _heartItem.SpawnRandom(_fieldWidth, WorldHeight);

            // สร้างดอกบัว 5 ดอก วางบนหญ้าด้านบน
            Image lotusImg = Properties.Resources.lotus;
            float lotusWidth = 110f;
            float lotusHeight = 70f;
            float padding = 25f;

            float availableWidth = _fieldWidth - (padding * 2) - lotusWidth;
            float step = availableWidth / 4f;

            _lotuses.Clear();
            for (int i = 0; i < 5; i++)
            {
                float x = padding + (i * step);
                float y = 20;
                _lotuses.Add(new TargetLotus(lotusImg, x, y, lotusWidth, lotusHeight));
            }

            timerGame.Interval = 16; // ประมาณ 60 FPS
            timerGame.Start();
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
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateLayout();
            Invalidate();
        }

        // ==========================================
        // 3. การแสดงผลกราฟิก (OnPaint)
        // ==========================================
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.ScaleTransform(_scale, _scale);

            // 1. พื้นหลัง
            g.DrawImage(_background, 0, 0, _fieldWidth, WorldHeight);

            // 2. ดอกบัว 5 ดอก (วาดเฉพาะดอกที่ยังไม่ถูกเก็บ)
            foreach (var lotus in _lotuses)
            {
                if (!lotus.IsCollected)
                {
                    g.DrawImage(Properties.Resources.lotus, lotus.X, lotus.Y, lotus.Width, lotus.Height);
                }
            }

            // 3. แม่น้ำและปลา
            if (river != null) river.Draw(g);

            // 4. ถนนและศัตรู
            _road.Draw(g);

            // 5. วาดไอเท็มหัวใจ (ถ้า IsActive เป็น true)
            _heartItem?.Draw(g);

            // 6. ตัวละครกบ
            _frog.Draw(g);

            // 7. แผงเมนูด้านขวา
            DrawSidePanel(g);

            // 8. ข้อความ PAUSED
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
            DrawCenteredText(g, "Level:", _titleFont, cx, 20);
            DrawCenteredText(g, _level + "/3", _valueFont, cx, 55);
            DrawCenteredText(g, "Score:", _titleFont, cx, 100);
            DrawCenteredText(g, _score.ToString(), _valueFont, cx, 135);
            DrawCenteredText(g, "Goal:", _titleFont, cx, 180);
            DrawCenteredText(g, _goal + "/5", _valueFont, cx, 215);
            DrawCenteredText(g, "Lives:", _titleFont, cx, 260);
            DrawCenteredText(g, _lives.ToString(), _valueFont, cx, 295);

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
                    _lives = _frog.Lives;
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
                {
                    _frog.Die();
                    _lives = _frog.Lives;
                }
            }

            if (_frog.X < -_frog.Width / 2f || _frog.X > _fieldWidth - _frog.Width / 2f)
            {
                _frog.Die();
                _lives = _frog.Lives;
            }

            // 3. ตรวจจับกบเก็บไอเท็มหัวใจ
            if (_heartItem != null && _heartItem.IsActive && _frog.Intersects(_heartItem))
            {
                _heartItem.IsActive = false;
                _lives++; // เพิ่มจำนวนชีวิต +1
                if (_frog != null) _frog.Lives = _lives;
            }

            // 4. ตรวจจับเข้าพื้นที่ดอกบัว
            foreach (var lotus in _lotuses)
            {
                if (_frog.Intersects(lotus))
                {
                    if (!lotus.IsCollected)
                    {
                        // ยังไม่เคยเก็บ -> ได้คะแนน วาร์ปกบกลับจุดเริ่มต้น
                        lotus.IsCollected = true;
                        _goal++;
                        _score += 1;

                        _frog.X = StartX;
                        _frog.Y = StartY;

                        if (_goal >= 5)
                        {
                            HandleLevelClear();
                        }
                    }
                    else
                    {
                        // 📍 เคยเก็บไปแล้ว แต่กบกระโดดมาลงจุดเดิม -> ตายทันที!
                        _frog.Die();
                        _lives = _frog.Lives;
                    }
                    break;
                }
            }

            // 5. เช็ก Game Over
            if (_frog.IsGameOver)
            {
                timerGame.Stop();
                FormGameOver gameOverForm = new FormGameOver(_score);
                this.Hide();
                gameOverForm.ShowDialog(this);
                this.Close();
            }

            Invalidate();
        }

        // ==========================================
        // 5. ระบบเก็บดอกบัวทางลัด (Z, X, C, V, B)
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

                    if (_goal >= 5)
                    {
                        HandleLevelClear();
                    }
                }
            }
        }

        // ==========================================
        // 6. ระบบจัดการด่าน
        // ==========================================
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
            _goal = 0;
            _paused = false;

            foreach (var lotus in _lotuses)
            {
                lotus.IsCollected = false;
            }

            // สุ่มตำแหน่งหัวใจใหม่สำหรับด่านถัดไป
            if (_heartItem != null)
            {
                _heartItem.SpawnRandom(_fieldWidth, WorldHeight);
            }

            _frog.X = StartX;
            _frog.Y = StartY;

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
        // 7. ปุ่มควบคุม (WASD, ปุ่มลูกศร, P, Z-B, N)
        // ==========================================
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // 📍 ปุ่มทางลัด N (เพิ่มชีวิต +1)
            if (keyData == Keys.N)
            {
                _lives++;
                if (_frog != null) _frog.Lives = _lives;
                Invalidate();
                return true;
            }

            // 📍 ปุ่ม P (Pause Game)
            if (keyData == Keys.P)
            {
                TogglePause();
                Invalidate();
                return true;
            }

            // 📍 ปุ่มเก็บดอกบัวทางลัด (Z, X, C, V, B)
            if (!_paused)
            {
                if (keyData == Keys.Z) { CollectLotusByIndex(0); return true; }
                if (keyData == Keys.X) { CollectLotusByIndex(1); return true; }
                if (keyData == Keys.C) { CollectLotusByIndex(2); return true; }
                if (keyData == Keys.V) { CollectLotusByIndex(3); return true; }
                if (keyData == Keys.B) { CollectLotusByIndex(4); return true; }
            }

            // 📍 ปุ่มควบคุมการเดิน (WASD และ Arrow Keys)
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