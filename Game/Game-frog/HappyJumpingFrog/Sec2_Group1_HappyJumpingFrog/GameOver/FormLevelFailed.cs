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

namespace Sec2_Group1_HappyJumpingFrog.GameOver
{
    public partial class FormLevelFailed : Form
    {
        private int _score;

        public FormLevelFailed(int score)
        {
            InitializeComponent();
            _score = score;
        }

        private void btntryagin_Click(object sender, EventArgs e)
        {
            this.Hide();

            // 2. เปิดหน้าเกมใหม่ขึ้นมา (เปลี่ยน FormGame เป็นชื่อหน้าเกมหลักของคุณ)
            FormGame gameForm = new FormGame();
            gameForm.ShowDialog(this);

            // 3. ปิดหน้าต่างคั่นตอนตายนี้ถาวร
            this.Close();
        }

        private void btnend_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormGameOver gameOverForm = new FormGameOver(_score); // ส่งคะแนนข้ามไปตรงนี้
            gameOverForm.ShowDialog(this);
            this.Close();
        }
    }
}
