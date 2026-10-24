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
        private List<Button> tabButtons = new List<Button>(); // ประกาศเก็บรายชื่อปุ่มแท็บเพื่อใช้ควบคุมการเปลี่ยนสี

        public FormHighScore()
        {
            SetupUI();
        }

        private void SetupUI()
        {
            // ตั้งค่าหน้าต่าง Form ให้อยู่กึ่งกลางจอพอดี
            this.Text = "High Scores";
            this.ClientSize = new Size(600, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None; // ซ่อนขอบหน้าต่างปกติเพื่อให้เหมือนเว็บ
            this.BackColor = Color.White;

            // วาดขอบหน้าต่างสีเทาอ่อนๆ ให้ดูมีมิติ
            this.Paint += (sender, e) =>
            {
                e.Graphics.DrawRectangle(Pens.LightGray, 0, 0, this.ClientSize.Width - 1, this.ClientSize.Height - 1);
            };

            // ปุ่มปิด (กากบาท X มุมขวาบน)
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

            // สร้างปุ่มแท็บเมนูด้านบน
            string[] tabs = { "TODAY", "LAST WEEK", "LAST MONTH", "ALL TIME", "MY SCORES" };
            int startX = 20;
            int startY = 20;

            for (int i = 0; i < tabs.Length; i++)
            {
                Button btn = new Button();
                btn.Text = tabs[i];
                btn.Font = new Font("Arial", 10, FontStyle.Bold);

                // กำหนดให้ TODAY เป็นสีดำตั้งต้น (เลือกอยู่) ปุ่มอื่นเป็นสีเทา
                btn.ForeColor = (tabs[i] == "TODAY") ? Color.Black : Color.Gray;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Location = new Point(startX, startY);
                btn.AutoSize = true;
                btn.Cursor = Cursors.Hand;

                string tabName = tabs[i];

                // เหตุการณ์เมื่อคลิกเลือกแท็บ
                btn.Click += (sender, e) => {
                    // เปลี่ยนสีปุ่มทั้งหมดกลับเป็นสีเทา
                    foreach (Button b in tabButtons)
                    {
                        b.ForeColor = Color.Gray;
                    }
                    // เปลี่ยนเฉพาะปุ่มที่ถูกคลิกให้เป็นสีดำเข้ม
                    btn.ForeColor = Color.Black;

                    // โหลดข้อมูลตามแท็บ
                    LoadScoreData(tabName);
                };

                this.Controls.Add(btn);
                tabButtons.Add(btn); // เก็บลงในลิสต์ควบคุม
                startX += btn.Width + 15;
            }

            // เส้นคั่นใต้เมนูแท็บ
            Panel line = new Panel();
            line.Size = new Size(560, 1);
            line.Location = new Point(20, 60);
            line.BackColor = Color.LightGray;
            this.Controls.Add(line);

            // สร้างตาราง DataGridView
            dgvScores = new DataGridView();
            dgvScores.Location = new Point(20, 80);
            dgvScores.Size = new Size(560, 340);

            // [จุดสำคัญ] เปลี่ยนมาใช้โหมด Fill เพื่อเกลี่ยขนาดให้พอดีตารางเป๊ะๆ ไม่มีเลื่อนแนวนอน
            dgvScores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvScores.BackgroundColor = Color.White;
            dgvScores.BorderStyle = BorderStyle.None;
            dgvScores.RowHeadersVisible = false;
            dgvScores.ReadOnly = true;
            dgvScores.ScrollBars = ScrollBars.Vertical;

            this.Controls.Add(dgvScores);

            // สั่งเปิดมาให้เลือกแท็บ TODAY เป็นค่าเริ่มต้นทันที
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
                int rank = 1;

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
                            passFilter = true; // แสดงทั้งหมด
                        }
                        else if (filter == "TODAY")
                        {
                            if (dateStr == todayStr) passFilter = true; // แสดงเฉพาะวันนี้
                        }
                        else if (filter == "LAST WEEK")
                        {
                            if (DateTime.TryParse(dateStr, out DateTime scoreDate))
                            {
                                if ((today - scoreDate).TotalDays <= 7) passFilter = true; // ย้อนหลัง 7 วัน
                            }
                        }
                        else if (filter == "LAST MONTH")
                        {
                            if (DateTime.TryParse(dateStr, out DateTime scoreDate))
                            {
                                if ((today - scoreDate).TotalDays <= 30) passFilter = true; // ย้อนหลัง 30 วัน
                            }
                        }
                        else if (filter == "MY SCORES")
                        {
                            if (playerName == "FF") passFilter = true; // กรองเฉพาะชื่อผู้เล่น
                        }

                        if (passFilter)
                        {
                            dt.Rows.Add(rank, playerName, score, dateStr);
                            rank++;
                        }
                    }
                }
            }
            else
            {
                dt.Rows.Add(1, "FF", 0, DateTime.Now.ToString("yyyy-MM-dd"));
            }

            dgvScores.DataSource = dt;

            // [จุดสำคัญ] กำหนดสัดส่วนความกว้าง (FillWeight) แทนการระบุพิกเซล (.Width) เพื่อให้พอดีกรอบเป๊ะๆ
            dgvScores.Columns["Rank"].FillWeight = 15;   // ช่องอันดับ (แคบ)
            dgvScores.Columns["Player"].FillWeight = 40; // ช่องชื่อผู้เล่น (กว้าง)
            dgvScores.Columns["Score"].FillWeight = 20;  // ช่องคะแนน (กลาง)
            dgvScores.Columns["Date"].FillWeight = 35;   // ช่องวันที่ (กว้างพอดี)
        }
    }
}

