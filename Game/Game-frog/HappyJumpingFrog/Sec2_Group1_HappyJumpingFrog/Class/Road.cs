using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Sec2_Group1_HappyJumpingFrog.Class
{
    public class Road
    {
        private const int EnemySize = 50;

        public List<EnemyMonster> Enemies { get; private set; }

        // topY = ตำแหน่งขอบบนของถนน, laneHeight = ความสูงของแต่ละเลน
        public Road(float topY, float laneHeight, int laneCount,
                    Image[] sprites, float baseSpeed, int areaWidth)
        {
            Enemies = new List<EnemyMonster>();
            Random rnd = new Random();

            for (int lane = 0; lane < laneCount; lane++)
            {
                // วางศัตรูให้อยู่กึ่งกลางเลน
                float y = topY + lane * laneHeight + (laneHeight - EnemySize) / 2f;
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