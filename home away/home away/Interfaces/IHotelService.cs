using HomeAway.DTOs;

namespace home_away.Interfaces
{
    public interface IHotelService
    {
        Task<IEnumerable<HotelDto>> GetAllAsync();
        Task<HotelDto> GetByIdAsync(int id);
        Task<int> CreateAsync(HotelDto dto);
        Task<bool> UpdateAsync(HotelDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
