using HomeAway.Models;
using System.Net.Http.Json; // يجب إضافة هذا لتمكين ReadFromJsonAsync و PostAsJsonAsync

namespace HomeAway.Service
{
    public class HotelService
    {
        private readonly HttpClient httpClient;

        public HotelService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            // ملاحظة: يُفترض أن BaseAddress تم تكوينه في Program.cs (كما ذكرنا سابقاً)
        }

        // 1. القراءة - جلب الكل (Read All)
        public async Task<List<Hotel>> GetAll()
        {
            var response = await httpClient.GetAsync("/api/Hotels");
            response.EnsureSuccessStatusCode(); // تأكد من أن الحالة هي 2xx
            return await response.Content.ReadFromJsonAsync<List<Hotel>>();
        }

        // 2. القراءة - جلب بالمعرف (Read By ID)
        public async Task<Hotel> GetById(int id)
        {
            var response = await httpClient.GetAsync($"/api/Hotels/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Hotel>();
        }

        // **3. الإنشاء - إضافة فندق جديد (Create - Add)**
        // تستخدم PostAsJsonAsync لإرسال كائن Hotel في جسم الطلب
        public async Task Add(Hotel hotel)
        {
            var response = await httpClient.PostAsJsonAsync("/api/Hotels", hotel);
            // استخدم EnsureSuccessStatusCode لرمي استثناء إذا لم يكن الرد ناجحاً
            response.EnsureSuccessStatusCode();
            // يمكن أيضاً إرجاع الكائن المُضاف إذا كان الـ API يرجع كائناً
        }

        // **4. التحديث - تحديث بيانات فندق (Update)**
        // تستخدم PutAsJsonAsync لإرسال كائن Hotel المحدث
        public async Task Update(Hotel hotel)
        {
            // يُفترض أن الـ API يقبل الـ ID داخل الـ Body أو كجزء من الرابط (سنستخدم الرابط هنا)
            var response = await httpClient.PutAsJsonAsync($"/api/Hotels/{hotel.Id}", hotel);
            response.EnsureSuccessStatusCode();
        }
        public async Task<List<Booking>> GetBookingsByHotelId(int hotelId)
        {
            // يُفترض أن الـ API الخاص بك يحتوي على مسار (Endpoint) 
            // مثل /api/Hotels/{hotelId}/bookings
            var response = await httpClient.GetAsync($"/api/Hotels/{hotelId}/Bookings");

            // تحقق من أن الاستجابة كانت ناجحة
            response.EnsureSuccessStatusCode();

            // قراءة محتوى الاستجابة وتحويله إلى قائمة من كائنات Booking
            return await response.Content.ReadFromJsonAsync<List<Booking>>();
        }

        // **5. الحذف - حذف فندق (Delete)**
        public async Task Delete(int id)
        {
            var response = await httpClient.DeleteAsync($"/api/Hotels/{id}");
            response.EnsureSuccessStatusCode();
        }

        // 6. البحث بالاسم (وظيفة إضافية كما في الصورة)
        // يُفترض أن الـ API يحتوي على Endpoint للبحث بالاسم مثل /api/Hotels/byname?name=...
        public async Task<Hotel> GetByName(string name)
        {
            var response = await httpClient.GetAsync($"/api/Hotels/byname?name={name}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Hotel>();
        }
    }
}