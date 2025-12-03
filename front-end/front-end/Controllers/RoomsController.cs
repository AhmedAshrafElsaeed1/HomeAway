using front_end.DTOs;
using front_end.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace front_end.Controllers
{
    public class RoomsController : Controller  //dont use for now !!!!!!!!!!
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var rooms = await _roomService.GetAllAsync();
                return View(rooms);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }

        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var room = await _roomService.GetByIdAsync(id);
                return room == null ? NotFound() : View(room);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Create(RoomDto dto)
        {
            try
            {
                var id = await _roomService.CreateAsync(dto);
                if (id == null) return BadRequest();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateRoomDto dto)
        {
            try
            {
                var ok = await _roomService.UpdateAsync(dto);
                return ok ? RedirectToAction("Index") : NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var ok = await _roomService.DeleteAsync(id);
                return ok ? RedirectToAction("Index") : NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }

        }
    }

}
