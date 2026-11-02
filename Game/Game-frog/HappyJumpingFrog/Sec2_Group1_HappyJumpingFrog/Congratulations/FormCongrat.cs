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
        public FormCongrat()
        {
            InitializeComponent();
        }
        public FormCongrat(int score)
        {
            currentScore = score;
        }
        private int currentScore;


        private void button1_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
            FormSubmitScore submitForm = new FormSubmitScore(currentScore);
            submitForm.ShowDialog(this);
        }
    }
}
