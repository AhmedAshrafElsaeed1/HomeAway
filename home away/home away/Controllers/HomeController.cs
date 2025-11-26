using Microsoft.AspNetCore.Mvc;
using HomeAway.Service;
using home_away.Interfaces;  // IHotelService interface
using System.Threading.Tasks;

namespace HomeAway.FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHotelService _hotelService;

        public HomeController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        public async Task<IActionResult> Index()
        {
            var hotels = await _hotelService.GetAllAsync();
            return View(hotels);
            //return View();
        }
    }
}
