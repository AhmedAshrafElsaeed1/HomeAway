namespace front_end.Services
{
    using front_end.Interfaces;
    using front_end.Auth;
    using front_end.Helpers;
    using front_end.DTOs;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using System.Net.Http.Json;
    using System.Text.Json;

    public class AuthService : IAuthService
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;
        private const string CookieName = "HomeAwayJwt";

        public AuthService(
            IHttpClientFactory clientFactory,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration config)
        {
            _client = clientFactory.CreateClient("HomeAwayAPI");
            _httpContextAccessor = httpContextAccessor;
            _config = config;
        }

        public async Task<bool> RegisterUserAsync(RegisterDto dto)
        {
            var resp = await _client.PostAsJsonAsync("Users/register", dto);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> RegisterProviderAsync(RegisterDto dto)
        {
            var resp = await _client.PostAsJsonAsync("Providers/register", dto);
            return resp.IsSuccessStatusCode;
        }

        // ------------------ LOGIN ------------------
        public async Task<string?> LoginAsync(LoginDto dto)
        {
            var resp = await _client.PostAsJsonAsync("auth/login", dto);
            if (!resp.IsSuccessStatusCode)
                return null;

            var obj = await resp.Content.ReadFromJsonAsync<JsonElement>();

            string? token = null;

            if (obj.TryGetProperty("token", out var tokenObj) &&
                tokenObj.TryGetProperty("result", out var resultProp))
            {
                token = resultProp.GetString();
            }

            if (string.IsNullOrWhiteSpace(token))
                return null;

            // خزّن التوكن في كوكي
            StoreToken(token);

            return token;
        }

        // ------------------ LOGOUT ------------------
        public Task LogoutAsync()
        {
            var ctx = _httpContextAccessor.HttpContext;
            ctx?.Response.Cookies.Delete(CookieName);

            return Task.CompletedTask;
        }

        // ------------------ CHECK AUTH ------------------
        public Task<bool> IsSignedInAsync()
        {
            var ctx = _httpContextAccessor.HttpContext;
            if (ctx == null) return Task.FromResult(false);

            bool hasCookie = ctx.Request.Cookies.ContainsKey(CookieName);
            return Task.FromResult(hasCookie);
        }

        // ------------------ GET CURRENT USER FROM JWT ------------------
        public UserDto? GetCurrentUser()
        {
            var ctx = _httpContextAccessor.HttpContext;

            if (ctx == null ||
                !ctx.Request.Cookies.TryGetValue(CookieName, out var token))
                return null;

            var payload = JwtHelper.DecodePayload(token);
            if (payload == null)
                return null;

            return new UserDto
            {
                Id = payload.TryGetValue("sub", out var id) ? id?.ToString() : "",
                FullName = payload.TryGetValue("name", out var name) ? name?.ToString() : "",
                Email = payload.TryGetValue("email", out var email) ? email?.ToString() : "",
                Role = payload.TryGetValue("role", out var role)
                        ? new List<string> { role?.ToString() ?? "" }
                        : new List<string>()
            };
        }

        // ------------------ STORE TOKEN IN COOKIE ------------------
        private void StoreToken(string token)
        {
            var ctx = _httpContextAccessor.HttpContext;
            if (ctx == null) return;

            ctx.Response.Cookies.Append(
                CookieName,
                token,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,       // خليه true فى الإنتاج على HTTPS
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddHours(12)
                });
        }
    }
}
