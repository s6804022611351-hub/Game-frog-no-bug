namespace Sec2_Group1_HappyJumpingFrog.Congratulations
{
    partial class FormCongrat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCongrat));
            btnsubmit = new Button();
            btnend = new Button();
            SuspendLayout();
            // 
            // btnsubmit
            // 
            btnsubmit.BackColor = Color.Transparent;
            btnsubmit.BackgroundImage = (Image)resources.GetObject("btnsubmit.BackgroundImage");
            btnsubmit.BackgroundImageLayout = ImageLayout.Zoom;
            btnsubmit.FlatAppearance.BorderSize = 0;
            btnsubmit.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnsubmit.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnsubmit.FlatStyle = FlatStyle.Flat;
            btnsubmit.ForeColor = Color.Transparent;
            btnsubmit.Location = new Point(661, 800);
            btnsubmit.Name = "btnsubmit";
            btnsubmit.Size = new Size(305, 145);
            btnsubmit.TabIndex = 0;
            btnsubmit.UseVisualStyleBackColor = false;
            btnsubmit.Click += btnsubmit_Click;
            // 
            // btnend
            // 
            btnend.BackColor = Color.Transparent;
            btnend.BackgroundImage = (Image)resources.GetObject("btnend.BackgroundImage");
            btnend.BackgroundImageLayout = ImageLayout.Zoom;
            btnend.FlatAppearance.BorderSize = 0;
            btnend.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnend.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnend.FlatStyle = FlatStyle.Flat;
            btnend.ForeColor = Color.Transparent;
            btnend.Location = new Point(943, 825);
            btnend.Name = "btnend";
            btnend.Size = new Size(232, 94);
            btnend.TabIndex = 1;
            btnend.UseVisualStyleBackColor = false;
            btnend.Click += button1_Click_2;
            // 
            // FormCongrat
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1783, 920);
            Controls.Add(btnend);
            Controls.Add(btnsubmit);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "FormCongrat";
            Text = "Classic Frog";
            WindowState = FormWindowState.Maximized;
            ResumeLayout(false);
        }

        #endregion

        private Button btnsubmit;
        private Button btnend;
    }
}