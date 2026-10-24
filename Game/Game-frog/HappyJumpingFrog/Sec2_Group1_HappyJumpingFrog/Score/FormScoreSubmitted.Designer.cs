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
            SuspendLayout();
            // 
            // btnviewsco
            // 
            btnviewsco.Location = new Point(91, 117);
            btnviewsco.Name = "btnviewsco";
            btnviewsco.Size = new Size(112, 42);
            btnviewsco.TabIndex = 0;
            btnviewsco.Text = "View leaderboard";
            btnviewsco.UseVisualStyleBackColor = true;
            btnviewsco.Click += btnviewsco_Click;
            // 
            // FormScoreSubmitted
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(598, 316);
            Controls.Add(btnviewsco);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormScoreSubmitted";
            Text = "FormScoreSubmitted";
            ResumeLayout(false);
        }

        #endregion

        private Button btnviewsco;
    }
}