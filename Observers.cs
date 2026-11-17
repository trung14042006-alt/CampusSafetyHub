/****************/
/* Mã sinh viên: 202419003 */
/* Họ tên: Nguyễn Thành Trung */
/****************/

using System;

namespace CampusSafetyHub
{
    public sealed class DashboardObserver : IAlertObserver
    {
        public void Update(AlertEvent alertEvent)
        {
            Console.WriteLine(
                $"[DASHBOARD] {alertEvent.SensorName}: " +
                $"{alertEvent.Value:F1}{alertEvent.Unit} " +
                $"[{alertEvent.Severity}] lúc {alertEvent.OccurredAt:HH:mm:ss}");
        }
    }

    public sealed class EmailAlertObserver : IAlertObserver
    {
        public void Update(AlertEvent alertEvent)
        {
            Console.WriteLine(
                $"[EMAIL SYSTEM] Gửi cảnh báo khẩn cấp tới quản trị viên - " +
                $"Cảm biến {alertEvent.SensorName} đạt mức {alertEvent.Severity} ({alertEvent.Value}{alertEvent.Unit}).");
        }
    }

    public sealed class AuditLogObserver : IAlertObserver
    {
        public void Update(AlertEvent alertEvent)
        {
            Console.WriteLine(
                $"[AUDIT LOG] Ghi nhận sự kiện: [{alertEvent.Severity}] {alertEvent.SensorName} - {alertEvent.Value}{alertEvent.Unit}");
        }
    }

    public sealed class EmergencyBridgeObserver : IAlertObserver
    {
        public void Update(AlertEvent alertEvent)
        {
            Console.WriteLine(
                $"[EMERGENCY BRIDGE] Kích hoạt quy trình xử lý khẩn cấp cho sự kiện mức độ: {alertEvent.Severity}");
        }
    }
}