using System;

namespace Sec2_Group1_HappyJumpingFrog.Class
{
    public class GameTimer
    {
        private float _remainingSeconds;

        public GameTimer(int seconds)
        {
            _remainingSeconds = seconds;
        }

        // 📍 ฟังก์ชันอัปเดตเวลานับถอยหลังตาม Delta Time
        public void Update(float dt)
        {
            if (_remainingSeconds > 0)
            {
                _remainingSeconds -= dt;
                if (_remainingSeconds < 0)
                    _remainingSeconds = 0;
            }
        }

        public void Reset(int seconds)
        {
            _remainingSeconds = seconds;
        }

        public bool IsTimeUp => _remainingSeconds <= 0;

        public string TimeText
        {
            get
            {
                int totalSec = (int)Math.Ceiling(_remainingSeconds);
                int mins = totalSec / 60;
                int secs = totalSec % 60;
                return $"{mins:D2}:{secs:D2}";
            }
        }
    }
}