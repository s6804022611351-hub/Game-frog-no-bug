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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMenu));
            btnplay = new Button();
            btnhelp = new Button();
            btnhighscores = new Button();
            btnMoreGame = new Button();
            SuspendLayout();
            // 
            // btnplay
            // 
            btnplay.BackColor = Color.Transparent;
            btnplay.BackgroundImage = (Image)resources.GetObject("btnplay.BackgroundImage");
            btnplay.BackgroundImageLayout = ImageLayout.Zoom;
            btnplay.Cursor = Cursors.Hand;
            btnplay.FlatAppearance.BorderSize = 0;
            btnplay.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnplay.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnplay.FlatStyle = FlatStyle.Flat;
            btnplay.Font = new Font("Javanese Text", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnplay.ForeColor = Color.Teal;
            btnplay.Location = new Point(1300, 100);
            btnplay.Name = "btnplay";
            btnplay.Size = new Size(485, 200);
            btnplay.TabIndex = 0;
            btnplay.Text = "Play";
            btnplay.UseVisualStyleBackColor = false;
            btnplay.Click += btnplay_Click;
            // 
            // btnhelp
            // 
            btnhelp.BackColor = Color.Transparent;
            btnhelp.BackgroundImage = (Image)resources.GetObject("btnhelp.BackgroundImage");
            btnhelp.BackgroundImageLayout = ImageLayout.Zoom;
            btnhelp.Cursor = Cursors.Hand;
            btnhelp.FlatAppearance.BorderSize = 0;
            btnhelp.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnhelp.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnhelp.FlatStyle = FlatStyle.Flat;
            btnhelp.Font = new Font("Javanese Text", 22.2F, FontStyle.Bold);
            btnhelp.ForeColor = Color.Teal;
            btnhelp.Location = new Point(1300, 260);
            btnhelp.Name = "btnhelp";
            btnhelp.Size = new Size(485, 200);
            btnhelp.TabIndex = 1;
            btnhelp.Text = "Help";
            btnhelp.UseVisualStyleBackColor = false;
            btnhelp.Click += btnhelp_Click;
            // 
            // btnhighscores
            // 
            btnhighscores.BackColor = Color.Transparent;
            btnhighscores.BackgroundImage = (Image)resources.GetObject("btnhighscores.BackgroundImage");
            btnhighscores.BackgroundImageLayout = ImageLayout.Zoom;
            btnhighscores.Cursor = Cursors.Hand;
            btnhighscores.FlatAppearance.BorderSize = 0;
            btnhighscores.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnhighscores.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnhighscores.FlatStyle = FlatStyle.Flat;
            btnhighscores.Font = new Font("Javanese Text", 22.2F, FontStyle.Bold);
            btnhighscores.ForeColor = Color.Teal;
            btnhighscores.Location = new Point(1300, 420);
            btnhighscores.Name = "btnhighscores";
            btnhighscores.Size = new Size(485, 200);
            btnhighscores.TabIndex = 2;
            btnhighscores.Text = "High Scores";
            btnhighscores.UseVisualStyleBackColor = false;
            btnhighscores.Click += btnhighscores_Click;
            // 
            // btnMoreGame
            // 
            btnMoreGame.BackColor = Color.Transparent;
            btnMoreGame.BackgroundImage = (Image)resources.GetObject("btnMoreGame.BackgroundImage");
            btnMoreGame.BackgroundImageLayout = ImageLayout.Zoom;
            btnMoreGame.Cursor = Cursors.Hand;
            btnMoreGame.FlatAppearance.BorderSize = 0;
            btnMoreGame.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnMoreGame.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnMoreGame.FlatStyle = FlatStyle.Flat;
            btnMoreGame.Font = new Font("Javanese Text", 22.2F, FontStyle.Bold);
            btnMoreGame.ForeColor = Color.Teal;
            btnMoreGame.Location = new Point(1300, 578);
            btnMoreGame.Name = "btnMoreGame";
            btnMoreGame.Size = new Size(485, 200);
            btnMoreGame.TabIndex = 3;
            btnMoreGame.Text = "More Games\r\n";
            btnMoreGame.UseVisualStyleBackColor = false;
            btnMoreGame.Click += btnMoreGame_Click;
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