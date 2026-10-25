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
            Score.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Score.ForeColor = SystemColors.ButtonHighlight;
            Score.Location = new Point(448, 200);
            Score.Name = "Score";
            Score.Size = new Size(28, 32);
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
            btnsub_score.Location = new Point(384, 450);
            btnsub_score.Name = "btnsub_score";
            btnsub_score.Size = new Size(164, 44);
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
            button1.Location = new Point(384, 392);
            button1.Name = "button1";
            button1.Size = new Size(164, 40);
            button1.TabIndex = 3;
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // FormGameOver
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Gameover;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(921, 528);
            Controls.Add(button1);
            Controls.Add(btnsub_score);
            Controls.Add(Score);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormGameOver";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormGameOver";
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