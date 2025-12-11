using front_end.DTOs;
using front_end.Interfaces;
using front_end.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace front_end.Controllers
{
    public class ProviderController : Controller
    {
        private readonly IRoomService _roomService;

        public ProviderController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        // Dashboard
        public async Task<IActionResult> Index()
        {
            var rooms = await _roomService.GetAllAsync();
            var model = new ProviderDashboardViewModel
            {
                HotelInfo = null,
                Rooms = rooms,
                Reservations = null,
                TotalRevenue = 0
            };
            return View(model);
        }

        // Manage Rooms
        public async Task<IActionResult> Rooms()
        {
            var rooms = await _roomService.GetAllAsync();
            return View(rooms);
        }

        // GET: استخدمي نفس صفحة الـ signup لتظهر فورم إدخال الغرفة
        public IActionResult CreateRoom()
        {
            // هيروح على View الخاص بالـ signup
            return View("~/Views/Signup/Rooms.cshtml");
        }

        // POST: Create Room من صفحة signup
        [HttpPost]
        public async Task<IActionResult> CreateRoom(CreateRoomViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // لو في خطأ في البيانات نرجع نفس الصفحة مع الـ ViewModel
                return View("~/Views/Signup/Rooms.cshtml", model);
            }

            // تحويل ViewModel إلى DTO
            var dto = new RoomDto
            {
                Number = model.Number,
                Type = int.TryParse(model.Type, out int typeValue) ? typeValue : 0, // أو أي طريقة تحويل مناسبة
                
                Quantity = model.Quantity,
                // لو عندك HotelId، حطيه هنا
            };

            var newRoomId = await _roomService.CreateAsync(dto);
            if (newRoomId == null)
            {
                TempData["Error"] = "Failed to create room.";
                return View("~/Views/Signup/Rooms.cshtml", model);
            }

            TempData["Success"] = "Room created successfully!";
            return RedirectToAction("Rooms"); // بعد الإضافة روح على صفحة Manage Rooms
        }


        // POST: Update Price
        [HttpPost]
        public async Task<IActionResult> UpdatePrice(int roomId, decimal newPrice)
        {
            var dto = new UpdateRoomDto
            {
                Id = roomId,
                Price = newPrice
            };

            var success = await _roomService.UpdateAsync(dto);

            if (!success)
                TempData["Error"] = "Failed to update price.";

            return RedirectToAction("Rooms");
        }

        // Delete Room
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var success = await _roomService.DeleteAsync(id);
            if (!success)
                TempData["Error"] = "Failed to delete room.";

            return RedirectToAction("Rooms");
        }
    }
}

