using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Sec2_Group1_HappyJumpingFrog
{
    public partial class FormHighScore : Form
    {
        private DataGridView dgvScores;
        private Button btnClose;
        private List<Button> tabButtons = new List<Button>();

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
            currentUserName = playerName; // เก็บชื่อผู้เล่นไว้ใช้เช็กในแท็บ MY SCORES
            SaveScoreToFile(playerName, score); // บันทึกข้อมูลลงไฟล์ scores.txt ทันที
            SetupUI();
        }

        // 3. ฟังก์ชันสำหรับบันทึกข้อมูลลงไฟล์ .txt
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

        private void SetupUI()
        {
            this.Text = "High Scores";
            this.ClientSize = new Size(580, 420);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(253, 253, 253); // ขาวนวลสะอาดตาแบบมินิมอล

            // ปุ่มปิด (Minimal X)
            btnClose = new Button();
            btnClose.Text = "✕";
            btnClose.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            btnClose.ForeColor = Color.FromArgb(150, 150, 150);
            btnClose.BackColor = Color.Transparent;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 240, 240);
            btnClose.Location = new Point(535, 12);
            btnClose.Size = new Size(32, 32);
            btnClose.Cursor = Cursors.Hand;
            btnClose.Click += (sender, e) => { this.Close(); };
            this.Controls.Add(btnClose);

            // แท็บเมนูด้านบน (สไตล์มินิมอล ตัวหนังสือสะอาดตา)
            string[] tabs = { "TODAY", "LAST WEEK", "LAST MONTH", "ALL TIME", "MY SCORES" };
            int startX = 25;
            int startY = 22;

            for (int i = 0; i < tabs.Length; i++)
            {
                Button btn = new Button();
                btn.Text = tabs[i];
                btn.Font = new Font("Segoe UI", 9, (tabs[i] == "TODAY") ? FontStyle.Bold : FontStyle.Regular);
                btn.ForeColor = (tabs[i] == "TODAY") ? Color.FromArgb(40, 40, 40) : Color.FromArgb(160, 160, 160);
                btn.BackColor = Color.Transparent;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Location = new Point(startX, startY);
                btn.AutoSize = true;
                btn.Cursor = Cursors.Hand;

                string tabName = tabs[i];

                btn.Click += (sender, e) => {
                    foreach (Button b in tabButtons)
                    {
                        b.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                        b.ForeColor = Color.FromArgb(160, 160, 160);
                    }
                    btn.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                    btn.ForeColor = Color.FromArgb(40, 40, 40);
                    LoadScoreData(tabName);
                };

                this.Controls.Add(btn);
                tabButtons.Add(btn);
                startX += btn.Width + 12;
            }

            // เส้นคั่นบางๆ แบบจางมากๆ
            Panel line = new Panel();
            line.Size = new Size(530, 1);
            line.Location = new Point(25, 55);
            line.BackColor = Color.FromArgb(235, 235, 235);
            this.Controls.Add(line);

            // DataGridView สไตล์มินิมอล (ไร้เส้นตารางรกตา)
            dgvScores = new DataGridView();
            dgvScores.Location = new Point(25, 70);
            dgvScores.Size = new Size(530, 320);
            dgvScores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvScores.BackgroundColor = Color.FromArgb(253, 253, 253);
            dgvScores.BorderStyle = BorderStyle.None;
            dgvScores.CellBorderStyle = DataGridViewCellBorderStyle.None; // ซ่อนเส้นตารางเพื่อให้ดูคลีนที่สุด
            dgvScores.RowHeadersVisible = false;
            dgvScores.ReadOnly = true;
            dgvScores.ScrollBars = ScrollBars.Vertical;
            dgvScores.EnableHeadersVisualStyles = false;

            // ปรับแต่งสีและฟอนต์หัวตาราง (Header) ให้ดูเบาสบาย
            dgvScores.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(253, 253, 253);
            dgvScores.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(140, 140, 140);
            dgvScores.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvScores.ColumnHeadersDefaultCellStyle.Padding = new Padding(0, 5, 0, 5);
            dgvScores.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // ปรับแต่งสีและฟอนต์ข้อมูลในแถว (Rows)
            dgvScores.DefaultCellStyle.BackColor = Color.FromArgb(253, 253, 253);
            dgvScores.DefaultCellStyle.ForeColor = Color.FromArgb(60, 60, 60);
            dgvScores.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dgvScores.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 245, 245);
            dgvScores.DefaultCellStyle.SelectionForeColor = Color.FromArgb(40, 40, 40);
            dgvScores.DefaultCellStyle.Padding = new Padding(0, 8, 0, 8);
            dgvScores.RowTemplate.Height = 35;

            this.Controls.Add(dgvScores);

            LoadScoreData("TODAY");
        }

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
                        int score = Convert.ToInt32(parts[1].Trim());
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

            // จัดเรียงข้อมูลใน DataTable ให้คะแนนมากที่สุดขึ้นก่อน (Score DESC)
            DataView dv = dt.DefaultView;
            dv.Sort = "Score DESC";
            DataTable sortedDt = dv.ToTable();

            // กำหนดเลข Rank ใหม่ตามลำดับคะแนนที่เรียงแล้ว
            for (int i = 0; i < sortedDt.Rows.Count; i++)
            {
                sortedDt.Rows[i]["Rank"] = i + 1;
            }

            dgvScores.DataSource = sortedDt;

            dgvScores.Columns["Rank"].FillWeight = 15;
            dgvScores.Columns["Player"].FillWeight = 45;
            dgvScores.Columns["Score"].FillWeight = 20;
            dgvScores.Columns["Date"].FillWeight = 30;
        }
    }
}