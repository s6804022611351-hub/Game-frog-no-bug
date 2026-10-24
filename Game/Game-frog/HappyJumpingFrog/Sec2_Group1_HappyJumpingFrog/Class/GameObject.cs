using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace HappyJumpingFrog
{
    public abstract class GameObject
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        protected Image Sprite;          // รูปของวัตถุ

        protected GameObject(Image sprite, float x, float y, int width, int height)
        {
            Sprite = sprite;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        // กรอบสี่เหลี่ยมไว้ตรวจการชน
        public RectangleF Bounds
        {
            get { return new RectangleF(X, Y, Width, Height); }
        }

        // คลาสลูกจะ override เพื่อกำหนดการเคลื่อนที่ของตัวเอง
        public virtual void Update() { }

        // วาดรูปลงหน้าจอ
        public virtual void Draw(Graphics g)
        {
            g.DrawImage(Sprite, X, Y, Width, Height);
        }

        public bool Intersects(GameObject other)
        {
            return Bounds.IntersectsWith(other.Bounds);
        }
    }
}