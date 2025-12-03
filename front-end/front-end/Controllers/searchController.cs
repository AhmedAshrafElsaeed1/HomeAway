using front_end.Interfaces;
using front_end.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace front_end.Controllers
{
    public class SearchController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IHotelService _hotelService; // خدمة لجلب بيانات الفندق

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
            int star = 0,
            bool wifi = false,
            bool pool = false,
            bool parking = false)
        {
            // جلب كل الغرف
            var allRooms = await _roomService.GetAllAsync();

            // إنشاء قائمة للنتائج مع عنوان الفندق
            var roomsWithAddress = new List<SearchViewModel.RoomWithHotelAddress>();

            foreach (var room in allRooms)
            {
                // جلب بيانات الفندق لكل غرفة
                var hotel = await _hotelService.GetByIdAsync(room.HotelId);

                // إضافة الغرفة مع عنوان الفندق
                roomsWithAddress.Add(new SearchViewModel.RoomWithHotelAddress
                {
                    Room = room,
                    HotelAddress = hotel?.Address ?? string.Empty
                });
            }

            // فلترة حسب الوجهة
            if (!string.IsNullOrEmpty(destination))
            {
                roomsWithAddress = roomsWithAddress
                    .Where(r => r.HotelAddress.Contains(destination, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // فلترة حسب السعر
            roomsWithAddress = roomsWithAddress
                .Where(r => r.Room.Price >= minPrice && r.Room.Price <= maxPrice)
                .ToList();

            // فلترة حسب ستار ريتنج
            if (star > 0)
            {
                roomsWithAddress = roomsWithAddress
                    .Where(r => r.Room.Type >= star)
                    .ToList();
            }

            // فلترة Amenities (مؤقتة، يمكن تعديلها حسب DTO حقيقي)
            if (wifi) roomsWithAddress = roomsWithAddress.Where(r => r.Room.Type > -1).ToList();
            if (pool) roomsWithAddress = roomsWithAddress.Where(r => r.Room.Type > -1).ToList();
            if (parking) roomsWithAddress = roomsWithAddress.Where(r => r.Room.Type > -1).ToList();

            // تعبئة ViewModel
            var model = new SearchViewModel
            {
                Results = roomsWithAddress,
                Destination = destination,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                StarRating = star,
                Wifi = wifi,
                Pool = pool,
                Parking = parking
            };

            return View(model);
        }
    }
}
