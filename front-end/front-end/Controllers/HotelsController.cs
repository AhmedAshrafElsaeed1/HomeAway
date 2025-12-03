using front_end.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace front_end.Controllers
{
    public class HotelsController : Controller               //  dont use for now !!!!!!!!!!
    {
        private readonly IHotelService _hotelService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var hotels = await _hotelService.GetAllAsync();
                return View(hotels);

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
                var hotel = await _hotelService.GetByIdAsync(id);
                return hotel == null ? NotFound() : View(hotel);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }

        }
    }
}
