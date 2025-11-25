using home_away.Interfaces;
using HomeAway.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace home_away.Controllers
{
    public class HotelsController : Controller
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

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(HotelDto dto)
        {
            await _hotelService.CreateAsync(dto);
            return RedirectToAction("Index");
        }
    }
}
