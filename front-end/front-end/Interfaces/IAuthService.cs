using HomeAway.Auth;

namespace front_end.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDto dto);
        Task<string?> LoginAsync(LoginDto dto); // returns token or null on failure
        Task LogoutAsync(); // removes token from storage
        Task<bool> IsSignedInAsync();
    }
}
