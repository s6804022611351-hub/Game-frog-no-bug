using System;
using System.Collections.Generic;
using System.Drawing;

namespace Sec2_Group1_HappyJumpingFrog.Class
{
    internal class River
    {
        public List<FriendMonster> Friends { get; private set; } = new List<FriendMonster>();

        public River(int formWidth)
        {
            // ดึงรูป Fish1 และ Fish2 (ตัวพิมพ์ใหญ่ F)
            Image fish1 = Properties.Resources.Fish1;
            Image fish2 = Properties.Resources.Fish2;

            // ==========================================
            // เลนที่ 1 (เลนบนสุด): Fish1 วิ่งไปทางขวา (Y = 110)
            // ==========================================
            Friends.Add(new FriendMonster(fish1, 50, 110, 2f, formWidth));
            Friends.Add(new FriendMonster(fish1, 350, 110, 2f, formWidth));
            Friends.Add(new FriendMonster(fish1, 650, 110, 2f, formWidth));

            // ==========================================
            // เลนที่ 2 (ตรงกลาง): Fish2 วิ่งไปทางซ้าย (Y = 180)
            // ==========================================
            Friends.Add(new FriendMonster(fish2, 200, 180, -3f, formWidth));
            Friends.Add(new FriendMonster(fish2, 550, 180, -3f, formWidth));

            // ==========================================
            // เลนที่ 3 (เลนล่างสุด): Fish1 วิ่งไปทางขวา (Y = 250)
            // ==========================================
            Friends.Add(new FriendMonster(fish1, 180, 250, 2.5f, formWidth));
            Friends.Add(new FriendMonster(fish1, 500, 250, 2.5f, formWidth));
            Friends.Add(new FriendMonster(fish1, 750, 250, 2.5f, formWidth));
        }

        public void Draw(Graphics g)
        {
            foreach (var friend in Friends)
            {
                friend.Draw(g);
            }
        }

        public void UpdateFriends()
        {
            foreach (var friend in Friends)
            {
                friend.Update();
            }
        }
    }
}