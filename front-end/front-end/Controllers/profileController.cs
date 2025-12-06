using front_end.DTOs;
using front_end.Interfaces;
using front_end.Services;
using front_end.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace front_end.Controllers
{
    public class ProfileController : Controller
    {
        private readonly HttpClient _client;
        private readonly IAuthService _authService;

        public ProfileController(IAuthService authService)
        {
            _authService = authService;
        }


        public async Task<IActionResult> Index()
        {
            var isSignedIn = await _authService.IsSignedInAsync();
            if (!isSignedIn)
            {
                // لو مش مسجل دخول، نودي على صفحة تسجيل الدخول
                return RedirectToAction("Index", "Login");
            }

            // لو مسجل دخول، نجيب بيانات المستخدم من API
            var ctx = HttpContext;
            var token = ctx.Request.Cookies["HomeAwayJwt"];
            if (token == null) return RedirectToAction("Index", "Login");

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var resp = await _client.GetAsync("Users/me"); // endpoint يرجع بيانات المستخدم الحالي

            if (!resp.IsSuccessStatusCode) return RedirectToAction("Index", "Login");

            var user = await resp.Content.ReadFromJsonAsync<UserDto>();

            var vm = new ProfileViewModel
            {
                CurrentUser = user,
                IsSignedIn = true
            };

            return View(vm);
        }
    }
}
