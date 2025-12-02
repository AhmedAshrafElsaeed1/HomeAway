using front_end.DTOs;
using front_end.Interfaces;
using front_end.ViewModel;
using Microsoft.AspNetCore.Mvc;


namespace HotelsMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IReservationService _reservationService;

        public AdminController(IAdminService adminService, IReservationService reservationService)
        {
            _adminService = adminService;
            _reservationService = reservationService;
        }

        public async Task<IActionResult> Dashboard()
        {
            var reservations = await _reservationService.GetAllAsync();

            var model = new AdminViewModel
            {
                Reservations = reservations ?? new List<ReservationDto>(),
                // لو عندك Hotels:
                // Hotels = await _hotelService.GetAllAsync() ?? new List<HotelDto>()
            };

            return View("Dashboard", model); // تحدد اسم الفيو صراحة
        }

    }
}

