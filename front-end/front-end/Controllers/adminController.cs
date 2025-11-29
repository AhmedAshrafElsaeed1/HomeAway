using Microsoft.AspNetCore.Mvc;
using front_end.Auth;
using front_end.Interfaces;


namespace front_end.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<IActionResult> Users()
        {
            var users = await _adminService.GetAllUsersAsync();
            return View(users);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var success = await _adminService.RegisterAdminAsync(dto);

            if (!success)
            {
                ViewBag.Error = "Failed to register admin.";
                return View(dto);
            }

            return RedirectToAction("Users");
        }





        public IActionResult dashboard()
        {
            return View();
        }
    }
}
