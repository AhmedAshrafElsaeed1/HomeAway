using front_end.DTOs;
using front_end.Interfaces;
using front_end.Auth;

namespace front_end.Services
{
    public class AdminService : IAdminService
    {
        private readonly HttpClient _httpClient;

        public AdminService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("HomeAwayAPI");
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var response = await _httpClient.GetAsync("Admin/GetAllUsers");

            if (!response.IsSuccessStatusCode)
                return new List<UserDto>();

            List<UserDto> users = await response.Content.ReadFromJsonAsync<List<UserDto>>();
            return users ?? new List<UserDto>();
        }

        public async Task<bool> RegisterAdminAsync(RegisterDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("Admin/register", dto);

            return response.IsSuccessStatusCode;
        }
        public async Task<decimal> HomeAwayProfit()
        {
            var response = await _httpClient.GetAsync("Admin/profit");

            if (!response.IsSuccessStatusCode)
                return -1;

            Decimal profit = await response.Content.ReadFromJsonAsync<Decimal>();
            return profit;
        }
        public async Task<bool> DeleteUserAsync(String id)
        {
            var response = await _httpClient.DeleteAsync($"Admin/{id}");
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> PromoteUserToAdminAsync(String id)
        {
            var response = await _httpClient.PostAsync($"Admin/Promote/{id}", null);
            return response.IsSuccessStatusCode;
        }

    }
}