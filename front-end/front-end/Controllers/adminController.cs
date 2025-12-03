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
        private readonly IHotelService _hotelService;


        public AdminController(IAdminService adminService, IReservationService reservationService, IHotelService hotelService)
        {
            _adminService = adminService;
            _reservationService = reservationService;
            _hotelService = hotelService;
        }

        public async Task<IActionResult> Dashboard()
        {
            var reservations = await _reservationService.GetAllAsync();
            var hotels = await _hotelService.GetAllAsync();
            var users = await _adminService.GetAllUsersAsync();
            decimal totalRevenue = await _adminService.HomeAwayProfit();

            var model = new AdminViewModel
            {
                Reservations = reservations ?? new List<ReservationDto>(),
                Hotels = hotels ?? new List<HotelDto>(),
                Users = users ?? new List<UserDto>(),
                TotalRevenue = totalRevenue,

            };

            return View("Dashboard", model); // تحدد اسم الفيو صراحة
        }

    }
}

