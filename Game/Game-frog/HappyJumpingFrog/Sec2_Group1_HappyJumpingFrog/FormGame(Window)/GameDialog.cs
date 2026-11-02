using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sec2_Group1_HappyJumpingFrog
{
    // กล่องข้อความสไตล์เดียวกับเกม (แทน MessageBox)
    public class GameDialog : Form
    {
        private readonly string _title;
        private readonly string _message;
        private readonly string _primaryText;
        private readonly string _secondaryText;   // null = มีปุ่มเดียว

        private Rectangle _primaryRect;
        private Rectangle _secondaryRect;
        private bool _primaryHover;
        private bool _secondaryHover;

        private readonly Font _titleFont = new Font("Segoe UI", 22, FontStyle.Bold);
        private readonly Font _msgFont = new Font("Segoe UI", 13, FontStyle.Bold);
        private readonly Font _btnFont = new Font("Segoe UI", 12, FontStyle.Bold);

        private GameDialog(string title, string message, string primary, string secondary)
        {
            _title = title;
            _message = message;
            _primaryText = primary;
            _secondaryText = secondary;

            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterParent;
            ShowInTaskbar = false;
            DoubleBuffered = true;
            KeyPreview = true;
            ClientSize = new Size(460, 270);

            const int btnW = 180, btnH = 48, btnY = 190;
            if (secondary == null)
            {
                _primaryRect = new Rectangle((Width - btnW) / 2, btnY, btnW, btnH);
            }
            else
            {
                int gap = 20;
                int startX = (Width - (btnW * 2 + gap)) / 2;
                _primaryRect = new Rectangle(startX, btnY, btnW, btnH);
                _secondaryRect = new Rectangle(startX + btnW + gap, btnY, btnW, btnH);
            }
        }

        // true = กดปุ่มหลัก, false = กดปุ่มรอง
        public static bool Ask(IWin32Window owner, string title, string message,
                               string primary, string secondary = null)
        {
            using (var dlg = new GameDialog(title, message, primary, secondary))
            {
                return dlg.ShowDialog(owner) == DialogResult.OK;
            }
        }

        // ---------- วาดหน้าตา ----------
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle full = ClientRectangle;
            using (var bg = new LinearGradientBrush(full,
                Color.FromArgb(150, 200, 240), Color.FromArgb(90, 150, 210), 90f))
            {
                g.FillRectangle(bg, full);
            }
            using (var border = new Pen(Color.Goldenrod, 6))
            {
                g.DrawRectangle(border, 3, 3, Width - 7, Height - 7);
            }

            DrawText(g, _title, _titleFont, new Rectangle(10, 35, Width - 20, 50));
            DrawText(g, _message, _msgFont, new Rectangle(20, 100, Width - 40, 70));

            DrawButton(g, _primaryRect, _primaryText, _primaryHover);
            if (_secondaryText != null)
                DrawButton(g, _secondaryRect, _secondaryText, _secondaryHover);
        }

        private void DrawText(Graphics g, string text, Font font, Rectangle r)
        {
            using (var sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })
            {
                var shadow = new RectangleF(r.X + 1.5f, r.Y + 1.5f, r.Width, r.Height);
                g.DrawString(text, font, Brushes.Black, shadow, sf);
                g.DrawString(text, font, Brushes.White, r, sf);
            }
        }

        private void DrawButton(Graphics g, Rectangle r, string text, bool hover)
        {
            Color fillColor = hover ? Color.FromArgb(135, 215, 70) : Color.FromArgb(110, 190, 50);

            using (GraphicsPath path = Pill(r))
            using (var fill = new SolidBrush(fillColor))
            using (var edge = new Pen(Color.FromArgb(50, 110, 20), 4))
            {
                g.FillPath(fill, path);
                g.DrawPath(edge, path);
            }
            DrawText(g, text, _btnFont, r);
        }

        private static GraphicsPath Pill(Rectangle r)
        {
            int d = r.Height;
            var p = new GraphicsPath();
            p.AddArc(r.X, r.Y, d, d, 90, 180);
            p.AddArc(r.Right - d, r.Y, d, d, 270, 180);
            p.CloseFigure();
            return p;
        }

        // ---------- เมาส์ / คีย์บอร์ด ----------
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            bool p = _primaryRect.Contains(e.Location);
            bool s = _secondaryText != null && _secondaryRect.Contains(e.Location);

            if (p != _primaryHover || s != _secondaryHover)
            {
                _primaryHover = p;
                _secondaryHover = s;
                Cursor = (p || s) ? Cursors.Hand : Cursors.Default;
                Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _primaryHover = _secondaryHover = false;
            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (_primaryRect.Contains(e.Location)) Finish(true);
            else if (_secondaryText != null && _secondaryRect.Contains(e.Location)) Finish(false);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Enter = ปุ่มหลัก, Esc = ปุ่มรอง (ถ้าไม่มีปุ่มรอง Esc = ปุ่มหลัก)
            if (keyData == Keys.Enter) { Finish(true); return true; }
            if (keyData == Keys.Escape) { Finish(_secondaryText == null); return true; }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Finish(bool primary)
        {
            DialogResult = primary ? DialogResult.OK : DialogResult.Cancel;
            Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _titleFont.Dispose();
                _msgFont.Dispose();
                _btnFont.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}