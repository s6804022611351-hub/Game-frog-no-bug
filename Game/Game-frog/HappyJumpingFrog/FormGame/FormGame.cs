using System;
using System.Drawing;
using System.Windows.Forms;



namespace HappyJumpingFrog
{
    public partial class FormGame : Form
    {
        private PlayerFrog _frog;
        private Image _background;

        public FormGame()
        {
            InitializeComponent();

            DoubleBuffered = true;               // กันภาพกะพริบ
            KeyPreview = true;                   // ให้ฟอร์มรับปุ่มคีย์บอร์ด
            ClientSize = new Size(750, 650);

            _background = Properties.Resources.background;
            _frog = new PlayerFrog(Properties.Resources.frog, 350, 600);

            timerGame.Interval = 16;             // ~60 เฟรมต่อวินาที
            timerGame.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawImage(_background, 0, 0, ClientSize.Width, ClientSize.Height);
            _frog.Draw(e.Graphics);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            _frog.Hop(e.KeyCode, ClientSize.Width, ClientSize.Height);
        }

        private void timerGame_Tick(object sender, EventArgs e)
        {
            _frog.Update();
            Invalidate();                        // สั่งวาดหน้าจอใหม่ทุกเฟรม
        }
    }
}