using front_end.DTOs;
using front_end.Interfaces;

namespace front_end.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IHttpClientFactory _clientFactory;

        public ReservationService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<List<ReservationDto>> GetAllAsync()
        {
            var client = _clientFactory.CreateClient("HomeAwayAPI");
            return await client.GetFromJsonAsync<List<ReservationDto>>("reservations")
                   ?? new List<ReservationDto>();
        }

        public async Task<ReservationDto?> GetByIdAsync(int id)
        {
            var client = _clientFactory.CreateClient("HomeAwayAPI");
            return await client.GetFromJsonAsync<ReservationDto>($"reservations/{id}");
        }

        public async Task<bool> CreateAsync(ReservationDto dto)
        {
            var client = _clientFactory.CreateClient("HomeAwayAPI");
            var response = await client.PostAsJsonAsync("reservations", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(int id, ReservationDto dto)
        {
            var client = _clientFactory.CreateClient("HomeAwayAPI");
            var response = await client.PutAsJsonAsync($"reservations/{id}", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var client = _clientFactory.CreateClient("HomeAwayAPI");
            var response = await client.DeleteAsync($"reservations/{id}");
            return response.IsSuccessStatusCode;
        }
    }

}
