using front_end.Auth;       // عشان RegisterDto
using front_end.Interfaces; // عشان IAuthService
using front_end.ViewModel;  // عشان RegisterViewModel
using Microsoft.AspNetCore.Mvc;

namespace front_end.Controllers
{
    public class SignupController : Controller
    {
        private readonly IAuthService _authService;

        public SignupController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        // GET: عرض صفحة التسجيل للعميل
        [HttpGet]
        public IActionResult AsCustomer()
        {
            return View(new RegisterViewModel());
        }

        // POST: معالجة تسجيل العميل
        [HttpPost]
        public async Task<IActionResult> RegisterCustomer(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                // لو الـ Validation فشل، نرجع نفس الصفحة مع البيانات المدخلة
                return View("AsCustomer", vm);
            }

            // تحويل البيانات من ViewModel لـ DTO
            var dto = new RegisterDto
            {
                FullName = vm.FullName,
                UserName = vm.UserName,
                Email = vm.Email,
                Password = vm.Password
            };

            // استدعاء خدمة المصادقة
            var success = await _authService.RegisterUserAsync(dto);

            if (!success)
            {
                ModelState.AddModelError("", "Registration failed. Please try again.");
                return View("AsCustomer", vm);
            }

            // إذا نجحت العملية، نروح لصفحة تسجيل الدخول
            return RedirectToAction("Index", "Login");
        }

        // GET: صفحة التسجيل لمالك الفندق
        [HttpGet]
        public IActionResult AsHotelOwner()
        {
            return View(new RegisterViewModel());
        }

        // POST: معالجة تسجيل مالك الفندق
        [HttpPost]
        public async Task<IActionResult> RegisterHotelOwner(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("AsHotelOwner", vm);
            }

            var dto = new RegisterDto
            {
                FullName = vm.FullName,
                UserName = vm.UserName,
                Email = vm.Email,
                Password = vm.Password
            };

            var success = await _authService.RegisterProviderAsync(dto);

            if (!success)
            {
                ModelState.AddModelError("", "Registration failed. Please try again.");
                return View("AsHotelOwner", vm);
            }

            return RedirectToAction("Index", "Signup");
        }
    }
}
