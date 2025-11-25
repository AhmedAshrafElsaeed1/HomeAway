using home_away.Interfaces;
using HomeAway.DTOs;

public class HotelService : IHotelService
{
    private readonly HttpClient _client;

    public HotelService(IHttpClientFactory httpClientFactory)
    {
        _client = httpClientFactory.CreateClient("HomeAwayAPI");

    }

    public async Task<List<HotelDto>> GetAllAsync()
    {
        var response = await _client.GetAsync("hotels");
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<List<HotelDto>>();
    }

    public async Task<HotelDto> GetByIdAsync(int id)
    {
        var response = await _client.GetAsync($"hotels/{id}");
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<HotelDto>();
    }

    public async Task CreateAsync(HotelDto dto)
    {
        var response = await _client.PostAsJsonAsync("hotels", dto);
        response.EnsureSuccessStatusCode();
    }
}
