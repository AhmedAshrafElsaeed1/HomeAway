using Microsoft.AspNetCore.Mvc;
using HomeAway.Service;
using HomeAway.Models;
using System.Threading.Tasks;

namespace HomeAway.FrontEnd.Controllers
{
    public class ReserveController : Controller
    {
        private readonly HotelService _hotelService;
        private readonly BookingService _bookingService; // سنفترض وجود خدمة حجز

        public ReserveController(HotelService hotelService, BookingService bookingService)
        {
            _hotelService = hotelService;
            _bookingService = bookingService;
        }

        //// دالة عرض تفاصيل الفندق والغرف المتاحة
        //public async Task<IActionResult> Details(int id)
        //{
        //    Hotel hotelDetails = await _hotelService.GetById(id);

        //    if (hotelDetails == null)
        //    {
        //        return NotFound();
        //    }

        //    // يُفترض أن موديل Hotel يحتوي على قائمة الغرف (Rooms) أو نستخدم خدمة أخرى لجلبها
        //    return View(hotelDetails);
        //}

        //// دالة معالجة طلب الحجز (زر "Book Now")
        //[HttpPost]
        //public async Task<IActionResult> Book(BookingModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // **ملاحظة:** هنا يجب أن تقوم بتحويل بيانات الموديل إلى موديل الحجز (Booking) 
        //        // ومن ثم استخدام _bookingService.Add(bookingObject)

        //        // هنا يتم إرسال طلب إلى الـ API لإنشاء الحجز
        //        // await _bookingService.Add(model); 

        //        // إعادة توجيه المستخدم إلى صفحة التأكيد أو صفحة الحجوزات الخاصة به
        //        return RedirectToAction("MyBookings", "Profile");
        //    }

        //    // العودة إلى صفحة التفاصيل إذا كان هناك خطأ في الإدخال
        //    return View("Details", await _hotelService.GetById(model.HotelId));
        //}
    }

    // موديل افتراضي لاستقبال بيانات الحجز من الفورم
    public class BookingModel
    {
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int Guests { get; set; }
        // ... خصائص أخرى ...
    }
}