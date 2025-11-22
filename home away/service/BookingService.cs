using HomeAway.Models;
using System.Net.Http.Json;

namespace HomeAway.Service
{
    public class BookingService
    {
        private readonly HttpClient httpClient;

        public BookingService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        // ✔ جلب الحجوزات حسب المستخدم
        public async Task<List<Booking>> GetBookingsByUserId(int userId)
        {
            var response = await httpClient.GetAsync($"/api/Booking/user/{userId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Booking>>();
        }

        // ✔ جلب حجز واحد
        public async Task<Booking> GetById(int id)
        {
            var response = await httpClient.GetAsync($"/api/Booking/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Booking>();
        }

        // ✔ إضافة حجز جديد
        public async Task Add(Booking booking)
        {
            var response = await httpClient.PostAsJsonAsync("/api/Booking", booking);
            response.EnsureSuccessStatusCode();
        }

        // ✔ تحديث حجز — تم تعديل Booking.Id → Booking.BookingId
        public async Task Update(Booking booking)
        {
            var response = await httpClient.PutAsJsonAsync($"/api/Booking/{booking.BookingId}", booking);
            response.EnsureSuccessStatusCode();
        }

        // ✔ حذف حجز — استخدمنا BookingId
        public async Task Delete(int bookingId)
        {
            var response = await httpClient.DeleteAsync($"/api/Booking/{bookingId}");
            response.EnsureSuccessStatusCode();
        }
    }
}