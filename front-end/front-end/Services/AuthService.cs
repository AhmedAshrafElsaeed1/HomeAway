namespace front_end.Services
{
    using front_end.Interfaces;
    using front_end.Auth;
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

        // returns JWT when successful (also stores it)
        public async Task<string?> LoginAsync(LoginDto dto)
        {
            var resp = await _client.PostAsJsonAsync("auth/login", dto);
            if (!resp.IsSuccessStatusCode) return null;

            var obj = await resp.Content.ReadFromJsonAsync<JsonElement>();
            if (obj.TryGetProperty("token", out var tokenObj))
            {
                if (tokenObj.TryGetProperty("result", out var tokenProp))
                {
                    var token = tokenProp.GetString();
                    if (!string.IsNullOrWhiteSpace(token))
                    {
                        StoreToken(token);
                        return token;
                    }
                }
            }

            return null;
        }

        public Task LogoutAsync()
        {
            var ctx = _httpContextAccessor.HttpContext;
            if (ctx != null)
            {
                // remove cookie
                ctx.Response.Cookies.Delete(CookieName);
                // or remove session
                // ctx.Session.Remove("BookifyJwt");
            }
            return Task.CompletedTask;
        }

        public Task<bool> IsSignedInAsync()
        {
            var ctx = _httpContextAccessor.HttpContext;
            if (ctx == null) return Task.FromResult(false);

            // cookie approach
            var has = ctx.Request.Cookies.ContainsKey(CookieName);
            // or session: var has = !string.IsNullOrEmpty(ctx.Session.GetString("BookifyJwt"));
            return Task.FromResult(has);
        }

        private void StoreToken(string token)
        {
            var ctx = _httpContextAccessor.HttpContext;
            if (ctx == null) return;

            // OPTION A — store in HttpOnly Secure cookie (recommended)
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false,       // true in production (HTTPS)
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(12)
            };
            ctx.Response.Cookies.Append(CookieName, token, cookieOptions);

            // OPTION B — store in session (alternative)
            // ctx.Session.SetString("BookifyJwt", token);
        }
    }
}
