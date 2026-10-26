using System;
using System.Collections.Generic;
using System.Drawing;

namespace Sec2_Group1_HappyJumpingFrog.Class
{
    public class River
    {
        public List<FriendMonster> Friends { get; private set; }

        public River(int areaWidth, int fishCountPerRow = 3, int level = 1)
        {
            Friends = new List<FriendMonster>();
            Random rnd = new Random();

            int laneCount = (level == 3) ? 4 : 3;

            // 📍 ปรับพิกัด Y ใหม่ให้ตรงกับก้าวโดด 4 ก้าวในแม่น้ำด่าน 3:
            // เลน 3 (ล่างสุด): 317f -> ตรงตำแหน่งกบในรูปพอดีเป๊ะ! (ข้ามถนนมาเจอปลาทันที)
            // เลน 2: 255f
            // เลน 1: 193f
            // เลน 0 (บนสุด): 131f
            float[] laneY = (level == 3)
                ? new float[] { 131f, 193f, 255f, 317f }
                : new float[] { 120f, 190f, 260f };

            for (int lane = 0; lane < laneCount; lane++)
            {
                float y = laneY[lane];
                float direction = (lane % 2 == 0) ? 1 : -1;
                Image sprite;

                if (level == 3)
                {
                    sprite = (lane % 2 == 0) ? Properties.Resources.Fish1 : Properties.Resources.Fish2;
                }
                else
                {
                    sprite = (lane == 1) ? Properties.Resources.Fish2 : Properties.Resources.Fish1;
                }

                float speed = (2.2f + lane * 0.3f + (level - 1) * 0.5f) * direction;
                float spacing = areaWidth / (float)fishCountPerRow;

                for (int i = 0; i < fishCountPerRow; i++)
                {
                    float x = i * spacing + rnd.Next(0, 30);
                    Friends.Add(new FriendMonster(sprite, x, y, speed, areaWidth));
                }
            }
        }

        public void UpdateFriends()
        {
            foreach (var fish in Friends)
                fish.Update();
        }

        public void Draw(Graphics g)
        {
            foreach (var fish in Friends)
                fish.Draw(g);
        }
    }
}