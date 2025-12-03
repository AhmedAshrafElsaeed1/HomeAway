using Microsoft.AspNetCore.Mvc;

namespace front_end.Controllers
{
    public class paymentController : Controller
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
    }
}
