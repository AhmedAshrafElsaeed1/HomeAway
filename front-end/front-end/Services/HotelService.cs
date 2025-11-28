using front_end.Interfaces;
using HomeAway.DTOs;
using Microsoft.Extensions.Hosting;

namespace front_end.Services
{
    public class HotelService : IHotelService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public HotelService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<IEnumerable<HotelDto>> GetAllAsync()
        {
            var url = $"{_config["ApiBaseUrl"]}/api/hotels";

            var result = await _httpClient.GetFromJsonAsync<IEnumerable<HotelDto>>(url);

            return result ?? Enumerable.Empty<HotelDto>();
        }

        public async Task<HotelDto?> GetByIdAsync(int id)
        {
            var url = $"{_config["ApiBaseUrl"]}/api/hotels/{id}";

            return await _httpClient.GetFromJsonAsync<HotelDto>(url);
        }

        public async Task<int?> CreateAsync(HotelDto dto)
        {
            var url = $"{_config["ApiBaseUrl"]}/api/hotels";

            var response = await _httpClient.PostAsJsonAsync(url, dto);

            if (!response.IsSuccessStatusCode)
                return null;

            var locationHeader = response.Headers.Location?.ToString();
            if (locationHeader == null)
                return null;

            // Extract ID from URL: /api/hotels/5
            var idSegment = locationHeader.Split('/').Last();
            return int.Parse(idSegment);
        }

        public async Task<bool> UpdateAsync(HotelDto dto)
        {
            var url = $"{_config["ApiBaseUrl"]}/api/hotels";

            var response = await _httpClient.PutAsJsonAsync(url, dto);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var url = $"{_config["ApiBaseUrl"]}/api/hotels/{id}";

            var response = await _httpClient.DeleteAsync(url);

            return response.IsSuccessStatusCode;
        }
    }

}
