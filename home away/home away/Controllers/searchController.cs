using Microsoft.AspNetCore.Mvc;
using HomeAway.Service;
using HomeAway.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAway.FrontEnd.Controllers
{
    public class SearchController : Controller
    {
        private readonly HotelService _hotelService;

        public SearchController(HotelService hotelService)
        {
            _hotelService = hotelService;
        }

        // دالة نتائج البحث
        //public async Task<IActionResult> Results(
        //    string location,
        //    DateTime checkIn,
        //    DateTime checkOut,
        //    int guests)
        //{
        //    // **ملاحظة:** هنا يجب أن يكون لديك دالة في HotelService 
        //    // لاستقبال معايير البحث المعقدة (الموقع، التواريخ، الضيوف). 
        //    // سنستخدم GetByName مؤقتاً لمحاكاة البحث بالموقع.

        //    List<Hotel> searchResults;

        //    // إذا كان الـ API الخاص بك يدعم البحث المتقدم
        //    // searchResults = await _hotelService.Search(location, checkIn, checkOut, guests);

        //    // كحل مؤقت، سنقوم بجلب الكل ثم التصفية (يجب تفضيل التصفية من الـ API)
        //    var allHotels = await _hotelService.GetAll();
        //    searchResults = allHotels.Where(h => h.City == location).ToList(); // تصفية وهمية

        //    // يمكن وضع معايير البحث في ViewBag أو موديل خاص
        //    ViewBag.SearchLocation = location;
        //    ViewBag.Count = searchResults.Count;

        //    return View(searchResults);
        //}
    }
}

