using front_end.DTOs;
using front_end.Interfaces;

namespace front_end.Services
{
    public class ReservationService : IReservationService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public ReservationService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<List<ReservationDto>> GetAllAsync()
        {
            var url = $"{_config["ApiBaseUrl"]}/api/reservations";

            var reservations = await _httpClient.GetFromJsonAsync<List<ReservationDto>>(url);

            return reservations ?? new List<ReservationDto>();
        }

        public async Task<ReservationDto?> GetByIdAsync(int id)
        {
            var url = $"{_config["ApiBaseUrl"]}/api/reservations/{id}";

            return await _httpClient.GetFromJsonAsync<ReservationDto>(url);
        }

        public async Task<bool> CreateAsync(ReservationDto dto)
        {
            var url = $"{_config["ApiBaseUrl"]}/api/reservations";

            var response = await _httpClient.PostAsJsonAsync(url, dto);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(int id, ReservationDto dto)
        {
            var url = $"{_config["ApiBaseUrl"]}/api/reservations/{id}";

            var response = await _httpClient.PutAsJsonAsync(url, dto);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var url = $"{_config["ApiBaseUrl"]}/api/reservations/{id}";

            var response = await _httpClient.DeleteAsync(url);

            return response.IsSuccessStatusCode;
        }
    }

}
