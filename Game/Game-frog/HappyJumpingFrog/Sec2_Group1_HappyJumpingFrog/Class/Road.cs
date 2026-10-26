using HappyJumpingFrog;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Sec2_Group1_HappyJumpingFrog.Class
{
    public class Road
    {
        private const int EnemySize = EnemyMonster.EnemyHeight;

        public List<EnemyMonster> Enemies { get; private set; }

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

                bool isBottomLaneGradual = gradualBottomLane && (lane == laneCount - 1);

                for (int i = 0; i < 3; i++)
                {
                    float x;

                    if (isBottomLaneGradual)
                    {
                        if (direction > 0)
                            x = -200f - (i * 280f) - rnd.Next(0, 50);
                        else
                            x = areaWidth + 200f + (i * 280f) + rnd.Next(0, 50);
                    }
                    else
                    {
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

        // 📍 เพิ่มเมธอดตรวจจับการชนระหว่างกบกับศัตรูบนถนน
        public bool CheckCollision(PlayerFrog frog)
        {
            if (frog == null || Enemies == null) return false;

            RectangleF frogBounds = new RectangleF(frog.X, frog.Y, frog.Width, frog.Height);

            foreach (EnemyMonster enemy in Enemies)
            {
                RectangleF enemyBounds = new RectangleF(enemy.X, enemy.Y, enemy.Width, enemy.Height);
                if (frogBounds.IntersectsWith(enemyBounds))
                {
                    return true;
                }
            }
            return false;
        }
        
    }

}