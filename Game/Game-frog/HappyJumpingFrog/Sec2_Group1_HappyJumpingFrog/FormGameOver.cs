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
            this.Hide();

            // เปิดหน้ากรอกชื่อ/บันทึกคะแนนแบบค้างไว้
            FormSubmitScore submitForm = new FormSubmitScore(currentScore);
            submitForm.ShowDialog(this);

            // ปิดหน้า Game Over นี้อย่างถาวรเมื่อใช้งานเสร็จ
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.Hide();

            // 1. สร้างหน้าเกมใหม่ขึ้นมา
            FormGame newGame = new FormGame();

            // 2. เปิดหน้าเกมใหม่ขึ้นมา
            newGame.Show();

            // 3. ปิดหน้าจอ Game Over นี้ลง
            this.Close();
        }
    }
}
