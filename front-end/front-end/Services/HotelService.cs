using front_end.Interfaces;
using HomeAway.DTOs;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace front_end.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHttpClientFactory _clientFactory;

        public HotelService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<List<HotelDto>> GetAllAsync()
        {
            var client = _clientFactory.CreateClient("HomeAwayAPI");
            return await client.GetFromJsonAsync<List<HotelDto>>("hotels")
                   ?? new List<HotelDto>();
        }

        public async Task<HotelDto?> GetByIdAsync(int id)
        {
            var client = _clientFactory.CreateClient("HomeAwayAPI");
            return await client.GetFromJsonAsync<HotelDto>($"hotels/{id}");
        }

        public async Task<int?> CreateAsync(HotelDto dto)
        {
            var client = _clientFactory.CreateClient("HomeAwayAPI");
            var response = await client.PostAsJsonAsync("hotels", dto);

            if (!response.IsSuccessStatusCode)
                return null;

            var createdObj = await response.Content.ReadFromJsonAsync<dynamic>();
            return (int?)createdObj?.id;
        }

        public async Task<bool> UpdateAsync(HotelDto dto)
        {
            var client = _clientFactory.CreateClient("HomeAwayAPI");
            var response = await client.PutAsJsonAsync("hotels", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var client = _clientFactory.CreateClient("HomeAwayAPI");
            var response = await client.DeleteAsync($"hotels/{id}");
            return response.IsSuccessStatusCode;
        }
    }

}
