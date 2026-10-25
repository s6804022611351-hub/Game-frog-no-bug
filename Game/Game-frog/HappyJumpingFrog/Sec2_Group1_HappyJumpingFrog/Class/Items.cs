using HappyJumpingFrog;
using System;
using System.Drawing;

namespace Sec2_Group1_HappyJumpingFrog.Class
{
    internal class Items : GameObject
    {
        public bool IsActive { get; set; } = false; // สถานะว่าไอเท็มยังแสดงอยู่บนแมพหรือไม่

        public Items(Image sprite, float x, float y, float width, float height)
            : base(sprite, x, y, (int)width, (int)height)
        {
        }

        // ฟังก์ชันสุ่มพิกัดของไอเท็มบนแผนที่
        public void SpawnRandom(int mapWidth, int mapHeight)
        {
            Random rand = new Random();
            X = rand.Next(40, mapWidth - (int)Width - 40);
            Y = rand.Next(120, mapHeight - (int)Height - 120);
            IsActive = true;
        }

        public override void Draw(Graphics g)
        {
            // วาดรูปไอเท็มเฉพาะตอนที่ IsActive เป็น true เท่านั้น
            if (IsActive && g != null)
            {
                base.Draw(g);
            }
        }
    }
}