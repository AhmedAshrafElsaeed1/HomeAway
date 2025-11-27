using Microsoft.AspNetCore.Mvc;

namespace front_end.Controllers
{
    public class profileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
