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
            btnContinue = new Button();
            btnMenu = new Button();
            btnSound = new Button();
            btnMusic = new Button();
            SuspendLayout();
            // 
            // btnContinue
            // 
            btnContinue.Location = new Point(105, 79);
            btnContinue.Name = "btnContinue";
            btnContinue.Size = new Size(119, 42);
            btnContinue.TabIndex = 0;
            btnContinue.Text = "Continue";
            btnContinue.UseVisualStyleBackColor = true;
            btnContinue.Click += btnContinue_Click;
            // 
            // btnMenu
            // 
            btnMenu.Location = new Point(105, 140);
            btnMenu.Name = "btnMenu";
            btnMenu.Size = new Size(119, 42);
            btnMenu.TabIndex = 1;
            btnMenu.Text = "Menu";
            btnMenu.UseVisualStyleBackColor = true;
            btnMenu.Click += btnMenu_Click;
            // 
            // btnSound
            // 
            btnSound.Location = new Point(105, 199);
            btnSound.Name = "btnSound";
            btnSound.Size = new Size(119, 42);
            btnSound.TabIndex = 2;
            btnSound.Text = "Sound On";
            btnSound.UseVisualStyleBackColor = true;
            btnSound.Click += btnSound_Click;
            // 
            // btnMusic
            // 
            btnMusic.Location = new Point(105, 263);
            btnMusic.Name = "btnMusic";
            btnMusic.Size = new Size(119, 42);
            btnMusic.TabIndex = 3;
            btnMusic.Text = "button4";
            btnMusic.UseVisualStyleBackColor = true;
            btnMusic.Click += btnMusic_Click;
            // 
            // UserControlPause
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnMusic);
            Controls.Add(btnSound);
            Controls.Add(btnMenu);
            Controls.Add(btnContinue);
            Name = "UserControlPause";
            Size = new Size(337, 379);
            ResumeLayout(false);
        }

        #endregion

        private Button btnContinue;
        private Button btnMenu;
        private Button btnSound;
        private Button btnMusic;
    }
}
