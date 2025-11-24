using HomeAway.DTOs;

namespace home_away.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDto>> GetAllAsync();
        Task<RoomDto> GetRoomByIdAsync(int id);
        Task<int> CreateRoomAsync(RoomDto dto);
        Task<RoomDto> UpdateAsync(RoomDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
