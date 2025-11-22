using Microsoft.AspNetCore.Mvc;
using HomeAway.Service;
using HomeAway.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAway.FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly HotelService _hotelService;

        // حقن خدمة الفنادق
        public HomeController(HotelService hotelService)
        {
            _hotelService = hotelService;
        }

        // دالة الصفحة الرئيسية - لعرض الفنادق المميزة
        public async Task<IActionResult> Index()
        {
            // جلب قائمة الفنادق (أو الفنادق المميزة بشكل محدد إذا كان الـ API يدعم ذلك)
            // سنفترض هنا جلب أول 4 فنادق كمثال للفنادق المميزة
            List<Hotel> featuredHotels = await _hotelService.GetAll();

            // يمكنك تمرير هذه القائمة إلى الـ View لعرضها في قسم "Featured Hotels"
            return View(featuredHotels?.Take(4).ToList());
            // إذا كان الـ View يتوقع موديل من نوع List<Hotel>
        }
    }
}

