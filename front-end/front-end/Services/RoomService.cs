using front_end.Interfaces;
using front_end.DTOs;

namespace front_end.Services
{
    public class RoomService : IRoomService
    {
        private readonly IHttpClientFactory _clientFactory;

        public RoomService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<List<RoomDto>> GetAllAsync()
        {
            var client = _clientFactory.CreateClient("HomeAwayAPI");
            return await client.GetFromJsonAsync<List<RoomDto>>("rooms")
                   ?? new List<RoomDto>();
        }

        public async Task<RoomDto?> GetByIdAsync(int id)
        {
            var client = _clientFactory.CreateClient("HomeAwayAPI");
            return await client.GetFromJsonAsync<RoomDto>($"rooms/{id}");
        }

        public async Task<int?> CreateAsync(RoomDto dto)
        {
            var client = _clientFactory.CreateClient("HomeAwayAPI");
            var response = await client.PostAsJsonAsync("rooms", dto);

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
            var client = _clientFactory.CreateClient("HomeAwayAPI");
            var response = await client.PutAsJsonAsync("rooms", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var client = _clientFactory.CreateClient("HomeAwayAPI");
            var response = await client.DeleteAsync($"rooms/{id}");
            return response.IsSuccessStatusCode;
        }
    }

}
