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
            Score = new Label();
            btnsub_score = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // Score
            // 
            Score.AutoSize = true;
            Score.BackColor = Color.Transparent;
            Score.FlatStyle = FlatStyle.Flat;
            Score.Font = new Font("Segoe UI", 30F, FontStyle.Bold);
            Score.ForeColor = SystemColors.ButtonHighlight;
            Score.Location = new Point(939, 419);
            Score.Name = "Score";
            Score.Size = new Size(58, 67);
            Score.TabIndex = 1;
            Score.Text = "0";
            // 
            // btnsub_score
            // 
            btnsub_score.BackColor = Color.Transparent;
            btnsub_score.FlatAppearance.BorderSize = 0;
            btnsub_score.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnsub_score.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnsub_score.FlatStyle = FlatStyle.Flat;
            btnsub_score.Location = new Point(849, 946);
            btnsub_score.Margin = new Padding(3, 4, 3, 4);
            btnsub_score.Name = "btnsub_score";
            btnsub_score.Size = new Size(264, 62);
            btnsub_score.TabIndex = 2;
            btnsub_score.UseVisualStyleBackColor = false;
            btnsub_score.Click += btnsub_score_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.Transparent;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 18F);
            button1.Location = new Point(849, 802);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(264, 70);
            button1.TabIndex = 3;
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // FormGameOver
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Gameover;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1193, 1021);
            Controls.Add(button1);
            Controls.Add(btnsub_score);
            Controls.Add(Score);
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
        private Label Score;
        private Button btnsub_score;
        private Button button1;
    }
}