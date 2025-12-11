using front_end.Auth;
using front_end.DTOs;
using front_end.Interfaces;
using front_end.ViewModel;
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

        // =============================== INDEX ===============================
        public IActionResult Index()
        {
            return View();   // Views/Signup/Index.cshtml
        }

        // ====================================================================
        // ============================ CUSTOMER ===============================
        // ====================================================================

        [HttpGet]
        public IActionResult AsCustomer()
        {
            return View();   // Views/Signup/AsCustomer.cshtml
        }

        [HttpPost]
        public async Task<IActionResult> AsCustomer(RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var created = await _authService.RegisterUserAsync(dto);

            if (!created)
            {
                ViewBag.Error = "Registration failed.";
                return View(dto);
            }

            // Auto login
            await _authService.LoginAsync(new LoginDto
            {
                UserName = dto.UserName,
                Password = dto.Password
            });

            return RedirectToAction("Index", "Home");
        }


        // ====================================================================
        // ============================ PROVIDER ===============================
        // ====================================================================

        // ======================= STEP 1: USER INFO ==========================

        [HttpGet]
        public IActionResult AsHotelOwner()
        {
            return View();   // Views/Signup/AsHotelOwner.cshtml
        }

        [HttpPost]
        public IActionResult RegisterProvider(AsHotelOwnerViewModel vm)
        {
            if (!ModelState.IsValid)
                return View("AsHotelOwner", vm);

            // حفظ البيانات مؤقتًا
            TempData["FullName"] = vm.OwnerName;
            TempData["UserName"] = vm.UserName;
            TempData["Email"] = vm.Email;
            TempData["Password"] = vm.Password;

            return RedirectToAction("RegisterHotel");
        }




        // ======================= STEP 2: HOTEL INFO ==========================

        [HttpGet]
        public IActionResult RegisterHotel()
        {
            return View();   // Views/Signup/RegisterHotel.cshtml
        }

        [HttpPost]
        public IActionResult RegisterHotel(HotelRegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            TempData["HotelName"] = model.HotelName;
            TempData["Address"] = model.HotelAddress;
            TempData["Description"] = model.Description;

            return RedirectToAction("Rooms");
        }


        // ======================= STEP 3: ROOMS INFO ==========================

        [HttpGet]
        public IActionResult Rooms()
        {
            return View();   // Views/Signup/Rooms.cshtml
        }

        [HttpPost]
        public IActionResult Rooms(CreateRoomViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            

            return RedirectToAction("SubmitProvider");
        }


        // ======================= STEP 4: SUBMIT ALL ==========================

        [HttpGet]
        public IActionResult SubmitProvider()
        {
            return View();   // Views/Signup/SubmitProvider.cshtml
        }

        [HttpPost]
        public async Task<IActionResult> RegisterProvider()
        {
            //Read TempData
            var fullName = TempData["FullName"]?.ToString();
            var userName = TempData["UserName"]?.ToString();
            var email = TempData["Email"]?.ToString();
            var password = TempData["Password"]?.ToString();

            if (fullName == null)
            {
                ViewBag.Error = "Session expired, please try again.";
                return RedirectToAction("AsHotelOwner");
            }

            var providerDto = new RegisterDto
            {
                FullName = fullName,
                UserName = userName,
                Email = email,
                Password = password
            };

            var created = await _authService.RegisterProviderAsync(providerDto);

            if (!created)
            {
                ViewBag.Error = "Provider registration failed.";
                return RedirectToAction("AsHotelOwner");
            }

            // Auto Login
            await _authService.LoginAsync(new LoginDto
            {
                UserName = userName,
                Password = password
            });

            return RedirectToAction("Index", "Home");
        }
    }
}
