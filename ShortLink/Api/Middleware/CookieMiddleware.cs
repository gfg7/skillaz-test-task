using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using ShortLink.Api.Identity;
using ShortLink.Services;

namespace ShortLink.Api.Middleware
{
    public class CookieMiddleware
    {
        private readonly RequestDelegate _request;

        public CookieMiddleware(RequestDelegate request)
        {
            _request = request;
        }

        public async Task Invoke(HttpContext httpContext, IUserRepository userRepository)
        {
            if (!httpContext.User.Identity!.IsAuthenticated)
            {
                var user = await userRepository.CreateUser();
                var claims = ClaimsBuilderService.CreateClaims(user.UserId, CookieAuthenticationDefaults.AuthenticationScheme);
                await httpContext.SignInAsync(claims, new AuthenticationProperties()
                {
                    IsPersistent = true
                });
                httpContext.User = claims;
            }

            await _request.Invoke(httpContext);
        }
    }
}