/****************/
/* Mã sinh viên: 202419003 */
/* Họ tên: Nguyễn Thành Trung */
/****************/

using System;

namespace CampusSafetyHub
{
    public sealed record AlertEvent(
        string SensorName,
        double Value,
        string Unit,
        string Severity,
        DateTime OccurredAt);
}