using front_end.Interfaces;
using front_end.DTOs;

namespace front_end.Services
{
    public class RoomService : IRoomService
    {
        private readonly HttpClient _httpClient;

        public RoomService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("HomeAwayAPI");
        }

        public async Task<List<RoomDto>> GetAllAsync()
        {
            var Rooms = await _httpClient.GetFromJsonAsync<List<RoomDto>>("Rooms");
            return Rooms ?? new List<RoomDto>();
        
        }

        public async Task<RoomDto?> GetByIdAsync(int id)
        {
            var room = await _httpClient.GetFromJsonAsync<RoomDto>($"Rooms/{id}");
            return room;

        }

        public async Task<int?> CreateAsync(RoomDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("Rooms", dto);
            //var client = _clientFactory.CreateClient("HomeAwayAPI");
            //var response = await client.PostAsJsonAsync("rooms", dto);

            if (!response.IsSuccessStatusCode)
                return null;

            // API returns CreatedAtAction (no body) — so parse location header
            if (response.Headers.Location != null)
            {
                var segments = response.Headers.Location.Segments;
                if (int.TryParse(segments.Last(), out int newId))
                    return newId;
            }

            return null;
        }

        public async Task<bool> UpdateAsync(UpdateRoomDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"Rooms", dto);
            
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Rooms/{id}");
            return response.IsSuccessStatusCode;
        }
    }

}
