
//namespace HomeAway.Services
//{
//    public class AuthService : IAuthService
//    {
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly JwtTokenService _jwtService;

//        public AuthService(UserManager<ApplicationUser> userManager, JwtTokenService jwtService)
//        {
//            _userManager = userManager;
//            _jwtService = jwtService;
//        }

//        public async Task<string> RegisterAsync(RegisterDto dto)
//        {
//            var user = new ApplicationUser
//            {
//                UserName = dto.UserName,
//                Email = dto.Email,
//                FullName = dto.FullName
//            };

//            var result = await _userManager.CreateAsync(user, dto.Password);

//            if (!result.Succeeded)
//            {
//                var errors = string.Join(" | ", result.Errors.Select(x => x.Description));
//                return $"ERROR: {errors}";
//            }

//            return "SUCCESS";
//        }

//        public async Task<string> LoginAsync(LoginDto dto)
//        {
//            var user = await _userManager.FindByNameAsync(dto.UserName);

//            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
//                return "INVALID";

//            var token = _jwtService.GenerateToken(user);

//            return token;
//        }
//    }
//}
