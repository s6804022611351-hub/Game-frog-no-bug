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
            btnviewsco.Location = new Point(181, 183);
            btnviewsco.Margin = new Padding(3, 4, 3, 4);
            btnviewsco.Name = "btnviewsco";
            btnviewsco.Size = new Size(198, 48);
            btnviewsco.TabIndex = 0;
            btnviewsco.Text = "View leaderboard";
            btnviewsco.UseVisualStyleBackColor = true;
            btnviewsco.Click += btnviewsco_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(147, 29);
            label1.Name = "label1";
            label1.Size = new Size(306, 50);
            label1.TabIndex = 1;
            label1.Text = "Score submitted";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(170, 109);
            label2.Name = "label2";
            label2.Size = new Size(249, 23);
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
            btnclose.Location = new Point(504, 28);
            btnclose.Margin = new Padding(3, 4, 3, 4);
            btnclose.Name = "btnclose";
            btnclose.Size = new Size(45, 55);
            btnclose.TabIndex = 7;
            btnclose.Text = "X";
            btnclose.UseVisualStyleBackColor = false;
            btnclose.Click += btnclose_Click;
            // 
            // FormScoreSubmitted
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(573, 381);
            Controls.Add(btnclose);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnviewsco);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormScoreSubmitted";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormScoreSubmitted";
            Load += FormScoreSubmitted_Load;
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