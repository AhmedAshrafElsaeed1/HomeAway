using front_end.DTOs;
using System.Collections.Generic;

namespace front_end.ViewModel
{
    public class SearchViewModel
    {
        // النتائج النهائية بعد ربط الغرفة بعنوان الفندق
        public List<RoomWithHotelAddress> Results { get; set; } = new List<RoomWithHotelAddress>();

        // Filters
        public string? Destination { get; set; }
        public int Guests { get; set; }
        public string? CheckIn { get; set; }
        public string? CheckOut { get; set; }

        public decimal MinPrice { get; set; } = 0;
        public decimal MaxPrice { get; set; } = 1000;

        // نستخدم رقم النجمة فقط لو حابين نفلتر حسب تقييم الفندق
        public int StarRating { get; set; } = 0;

        // Nested class لربط الغرفة بعنوان الفندق
        public class RoomWithHotelAddress
        {
            public RoomDto Room { get; set; } = null!; // بيانات الغرفة
            public string HotelAddress { get; set; } = string.Empty; // عنوان الفندق
        }
    }
}
