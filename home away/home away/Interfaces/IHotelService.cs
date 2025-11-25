using HomeAway.DTOs;

namespace home_away.Interfaces
{
    public interface IHotelService
    {
        Task<List<HotelDto>> GetAllAsync();

        Task<HotelDto> GetByIdAsync(int id);

        Task CreateAsync(HotelDto dto);
    }
}
