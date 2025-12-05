using front_end.Auth;
using front_end.Interfaces;
using front_end.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace front_end.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthService _authService;

        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var loginDto = new LoginDto
            {
                UserName = vm.UserName,
                Password = vm.Password
            };

            var token = await _authService.LoginAsync(loginDto);

            if (token == null)
            {
                ViewBag.Error = "Invalid username or password";
                return View(vm);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
