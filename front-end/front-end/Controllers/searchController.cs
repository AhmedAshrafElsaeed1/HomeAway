using front_end.Interfaces;
using front_end.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace front_end.Controllers
{
    public class SearchController : Controller
    {
        private readonly IHotelService _hotelService;

        public SearchController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // جيبي كل الفنادق
            var allHotels = await _hotelService.GetAllAsync();

            // اعملي الموديل
            var model = new SearchViewModel
            {
                Results = allHotels,
                Destination = "All Hotels"
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string destination)
        {
            var allHotels = await _hotelService.GetAllAsync();

            var filtered = allHotels
                .Where(h => string.IsNullOrEmpty(destination) ||
                            h.Address.Contains(destination, StringComparison.OrdinalIgnoreCase))
                .ToList();

            var model = new SearchViewModel
            {
                Results = filtered,
                Destination = destination
            };

            return View(model);
        }
    }
}
