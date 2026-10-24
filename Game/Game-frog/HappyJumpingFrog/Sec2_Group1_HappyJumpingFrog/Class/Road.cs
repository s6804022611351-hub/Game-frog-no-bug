using System;
using System.Collections.Generic;
using System.Text;

namespace Sec2_Group1_HappyJumpingFrog.Class
{
    public class Road
    {
        public List<EnemyMonster> Enemies { get; private set; }

        // topRow = แถวบนสุดของถนน (แถวละ 50 พิกเซล)
        public Road(int topRow, int laneCount, Image[] sprites, float baseSpeed, int areaWidth)
        {
            Enemies = new List<EnemyMonster>();
            Random rnd = new Random();

            for (int lane = 0; lane < laneCount; lane++)
            {
                float y = (topRow + lane) * 50;
                float direction = (lane % 2 == 0) ? 1 : -1;       // สลับทิศทุกเลน
                float speed = (baseSpeed + lane * 0.5f) * direction;
                Image sprite = sprites[lane % sprites.Length];     // สลับแบบศัตรู

                for (int i = 0; i < 3; i++)                        // เลนละ 3 ตัว
                {
                    float x = i * 250 + rnd.Next(0, 100);
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
