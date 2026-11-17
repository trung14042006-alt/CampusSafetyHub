/****************/
/* Mã sinh viên: 202419003 */
/* Họ tên: Nguyễn Thành Trung */
/****************/

using System;

namespace CampusSafetyHub
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== HỆ THỐNG GIÁM SÁT AN TOÀN PHÒNG MÁY (CAMPUS SAFETY HUB) ===\n");

            // 1. Kiểm thử Adapter Pattern
            Console.WriteLine("--- Kiểm thử Adapter ---");
            var legacyDetector = new LegacySmokeDetector(735);
            ISensor smokeSensor = new SmokeSensorAdapter(legacyDetector);
            Console.WriteLine($"Giá trị đọc từ Adapter: {smokeSensor.ReadValue()}"); // 73.5
            Console.WriteLine($"Đơn vị: {smokeSensor.GetUnit()}\n"); // % smoke

            // 2. Thiết lập Subject và đăng ký các Observer
            var monitor = new SensorMonitor();

            var dashboard = new DashboardObserver();
            var emailAlert = new EmailAlertObserver();
            var auditLog = new AuditLogObserver();
            var emergencyBridge = new EmergencyBridgeObserver();

            monitor.Attach(dashboard);
            monitor.Attach(emailAlert);
            monitor.Attach(auditLog);
            monitor.Attach(emergencyBridge);

            // Thử đăng ký trùng lặp (đảm bảo không bị lặp observer)
            monitor.Attach(dashboard);

            // 3. Kiểm thử kịch bản vượt ngưỡng (WARNING và CRITICAL)
            Console.WriteLine("--- Kiểm thử giám sát cảm biến (Mức WARNING) ---");
            legacyDetector.SetSmokeLevel(550); // 55% -> WARNING
            monitor.CheckSensor("Phòng máy Server 01", smokeSensor);

            Console.WriteLine("\n--- Kiểm thử giám sát cảm biến (Mức CRITICAL) ---");
            legacyDetector.SetSmokeLevel(820); // 82% -> CRITICAL
            monitor.CheckSensor("Phòng máy Server 01", smokeSensor);

            Console.WriteLine("\n=== HOÀN TẤT KIỂM THỬ ===");
        }
    }
}