namespace Sec2_Group1_HappyJumpingFrog
{
    partial class UserControlLevelFailed
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlLevelFailed));
            btnend = new Button();
            btntryagin = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnend
            // 
            btnend.FlatAppearance.BorderSize = 0;
            btnend.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnend.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnend.FlatStyle = FlatStyle.Flat;
            btnend.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnend.Location = new Point(85, 266);
            btnend.Name = "btnend";
            btnend.Size = new Size(321, 74);
            btnend.TabIndex = 5;
            btnend.UseVisualStyleBackColor = true;
            btnend.Click += btnend_Click;
            // 
            // btntryagin
            // 
            btntryagin.FlatAppearance.BorderSize = 0;
            btntryagin.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btntryagin.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btntryagin.FlatStyle = FlatStyle.Flat;
            btntryagin.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btntryagin.Location = new Point(94, 144);
            btntryagin.Name = "btntryagin";
            btntryagin.Size = new Size(312, 78);
            btntryagin.TabIndex = 4;
            btntryagin.UseVisualStyleBackColor = true;
            btntryagin.Click += btntryagin_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(-31, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(556, 135);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // UserControlLevelFailed
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            Controls.Add(pictureBox1);
            Controls.Add(btnend);
            Controls.Add(btntryagin);
            DoubleBuffered = true;
            Name = "UserControlLevelFailed";
            Size = new Size(508, 462);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnend;
        private Button btntryagin;
        private PictureBox pictureBox1;
    }
}
