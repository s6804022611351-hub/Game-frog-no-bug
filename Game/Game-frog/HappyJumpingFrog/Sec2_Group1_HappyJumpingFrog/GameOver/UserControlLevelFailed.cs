using Sec2_Group1_HappyJumpingFrog.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sec2_Group1_HappyJumpingFrog
{
    public partial class UserControlLevelFailed : UserControl
    {
        private int _score;

        // แก้ตรงนี้ให้รองรับการส่ง _score เข้ามาครับ
        public UserControlLevelFailed(int score)
        {
            InitializeComponent();
            _score = score;

            // ถ้ามี Label แสดงคะแนน สามารถเอามาใส่ตรงนี้ได้ เช่น:
            // lblScore.Text = score.ToString();
        }

        // สร้าง Event สำหรับส่งสัญญาณกลับไปที่ Form หลัก
        public event EventHandler TryAgainClicked;
        public event EventHandler EndGameClicked;

        public UserControlLevelFailed()
        {
            InitializeComponent();
        }

        // เมธอดสำหรับรับคะแนนมาแสดง (ถ้ามี Label แสดงคะแนน)
        public void SetScore(int score)
        {
            _score = score;
            // lblScore.Text = score.ToString(); // เปิดบรรทัดนี้ถ้ามี Label แสดงคะแนน
        }

        // เมื่อกดปุ่ม Try Again (เล่นใหม่)
        private void btntryagin_Click(object sender, EventArgs e)
        {
            this.Visible = false; // ซ่อนหน้า Level Failed ลงไป
            TryAgainClicked?.Invoke(this, EventArgs.Empty); // ส่งสัญญาณไปที่ Form หลักให้รีเซ็ตเกม
        }

        // เมื่อกดปุ่ม Submit Score / ออกจากเกม
        private void btnend_Click(object sender, EventArgs e)
        {
            this.Visible = false; // ซ่อนหน้า Level Failed ลงไป
            EndGameClicked?.Invoke(this, EventArgs.Empty); // ส่งสัญญาณไปที่ Form หลัก
        }
    }
}
