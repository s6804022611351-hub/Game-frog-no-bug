using HappyJumpingFrog;
using System;
using System.Drawing;

namespace Sec2_Group1_HappyJumpingFrog.Class
{
    public class FriendMonster : GameObject
    {
        public float SwimSpeed { get; set; }
        public int AreaWidth { get; set; }

        // 📍 เปลี่ยน 100f, 45f เป็น 100, 45 (ใช้ int)
        public FriendMonster(Image sprite, float x, float y, float speed, int areaWidth)
            : base(sprite, x, y, 100, 45)
        {
            SwimSpeed = speed;
            AreaWidth = areaWidth;
        }

        // การขยับของเพื่อนแต่ละตัว
        public override void Update()
        {
            X += SwimSpeed;

            // วาร์ปกลับมาอีกฝั่งเมื่อหลุดขอบจอ
            if (SwimSpeed > 0 && X > AreaWidth) X = -Width;
            if (SwimSpeed < 0 && X < -Width) X = AreaWidth;
        }
    }
}