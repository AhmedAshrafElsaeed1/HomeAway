using Microsoft.AspNetCore.Mvc;

namespace front_end.Controllers
{
    public class paymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
