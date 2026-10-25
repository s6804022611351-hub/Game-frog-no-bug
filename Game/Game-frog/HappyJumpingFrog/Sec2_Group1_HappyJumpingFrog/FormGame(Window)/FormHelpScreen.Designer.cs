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
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureHelp).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureHelp
            // 
            pictureHelp.BackColor = Color.Transparent;
            pictureHelp.BackgroundImage = (Image)resources.GetObject("pictureHelp.BackgroundImage");
            pictureHelp.BackgroundImageLayout = ImageLayout.Zoom;
            pictureHelp.Dock = DockStyle.Fill;
            pictureHelp.Location = new Point(3, 223);
            pictureHelp.Name = "pictureHelp";
            pictureHelp.Size = new Size(1394, 403);
            pictureHelp.SizeMode = PictureBoxSizeMode.Zoom;
            pictureHelp.TabIndex = 0;
            pictureHelp.TabStop = false;
            pictureHelp.Click += pictureHelp_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.FlatStyle = FlatStyle.Flat;
            label1.Font = new Font("Javanese Text", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Aquamarine;
            label1.ImageAlign = ContentAlignment.TopCenter;
            label1.Location = new Point(580, 0);
            label1.Name = "label1";
            label1.Size = new Size(240, 84);
            label1.TabIndex = 1;
            label1.Text = "How to play";
            // 
            // btnBackformHelp
            // 
            btnBackformHelp.Anchor = AnchorStyles.Bottom;
            btnBackformHelp.BackColor = Color.Transparent;
            btnBackformHelp.BackgroundImage = (Image)resources.GetObject("btnBackformHelp.BackgroundImage");
            btnBackformHelp.BackgroundImageLayout = ImageLayout.Zoom;
            btnBackformHelp.FlatAppearance.BorderSize = 0;
            btnBackformHelp.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnBackformHelp.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnBackformHelp.FlatStyle = FlatStyle.Flat;
            btnBackformHelp.Font = new Font("Javanese Text", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBackformHelp.ForeColor = Color.FromArgb(0, 64, 64);
            btnBackformHelp.Location = new Point(379, 632);
            btnBackformHelp.Name = "btnBackformHelp";
            btnBackformHelp.Size = new Size(642, 73);
            btnBackformHelp.TabIndex = 2;
            btnBackformHelp.Text = "Back";
            btnBackformHelp.UseVisualStyleBackColor = false;
            btnBackformHelp.Click += btnBackformHelp_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.Transparent;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(pictureHelp, 0, 2);
            tableLayoutPanel1.Controls.Add(panel1, 0, 1);
            tableLayoutPanel1.Controls.Add(btnBackformHelp, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 86F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 21.5434074F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 65.75563F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.6453485F));
            tableLayoutPanel1.Size = new Size(1400, 708);
            tableLayoutPanel1.TabIndex = 3;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.None;
            panel1.BackColor = Color.FromArgb(0, 64, 64);
            panel1.BackgroundImageLayout = ImageLayout.Zoom;
            panel1.Controls.Add(label2);
            panel1.Location = new Point(13, 90);
            panel1.Name = "panel1";
            panel1.Size = new Size(1374, 125);
            panel1.TabIndex = 3;
            panel1.TabStop = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Dock = DockStyle.Top;
            label2.Font = new Font("Javanese Text", 18F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Aquamarine;
            label2.Location = new Point(0, 0);
            label2.Name = "label2";
            label2.Size = new Size(1373, 138);
            label2.TabIndex = 4;
            label2.Text = "Get your frog safly across the road by don't let's it hits anybody on the road and your frog must stays on \r\nlotus leaf to cross the river to the other side.";
            label2.Click += label2_Click;
            // 
            // FormHelpScreen
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(192, 255, 255);
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1400, 708);
            Controls.Add(tableLayoutPanel1);
            ForeColor = Color.FromArgb(0, 64, 64);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "FormHelpScreen";
            Text = "FormHelpScreen";
            Load += FormHelpScreen_Load;
            ((System.ComponentModel.ISupportInitialize)pictureHelp).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureHelp;
        private Label label1;
        private Button btnBackformHelp;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private Label label2;
    }
}