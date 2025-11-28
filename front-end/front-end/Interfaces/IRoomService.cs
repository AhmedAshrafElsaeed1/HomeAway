using HomeAway.Application.DTOs;

namespace front_end.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDto>> GetAllAsync();
        Task<RoomDto?> GetByIdAsync(int id);
        Task<int?> CreateAsync(RoomDto dto);
        Task<bool> UpdateAsync(RoomDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
