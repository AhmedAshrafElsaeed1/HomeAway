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
        private readonly IRoomService _roomService;

        public AdminController
            (IAdminService adminService,
            IReservationService reservationService
            , IHotelService hotelService,
            IRoomService roomService
            )
        {
            _adminService = adminService;
            _reservationService = reservationService;
            _hotelService = hotelService;
            _roomService = roomService;
        }

        public async Task<IActionResult> Dashboard()
        {
            try
            {
                var reservations = await _reservationService.GetAllAsync();
                var hotels = await _hotelService.GetAllAsync();
                var users = await _adminService.GetAllUsersAsync();
                decimal totalRevenue = await _adminService.HomeAwayProfit();
                var Rooms = await _roomService.GetAllAsync();

                var model = new AdminViewModel
                {
                    Reservations = reservations ?? new List<ReservationDto>(),
                    Hotels = hotels ?? new List<HotelDto>(),
                    Users = users ?? new List<UserDto>(),
                    TotalRevenue = totalRevenue,
                    Rooms = Rooms

                };

                return View("Dashboard", model); // تحدد اسم الفيو صراحة
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }

        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var reservations = await _reservationService.GetAllAsync();
                var hotels = await _hotelService.GetAllAsync();
                var users = await _adminService.GetAllUsersAsync();
                var Rooms = await _roomService.GetAllAsync();
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }

        }
        public async Task<IActionResult> MakeAdmin(string id)
        {
            try
            {
                if (await _adminService.PromoteUserToAdminAsync(id))
                {
                    return RedirectToAction("Index", "Users");
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }

    }
}

