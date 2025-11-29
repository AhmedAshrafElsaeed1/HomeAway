using front_end.Auth;
using front_end.DTOs;
using front_end.Interfaces;
using front_end.Services;
using Microsoft.AspNetCore.Mvc;

namespace front_end.Controllers
{
    public class loginController : Controller
    {
        IAuthService authService;
        public loginController(IAuthService _authService)
        {
            authService = _authService;
        }
        public IActionResult Index(LoginDto loginDto)
        {
            //LoginDto user = new LoginDto();
            //user.UserName = Request.Form["UserName"];
            //user.Password = Request.Form["Password"];
            //var result = authService.LoginAsync(loginDto);
            return View();
        }
    }
}
