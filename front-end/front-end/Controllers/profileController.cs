using Microsoft.AspNetCore.Mvc;

namespace front_end.Controllers
{
    public class profileController : Controller
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
