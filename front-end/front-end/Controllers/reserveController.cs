using front_end.DTOs;
using front_end.Interfaces;
using front_end.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace front_end.Controllers
{
    public class ReserveController : Controller
    {
        private readonly IRoomService _roomService;

        public ReserveController(IRoomService RoomService)
        {
            _roomService = RoomService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id =2)
        {
            var room = await _roomService.GetByIdAsync(id);
            if (room == null)
                return NotFound();

            var vm = new ReserveViewModel
            {
                Room = room
            };

            return View(vm);
        }
    }
}
