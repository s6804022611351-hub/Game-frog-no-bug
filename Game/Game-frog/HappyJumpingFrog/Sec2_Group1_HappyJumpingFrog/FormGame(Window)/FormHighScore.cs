using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            this.ClientSize = new Size(600, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;

            this.Paint += (sender, e) =>
            {
                e.Graphics.DrawRectangle(Pens.LightGray, 0, 0, this.ClientSize.Width - 1, this.ClientSize.Height - 1);
            };

            btnClose = new Button();
            btnClose.Text = "✕";
            btnClose.Font = new Font("Arial", 12, FontStyle.Bold);
            btnClose.ForeColor = Color.Gray;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Location = new Point(555, 12);
            btnClose.Size = new Size(35, 35);
            btnClose.Click += (sender, e) => { this.Close(); };
            this.Controls.Add(btnClose);

            string[] tabs = { "TODAY", "LAST WEEK", "LAST MONTH", "ALL TIME", "MY SCORES" };
            int startX = 20;
            int startY = 20;

            for (int i = 0; i < tabs.Length; i++)
            {
                Button btn = new Button();
                btn.Text = tabs[i];
                btn.Font = new Font("Arial", 10, FontStyle.Bold);
                btn.ForeColor = (tabs[i] == "TODAY") ? Color.Black : Color.Gray;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Location = new Point(startX, startY);
                btn.AutoSize = true;
                btn.Cursor = Cursors.Hand;

                string tabName = tabs[i];

                btn.Click += (sender, e) => {
                    foreach (Button b in tabButtons)
                    {
                        b.ForeColor = Color.Gray;
                    }
                    btn.ForeColor = Color.Black;
                    LoadScoreData(tabName);
                };

                this.Controls.Add(btn);
                tabButtons.Add(btn);
                startX += btn.Width + 15;
            }

            Panel line = new Panel();
            line.Size = new Size(560, 1);
            line.Location = new Point(20, 60);
            line.BackColor = Color.LightGray;
            this.Controls.Add(line);

            dgvScores = new DataGridView();
            dgvScores.Location = new Point(20, 80);
            dgvScores.Size = new Size(560, 340);
            dgvScores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvScores.BackgroundColor = Color.White;
            dgvScores.BorderStyle = BorderStyle.None;
            dgvScores.RowHeadersVisible = false;
            dgvScores.ReadOnly = true;
            dgvScores.ScrollBars = ScrollBars.Vertical;
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
                            // ยังไม่ใส่ Rank ตอนนี้ เดี๋ยวมาจัดเรียงและใส่ทีหลัง
                            dt.Rows.Add(0, playerName, score, dateStr);
                        }
                    }
                }
            }

            // --- [จุดที่แก้ไข] จัดเรียงข้อมูลใน DataTable ให้คะแนนมากที่สุดขึ้นก่อน (Score DESC) ---
            DataView dv = dt.DefaultView;
            dv.Sort = "Score DESC";
            DataTable sortedDt = dv.ToTable();

            // กำหนดเลข Rank ใหม่ตามลำดับคะแนนที่เรียงแล้ว (1, 2, 3, ...)
            for (int i = 0; i < sortedDt.Rows.Count; i++)
            {
                sortedDt.Rows[i]["Rank"] = i + 1;
            }

            dgvScores.DataSource = sortedDt;

            dgvScores.Columns["Rank"].FillWeight = 15;
            dgvScores.Columns["Player"].FillWeight = 40;
            dgvScores.Columns["Score"].FillWeight = 20;
            dgvScores.Columns["Date"].FillWeight = 35;
        }
    }
}

