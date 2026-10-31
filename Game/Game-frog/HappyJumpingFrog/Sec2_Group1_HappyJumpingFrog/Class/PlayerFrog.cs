using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HappyJumpingFrog
{
    public class PlayerFrog : GameObject
    {
        // แถวเริ่มต้น (ด่าน 1-2)
        public static readonly float[] DefaultRows = { 590, 513, 451, 389, 334, 270, 200, 130, 50 };

        // แถวที่กบกระโดดไปได้ เปลี่ยนได้ตามด่าน
        public float[] RowCenters { get; set; } = DefaultRows;

        private DateTime _invulnerableUntil = DateTime.MinValue; //อมตะ
        public bool IsInvulnerable => DateTime.Now < _invulnerableUntil;

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

        // 📍 ปรับความไว / ระยะกระโดดต่อก้าวตรงนี้
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
            // ถ้ากบยังอยู่ในสถานะอมตะ จะไม่โดนคำสั่ง Die ซ้ำ
            if (IsInvulnerable) return;

            Lives--;

            // วาร์ปกบกลับจุดเกิดทันที
            this.X = 350f;
            this.Y = 565f;

            // ตั้งเวลาให้อมตะเป็นเวลา 1 วินาที
            _invulnerableUntil = DateTime.Now.AddSeconds(1);
        }

        public void Draw(Graphics g)
        {
            // ถ้ากำลังอมตะ ให้กระพริบ
            if (IsInvulnerable && (DateTime.Now.Millisecond / 150) % 2 == 0)
            {
                return;
            }

            g.DrawImage(Sprite, X, Y, Width, Height);
        }
    }
}