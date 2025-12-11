using front_end.Auth;
using front_end.DTOs;

namespace front_end.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterUserAsync(RegisterDto dto);
        Task<bool> RegisterProviderAsync(RegisterDto dto);

        Task<string?> LoginAsync(LoginDto dto);

        Task LogoutAsync();
        Task<bool> IsSignedInAsync();
        void StoreToken(string token);

        // 🔥 New
        UserDto? GetCurrentUser();
    }
}
