using front_end.Interfaces;
using front_end.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace front_end.Controllers
{
    public class SearchController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IHotelService _hotelService;

        public SearchController(IRoomService roomService, IHotelService hotelService)
        {
            _roomService = roomService;
            _hotelService = hotelService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(
            string? destination,
            decimal minPrice = 0,
            decimal maxPrice = 1000,
            int star = 0) // star هنا ممكن تمثل تصنيف أو نوع الغرفة
        {
            // 1️⃣ جلب كل الغرف
            var allRooms = await _roomService.GetAllAsync();

            // 2️⃣ ربط كل غرفة بعنوان الفندق
            var roomsWithAddress = new List<SearchViewModel.RoomWithHotelAddress>();

            foreach (var room in allRooms)
            {
                string hotelAddress = string.Empty;

                if (room.HotelId.HasValue)
                {
                    var hotel = await _hotelService.GetByIdAsync(room.HotelId.Value);
                    hotelAddress = hotel?.Address ?? string.Empty;
                }

                roomsWithAddress.Add(new SearchViewModel.RoomWithHotelAddress
                {
                    Room = room,
                    HotelAddress = hotelAddress
                });
            }

            // 3️⃣ فلترة حسب الوجهة
            if (!string.IsNullOrEmpty(destination))
            {
                roomsWithAddress = roomsWithAddress
                    .Where(r => r.HotelAddress.Contains(destination, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // 4️⃣ فلترة حسب السعر
            roomsWithAddress = roomsWithAddress
                .Where(r => r.Room.Price >= minPrice && r.Room.Price <= maxPrice)
                .ToList();

            // 5️⃣ فلترة حسب ستار ريتنج / نوع الغرفة
            if (star > 0)
            {
                roomsWithAddress = roomsWithAddress
                    .Where(r => r.Room.Type >= star)
                    .ToList();
            }

            // 6️⃣ تعبئة الـ ViewModel
            var model = new SearchViewModel
            {
                Results = roomsWithAddress,
                Destination = destination,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                StarRating = star
            };

            return View(model);
        }
    }
}
