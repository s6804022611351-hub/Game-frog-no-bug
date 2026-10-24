using HappyJumpingFrog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Policy;
using System.Text;

namespace Sec2_Group1_HappyJumpingFrog.Class
{
    public class EnemyMonster : GameObject  
    {
        private readonly float _speed;        // บวก = ไปขวา, ลบ = ไปซ้าย
        public int AreaWidth { get; set; }  

        public EnemyMonster(Image sprite, float x, float y, float speed, int areaWidth)
            : base(sprite, x, y, 50, 50)
        {
            _speed = speed;
            AreaWidth = areaWidth;
        }

        public override void Update()
        {
            X += _speed;

            // วิ่งพ้นจอแล้วให้โผล่อีกฝั่ง
            if (_speed > 0 && X > AreaWidth) X = -Width;
            if (_speed < 0 && X < -Width) X = AreaWidth;
        }

        public override void Draw(Graphics g)
        {
            if (_speed >= 0)
            {
                base.Draw(g);
            }
            else
            {
                // พลิกรูปซ้าย-ขวาเมื่อวิ่งไปทางซ้าย
                PointF[] flipped =
                {
                    new PointF(X + Width, Y),           // มุมซ้ายบน
                    new PointF(X, Y),                   // มุมขวาบน
                    new PointF(X + Width, Y + Height)   // มุมซ้ายล่าง
                };
                g.DrawImage(Sprite, flipped);
            }
        }
    }
}
