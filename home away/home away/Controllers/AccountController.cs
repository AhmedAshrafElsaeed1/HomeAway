using home_away.Interfaces;
using HomeAway.Auth;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    private readonly IAuthService _auth;

    public AccountController(IAuthService auth) => _auth = auth;

    [HttpGet]
    public IActionResult Login() => View(new LoginDto());

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        if (!ModelState.IsValid) return View(dto);

        var token = await _auth.LoginAsync(dto);
        if (token == null)
        {
            ModelState.AddModelError("", "Invalid credentials");
            return View(dto);
        }

        // token already stored by AuthService (cookie)
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register() => View(new RegisterDto());

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        if (!ModelState.IsValid) return View(dto);

        var created = await _auth.RegisterAsync(dto);
        if (!created)
        {
            ModelState.AddModelError("", "Registration failed");
            return View(dto);
        }

        // Optionally auto-login after register
        await _auth.LoginAsync(new LoginDto { UserName = dto.UserName, Password = dto.Password });
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await _auth.LogoutAsync();
        return RedirectToAction("Login");
    }
}
