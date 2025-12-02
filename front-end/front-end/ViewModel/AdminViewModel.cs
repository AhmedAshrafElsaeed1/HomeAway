using front_end.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace front_end.ViewModel
{
    public class AdminViewModel
    {
        // قائمة الحجوزات
        public List<ReservationDto>? Reservations { get; set; }

        // قائمة الفنادق (لو موجودة)
        public List<HotelDto>? Hotels { get; set; }

        // حسابات جاهزة للإحصائيات
        public int TotalBookings => Reservations?.Count ?? 0;

        public decimal TotalRevenue => Reservations?.Sum(r => r.TotalPrice) ?? 0;

        public int Pending => Reservations?.Count(r => r.Status == 0) ?? 0;

        public int Confirmed => Reservations?.Count(r => r.Status == 1) ?? 0;

        public int Cancelled => Reservations?.Count(r => r.Status == 2) ?? 0;
    }
}
