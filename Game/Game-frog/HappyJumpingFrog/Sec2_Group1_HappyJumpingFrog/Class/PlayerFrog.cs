using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace HappyJumpingFrog
{
    public class PlayerFrog : GameObject
    {
        private const int HopSize = 50;           // กระโดดทีละ 1 ช่อง
        private readonly float _startX, _startY;

        public int Lives { get; private set; }
        public bool IsGameOver { get { return Lives <= 0; } }

        public PlayerFrog(Image sprite, float x, float y)
            : base(sprite, x, y, 50, 50)
        {
            _startX = x;
            _startY = y;
            Lives = 5;
        }

        public void Hop(Keys key, int areaWidth, int areaHeight)
        {
            if (key == Keys.Up) Y -= HopSize;
            if (key == Keys.Down) Y += HopSize;
            if (key == Keys.Left) X -= HopSize;
            if (key == Keys.Right) X += HopSize;

            // ไม่ให้กบออกนอกจอ
            if (X < 0) X = 0;
            if (Y < 0) Y = 0;
            if (X > areaWidth - Width) X = areaWidth - Width;
            if (Y > areaHeight - Height) Y = areaHeight - Height;
        }

        public void Die()
        {
            Lives--;
            X = _startX;     // กลับจุดเริ่มต้น
            Y = _startY;
        }
    }
}