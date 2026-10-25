namespace Sec2_Group1_HappyJumpingFrog
{
    partial class FormSubmitScore
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
            label2 = new Label();
            textentername = new TextBox();
            btnsubmit = new Button();
            Score = new Label();
            label4 = new Label();
            btnclose = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(36, 69);
            label1.Name = "label1";
            label1.Size = new Size(73, 17);
            label1.TabIndex = 0;
            label1.Text = "Your Score";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(36, 140);
            label2.Name = "label2";
            label2.Size = new Size(74, 17);
            label2.TabIndex = 1;
            label2.Text = "Your name";
            // 
            // textentername
            // 
            textentername.BackColor = SystemColors.Window;
            textentername.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textentername.ForeColor = Color.Black;
            textentername.Location = new Point(36, 160);
            textentername.Multiline = true;
            textentername.Name = "textentername";
            textentername.PlaceholderText = "Enter your name";
            textentername.Size = new Size(347, 38);
            textentername.TabIndex = 2;
            // 
            // btnsubmit
            // 
            btnsubmit.BackColor = Color.DeepSkyBlue;
            btnsubmit.Cursor = Cursors.Hand;
            btnsubmit.FlatAppearance.BorderColor = Color.DeepSkyBlue;
            btnsubmit.FlatAppearance.BorderSize = 0;
            btnsubmit.FlatStyle = FlatStyle.System;
            btnsubmit.ForeColor = SystemColors.ControlLightLight;
            btnsubmit.Location = new Point(36, 213);
            btnsubmit.Name = "btnsubmit";
            btnsubmit.Size = new Size(84, 35);
            btnsubmit.TabIndex = 3;
            btnsubmit.Text = "Submit";
            btnsubmit.UseVisualStyleBackColor = false;
            btnsubmit.Click += btnsubmit_Click;
            // 
            // Score
            // 
            Score.AutoSize = true;
            Score.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Score.Location = new Point(35, 86);
            Score.Name = "Score";
            Score.Size = new Size(38, 45);
            Score.TabIndex = 4;
            Score.Text = "0";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(35, 19);
            label4.Name = "label4";
            label4.Size = new Size(268, 40);
            label4.TabIndex = 5;
            label4.Text = "Submit your score";
            // 
            // btnclose
            // 
            btnclose.BackColor = Color.Transparent;
            btnclose.Cursor = Cursors.Hand;
            btnclose.FlatAppearance.BorderSize = 0;
            btnclose.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnclose.FlatAppearance.MouseOverBackColor = Color.Gainsboro;
            btnclose.FlatStyle = FlatStyle.Flat;
            btnclose.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnclose.Location = new Point(428, 11);
            btnclose.Name = "btnclose";
            btnclose.Size = new Size(39, 41);
            btnclose.TabIndex = 6;
            btnclose.Text = "X";
            btnclose.UseVisualStyleBackColor = false;
            btnclose.Click += btnclose_Click;
            // 
            // FormSubmitScore
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(501, 291);
            Controls.Add(btnclose);
            Controls.Add(label4);
            Controls.Add(Score);
            Controls.Add(btnsubmit);
            Controls.Add(textentername);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormSubmitScore";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SubmitScore";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox textentername;
        private Button btnsubmit;
        private Label Score;
        private Label label4;
        private Button btnclose;
    }
}