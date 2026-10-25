using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sec2_Group1_HappyJumpingFrog
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
            /////////
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
           
        }

        private void btnplay_Click(object sender, EventArgs e)
        {
            FormGame game = new FormGame();    
            game.Show();
            this.Hide();
        }

        private void btnhighscores_Click(object sender, EventArgs e)
        {
            FormHighScore highScoreForm = new FormHighScore();
            highScoreForm.ShowDialog(this);
        }
    }
}
