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
    public partial class FormScoreSubmitted : Form
    {
        private string _playerName = "";
        private int _score = 0;

        public FormScoreSubmitted(string playerName, int score)
        {
            InitializeComponent();
            _playerName = playerName;
            _score = score;
        }

        private void btnviewsco_Click(object sender, EventArgs e)
        {
            this.Hide();

            // เปิดหน้า High Score
            FormHighScore highScoreForm = new FormHighScore(_playerName, _score);
            highScoreForm.ShowDialog(this);

            this.Close();
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
