using Microsoft.AspNetCore.Mvc;
using HomeAway.Service;
using HomeAway.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAway.FrontEnd.Controllers
{
    public class ProfileController : Controller
    {
        private readonly BookingService _bookingService;   // ✔ تم تصحيح النوع
        private readonly HotelService _userService;

        public ProfileController(BookingService bookingService, HotelService userService)
        {
            _bookingService = bookingService;  // ✔ تم تصحيح الحقن
            _userService = userService;
        }

        // عرض الحجوزات الخاصة بالمستخدم
        public async Task<IActionResult> MyBookings()
        {
            int currentUserId = 1; // مؤقتاً لحد ما نربط الـ Login

            try
            {
                List<Booking> userBookings = await _bookingService.GetBookingsByUserId(currentUserId);

                var viewModel = new ProfileViewModel
                {
                    FullName = "John Doe",
                    Email = "john.doe@email.com",

                    Bookings = userBookings,
                    TotalBookingsCount = userBookings.Count,
                    CompletedStaysCount = userBookings.Count(b => b.Status == "Completed"),
                    UpcomingTripsCount = userBookings.Count(b => b.Status == "Confirmed")
                };

                return View(viewModel);
            }
            catch
            {
                ViewBag.Error = "تعذّر جلب البيانات.";
                return View(new ProfileViewModel());
            }
        }

        public IActionResult Settings()
        {
            return View();
        }

        public IActionResult Wishlist()
        {
            return View();
        }
    }

    public class ProfileViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<Booking> Bookings { get; set; }
        public int TotalBookingsCount { get; set; }
        public int CompletedStaysCount { get; set; }
        public int UpcomingTripsCount { get; set; }
    }
}