using HomeAway.Application.DTOs;

namespace front_end.Interfaces
{
    public interface IReservationService
    {
        Task<List<ReservationDto>> GetAllAsync();
        Task<ReservationDto?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ReservationDto dto);
        Task<bool> UpdateAsync(int id, ReservationDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
