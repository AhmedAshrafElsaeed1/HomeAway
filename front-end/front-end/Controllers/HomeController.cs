using front_end.Interfaces;
using front_end.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace front_end.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHotelService _hotelService;

        public HomeController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        // الصفحة الرئيسية
        public IActionResult Index()
        {
            return View();
        }

        // GET: Search page (from navbar)
        [HttpGet]
        public async Task<IActionResult> Search()
        {
            var hotels = await _hotelService.GetAllAsync();

            var model = new SearchViewModel
            {
                Results = hotels,   // هنا المهم جداً
                Destination = "",
                CheckIn = "",
                CheckOut = "",
                Guests = 1
            };

            return View(model);
        }


        // POST: Search with filters
        [HttpPost]
        public async Task<IActionResult> Search(string destination, string checkIn, string checkOut, int guests)
        {
            var allHotels = await _hotelService.GetAllAsync();

            var filteredHotels = allHotels
                .FindAll(h =>
                    string.IsNullOrEmpty(destination) ||
                    h.Address.Contains(destination, StringComparison.OrdinalIgnoreCase)
                );

            var model = new SearchViewModel
            {
                Results = filteredHotels,
                Destination = destination,
                CheckIn = checkIn,
                CheckOut = checkOut,
                Guests = guests
            };

            return View("~/Views/Search/Index.cshtml", model);
        }
    }
}
