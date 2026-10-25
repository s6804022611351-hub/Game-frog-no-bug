namespace Sec2_Group1_HappyJumpingFrog
{
    partial class FormMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnplay = new Button();
            btnhelp = new Button();
            btnhighscores = new Button();
            btnMoreGame = new Button();
            SuspendLayout();
            // 
            // btnplay
            // 
            btnplay.BackColor = Color.FromArgb(192, 255, 255);
            btnplay.BackgroundImageLayout = ImageLayout.None;
            btnplay.Cursor = Cursors.IBeam;
            btnplay.Font = new Font("Palatino Linotype", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnplay.ForeColor = Color.MidnightBlue;
            btnplay.Location = new Point(1300, 100);
            btnplay.Name = "btnplay";
            btnplay.Size = new Size(420, 100);
            btnplay.TabIndex = 0;
            btnplay.Text = "Play";
            btnplay.UseVisualStyleBackColor = false;
            btnplay.Click += btnplay_Click;
            // 
            // btnhelp
            // 
            btnhelp.BackColor = Color.FromArgb(192, 255, 255);
            btnhelp.BackgroundImageLayout = ImageLayout.None;
            btnhelp.Cursor = Cursors.IBeam;
            btnhelp.Font = new Font("Palatino Linotype", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnhelp.ForeColor = Color.MidnightBlue;
            btnhelp.Location = new Point(1300, 250);
            btnhelp.Name = "btnhelp";
            btnhelp.Size = new Size(420, 100);
            btnhelp.TabIndex = 1;
            btnhelp.Text = "Help";
            btnhelp.UseVisualStyleBackColor = false;
            // 
            // btnhighscores
            // 
            btnhighscores.BackColor = Color.FromArgb(192, 255, 255);
            btnhighscores.BackgroundImageLayout = ImageLayout.None;
            btnhighscores.Cursor = Cursors.IBeam;
            btnhighscores.Font = new Font("Palatino Linotype", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnhighscores.ForeColor = Color.MidnightBlue;
            btnhighscores.Location = new Point(1300, 400);
            btnhighscores.Name = "btnhighscores";
            btnhighscores.Size = new Size(420, 100);
            btnhighscores.TabIndex = 2;
            btnhighscores.Text = "High Scores";
            btnhighscores.UseVisualStyleBackColor = false;
            btnhighscores.Click += btnhighscores_Click;
            // 
            // btnMoreGame
            // 
            btnMoreGame.BackColor = Color.FromArgb(192, 255, 255);
            btnMoreGame.BackgroundImageLayout = ImageLayout.None;
            btnMoreGame.Cursor = Cursors.IBeam;
            btnMoreGame.Font = new Font("Palatino Linotype", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnMoreGame.ForeColor = Color.MidnightBlue;
            btnMoreGame.Location = new Point(1300, 550);
            btnMoreGame.Name = "btnMoreGame";
            btnMoreGame.Size = new Size(420, 100);
            btnMoreGame.TabIndex = 3;
            btnMoreGame.Text = "More Games";
            btnMoreGame.UseVisualStyleBackColor = false;
            // 
            // FormMenu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.BackgroundFormMenu;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(1792, 799);
            Controls.Add(btnMoreGame);
            Controls.Add(btnhighscores);
            Controls.Add(btnhelp);
            Controls.Add(btnplay);
            Name = "FormMenu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Happy Jumping frog";
            WindowState = FormWindowState.Maximized;
            Load += FormMenu_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnplay;
        private Button btnhelp;
        private Button btnhighscores;
        private Button btnMoreGame;
    }
}