using front_end.Auth;
using front_end.Interfaces;
using front_end.ViewModel;
using Microsoft.AspNetCore.Mvc;



namespace front_end.Controllers
{
    public class SignupController : Controller
    {
        public IActionResult Index()
        {
            return View(); // Views/Signup/Index.cshtml
        }

        public IActionResult AsCustomer()
        {
            return View("AsCustomer"); // Views/Signup/AsCustomer.cshtml
        }

        public IActionResult AsHotelOwner()
        {
            return View("AsHotelOwner"); // Views/Signup/AsHotelOwner.cshtml
        }

        public IActionResult RegisterHotel()
        {
            return View("RegisterHotel"); // Views/Signup/RegisterHotel.cshtml
        }

        public IActionResult Rooms()
        {
            return View("Rooms"); // Views/Signup/Rooms.cshtml
        }
    }
}
