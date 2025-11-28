using front_end.Interfaces;
using HomeAway.Application.DTOs;

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

        public async Task<IEnumerable<ReservationDto>> GetAllAsync()
        {
            var url = $"{_config["ApiBaseUrl"]}/api/reservations";

            var reservations = await _httpClient.GetFromJsonAsync<IEnumerable<ReservationDto>>(url);

            return reservations ?? Enumerable.Empty<ReservationDto>();
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
