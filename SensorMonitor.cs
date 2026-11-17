/****************/
/* Mã sinh viên: 202419003 */
/* Họ tên: Nguyễn Thành Trung */
/****************/

using System;
using System.Collections.Generic;

namespace CampusSafetyHub
{
    public sealed class SensorMonitor
    {
        private readonly List<IAlertObserver> observers = new();

        public void Attach(IAlertObserver observer)
        {
            ArgumentNullException.ThrowIfNull(observer);
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }

        public void Detach(IAlertObserver observer)
        {
            observers.Remove(observer);
        }

        public void CheckSensor(string sensorName, ISensor sensor)
        {
            double value = sensor.ReadValue();
            string? severity = value switch
            {
                >= 70 => "CRITICAL",
                >= 50 => "WARNING",
                _ => null
            };

            if (severity is null)
            {
                return;
            }

            var alertEvent = new AlertEvent(
                sensorName,
                value,
                sensor.GetUnit(),
                severity,
                DateTime.Now);

            Notify(alertEvent);
        }

        private void Notify(AlertEvent alertEvent)
        {
            // Duyệt trên bản sao ToArray() để tránh lỗi khi observer tự hủy đăng ký trong lúc thông báo
            foreach (IAlertObserver observer in observers.ToArray())
            {
                try
                {
                    observer.Update(alertEvent);
                }
                catch (Exception ex)
                {
                    // Đảm bảo một observer lỗi không làm gián đoạn các observer khác
                    Console.WriteLine($"[ERROR] Observer gặp sự cố: {ex.Message}");
                }
            }
        }
    }
}