using front_end.DTOs;
using front_end.Interfaces;
using front_end.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace front_end.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IUserService _paymentService;
        private readonly IReservationService _reservationService;
        private readonly IAuthService _authService;

        public PaymentController(
            IUserService paymentService,
            IReservationService reservationService,
            IAuthService authService)
        {
            _paymentService = paymentService;
            _reservationService = reservationService;
            _authService = authService;
        }

        // GET: صفحة الدفع
        public IActionResult Index(PaymentViewModel model)
        {
            var currentUser = _authService.GetCurrentUser();
            if (currentUser == null)
                return RedirectToAction("Index", "Login");

            model.UserId = currentUser.Id;
            return View(model);
        }

        // POST: تأكيد الدفع
        [HttpPost]
        public async Task<IActionResult> Confirm(PaymentViewModel model)
        {
            try
            {
                var currentUser = _authService.GetCurrentUser();
                if (currentUser == null)
                    return RedirectToAction("Index", "Login");

                if (!currentUser.Role.Contains("User") && !currentUser.Role.Contains("Admin"))
                    return Forbid();

                model.UserId = currentUser.Id;

                // تحويل ViewModel إلى DTO للدفع
                var paymentDto = new PaymentDto
                {
                    UserId = model.UserId,
                    CardHolderName = model.CardHolderName,
                    CardNumber = model.CardNumber,
                    Expiry = model.Expiry,
                    CVV = model.CVV
                };

                bool paymentSuccess = await _paymentService.SetPaymentAsync(paymentDto);
                if (!paymentSuccess)
                {
                    ViewBag.Error = "Payment failed. Please try again.";
                    return View("Index", model);
                }

                // إنشاء الحجز بعد نجاح الدفع
                var reservationDto = new ReservationDto
                {
                    UserId = model.UserId,
                    RoomId = model.RoomId,
                    From = model.From,
                    To = model.To,
                    TotalPrice = model.TotalPrice
                };

                bool reservationSuccess = await _reservationService.CreateAsync(reservationDto);
                if (!reservationSuccess)
                {
                    ViewBag.Error = "Payment succeeded but reservation failed. Contact support.";
                    return View("Index", model);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }
    }
}
