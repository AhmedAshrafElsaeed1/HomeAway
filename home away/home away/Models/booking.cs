namespace HomeAway.Models
{
    public class Booking
    {
        // 1. الخصائص الأساسية للحجز
        public int BookingId { get; set; }
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public string CustomerName { get; set; }

        // 2. خصائص التواريخ والمبالغ
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public decimal TotalAmount { get; set; }

        // 3. خصائص الحالة
        public string Status { get; set; } // مثال: "Confirmed", "Pending", "Cancelled"

        // 4. خصائص لعرض اسم الفندق (إذا لم يرسله الـ API مباشرة، يجب جلبه بشكل منفصل)
        public string HotelName { get; set; }

        // ... يمكن إضافة المزيد من الخصائص حسب تصميم قاعدة البيانات ...
    }
}