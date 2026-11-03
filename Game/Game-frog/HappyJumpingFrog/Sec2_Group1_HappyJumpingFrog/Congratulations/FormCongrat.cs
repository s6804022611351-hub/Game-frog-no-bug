using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sec2_Group1_HappyJumpingFrog.Congratulations
{
    public partial class FormCongrat : Form
    {
        private int currentScore;

        public FormCongrat()
        {
            InitializeComponent();
        }

        public FormCongrat(int score) : this()
        {
            currentScore = score;
        }

        // ปุ่ม End Game -> กลับหน้าเมนู
        private void button1_Click_2(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is FormMenu)
                {
                    f.Show();
                    f.Activate();
                    break;
                }
            }
            this.Close();
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
            FormSubmitScore submitForm = new FormSubmitScore(currentScore);
            submitForm.ShowDialog(this);
        }

        private void FormCongrat_Load(object sender, EventArgs e)
        {

        }
    }
}