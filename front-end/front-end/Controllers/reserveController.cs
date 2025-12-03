using Microsoft.AspNetCore.Mvc;

namespace front_end.Controllers
{
    public class reserveController : Controller
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
