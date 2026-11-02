namespace Sec2_Group1_HappyJumpingFrog
{
    partial class FormGameOver
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
            btnsub_score = new Button();
            button1 = new Button();
            Score = new Label();
            SuspendLayout();
            // 
            // btnsub_score
            // 
            btnsub_score.Anchor = AnchorStyles.None;
            btnsub_score.BackColor = Color.Transparent;
            btnsub_score.FlatAppearance.BorderSize = 0;
            btnsub_score.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnsub_score.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnsub_score.FlatStyle = FlatStyle.Flat;
            btnsub_score.Location = new Point(783, 918);
            btnsub_score.Margin = new Padding(3, 4, 3, 4);
            btnsub_score.Name = "btnsub_score";
            btnsub_score.Size = new Size(376, 92);
            btnsub_score.TabIndex = 2;
            btnsub_score.UseVisualStyleBackColor = false;
            btnsub_score.Click += btnsub_score_Click;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.None;
            button1.BackColor = Color.Transparent;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 18F);
            button1.Location = new Point(783, 807);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(376, 83);
            button1.TabIndex = 3;
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // Score
            // 
            Score.Anchor = AnchorStyles.None;
            Score.AutoSize = true;
            Score.BackColor = Color.Transparent;
            Score.FlatStyle = FlatStyle.Flat;
            Score.Font = new Font("Segoe UI", 40F, FontStyle.Bold);
            Score.ForeColor = SystemColors.ButtonHighlight;
            Score.Location = new Point(929, 399);
            Score.Name = "Score";
            Score.Size = new Size(77, 89);
            Score.TabIndex = 4;
            Score.Text = "0";
            // 
            // FormGameOver
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Gameover;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1920, 1080);
            Controls.Add(Score);
            Controls.Add(button1);
            Controls.Add(btnsub_score);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormGameOver";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormGameOver";
            WindowState = FormWindowState.Maximized;
            Load += FormGameOver_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnsub_score;
        private Button button1;
        private Label Score;
    }
}