using front_end.DTOs;
using front_end.Interfaces;
using front_end.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using front_end.Models;

namespace front_end.Controllers
{
    public class ProviderController : Controller
    {
        // الخدمات موجودة لكن لن نستخدمها الآن لتجنب الايرور
        private readonly IHotelService _hotelService;
        private readonly IRoomService _roomService;
        private readonly IReservationService _reservationService;

        public ProviderController(IHotelService hotelService, IRoomService roomService, IReservationService reservationService)
        {
            _hotelService = hotelService;
            _roomService = roomService;
            _reservationService = reservationService;
        }

        // 1. Dashboard Page (بيانات وهمية للتجربة)
        // 1. Dashboard Page (Mock Data Updated)
        public IActionResult Index()
        {
            try
            {
                var hotel = new HotelDto
                {
                    Id = 1,
                    Name = "Grand Plaza Hotel (Test)",
                    Address = "Cairo, Egypt",
                    Rating = 5
                };

                var myRooms = new List<RoomDto>
        {
            new RoomDto { Id = 101, Number = "101", Type = 0, Price = 100, IsAvailable = true, HotelId = 1 },
            new RoomDto { Id = 102, Number = "102", Type = 1, Price = 200, IsAvailable = false, HotelId = 1 },
            new RoomDto { Id = 103, Number = "Suite 1", Type = 3, Price = 500, IsAvailable = true, HotelId = 1 }
        };

                // هنا ضفتلك حجوزات بكل الحالات (0, 1, 2, 3)
                var myReservations = new List<ReservationDto>
        {
            new ReservationDto { Id = 50, RoomId = 102, From = DateTime.Now, To = DateTime.Now.AddDays(3), TotalPrice = 600, Status = 1 }, // Confirmed
            new ReservationDto { Id = 51, RoomId = 101, From = DateTime.Now.AddDays(5), To = DateTime.Now.AddDays(7), TotalPrice = 200, Status = 0 }, // Pending
            new ReservationDto { Id = 52, RoomId = 103, From = DateTime.Now.AddDays(-5), To = DateTime.Now.AddDays(-2), TotalPrice = 1500, Status = 3 }, // Completed
            new ReservationDto { Id = 53, RoomId = 101, From = DateTime.Now.AddDays(10), To = DateTime.Now.AddDays(12), TotalPrice = 200, Status = 2 } // Canceled
        };

                var model = new ProviderDashboardViewModel
                {
                    HotelInfo = hotel,
                    Rooms = myRooms,
                    Reservations = myReservations,
                    // بنحسب الأرباح فقط للحجوزات المكتملة أو المؤكدة (حسب البيزنس بتاعك، هنا حسبت الاتنين)
                    TotalRevenue = myReservations.Where(r => r.Status == 1 || r.Status == 3).Sum(r => r.TotalPrice)
                };

                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                var errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
                return View("Error", errorModel);
            }
        }

        // 2. Manage Rooms Page (بيانات وهمية)
        // 2. Manage Rooms Page (Updated Mock Data)
        public IActionResult Rooms()
        {
            var myRooms = new List<RoomDto>
    {
        // غرفة فردية متاحة
        new RoomDto { Id = 101, Number = "101", Type = 0, Price = 100, IsAvailable = true, HotelId = 1 },
        
        // غرفة مزدوجة مشغولة
        new RoomDto { Id = 102, Number = "102", Type = 1, Price = 200, IsAvailable = false, HotelId = 1 },
        
        // جناح (Penthouse) متاح
        new RoomDto { Id = 105, Number = "ROYAL SUITE", Type = 4, Price = 1500, IsAvailable = true, HotelId = 1 },
        
        // غرفة ثلاثية
        new RoomDto { Id = 103, Number = "205", Type = 2, Price = 350, IsAvailable = true, HotelId = 1 },

         // غرفة رباعية
        new RoomDto { Id = 104, Number = "206", Type = 3, Price = 500, IsAvailable = true, HotelId = 1 }
    };

            return View(myRooms);
        }

        // 3. Create Room (GET) - بتفتح عادي
        public IActionResult CreateRoom()
        {
            return View();
        }

        // 3. Create Room (POST) - مش هتحفظ بجد لكن هترجعك للصفحة
        [HttpPost]
        public IActionResult CreateRoom(RoomDto dto)
        {
            // تمثيل عملية الحفظ
            return RedirectToAction("Rooms");
        }

        // 4. Update Price - تمثيل فقط
        [HttpPost]
        public IActionResult UpdatePrice(int roomId, decimal newPrice)
        {
            return RedirectToAction("Rooms");
        }

        // 5. Delete Room - تمثيل فقط
        public IActionResult DeleteRoom(int id)
        {
            return RedirectToAction("Rooms");
        }
    }
}