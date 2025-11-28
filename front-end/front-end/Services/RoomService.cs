using front_end.Interfaces;
using HomeAway.Application.DTOs;

namespace front_end.Services
{
    public class RoomService : IRoomService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public RoomService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<IEnumerable<RoomDto>> GetAllAsync()
        {
            var url = $"{_config["ApiBaseUrl"]}/api/rooms";

            var result = await _httpClient.GetFromJsonAsync<IEnumerable<RoomDto>>(url);

            return result ?? Enumerable.Empty<RoomDto>();
        }

        public async Task<RoomDto?> GetByIdAsync(int id)
        {
            var url = $"{_config["ApiBaseUrl"]}/api/rooms/{id}";

            return await _httpClient.GetFromJsonAsync<RoomDto>(url);
        }

        public async Task<int?> CreateAsync(RoomDto dto)
        {
            var url = $"{_config["ApiBaseUrl"]}/api/rooms";

            var response = await _httpClient.PostAsJsonAsync(url, dto);

            if (!response.IsSuccessStatusCode)
                return null;

            var locationHeader = response.Headers.Location?.ToString();
            if (locationHeader == null)
                return null;

            return int.Parse(locationHeader.Split('/').Last());
        }

        public async Task<bool> UpdateAsync(RoomDto dto)
        {
            var url = $"{_config["ApiBaseUrl"]}/api/rooms";

            var response = await _httpClient.PutAsJsonAsync(url, dto);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var url = $"{_config["ApiBaseUrl"]}/api/rooms/{id}";

            var response = await _httpClient.DeleteAsync(url);

            return response.IsSuccessStatusCode;
        }
    }

}
