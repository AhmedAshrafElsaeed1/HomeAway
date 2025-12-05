using front_end.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace front_end.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRoomService _roomService;

        public HomeController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult Search(string destination, int guests, string checkIn, string checkOut)
        {
            return RedirectToAction("Index", "Search",
                new { destination, guests, checkIn, checkOut });
        }
    }
}
