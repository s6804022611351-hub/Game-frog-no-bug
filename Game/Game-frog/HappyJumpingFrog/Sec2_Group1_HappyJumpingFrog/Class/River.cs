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
            // === ดึงรูปภาพที่คุณเพิ่งอัปโหลดมาใช้งานตรงนี้ ===
            // (เปลี่ยน enemy1 หรือ enemy2 เป็นชื่อรูปภาพที่คุณเพิ่งเพิ่มเข้าไป)
            Image monsterImg1 = Properties.Resources.enemy1;
            Image monsterImg2 = Properties.Resources.enemy2;

            // สปอว์นมอนสเตอร์เลนที่ 1 (วิ่งไปทางขวา)
            Friends.Add(new FriendMonster(monsterImg1, 0, 120, 3f, formWidth));
            Friends.Add(new FriendMonster(monsterImg1, 250, 120, 3f, formWidth));

            // สปอว์นมอนสเตอร์เลนที่ 2 (วิ่งไปทางซ้าย)
            Friends.Add(new FriendMonster(monsterImg2, 100, 180, -2f, formWidth));
            Friends.Add(new FriendMonster(monsterImg2, 350, 180, -2f, formWidth));
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