using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Formats.Asn1.AsnWriter;

namespace Sec2_Group1_HappyJumpingFrog
{
    public partial class FormSubmitScore : Form
    {
        private int finalScore;

        // 1. เพิ่ม Constructor ตัวนี้เข้ามารับค่าคะแนนจากหน้าเกม
        public FormSubmitScore(int scoreValue)
        {
            InitializeComponent();

            finalScore = scoreValue;

            // นำคะแนนที่รับมาแสดงผลที่ Label ชื่อ score
            Score.Text = finalScore.ToString();
        }

        // (เผื่อไว้) คง Constructor เดิมไว้ด้วยกัน Error กรณีมีการเรียกแบบไม่มีพารามิเตอร์ที่อื่น
        public FormSubmitScore()
        {
            InitializeComponent();
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
            string playerName = textentername.Text.Trim();

            if (string.IsNullOrEmpty(playerName))
            {
                playerName = "Player"; // หรือชื่อสำรอง
            }

            // 2. ซ่อนหน้ากรอกชื่อนี้ไว้ก่อน
            this.Hide();

            // 2. เปิดหน้า Score Submitted (ส่งชื่อและคะแนนไปด้วย)
            FormScoreSubmitted submittedForm = new FormScoreSubmitted(playerName, finalScore);
            submittedForm.ShowDialog(this);

            // 4. ปิดหน้ากรอกชื่อนี้ถาวร
            this.Close();
        }
    }
}
