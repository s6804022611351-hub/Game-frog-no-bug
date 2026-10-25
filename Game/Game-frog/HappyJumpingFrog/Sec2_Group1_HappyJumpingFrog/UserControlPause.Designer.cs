namespace Sec2_Group1_HappyJumpingFrog
{
    partial class UserControlPause
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlPause));
            btnContinue = new Button();
            btnMenu = new Button();
            btnSound = new Button();
            btnMusic = new Button();
            btnClose = new Button();
            SuspendLayout();
            // 
            // btnContinue
            // 
            btnContinue.FlatAppearance.BorderSize = 0;
            btnContinue.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnContinue.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnContinue.FlatStyle = FlatStyle.Flat;
            btnContinue.Font = new Font("Sylfaen", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnContinue.ForeColor = SystemColors.ButtonHighlight;
            btnContinue.Location = new Point(47, 41);
            btnContinue.Name = "btnContinue";
            btnContinue.Size = new Size(388, 76);
            btnContinue.TabIndex = 0;
            btnContinue.Text = "Continue";
            btnContinue.UseVisualStyleBackColor = true;
            btnContinue.Click += btnContinue_Click;
            // 
            // btnMenu
            // 
            btnMenu.FlatAppearance.BorderSize = 0;
            btnMenu.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnMenu.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnMenu.FlatStyle = FlatStyle.Flat;
            btnMenu.Font = new Font("Sylfaen", 26.25F, FontStyle.Bold);
            btnMenu.ForeColor = SystemColors.ButtonHighlight;
            btnMenu.Location = new Point(47, 145);
            btnMenu.Name = "btnMenu";
            btnMenu.Size = new Size(388, 73);
            btnMenu.TabIndex = 1;
            btnMenu.Text = "Menu";
            btnMenu.UseVisualStyleBackColor = true;
            btnMenu.Click += btnMenu_Click;
            // 
            // btnSound
            // 
            btnSound.FlatAppearance.BorderSize = 0;
            btnSound.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnSound.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnSound.FlatStyle = FlatStyle.Flat;
            btnSound.Font = new Font("Sylfaen", 26.25F, FontStyle.Bold);
            btnSound.ForeColor = SystemColors.ButtonHighlight;
            btnSound.Location = new Point(47, 239);
            btnSound.Name = "btnSound";
            btnSound.Size = new Size(388, 83);
            btnSound.TabIndex = 2;
            btnSound.Text = "Sound";
            btnSound.UseVisualStyleBackColor = true;
            btnSound.Click += btnSound_Click;
            // 
            // btnMusic
            // 
            btnMusic.FlatAppearance.BorderSize = 0;
            btnMusic.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnMusic.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnMusic.FlatStyle = FlatStyle.Flat;
            btnMusic.Font = new Font("Sylfaen", 26.25F, FontStyle.Bold);
            btnMusic.ForeColor = SystemColors.ButtonHighlight;
            btnMusic.Location = new Point(47, 341);
            btnMusic.Name = "btnMusic";
            btnMusic.Size = new Size(388, 79);
            btnMusic.TabIndex = 3;
            btnMusic.Text = "Music";
            btnMusic.UseVisualStyleBackColor = true;
            btnMusic.Click += btnMusic_Click;
            // 
            // btnClose
            // 
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnClose.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Location = new Point(411, 18);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(46, 45);
            btnClose.TabIndex = 4;
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // UserControlPause
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            Controls.Add(btnClose);
            Controls.Add(btnMusic);
            Controls.Add(btnSound);
            Controls.Add(btnMenu);
            Controls.Add(btnContinue);
            DoubleBuffered = true;
            Name = "UserControlPause";
            Size = new Size(473, 461);
            ResumeLayout(false);
        }

        #endregion

        private Button btnContinue;
        private Button btnMenu;
        private Button btnSound;
        private Button btnMusic;
        private Button btnClose;
    }
}
