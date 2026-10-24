using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sec2_Group1_HappyJumpingFrog
{
    public partial class FormScoreSubmitted : Form
    {
        public FormScoreSubmitted()
        {
            InitializeComponent();
        }

        private void btnviewsco_Click(object sender, EventArgs e)
        {
            // ซ่อนหน้ากรอกชื่อชั่วคราว
            this.Hide();

            // เปิดหน้าต่าง Score Submitted
            FormHighScore leaderboardForm = new FormHighScore();
            leaderboardForm.ShowDialog(this);

            
        }
    }
}
