using front_end.DTOs;
using System.Collections.Generic;

namespace front_end.ViewModel
{
    public class SearchViewModel
    {
        // بدل List<RoomDto>، نعمل List من RoomWithHotelAddress
        public List<RoomWithHotelAddress>? Results { get; set; }

        // Filters
        public string? Destination { get; set; }
        public int Guests { get; set; }
        public string? CheckIn { get; set; }
        public string? CheckOut { get; set; }

        public decimal MinPrice { get; set; } = 0;
        public decimal MaxPrice { get; set; } = 1000;

        public int StarRating { get; set; } = 0;

        public bool Wifi { get; set; }
        public bool Parking { get; set; }
        public bool Pool { get; set; }

        // Nested class لربط الغرفة بعنوان الفندق
        public class RoomWithHotelAddress
        {
            public RoomDto Room { get; set; } = null!;
            public string HotelAddress { get; set; } = string.Empty;
        }
    }
}
