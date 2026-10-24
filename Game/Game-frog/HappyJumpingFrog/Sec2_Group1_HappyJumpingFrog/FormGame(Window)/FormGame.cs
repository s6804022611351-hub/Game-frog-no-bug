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

        private River river;
        private PlayerFrog player;
        private const int WorldHeight = 650;   // ความสูงโลกเกม (คงที่)
        private const int PanelWidth = 160;    // ความกว้างแผงด้านขวา
        private const int ButtonSize = 64;

        private float _scale = 1f;             // อัตราขยายตามความสูงหน้าต่าง
        private int _fieldWidth = 750;         // ความกว้างสนามเล่น (เปลี่ยนตามหน้าต่าง)

        private PlayerFrog _frog;
        private Image _background;
        private Road _road;

        // ข้อมูลบนแผงด้านขวา (เฟสหลังๆ จะผูกกับระบบจริง)
        private int _level = 1;
        private int _score = 0;
        private int _goal = 0;
        private bool _paused = false;
        private bool _soundOn = true;
        private bool _musicOn = true;

        private readonly Font _titleFont = new Font("Segoe UI", 18, FontStyle.Bold);
        private readonly Font _valueFont = new Font("Segoe UI", 15, FontStyle.Bold);
        private readonly Font _smallFont = new Font("Segoe UI", 10, FontStyle.Bold);



        public FormGame()
        {
            InitializeComponent();


            DoubleBuffered = true;
            ResizeRedraw = true;
            KeyPreview = true;
            ClientSize = new Size(750 + PanelWidth, WorldHeight);
            UpdateLayout();

            _background = Properties.Resources.background;
            _frog = new PlayerFrog(Properties.Resources.frog, 350, 600);
            _road = new Road(7, 3,
                new Image[] { Properties.Resources.enemy1, Properties.Resources.enemy2 },
                2f, _fieldWidth);

            timerGame.Interval = 16;
            timerGame.Start();
        }

        private void UpdateLayout()
        {
            if (ClientSize.Height <= 0) return;   // ตอนย่อหน้าต่าง

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



        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.ScaleTransform(_scale, _scale);

            g.DrawImage(_background, 0, 0, _fieldWidth, WorldHeight);
            _road.Draw(g);
            _frog.Draw(g);
            DrawSidePanel(g);

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

            if (!active)   // ปิดเสียงอยู่ ให้ขีดเส้นแดงทับ
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
                g.DrawString(text, font, Brushes.Black, centerX + 1.5f, y + 1.5f, sf); // เงา
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.P)
            {
                TogglePause();
                Invalidate();
                return true;
            }

            if (keyData == Keys.Up || keyData == Keys.Down ||
                keyData == Keys.Left || keyData == Keys.Right)
            {
                if (!_paused)
                {
                    _frog.Hop(keyData, _fieldWidth, WorldHeight);
                    Invalidate();
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void timerGame_Tick(object sender, EventArgs e)
        {
            _frog.Update();
            _road.Update();

            foreach (EnemyMonster enemy in _road.Enemies)
            {
                if (_frog.Intersects(enemy))
                {
                    _frog.Die();
                    break;
                }
            }

            if (_frog.IsGameOver)
            {
                timerGame.Stop();
                MessageBox.Show("Game Over");
                Close();
            }

            Invalidate();
        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            
        }
        //protected override void OnPaint(PaintEventArgs e)
        //{
            //base.OnPaint(e);

            // 1. สั่งวาดวัตถุในแม่น้ำทั้งหมด
            //if (river != null)
            //{
                //river.Draw(e.Graphics);
            //}

            // 2. สั่งวาดตัวละครกบ
            ////if (player != null)
            ///{
                ///player.Draw(e.Graphics);
            ///}
        ////    }
    }
}
