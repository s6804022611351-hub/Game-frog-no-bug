using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HappyJumpingFrog
{
    public class PlayerFrog : GameObject
    {

        private static readonly float[] RowCenters = { 590, 513, 451, 389, 334, 270, 200, 130, 50 };

        // แถวที่กบอยู่ตอนนี้ (หาแถวที่ใกล้ที่สุด)
        public int CurrentRow
        {
            get
            {
                float cy = Y + Height / 2f;
                int best = 0;
                for (int i = 1; i < RowCenters.Length; i++)
                    if (Math.Abs(RowCenters[i] - cy) < Math.Abs(RowCenters[best] - cy))
                        best = i;
                return best;
            }
        }

        public void Hop(Keys key, int areaWidth, int areaHeight)
        {
            int row = CurrentRow;

            if (key == Keys.Up && row < RowCenters.Length - 1) row++;
            if (key == Keys.Down && row > 0) row--;

            if (key == Keys.Up || key == Keys.Down)
                Y = RowCenters[row] - Height / 2f;       // กระโดดทีละแถว ตรงกลางเลน

            if (key == Keys.Left) X -= HopSize;
            if (key == Keys.Right) X += HopSize;

            // ไม่ให้กบออกนอกสนาม
            if (X < 0) X = 0;
            if (X > areaWidth - Width) X = areaWidth - Width;
        }




        // 📍 ปรับความไว / ระยะกระโดดต่อก้าวตรงนี้ (ปรับตัวเลขเปลี่ยนความไวได้ทันที)
        // - อยากให้กระโดดเร็ว/ไกลขึ้น: ปรับเพิ่มเป็น 60, 65, หรือ 70
        // - อยากให้กระโดดช้า/ละเอียดขึ้น: ปรับลดเป็น 35, 40, หรือ 45
        public int HopSize { get; set; } = 50; 

        private readonly float _startX, _startY;

        public int Lives { get; set; } = 3;
        public bool IsGameOver { get { return Lives <= 0; } }

        public PlayerFrog(Image sprite, float x, float y)
            : base(sprite, x, y, 50, 50)
        {
            _startX = x;
            _startY = y;
            Lives = 5;
        }

        
        public void Die()
        {
            Lives--;
            X = _startX;     // ส่งกบกลับจุดเริ่มต้น
            Y = _startY;
        }
    }
}