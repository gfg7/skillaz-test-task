using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using ShortLink.Services;

namespace ShortLink.Api.Identity
{
    public class SignInService : IAuthService, ICurrentUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly HttpContext _context;

        public SignInService(IUserRepository userRepository,
                             IHttpContextAccessor contextAccessor)
        {
            _userRepository = userRepository;
            _context = contextAccessor.HttpContext!;
        }

        public string UserId => _context.User.Identity!.Name!;

        public async Task SignIn()
        {
            if (_context.User.Identity!.IsAuthenticated)
            {
                return;
            }

            var user = await _userRepository.CreateUser();
            var claims = ClaimsBuilderService.CreateClaims(user.UserId, CookieAuthenticationDefaults.AuthenticationScheme);
            await _context.SignInAsync(claims, new AuthenticationProperties()
            {
                IsPersistent = true
            });

            _context.User = claims;
        }
    }
}