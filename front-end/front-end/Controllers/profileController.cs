using front_end.Interfaces;
using front_end.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace front_end.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IReservationService _reservationService;

        public ProfileController(IAuthService authService, IReservationService reservationService)
        {
            _authService = authService;
            _reservationService = reservationService;
        }

        public async Task<IActionResult> Index()
        {
            // جلب بيانات المستخدم من التوكن
            var currentUser = _authService.GetCurrentUser();
            if (currentUser == null)
            {
                // لو مش مسجل دخول
                return RedirectToAction("Index", "Login");
            }

            // جلب كل الحجوزات
            var allBookings = await _reservationService.GetAllAsync();

            // فلترة الحجوزات للمستخدم الحالي
            var userBookings = allBookings?
                .Where(b => b.UserId == currentUser.Id)
                .OrderByDescending(b => b.From)
                .ToList();

            // احصائيات
            int total = userBookings?.Count ?? 0;
            int completed = userBookings?.Count(b => b.Status == 1) ?? 0;
            int upcoming = userBookings?.Count(b => b.Status != 1) ?? 0;

            // تجهيز ViewModel
            var vm = new ProfileViewModel
            {
                CurrentUser = currentUser,
                Bookings = userBookings,
                TotalBookings = total,
                Completed = completed,
                Upcoming = upcoming
            };

            return View(vm);
        }
    }
}
