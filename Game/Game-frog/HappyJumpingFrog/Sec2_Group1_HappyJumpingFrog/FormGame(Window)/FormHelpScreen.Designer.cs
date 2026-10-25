namespace Sec2_Group1_HappyJumpingFrog.FormGame_Window_
{
    partial class FormHelpScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHelpScreen));
            pictureHelp = new PictureBox();
            label1 = new Label();
            btnBackformHelp = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureHelp).BeginInit();
            SuspendLayout();
            // 
            // pictureHelp
            // 
            pictureHelp.Anchor = AnchorStyles.None;
            pictureHelp.BackColor = Color.Transparent;
            pictureHelp.BackgroundImage = (Image)resources.GetObject("pictureHelp.BackgroundImage");
            pictureHelp.BackgroundImageLayout = ImageLayout.Zoom;
            pictureHelp.Location = new Point(-2, 126);
            pictureHelp.Name = "pictureHelp";
            pictureHelp.Size = new Size(1140, 490);
            pictureHelp.SizeMode = PictureBoxSizeMode.Zoom;
            pictureHelp.TabIndex = 0;
            pictureHelp.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.FlatStyle = FlatStyle.Flat;
            label1.Font = new Font("Javanese Text", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Teal;
            label1.ImageAlign = ContentAlignment.TopCenter;
            label1.Location = new Point(589, 4);
            label1.Name = "label1";
            label1.Size = new Size(199, 69);
            label1.TabIndex = 1;
            label1.Text = "How to play";
            // 
            // btnBackformHelp
            // 
            btnBackformHelp.BackColor = Color.Turquoise;
            btnBackformHelp.FlatAppearance.BorderSize = 0;
            btnBackformHelp.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnBackformHelp.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnBackformHelp.Font = new Font("Javanese Text", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBackformHelp.ForeColor = Color.Teal;
            btnBackformHelp.Location = new Point(589, 622);
            btnBackformHelp.Name = "btnBackformHelp";
            btnBackformHelp.Size = new Size(120, 66);
            btnBackformHelp.TabIndex = 2;
            btnBackformHelp.Text = "Back";
            btnBackformHelp.UseVisualStyleBackColor = false;
            btnBackformHelp.Click += btnBackformHelp_Click;
            // 
            // FormHelpScreen
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(192, 255, 255);
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(1400, 708);
            Controls.Add(btnBackformHelp);
            Controls.Add(label1);
            Controls.Add(pictureHelp);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "FormHelpScreen";
            Text = "FormHelpScreen";
            Load += FormHelpScreen_Load;
            ((System.ComponentModel.ISupportInitialize)pictureHelp).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureHelp;
        private Label label1;
        private Button btnBackformHelp;
    }
}