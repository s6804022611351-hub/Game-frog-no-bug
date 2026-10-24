using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HappyJumpingFrog
{
    public class PlayerFrog : GameObject
    {
        // 📍 ปรับความไว / ระยะกระโดดต่อก้าวตรงนี้ (ปรับตัวเลขเปลี่ยนความไวได้ทันที)
        // - อยากให้กระโดดเร็ว/ไกลขึ้น: ปรับเพิ่มเป็น 60, 65, หรือ 70
        // - อยากให้กระโดดช้า/ละเอียดขึ้น: ปรับลดเป็น 35, 40, หรือ 45
        public int HopSize { get; set; } = 50; 

        private readonly float _startX, _startY;

        public int Lives { get; private set; }
        public bool IsGameOver { get { return Lives <= 0; } }

        public PlayerFrog(Image sprite, float x, float y)
            : base(sprite, x, y, 50, 50)
        {
            _startX = x;
            _startY = y;
            Lives = 5;
        }

        public void Hop(Keys key, int areaWidth, int areaHeight)
        {
            // รองรับทั้งปุ่มลูกศร (Up, Down, Left, Right) และปุ่ม WASD
            if (key == Keys.Up || key == Keys.W) Y -= HopSize;
            if (key == Keys.Down || key == Keys.S) Y += HopSize;
            if (key == Keys.Left || key == Keys.A) X -= HopSize;
            if (key == Keys.Right || key == Keys.D) X += HopSize;

            // ป้องกันไม่ให้กบออกนอกขอบพื้นที่เล่นเกม
            if (X < 0) X = 0;
            if (Y < 0) Y = 0;
            if (X > areaWidth - Width) X = areaWidth - Width;
            if (Y > areaHeight - Height) Y = areaHeight - Height;
        }

        public void Die()
        {
            Lives--;
            X = _startX;     // ส่งกบกลับจุดเริ่มต้น
            Y = _startY;
        }
    }
}