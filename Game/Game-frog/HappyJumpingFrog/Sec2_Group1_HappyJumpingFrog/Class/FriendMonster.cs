using HappyJumpingFrog;
using System;
using System.Drawing;

namespace Sec2_Group1_HappyJumpingFrog.Class
{
    
    public class FriendMonster : GameObject
    {
        public float SwimSpeed { get; set; }
        private int _areaWidth;

       
        public FriendMonster(Image sprite, float x, float y, float speed, int areaWidth)
            : base(sprite, x, y, 100, 40) // 100, 40 คือความกว้างและสูงของมอนสเตอร์

        {
            SwimSpeed = speed;
            _areaWidth = areaWidth;
        }

        // 3. ฟังก์ชันการขยับของมอนสเตอร์แต่ละตัว
        public override void Update()
        {
            X += SwimSpeed;

            // วาร์ปกลับมาเริ่มต้นใหม่เมื่อหลุดขอบจอ
            if (SwimSpeed > 0 && X > _areaWidth) X = -Width;
            if (SwimSpeed < 0 && X < -Width) X = _areaWidth;
        }
    }
}