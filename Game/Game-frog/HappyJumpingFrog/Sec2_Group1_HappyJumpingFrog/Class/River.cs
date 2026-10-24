using System;
using System.Collections.Generic;
using System.Drawing;

namespace Sec2_Group1_HappyJumpingFrog.Class
{
    internal class River
    {
        // กึ่งกลางแนวตั้งของแต่ละเลนแม่น้ำ (ต้องตรงกับ RowCenters ใน PlayerFrog)
        private const float Lane1Center = 130;   // เลนบนสุด
        private const float Lane2Center = 200;   // เลนกลาง
        private const float Lane3Center = 270;   // เลนล่างสุด (ใกล้ถนน)

        public List<FriendMonster> Friends { get; private set; } = new List<FriendMonster>();

        // แปลงจุดกึ่งกลางเลนเป็นพิกัดขอบบนของปลา
        private static float TopOf(float laneCenter)
        {
            return laneCenter - FriendMonster.FishHeight / 2f;
        }

        public River(int formWidth)
        {
            Image fish1 = Properties.Resources.Fish1;
            Image fish2 = Properties.Resources.Fish2;

            // เลนบนสุด: Fish1 วิ่งไปทางขวา
            Friends.Add(new FriendMonster(fish1, 50, TopOf(Lane1Center), 2f, formWidth));
            Friends.Add(new FriendMonster(fish1, 350, TopOf(Lane1Center), 2f, formWidth));
            Friends.Add(new FriendMonster(fish1, 650, TopOf(Lane1Center), 2f, formWidth));

            // เลนกลาง: Fish2 วิ่งไปทางซ้าย
            Friends.Add(new FriendMonster(fish2, 200, TopOf(Lane2Center), -3f, formWidth));
            Friends.Add(new FriendMonster(fish2, 550, TopOf(Lane2Center), -3f, formWidth));

            // เลนล่างสุด: Fish1 วิ่งไปทางขวา
            Friends.Add(new FriendMonster(fish1, 180, TopOf(Lane3Center), 2.5f, formWidth));
            Friends.Add(new FriendMonster(fish1, 500, TopOf(Lane3Center), 2.5f, formWidth));
            Friends.Add(new FriendMonster(fish1, 750, TopOf(Lane3Center), 2.5f, formWidth));
        }

        public void Draw(Graphics g)
        {
            foreach (var friend in Friends)
                friend.Draw(g);
        }

        public void UpdateFriends()
        {
            foreach (var friend in Friends)
                friend.Update();
        }

        public void SetAreaWidth(int width)
        {
            foreach (var friend in Friends)
                friend.AreaWidth = width;
        }
    }
}