/****************/
/* Mã sinh viên: 202419003 */
/* Họ tên: Nguyễn Thành Trung */
/****************/

using System;

namespace CampusSafetyHub
{
    // Lớp có sẵn, giả định không được sửa đổi
    public sealed class LegacySmokeDetector
    {
        private int smokeLevel;

        public LegacySmokeDetector(int smokeLevel)
        {
            this.smokeLevel = smokeLevel;
        }

        public int GetSmokeLevel() => smokeLevel; // 0..1000

        public string GetDescription() => "Legacy smoke detector";

        public void SetSmokeLevel(int value)
        {
            smokeLevel = Math.Clamp(value, 0, 1000);
        }
    }

    // Lớp Adapter chuyển đổi giao diện và thang đo từ hệ thống cũ sang ISensor
    public sealed class SmokeSensorAdapter : ISensor
    {
        private readonly LegacySmokeDetector legacy;

        public SmokeSensorAdapter(LegacySmokeDetector legacy)
        {
            this.legacy = legacy ?? throw new ArgumentNullException(nameof(legacy));
        }

        public double ReadValue()
        {
            // Chuyển đổi thang đo 0..1000 thành phần trăm 0..100
            return legacy.GetSmokeLevel() / 10.0;
        }

        public string GetUnit() => "% smoke";
    }
}