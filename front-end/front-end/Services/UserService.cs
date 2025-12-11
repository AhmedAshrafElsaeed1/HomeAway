using front_end.Interfaces;
using front_end.DTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace front_end.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpClientFactory _clientFactory;

        public UserService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<GetPaymentDto> GetPaymentAsync(string userId)
        {
            var client = _clientFactory.CreateClient("HomeAwayAPI");
            var response = await client.GetAsync($"Users/GetPayment?userId={userId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<GetPaymentDto>();
            }
            return null; // لو مفيش بيانات
        }

        public async Task<bool> SetPaymentAsync(PaymentDto payment)
        {
            var client = _clientFactory.CreateClient("HomeAwayAPI");
            var response = await client.PostAsJsonAsync("Users/SetPayment", payment);
            return response.IsSuccessStatusCode;
        }
    }
}
