using Microsoft.AspNetCore.Mvc;

namespace front_end.Controllers
{
    public class signupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AsCustomer()
        {
            return View("ascustomer");
        }
        public IActionResult Ashotelowner()
        {
            return View("ashotelowner");
        }
        public IActionResult registerhotel()
        {
            return View("registerhotel");
        }
        public IActionResult Rooms()
        {
            return View("rooms");
        }
    }
}
