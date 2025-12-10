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
            List<ReservationDto> bookings = new List<ReservationDto>();

            try
            {
                // 1️⃣ جلب الـ JWT من الكوكي
                var token = HttpContext.Request.Cookies["HomeAwayJwt"];
                if (!string.IsNullOrEmpty(token))
                {
                    _client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token);

                    // 2️⃣ جلب بيانات المستخدم
                    var userResponse = await _client.GetAsync("Users/me");
                    if (userResponse.IsSuccessStatusCode)
                    {
                        user = await userResponse.Content.ReadFromJsonAsync<UserDto>();
                    }

                    // 3️⃣ جلب الحجوزات الخاصة بالمستخدم
                    if (user != null)
                    {
                        var bookingResponse = await _client.GetAsync($"Reservations/user/{user.Id}");
                        if (bookingResponse.IsSuccessStatusCode)
                        {
                            bookings = await bookingResponse.Content.ReadFromJsonAsync<List<ReservationDto>>()
                                       ?? new List<ReservationDto>();
                        }
                    }
                }
            }
            catch
            {
                // أي خطأ هيتجاهل
            }

            var vm = new ProfileViewModel
            {
                CurrentUser = user,
                
                IsSignedIn = user != null,
                Bookings = bookings
            };

            return View(vm);
        }
    }
}
