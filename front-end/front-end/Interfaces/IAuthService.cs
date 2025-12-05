using front_end.Auth;

namespace front_end.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterUserAsync(RegisterDto dto);
        Task<bool> RegisterProviderAsync(RegisterDto dto);
        Task<string?> LoginAsync(LoginDto dto); // returns token or null on failure
        Task LogoutAsync(); // removes token from storage
        Task<bool> IsSignedInAsync();
    }
}
