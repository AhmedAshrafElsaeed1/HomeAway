using front_end.DTOs;
using front_end.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace front_end.Controllers
{
    public class ReservationsController : Controller     //  dont use for now !!!!!!!!!!
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<IActionResult> Index()
        {
            var reservations = await _reservationService.GetAllAsync();
            return View(reservations);
        }

        public async Task<IActionResult> Details(int id)
        {
            var reservation = await _reservationService.GetByIdAsync(id);
            return reservation == null ? NotFound() : View(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReservationDto dto)
        {
            var result = await _reservationService.CreateAsync(dto);
            if (!result)
                return BadRequest();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, ReservationDto dto)
        {
            var result = await _reservationService.UpdateAsync(id, dto);
            return result ? RedirectToAction("Index") : NotFound();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _reservationService.DeleteAsync(id);
            return result ? RedirectToAction("Index") : NotFound();
        }
    }
}
