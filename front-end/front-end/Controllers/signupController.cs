using front_end.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace front_end.Controllers
{
    public class SignupController : Controller
    {
        // GET: الصفحة الرئيسية للـ signup (اختيار نوع الحساب)
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AsCustomer()
        {
            // ViewModel الخاص بالعميل
            return View(new RegisterViewModel());
        }

        [HttpGet]
        public IActionResult AsHotelOwner()
        {
            return View(new HotelOwnerRegisterViewModel());
        }

        // ====== صفحة تسجيل الفندق ======
        [HttpGet]
        public IActionResult RegisterHotel()
        {
            return View(new HotelRegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterHotel(HotelRegisterViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            // تحويل الصور لـ Base64
            var photosBase64 = new List<string>();

            if (vm.Photos != null && vm.Photos.Count > 0)
            {
                foreach (var photo in vm.Photos)
                {
                    if (photo.Length > 0)
                    {
                        using var ms = new MemoryStream();
                        await photo.CopyToAsync(ms);
                        var fileBytes = ms.ToArray();
                        var base64String = Convert.ToBase64String(fileBytes);
                        photosBase64.Add(base64String);
                    }
                }
            }

            // مثال: إنشاء DTO لإرسال البيانات للـ API
            var hotelDto = new RegisterHotelDto
            {
                HotelName = vm.HotelName,
                Description = vm.Description,
                HotelAddress = vm.HotelAddress,
                Email = vm.Email,
                Phone = vm.Phone,
                PhotosBase64 = photosBase64
            };

            // هنا ممكن تبعتي الـ DTO للـ API
            // await _hotelService.RegisterHotelAsync(hotelDto);

            // بعد التسجيل، نروح لصفحة الغرف
            return RedirectToAction("Rooms");
        }

        // ====== صفحة إدخال الغرف بعد تسجيل الفندق ======
        [HttpGet]
        public IActionResult Rooms()
        {
            return View();
        }
    }

    // ====== DTO مثال لإرسال البيانات للـ API ======
    public class RegisterHotelDto
    {
        public string HotelName { get; set; }
        public string Description { get; set; }
        public string HotelAddress { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<string> PhotosBase64 { get; set; }
    }
}
