using HappyJumpingFrog;
using System.Drawing;

namespace Sec2_Group1_HappyJumpingFrog.Class
{
    internal class TargetLotus : GameObject
    {
        public bool IsCollected { get; set; } = false; // สถานะว่าถูกเก็บไปแล้วหรือยัง

        // แปลงค่า width และ height เป็น (int) ก่อนส่งให้ GameObject
        public TargetLotus(Image sprite, float x, float y, float width, float height)
            : base(sprite, x, y, (int)width, (int)height)
        {
        }

        public override void Draw(Graphics g)
        {
            // วาดเฉพาะดอกบัวที่ยังไม่ถูกเก็บเท่านั้น
            if (!IsCollected)
            {
                base.Draw(g);
            }
        }
    }
}