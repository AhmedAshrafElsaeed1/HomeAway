using front_end.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace front_end.Controllers
{
    public class HotelsController : Controller               // dont use !!!!!!!!!!             w8 for more info
    {
        private readonly IHotelService _hotelService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        public async Task<IActionResult> Index()
        {
            var hotels = await _hotelService.GetAllAsync();
            return View(hotels);
        }

        public async Task<IActionResult> Details(int id)
        {
            var hotel = await _hotelService.GetByIdAsync(id);
            return hotel == null ? NotFound() : View(hotel);
        }
    }
}
