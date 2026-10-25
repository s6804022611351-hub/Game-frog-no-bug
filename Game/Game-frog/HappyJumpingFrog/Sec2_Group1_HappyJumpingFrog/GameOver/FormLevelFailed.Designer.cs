namespace Sec2_Group1_HappyJumpingFrog.GameOver
{
    partial class FormLevelFailed
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
            btntryagin = new Button();
            btnend = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(95, 58);
            label1.Name = "label1";
            label1.Size = new Size(193, 45);
            label1.TabIndex = 0;
            label1.Text = "Level Failed";
            // 
            // btntryagin
            // 
            btntryagin.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btntryagin.Location = new Point(125, 147);
            btntryagin.Name = "btntryagin";
            btntryagin.Size = new Size(136, 42);
            btntryagin.TabIndex = 1;
            btntryagin.Text = "Try Again";
            btntryagin.UseVisualStyleBackColor = true;
            btntryagin.Click += btntryagin_Click;
            // 
            // btnend
            // 
            btnend.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnend.Location = new Point(125, 216);
            btnend.Name = "btnend";
            btnend.Size = new Size(136, 39);
            btnend.TabIndex = 2;
            btnend.Text = "End Game";
            btnend.UseVisualStyleBackColor = true;
            btnend.Click += btnend_Click;
            // 
            // LevelFailed
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(394, 356);
            Controls.Add(btnend);
            Controls.Add(btntryagin);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "LevelFailed";
            Text = "LevelFailed";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btntryagin;
        private Button btnend;
    }
}