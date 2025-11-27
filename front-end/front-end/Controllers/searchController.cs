using Microsoft.AspNetCore.Mvc;

namespace front_end.Controllers
{
    public class searchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
