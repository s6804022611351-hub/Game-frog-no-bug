using HappyJumpingFrog;
using System;
using System.Drawing;

namespace Sec2_Group1_HappyJumpingFrog.Class
{
    public class FriendMonster : GameObject
    {
        // ขนาดปลา (ปรับตรงนี้ถ้าอยากให้ใหญ่/เล็กกว่านี้ 
        public const int FishWidth = 170;
        public const int FishHeight = 68;

        public float SwimSpeed { get; set; }
        public int AreaWidth { get; set; }     // ความกว้างสนาม (เปลี่ยนได้ตอนย่อ/ขยายหน้าต่าง)

        public FriendMonster(Image sprite, float x, float y, float speed, int areaWidth)
            : base(sprite, x, y, FishWidth, FishHeight)
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