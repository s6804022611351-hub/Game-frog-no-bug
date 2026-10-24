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
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(370, 193);
            label1.Name = "label1";
            label1.Size = new Size(63, 15);
            label1.TabIndex = 0;
            label1.Text = "Your Score";
            // 
            // Score
            // 
            Score.AutoSize = true;
            Score.Location = new Point(393, 222);
            Score.Name = "Score";
            Score.Size = new Size(13, 15);
            Score.TabIndex = 1;
            Score.Text = "0";
            // 
            // btnsub_score
            // 
            btnsub_score.Location = new Point(257, 348);
            btnsub_score.Name = "btnsub_score";
            btnsub_score.Size = new Size(311, 59);
            btnsub_score.TabIndex = 2;
            btnsub_score.Text = "Submit Score";
            btnsub_score.UseVisualStyleBackColor = true;
            btnsub_score.Click += btnsub_score_Click;
            // 
            // button1
            // 
            button1.Location = new Point(257, 268);
            button1.Name = "button1";
            button1.Size = new Size(301, 52);
            button1.TabIndex = 3;
            button1.Text = "Try Again";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // FormGameOver
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
    }
}