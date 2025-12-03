using Microsoft.AspNetCore.Mvc;

namespace front_end.Controllers
{
    public class signupController : Controller
    {
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
        public IActionResult AsCustomer()
        {
            try
            {
                return View("ascustomer");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }

        }
        public IActionResult Ashotelowner()
        {
            try
            {
                return View("ashotelowner");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }

        }
        public IActionResult registerhotel()
        {
            try
            {
                return View("registerhotel");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }

        }
        public IActionResult Rooms()
        {
            try
            {
                return View("rooms");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }

        }
    }
}
