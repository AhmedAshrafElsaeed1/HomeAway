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
            var reservations = await _reservationService.GetAllAsync();
            return View(reservations);
        }

        public async Task<IActionResult> Accept(int id)
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
            return RedirectToAction("Dashboard","Admin");
        }

        public async Task<IActionResult> Reject(int id)
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

        public async Task<IActionResult> Details(int id)
        {
            var reservation = await _reservationService.GetByIdAsync(id);
            if (reservation == null)
                return NotFound();

            return View(reservation);
        }
    }
}

