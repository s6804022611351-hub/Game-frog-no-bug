using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace Sec2_Group1_HappyJumpingFrog
{
    public partial class FormHighScore : Form
    {
        // ---------- สี (ใช้ชุดเดียวกับแผงด้านขวาของเกม ปรับตรงนี้ที่เดียว) ----------
        private static readonly Color SkyTop = Color.FromArgb(150, 200, 240);
        private static readonly Color SkyBottom = Color.FromArgb(90, 150, 210);
        private static readonly Color GoldBorder = Color.Goldenrod;
        private static readonly Color CardColor = Color.FromArgb(236, 246, 252);
        private static readonly Color CardLine = Color.FromArgb(208, 226, 240);
        private static readonly Color InkColor = Color.FromArgb(40, 65, 95);
        private static readonly Color MutedInk = Color.FromArgb(110, 140, 170);
        private static readonly Color ScoreGreen = Color.FromArgb(50, 135, 30);
        private static readonly Color GoldRank = Color.FromArgb(214, 158, 10);
        private static readonly Color SilverRank = Color.FromArgb(135, 148, 162);
        private static readonly Color BronzeRank = Color.FromArgb(182, 108, 52);

        private DataGridView dgvScores;
        private GamePillButton btnClose;
        private Label lblEmpty;
        private List<GamePillButton> tabButtons = new List<GamePillButton>();
        private Rectangle cardRect;

        private readonly Font titleFont = new Font("Segoe UI", 20f, FontStyle.Bold);
        private readonly Font rankFont = new Font("Segoe UI", 11f, FontStyle.Bold);
        private readonly Font scoreFont = new Font("Segoe UI", 10.5f, FontStyle.Bold);

        // ตัวแปรเก็บชื่อผู้เล่นปัจจุบันที่เพิ่งส่งเข้ามา เพื่อใช้กรองในแท็บ MY SCORES
        private string currentUserName = "";

        // 1. Constructor แบบเดิม (เปิดดูตารางเฉยๆ)
        public FormHighScore()
        {
            SetupUI();
        }

        // 2. Constructor แบบรับค่าชื่อและคะแนนเข้ามาบันทึกอัตโนมัติ
        public FormHighScore(string playerName, int score)
        {
            currentUserName = playerName;
            SaveScoreToFile(playerName, score);
            SetupUI();
        }

        // 3. บันทึกข้อมูลลงไฟล์ .txt
        private void SaveScoreToFile(string playerName, int score)
        {
            if (string.IsNullOrWhiteSpace(playerName)) return;

            string filePath = "scores.txt";
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            string newScoreLine = $"{playerName},{score},{currentDate}";

            try
            {
                File.AppendAllText(filePath, newScoreLine + Environment.NewLine);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving score: " + ex.Message);
            }
        }

        // ==========================================
        // หน้าตา (สไตล์เดียวกับเกม)
        // ==========================================
        private void SetupUI()
        {
            this.Text = "High Scores";
            this.ClientSize = new Size(640, 470);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = SkyBottom;
            this.DoubleBuffered = true;
            this.KeyPreview = true;

            cardRect = new Rectangle(24, 122, Width - 48, Height - 146);

            // ปุ่มปิด: วงกลมเขียวเหมือนปุ่มในเกม
            btnClose = new GamePillButton();
            btnClose.Text = "X";
            btnClose.IsCircle = true;
            btnClose.Size = new Size(38, 38);
            btnClose.Location = new Point(Width - 38 - 18, 16);
            btnClose.Click += (s, e) => this.Close();
            this.Controls.Add(btnClose);

            // แท็บ: ปุ่มแคปซูล แท็บที่เลือกเป็นสีเขียวสด
            string[] tabs = { "TODAY", "LAST WEEK", "LAST MONTH", "ALL TIME", "MY SCORES" };
            Font measureFont = new Font("Segoe UI", 9f, FontStyle.Bold);
            const int gap = 8, tabH = 34, tabY = 72;

            int[] widths = new int[tabs.Length];
            int total = gap * (tabs.Length - 1);
            for (int i = 0; i < tabs.Length; i++)
            {
                widths[i] = TextRenderer.MeasureText(tabs[i], measureFont).Width + 30;
                total += widths[i];
            }
            measureFont.Dispose();

            int x = (Width - total) / 2;
            for (int i = 0; i < tabs.Length; i++)
            {
                GamePillButton btn = new GamePillButton();
                btn.Text = tabs[i];
                btn.Size = new Size(widths[i], tabH);
                btn.Location = new Point(x, tabY);
                x += widths[i] + gap;

                string tabName = tabs[i];
                btn.Click += (s, e) => SelectTab(btn, tabName);

                this.Controls.Add(btn);
                tabButtons.Add(btn);
            }

            // ตารางบนการ์ดสีอ่อน
            dgvScores = new DataGridView();
            dgvScores.Location = new Point(cardRect.X + 14, cardRect.Y + 12);
            dgvScores.Size = new Size(cardRect.Width - 28, cardRect.Height - 24);
            dgvScores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvScores.BackgroundColor = CardColor;
            dgvScores.BorderStyle = BorderStyle.None;
            dgvScores.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvScores.GridColor = CardLine;
            dgvScores.RowHeadersVisible = false;
            dgvScores.ReadOnly = true;
            dgvScores.AllowUserToAddRows = false;
            dgvScores.AllowUserToResizeRows = false;
            dgvScores.AllowUserToResizeColumns = false;
            dgvScores.MultiSelect = false;
            dgvScores.TabStop = false;
            dgvScores.ScrollBars = ScrollBars.Vertical;
            dgvScores.EnableHeadersVisualStyles = false;

            dgvScores.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvScores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvScores.ColumnHeadersHeight = 34;
            dgvScores.ColumnHeadersDefaultCellStyle.BackColor = CardColor;
            dgvScores.ColumnHeadersDefaultCellStyle.ForeColor = MutedInk;
            dgvScores.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgvScores.ColumnHeadersDefaultCellStyle.SelectionBackColor = CardColor;
            dgvScores.ColumnHeadersDefaultCellStyle.SelectionForeColor = MutedInk;

            dgvScores.RowTemplate.Height = 40;
            dgvScores.DefaultCellStyle.BackColor = CardColor;
            dgvScores.DefaultCellStyle.ForeColor = InkColor;
            dgvScores.DefaultCellStyle.Font = new Font("Segoe UI", 10.5f, FontStyle.Regular);
            dgvScores.DefaultCellStyle.SelectionBackColor = CardColor;   // ไม่ไฮไลต์แถว
            dgvScores.DefaultCellStyle.SelectionForeColor = InkColor;

            // อันดับ 1-3 ใช้สีทอง / เงิน / ทองแดง
            dgvScores.CellFormatting += (s, ev) =>
            {
                if (ev.RowIndex < 0 || ev.RowIndex > 2) return;
                if (dgvScores.Columns[ev.ColumnIndex].Name != "Rank") return;

                ev.CellStyle.Font = rankFont;
                ev.CellStyle.ForeColor = ev.RowIndex == 0 ? GoldRank
                                       : ev.RowIndex == 1 ? SilverRank
                                       : BronzeRank;
            };
            this.Controls.Add(dgvScores);

            // ข้อความตอนยังไม่มีคะแนน
            lblEmpty = new Label();
            lblEmpty.Text = "ยังไม่มีคะแนน";
            lblEmpty.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
            lblEmpty.ForeColor = MutedInk;
            lblEmpty.BackColor = CardColor;
            lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
            lblEmpty.Size = new Size(cardRect.Width - 28, 60);
            lblEmpty.Location = new Point(cardRect.X + 14, cardRect.Y + (cardRect.Height - 60) / 2);
            lblEmpty.Visible = false;
            this.Controls.Add(lblEmpty);

            SelectTab(tabButtons[0], "TODAY");
        }

        private void SelectTab(GamePillButton selected, string tabName)
        {
            foreach (GamePillButton b in tabButtons)
            {
                b.Selected = (b == selected);
                b.Invalidate();
            }
            LoadScoreData(tabName);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // พื้นหลังไล่เฉดฟ้า เหมือนแผงด้านขวาของเกม
            using (var bg = new LinearGradientBrush(ClientRectangle, SkyTop, SkyBottom, 90f))
            {
                g.FillRectangle(bg, ClientRectangle);
            }

            // หัวข้อ: ตัวขาวมีเงาดำ
            using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                var area = new RectangleF(0, 18, Width, 46);
                var shadow = new RectangleF(area.X + 1.5f, area.Y + 1.5f, area.Width, area.Height);
                g.DrawString("HIGH SCORES", titleFont, Brushes.Black, shadow, sf);
                g.DrawString("HIGH SCORES", titleFont, Brushes.White, area, sf);
            }

            // การ์ดรองตาราง
            using (GraphicsPath card = RoundedRect(cardRect, 14))
            using (var fill = new SolidBrush(CardColor))
            using (var edge = new Pen(Color.FromArgb(50, 110, 20), 2))
            {
                g.FillPath(fill, card);
                g.DrawPath(edge, card);
            }

            // กรอบทองรอบหน้าต่าง
            using (var border = new Pen(GoldBorder, 6))
            {
                g.DrawRectangle(border, 3, 3, Width - 7, Height - 7);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) { this.Close(); return true; }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // คืนทรัพยากรฟอนต์ตอนปิดหน้าต่าง (ไม่ใช้ Dispose เพราะซ้ำกับไฟล์ Designer)
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            titleFont.Dispose();
            rankFont.Dispose();
            scoreFont.Dispose();
            base.OnFormClosed(e);
        }

        private static GraphicsPath RoundedRect(Rectangle r, int radius)
        {
            int d = radius * 2;
            var p = new GraphicsPath();
            p.AddArc(r.X, r.Y, d, d, 180, 90);
            p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            p.CloseFigure();
            return p;
        }

        // ==========================================
        // โหลดข้อมูล (ตรรกะเดิม)
        // ==========================================
        private void LoadScoreData(string filter)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Rank", typeof(int));
            dt.Columns.Add("Player", typeof(string));
            dt.Columns.Add("Score", typeof(int));
            dt.Columns.Add("Date", typeof(string));

            string filePath = "scores.txt";
            string todayStr = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime today = DateTime.Today;

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length >= 3)
                    {
                        string playerName = parts[0].Trim();
                        if (!int.TryParse(parts[1].Trim(), out int score)) continue;   // ข้ามบรรทัดที่ข้อมูลเสีย
                        string dateStr = parts[2].Trim();

                        bool passFilter = false;

                        if (filter == "ALL TIME")
                        {
                            passFilter = true;
                        }
                        else if (filter == "TODAY")
                        {
                            if (dateStr == todayStr) passFilter = true;
                        }
                        else if (filter == "LAST WEEK")
                        {
                            if (DateTime.TryParse(dateStr, out DateTime scoreDate))
                            {
                                if ((today - scoreDate).TotalDays <= 7) passFilter = true;
                            }
                        }
                        else if (filter == "LAST MONTH")
                        {
                            if (DateTime.TryParse(dateStr, out DateTime scoreDate))
                            {
                                if ((today - scoreDate).TotalDays <= 30) passFilter = true;
                            }
                        }
                        else if (filter == "MY SCORES")
                        {
                            string targetUser = string.IsNullOrEmpty(currentUserName) ? "FF" : currentUserName;
                            if (playerName.Equals(targetUser, StringComparison.OrdinalIgnoreCase))
                                passFilter = true;
                        }

                        if (passFilter)
                        {
                            dt.Rows.Add(0, playerName, score, dateStr);
                        }
                    }
                }
            }

            // เรียงคะแนนมากไปน้อย แล้วใส่เลข Rank
            DataView dv = dt.DefaultView;
            dv.Sort = "Score DESC";
            DataTable sortedDt = dv.ToTable();

            for (int i = 0; i < sortedDt.Rows.Count; i++)
            {
                sortedDt.Rows[i]["Rank"] = i + 1;
            }

            dgvScores.DataSource = sortedDt;

            // สัดส่วนคอลัมน์ + การจัดตำแหน่ง
            dgvScores.Columns["Rank"].FillWeight = 14;
            dgvScores.Columns["Player"].FillWeight = 40;
            dgvScores.Columns["Score"].FillWeight = 20;
            dgvScores.Columns["Date"].FillWeight = 26;

            dgvScores.Columns["Rank"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvScores.Columns["Rank"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvScores.Columns["Rank"].DefaultCellStyle.ForeColor = MutedInk;

            dgvScores.Columns["Score"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvScores.Columns["Score"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvScores.Columns["Score"].DefaultCellStyle.ForeColor = ScoreGreen;
            dgvScores.Columns["Score"].DefaultCellStyle.Font = scoreFont;

            dgvScores.Columns["Date"].DefaultCellStyle.ForeColor = MutedInk;

            dgvScores.ClearSelection();
            dgvScores.CurrentCell = null;

            // ไม่มีข้อมูล = โชว์ข้อความกลางการ์ดแทนตารางว่าง
            bool empty = sortedDt.Rows.Count == 0;
            lblEmpty.Visible = empty;
            if (empty) lblEmpty.BringToFront();
        }

        private void FormHighScore_Load(object sender, EventArgs e)
        {

        }
    }

    // ==========================================
    // ปุ่มแคปซูล / วงกลม สไตล์ปุ่มเขียวในเกม
    // ==========================================
    public class GamePillButton : Control
    {
        public bool Selected { get; set; }
        public bool IsCircle { get; set; }

        private bool _hover;
        private readonly Font _font = new Font("Segoe UI", 9f, FontStyle.Bold);

        public GamePillButton()
        {
            SetStyle(ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Selectable, false);

            BackColor = Color.Transparent;
            Cursor = Cursors.Hand;
            TabStop = false;
        }

        protected override void OnMouseEnter(EventArgs e) { base.OnMouseEnter(e); _hover = true; Invalidate(); }
        protected override void OnMouseLeave(EventArgs e) { base.OnMouseLeave(e); _hover = false; Invalidate(); }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var r = new Rectangle(2, 2, Width - 5, Height - 5);

            Color fill, edge;
            float edgeW;
            if (Selected || IsCircle)
            {
                // เขียวเหมือนปุ่ม SFX / BGM ในเกม
                fill = _hover ? Color.FromArgb(135, 215, 70) : Color.FromArgb(110, 190, 50);
                edge = Color.FromArgb(50, 110, 20);
                edgeW = 3f;
            }
            else
            {
                // แท็บที่ไม่ได้เลือก = ฟ้าเข้มโปร่ง
                fill = Color.FromArgb(_hover ? 120 : 70, 40, 90, 150);
                edge = Color.FromArgb(150, 255, 255, 255);
                edgeW = 1.5f;
            }

            using (GraphicsPath path = new GraphicsPath())
            {
                if (IsCircle)
                {
                    path.AddEllipse(r);
                }
                else
                {
                    int d = r.Height;
                    path.AddArc(r.X, r.Y, d, d, 90, 180);
                    path.AddArc(r.Right - d, r.Y, d, d, 270, 180);
                    path.CloseFigure();
                }

                using (var fb = new SolidBrush(fill)) g.FillPath(fb, path);
                using (var pen = new Pen(edge, edgeW)) g.DrawPath(pen, path);
            }

            // ตัวหนังสือขาวมีเงาดำ
            using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                var area = new RectangleF(0, 0, Width, Height);
                var shadow = new RectangleF(1, 1, Width, Height);
                g.DrawString(Text, _font, Brushes.Black, shadow, sf);
                g.DrawString(Text, _font, Brushes.White, area, sf);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _font.Dispose();
            base.Dispose(disposing);
        }
    }
}