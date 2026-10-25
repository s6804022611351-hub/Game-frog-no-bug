using HappyJumpingFrog;
using System;
using System.Drawing;

namespace Sec2_Group1_HappyJumpingFrog.Class
{
    internal class TargetLotus : GameObject
    {
        public bool IsCollected { get; set; } = false;

        public TargetLotus(Image sprite, float x, float y, float width, float height)
            : base(sprite, x, y, (int)width, (int)height)
        {
        }

        public override void Draw(Graphics g)
        {
            if (g == null) return;

            // ถ้ายังไม่ถูกเก็บ -> วาดรูปดอกบัวตามปกติ
            // ถ้าถูกเก็บแล้ว -> ไม่ต้องวาดอะไรเลย (หายไปเลย ไม่มีวงกลมดำ)
            if (!IsCollected)
            {
                base.Draw(g);
            }
        }
    }
}