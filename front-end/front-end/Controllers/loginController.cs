using front_end.Auth;        // LoginDto
using front_end.Interfaces;  // IAuthService
using front_end.ViewModel;   // LoginViewModel
using Microsoft.AspNetCore.Mvc;

namespace front_end.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthService _authService;

        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }

        // GET: عرض صفحة تسجيل الدخول
        [HttpGet]
        public IActionResult Index()
        {
            // نرجع الصفحة مع ViewModel فارغ
            return View(new ViewModel.LogInViewModel());
        }

        // POST: معالجة تسجيل الدخول
        [HttpPost]
        public async Task<IActionResult> Index(LogInViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                // لو فيه validation errors نرجع الصفحة مع البيانات المدخلة
                return View(vm);
            }

            try
            {
                // تحويل البيانات من ViewModel لـ DTO
                var loginDto = new LoginDto
                {
                    UserName = vm.UserName,
                    Password = vm.Password
                };

                // استدعاء خدمة المصادقة
                var token = await _authService.LoginAsync(loginDto);

                if (string.IsNullOrWhiteSpace(token))
                {
                    // في حالة خطأ في تسجيل الدخول
                    ViewBag.Error = "Invalid username or password";
                    return View(vm);
                }

                // تخزين الـ token يتم داخل الـ AuthService
                // إعادة التوجيه للصفحة الرئيسية بعد تسجيل الدخول بنجاح
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ في الـ console
                Console.WriteLine($"Login Error: {ex.Message}");
                ViewBag.Error = "An error occurred while logging in. Please try again.";
                return View(vm);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Index", "Home"); // بعد الخروج نرجع للصفحة الرئيسية
        }
    }
}
