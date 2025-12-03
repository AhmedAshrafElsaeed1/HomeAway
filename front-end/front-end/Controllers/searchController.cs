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
            try
            {
                var allHotels = await _hotelService.GetAllAsync();

                var model = new SearchViewModel
                {
                    Results = allHotels,
                    Destination = "All Hotels"
                };

                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Index(string destination)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }

        }
    }
}
