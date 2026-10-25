using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sec2_Group1_HappyJumpingFrog.FormGame_Window_
{
    public partial class FormHelpScreen : Form
    {
        public FormHelpScreen()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormHelpScreen_Load(object sender, EventArgs e)
        {
            CenterPictureHelp();
        }
        private void CenterPictureHelp()
        {
            pictureHelp.Left =
                (this.ClientSize.Width - pictureHelp.Width) / 2;

            pictureHelp.Top =
                (this.ClientSize.Height - pictureHelp.Height) / 2;
        }

        private void btnBackformHelp_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
