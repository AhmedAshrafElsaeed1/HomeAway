using front_end.DTOs;
using front_end.Interfaces;
using front_end.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace front_end.Controllers
{
    public class ProfileController : Controller
    {
        private readonly HttpClient _client;
        private readonly IAuthService _authService;

        public ProfileController(IAuthService authService, IHttpClientFactory clientFactory)
        {
            _authService = authService;
            _client = clientFactory.CreateClient("HomeAwayAPI");
        }

        public async Task<IActionResult> Index()
        {
            UserDto? user = null;

            try
            {
                // جلب الـ JWT من الكوكي (لو موجود)
                var token = HttpContext.Request.Cookies["HomeAwayJwt"];
                if (!string.IsNullOrEmpty(token))
                {
                    _client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token);

                    var response = await _client.GetAsync("Users/me");
                    if (response.IsSuccessStatusCode)
                    {
                        user = await response.Content.ReadFromJsonAsync<UserDto>();
                    }
                }
            }
            catch
            {
                // أي خطأ هنا هيتجاهل ويعرض الصفحة فاضية
            }

            // إنشاء ViewModel وإرسالها للـ View
            var vm = new ProfileViewModel
            {
                CurrentUser = user,
                IsSignedIn = user != null
            };

            return View(vm);
        }
    }
}
