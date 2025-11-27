using Microsoft.AspNetCore.Mvc;

namespace front_end.Controllers
{
    public class loginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
