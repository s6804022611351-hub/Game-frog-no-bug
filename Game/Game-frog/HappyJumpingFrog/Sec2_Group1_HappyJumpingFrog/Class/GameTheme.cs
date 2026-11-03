using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;

namespace Sec2_Group1_HappyJumpingFrog
{
    public static class GameTheme
    {
        private static readonly Color SkyTop = Color.FromArgb(110, 175, 235);
        private static readonly Color SkyBottom = Color.FromArgb(50, 110, 180);
        private static readonly Color Gold = Color.Goldenrod;
        private static readonly Color BtnFill = Color.FromArgb(110, 190, 50);
        private static readonly Color BtnHover = Color.FromArgb(135, 215, 70);
        private static readonly Color BtnDown = Color.FromArgb(85, 160, 35);
        private static readonly Color BtnEdge = Color.FromArgb(50, 110, 20);
        private static readonly Color CloseFill = Color.FromArgb(205, 60, 50);

        // เรียกครั้งเดียวต่อฟอร์ม ต่อจาก InitializeComponent()
        public static void Apply(Form form)
        {
            form.FormBorderStyle = FormBorderStyle.None;
            form.StartPosition = FormStartPosition.CenterParent;
            form.BackColor = SkyBottom;

            typeof(Control).GetProperty("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance)
                ?.SetValue(form, true, null);

            form.Paint += (s, e) =>
            {
                Rectangle r = form.ClientRectangle;
                if (r.Width < 10 || r.Height < 10) return;

                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                using (LinearGradientBrush br = new LinearGradientBrush(r, SkyTop, SkyBottom, 90f))
                    g.FillRectangle(br, r);

                Rectangle inner = new Rectangle(3, 3, r.Width - 7, r.Height - 7);
                using (GraphicsPath path = RoundedRect(inner, 22))
                using (Pen pen = new Pen(Gold, 6))
                    g.DrawPath(pen, path);
            };

            form.Resize += (s, e) =>
            {
                SetRoundedRegion(form);
                form.Invalidate();
            };

            SetRoundedRegion(form);
            StyleChildren(form, form);
        }

        private static void StyleChildren(Form form, Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is Button b)
                {
                    if (b.Text.Trim().ToUpper() == "X") StyleClose(form, b);
                    else StyleButton(b);
                }
                else if (c is TextBox t)
                {
                    StyleTextBox(t);
                }
                else if (c is Label l)
                {
                    if (l.Text.Trim().ToUpper() == "X") StyleClose(form, l);
                    else StyleLabel(l);
                }
                else if (c is Panel || c is GroupBox || c is TableLayoutPanel || c is FlowLayoutPanel)
                {
                    c.BackColor = Color.Transparent;
                }

                if (c.HasChildren) StyleChildren(form, c);
            }
        }

        private static void StyleLabel(Label l)
        {
            l.BackColor = Color.Transparent;
            l.ForeColor = Color.White;
            l.Font = new Font("Segoe UI", l.Font.Size, FontStyle.Bold);
        }

        private static void StyleTextBox(TextBox t)
        {
            t.BorderStyle = BorderStyle.FixedSingle;
            t.BackColor = Color.White;
            t.ForeColor = Color.FromArgb(40, 40, 40);
            t.Font = new Font("Segoe UI", t.Font.Size, FontStyle.Bold);
        }

        private static void StyleButton(Button b)
        {
            b.UseVisualStyleBackColor = false;
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 3;
            b.FlatAppearance.BorderColor = BtnEdge;
            b.FlatAppearance.MouseOverBackColor = BtnHover;
            b.FlatAppearance.MouseDownBackColor = BtnDown;
            b.BackColor = BtnFill;
            b.ForeColor = Color.White;
            b.Font = new Font("Segoe UI", b.Font.Size, FontStyle.Bold);
            b.Cursor = Cursors.Hand;
        }

        // ปุ่ม X กลมสีแดงมุมขวาบน (event Click เดิมยังใช้ได้)
        private static void StyleClose(Form form, Control c)
        {
            c.BackColor = CloseFill;
            c.ForeColor = Color.White;
            c.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            c.Cursor = Cursors.Hand;
            c.Size = new Size(32, 32);

            if (c is Label l)
            {
                l.AutoSize = false;
                l.TextAlign = ContentAlignment.MiddleCenter;
            }
            else if (c is Button b)
            {
                b.FlatStyle = FlatStyle.Flat;
                b.FlatAppearance.BorderSize = 0;
                b.UseVisualStyleBackColor = false;
            }

            using (GraphicsPath p = new GraphicsPath())
            {
                p.AddEllipse(0, 0, c.Width, c.Height);
                c.Region = new Region(p);
            }

            if (c.Parent == form)
            {
                c.Location = new Point(form.ClientSize.Width - c.Width - 16, 14);
                c.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            }
            c.BringToFront();
        }

        private static void SetRoundedRegion(Form form)
        {
            if (form.Width < 10 || form.Height < 10) return;
            using (GraphicsPath p = RoundedRect(new Rectangle(0, 0, form.Width, form.Height), 24))
                form.Region = new Region(p);
        }

        private static GraphicsPath RoundedRect(Rectangle r, int radius)
        {
            int d = radius * 2;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}