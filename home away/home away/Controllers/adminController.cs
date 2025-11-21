using Microsoft.AspNetCore.Mvc;
using HomeAway.Service;
using HomeAway.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization; // ضروري لصفحات الإدارة

namespace HomeAway.FrontEnd.Controllers
{
    // [Authorize(Roles = "Admin")] // يجب أن يكون المستخدم مسؤولاً
    public class AdminController : Controller
    {
        private readonly HotelService _hotelService;
        private readonly AdminDashboardService _adminService; // خدمة لبيانات لوحة التحكم

        public AdminController(HotelService hotelService, AdminDashboardService adminService)
        {
            _hotelService = hotelService;
            _adminService = adminService;
        }

        // دالة لوحة التحكم الرئيسية (Dashboard)
        public async Task<IActionResult> Dashboard()
        {
            // **ملاحظة:** يجب أن تكون لديك دالة في خدمة الإدارة لجلب البيانات الإحصائية
            // DashboardData data = await _adminService.GetDashboardSummary();

            // **مثال مؤقت**: إرسال موديل بيانات وهمية
            var dashboardData = new AdminDashboardModel
            {
                TotalBookings = 156,
                Revenue = 45200m,
                PendingBookings = 42,
                CancelledBookings = 8,
                RecentBookings = new List<Booking>() // يجب جلب قائمة الحجوزات الأخيرة
            };

            return View(dashboardData);
        }

        // دالة لإدارة الغرف (CRUD)
        public IActionResult Rooms()
        {
            // يمكن استخدام _hotelService لجلب وقائمة الغرف وتعديلها
            return View();
        }

        // ... يمكن إضافة دوال لـ Bookings, Customers, Reports ...
    }

    // موديل افتراضي لبيانات لوحة التحكم
    public class AdminDashboardModel
    {
        public int TotalBookings { get; set; }
        public decimal Revenue { get; set; }
        public int PendingBookings { get; set; }
        public int CancelledBookings { get; set; }
        public List<Booking> RecentBookings { get; set; }
    }
}

