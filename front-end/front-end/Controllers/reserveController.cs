using Microsoft.AspNetCore.Mvc;

namespace front_end.Controllers
{
    public class reserveController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
