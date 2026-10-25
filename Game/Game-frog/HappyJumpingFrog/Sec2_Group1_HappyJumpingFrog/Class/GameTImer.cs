using System;
using System.Collections.Generic;
using System.Text;

namespace Sec2_Group1_HappyJumpingFrog.Class
{
    internal class GameTImer
    {
        // นาฬิกาจับเวลาถอยหลังของแต่ละด่าน
        public class GameTimer
        {
            public int TotalSeconds { get; private set; }        // เวลาทั้งหมดของด่าน
            public double RemainingSeconds { get; private set; } // เวลาที่เหลือ

            public bool IsTimeUp
            {
                get { return RemainingSeconds <= 0; }
            }

            // เวลาที่ใช้ไปแล้ว (เอาไว้คิดคะแนนในเฟสหลัง)
            public double ElapsedSeconds
            {
                get { return TotalSeconds - RemainingSeconds; }
            }

            // แสดงเป็น mm:ss เช่น 25:00
            public string TimeText
            {
                get
                {
                    int s = (int)Math.Ceiling(RemainingSeconds);
                    return string.Format("{0:00}:{1:00}", s / 60, s % 60);
                }
            }

            public GameTimer(int totalSeconds)
            {
                Reset(totalSeconds);
            }

            // ตั้งเวลาใหม่ (ใช้ตอนเปลี่ยนด่าน)
            public void Reset(int totalSeconds)
            {
                TotalSeconds = totalSeconds;
                RemainingSeconds = totalSeconds;
            }

            // เริ่มนับใหม่ด้วยเวลาเดิมของด่าน
            public void Restart()
            {
                RemainingSeconds = TotalSeconds;
            }

            // ลดเวลาลงตามที่ผ่านไปจริง (หน่วยวินาที)
            public void Tick(double elapsedSeconds)
            {
                RemainingSeconds -= elapsedSeconds;
                if (RemainingSeconds < 0) RemainingSeconds = 0;
            }
        }
    }
}
