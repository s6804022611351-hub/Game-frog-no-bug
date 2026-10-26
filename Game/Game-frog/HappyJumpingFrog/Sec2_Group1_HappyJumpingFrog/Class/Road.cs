using System;
using System.Collections.Generic;
using System.Drawing;

namespace Sec2_Group1_HappyJumpingFrog.Class
{
    public class Road
    {
        private const int EnemySize = EnemyMonster.EnemyHeight;

        public List<EnemyMonster> Enemies { get; private set; }

        // 📍 เพิ่มพารามิเตอร์ gradualBottomLane (กำหนดค่าเริ่มต้นเป็น false)
        public Road(float topY, float laneHeight, int laneCount,
                    Image[] sprites, float baseSpeed, int areaWidth,
                    bool gradualBottomLane = false)
        {
            Enemies = new List<EnemyMonster>();
            Random rnd = new Random();

            for (int lane = 0; lane < laneCount; lane++)
            {
                float y = topY + lane * laneHeight + (laneHeight - EnemySize) / 2f;
                float direction = (lane % 2 == 0) ? 1 : -1;
                float speed = (baseSpeed + lane * 0.4f) * direction;

                Image sprite = (lane < sprites.Length) ? sprites[lane] : sprites[sprites.Length - 1];

                // 📍 เช็กว่าเลนนี้คือเลนล่างสุด (lane == laneCount - 1) และถูกเปิดใช้งาน gradualBottomLane หรือไม่
                bool isBottomLaneGradual = gradualBottomLane && (lane == laneCount - 1);

                for (int i = 0; i < 3; i++)
                {
                    float x;

                    if (isBottomLaneGradual)
                    {
                        // 📍 เฉพาะเลนล่างสุดติดกบ: ให้วางนอกจอ ค่อยๆ ทยอยวิ่งเข้าทีละตัว
                        if (direction > 0)
                            x = -200f - (i * 280f) - rnd.Next(0, 50);
                        else
                            x = areaWidth + 200f + (i * 280f) + rnd.Next(0, 50);
                    }
                    else
                    {
                        // 📍 เลนอื่นๆ ทั้งหมด: วางสุ่มกระจายบนถนนทันที ฉากจะไม่ดูโล่ง
                        x = i * 250 + rnd.Next(0, 100);
                    }

                    Enemies.Add(new EnemyMonster(sprite, x, y, speed, areaWidth));
                }
            }
        }

        public void Update()
        {
            foreach (EnemyMonster enemy in Enemies)
                enemy.Update();
        }

        public void Draw(Graphics g)
        {
            foreach (EnemyMonster enemy in Enemies)
                enemy.Draw(g);
        }

        public void SetAreaWidth(int width)
        {
            foreach (EnemyMonster enemy in Enemies)
                enemy.AreaWidth = width;
        }
    }
}