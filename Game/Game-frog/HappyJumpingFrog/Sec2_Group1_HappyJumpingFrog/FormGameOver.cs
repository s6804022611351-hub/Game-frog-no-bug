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
    public partial class FormGameOver : Form
    {
        private int currentScore;

        // 1. รับค่าคะแนนจากหน้าเกมมาแสดงผลที่ Label ชื่อ Score ในฟอร์มนี้ทันที
        public FormGameOver(int score)
        {
            InitializeComponent();
            currentScore = score;

            // นำคะแนนมาแสดงที่ Label ชื่อ Score (ถ้าขึ้นเส้นแดง ให้เช็ค Modifier เป็น Public ตามด้านล่างครับ)
            Score.Text = currentScore.ToString();
        }

        public FormGameOver()
        {
            InitializeComponent();
        }

        private void FormGameOver_Load(object sender, EventArgs e)
        {
        }



        // ปุ่ม Submit Score (ถ้าจะส่งคะแนนไปหน้ากรอกชื่อต่อ)
        private void btnSubmitScore_Click(object sender, EventArgs e)
        {
            FormSubmitScore submitForm = new FormSubmitScore(currentScore);
            submitForm.ShowDialog(this);
            this.Close();
        }

        private void btnsub_score_Click(object sender, EventArgs e)
        {

            // เปิดหน้ากรอกชื่อ/บันทึกคะแนนแบบค้างไว้
            FormSubmitScore submitForm = new FormSubmitScore(currentScore);
            submitForm.ShowDialog(this);

        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.Hide();

            FormMenu menuForm = new FormMenu();
            menuForm.ShowDialog(this);

            // 3. ปิดหน้าจอ Game Over นี้ลง
            this.Close();
        }
    }
}
