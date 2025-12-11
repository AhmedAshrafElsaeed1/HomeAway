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
        private readonly IHotelService _hotelService;

        public SignupController(IAuthService authService, IHotelService hotelService)
        {
            _authService = authService;
            _hotelService = hotelService;
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

        //[HttpPost]
        //public IActionResult RegisterProvider(AsHotelOwnerViewModel vm)
        //{
        //    if (!ModelState.IsValid)
        //        return View("AsHotelOwner", vm);

        //    // حفظ البيانات مؤقتًا
        //    TempData["FullName"] = vm.OwnerName;
        //    TempData["UserName"] = vm.UserName;
        //    TempData["Email"] = vm.Email;
        //    TempData["Password"] = vm.Password;

        //    return RedirectToAction("RegisterHotel");
        //}




        // ======================= STEP 2: HOTEL INFO ==========================

        [HttpGet]
        public IActionResult RegisterHotel()
        {
            return View();   // Views/Signup/RegisterHotel.cshtml
        }

        [HttpPost]
        public async Task<IActionResult> RegisterHotelinDB(HotelRegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);


            var imagesBase64 = await ConvertImagesToBase64(model.Photos);

            var providerDto = new HotelDto
            {
                Name = model.HotelName,
                Description = model.Description,
                Address = model.HotelAddress,
                Email = model.Email,
                PhoneNumber = model.Phone,
                images = imagesBase64,
                Rating = model.Rating

            };

            var created = await _hotelService.CreateAsync(providerDto);

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
        public async Task<IActionResult> RegisterProvider(AsHotelOwnerViewModel hotelOwner)
        {
            var providerDto = new RegisterDto
            {
                FullName = hotelOwner.FullName,
                UserName = hotelOwner.UserName,
                Email = hotelOwner.Email,
                Password = hotelOwner.Password
            };

            var created = await _authService.RegisterProviderAsync(providerDto);

            if (!created)
            {
                ViewBag.Error = "Provider registration failed.";
                return RedirectToAction("AsHotelOwner");
            }

            // Auto Login
            var p = await _authService.LoginAsync(new LoginDto
            {
                UserName = hotelOwner.UserName,
                Password = hotelOwner.Password

            });
            _authService.StoreToken(p);
            return RedirectToAction("RegisterHotel", "Signup");
        }
        private async Task<string[]> ConvertImagesToBase64(List<IFormFile> photos)
        {
            if (photos == null || !photos.Any())
                return Array.Empty<string>();

            List<string> base64Images = new List<string>();

            foreach (var photo in photos)
            {
                using var ms = new MemoryStream();
                await photo.CopyToAsync(ms);
                var bytes = ms.ToArray();
                string base64 = Convert.ToBase64String(bytes);

                // Add prefix to allow `<img src="data:image...">`
                string formatted = $"data:{photo.ContentType};base64,{base64}";

                base64Images.Add(formatted);
            }

            return base64Images.ToArray();
        }

    }
}
