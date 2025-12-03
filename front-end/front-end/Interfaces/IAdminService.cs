using front_end.DTOs;
using front_end.Auth;

namespace front_end.Interfaces
{
    public interface IAdminService
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task<bool> RegisterAdminAsync(RegisterDto dto);
        Task<decimal> HomeAwayProfit();
    }
}
