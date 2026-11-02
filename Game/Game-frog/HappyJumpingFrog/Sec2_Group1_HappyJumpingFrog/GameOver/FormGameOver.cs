using System;
using System.Windows.Forms;

namespace Sec2_Group1_HappyJumpingFrog
{
    public partial class FormGameOver : Form
    {
        private int currentScore;

        public FormGameOver(int score)
        {
            InitializeComponent();
            currentScore = score;
            Score.Text = currentScore.ToString();
            this.WindowState = FormWindowState.Maximized;
        }

        public FormGameOver()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void FormGameOver_Load(object sender, EventArgs e)
        {
            // ไม่ต้องใส่โค้ดคำนวณพิกัดใดๆ ปล่อยให้ระบบ Anchor จัดการตำแหน่งให้อัตโนมัติ
        }

        private void btnSubmitScore_Click(object sender, EventArgs e)
        {
            FormSubmitScore submitForm = new FormSubmitScore(currentScore);
            submitForm.ShowDialog(this);
            this.Close();
        }

        private void btnsub_score_Click(object sender, EventArgs e)
        {
            FormSubmitScore submitForm = new FormSubmitScore(currentScore);
            submitForm.ShowDialog(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormMenu menuForm = new FormMenu();
            menuForm.ShowDialog(this);
            this.Close();
        }
    }
}