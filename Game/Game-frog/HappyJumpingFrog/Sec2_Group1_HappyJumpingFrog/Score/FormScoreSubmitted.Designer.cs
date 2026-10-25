namespace Sec2_Group1_HappyJumpingFrog
{
    partial class FormScoreSubmitted
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
            btnviewsco = new Button();
            label1 = new Label();
            label2 = new Label();
            btnclose = new Button();
            SuspendLayout();
            // 
            // btnviewsco
            // 
            btnviewsco.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnviewsco.Location = new Point(158, 137);
            btnviewsco.Name = "btnviewsco";
            btnviewsco.Size = new Size(173, 36);
            btnviewsco.TabIndex = 0;
            btnviewsco.Text = "View leaderboard";
            btnviewsco.UseVisualStyleBackColor = true;
            btnviewsco.Click += btnviewsco_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(129, 22);
            label1.Name = "label1";
            label1.Size = new Size(241, 40);
            label1.TabIndex = 1;
            label1.Text = "Score submitted";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(149, 82);
            label2.Name = "label2";
            label2.Size = new Size(192, 17);
            label2.TabIndex = 2;
            label2.Text = "Your score has been submitted.";
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
            btnclose.Location = new Point(441, 21);
            btnclose.Name = "btnclose";
            btnclose.Size = new Size(39, 41);
            btnclose.TabIndex = 7;
            btnclose.Text = "X";
            btnclose.UseVisualStyleBackColor = false;
            btnclose.Click += btnclose_Click;
            // 
            // FormScoreSubmitted
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(501, 286);
            Controls.Add(btnclose);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnviewsco);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormScoreSubmitted";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormScoreSubmitted";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnviewsco;
        private Label label1;
        private Label label2;
        private Button btnclose;
    }
}