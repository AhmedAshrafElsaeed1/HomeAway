using front_end.DTOs;
using front_end.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace front_end.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<IActionResult> AdminList()
        {
            try
            {
                var reservations = await _reservationService.GetAllAsync();
                return View(reservations);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }

        }

        public async Task<IActionResult> Accept(int id)
        {
            try
            {
                var reservation = await _reservationService.GetByIdAsync(id);
                if (reservation == null)
                    return NotFound();

                reservation.Status = 1; // confirmed

                var updateDto = new UpdateResrvationDto
                {
                    Id = reservation.Id,

                    From = reservation.From,
                    To = reservation.To,
                    Status = reservation.Status,

                };

                await _reservationService.UpdateAsync(updateDto);
                return RedirectToAction("Dashboard", "Admin");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }

        }

        public async Task<IActionResult> Reject(int id)
        {
            try
            {
                var reservation = await _reservationService.GetByIdAsync(id);
                if (reservation == null)
                    return NotFound();

                reservation.Status = 2; // canceled

                var updateDto = new UpdateResrvationDto
                {
                    Id = reservation.Id,

                    From = reservation.From,
                    To = reservation.To,
                    Status = reservation.Status,

                };

                await _reservationService.UpdateAsync(updateDto);
                return RedirectToAction("Dashboard", "Admin");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }

        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var reservation = await _reservationService.GetByIdAsync(id);
                if (reservation == null)
                    return NotFound();

                return View(reservation);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }

        }
    }
}

