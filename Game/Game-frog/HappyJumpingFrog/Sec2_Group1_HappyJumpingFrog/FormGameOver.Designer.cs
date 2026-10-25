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
            label1 = new Label();
            Score = new Label();
            btnsub_score = new Button();
            button1 = new Button();
            label2 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(358, 185);
            label1.Name = "label1";
            label1.Size = new Size(109, 25);
            label1.TabIndex = 0;
            label1.Text = "Your Score";
            // 
            // Score
            // 
            Score.AutoSize = true;
            Score.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Score.Location = new Point(396, 220);
            Score.Name = "Score";
            Score.Size = new Size(28, 32);
            Score.TabIndex = 1;
            Score.Text = "0";
            // 
            // btnsub_score
            // 
            btnsub_score.Location = new Point(328, 351);
            btnsub_score.Name = "btnsub_score";
            btnsub_score.Size = new Size(167, 69);
            btnsub_score.TabIndex = 2;
            btnsub_score.Text = "Submit Score";
            btnsub_score.UseVisualStyleBackColor = true;
            btnsub_score.Click += btnsub_score_Click;
            // 
            // button1
            // 
            button1.Location = new Point(328, 268);
            button1.Name = "button1";
            button1.Size = new Size(167, 65);
            button1.TabIndex = 3;
            button1.Text = "Try Again";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 72F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(95, 31);
            label2.Name = "label2";
            label2.Size = new Size(610, 128);
            label2.TabIndex = 4;
            label2.Text = "GAME OVER";
            // 
            // FormGameOver
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(button1);
            Controls.Add(btnsub_score);
            Controls.Add(Score);
            Controls.Add(label1);
            Name = "FormGameOver";
            Text = "FormGameOver";
            Load += FormGameOver_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label Score;
        private Button btnsub_score;
        private Button button1;
        private Label label2;
    }
}