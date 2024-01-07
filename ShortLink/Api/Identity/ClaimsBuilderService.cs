using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ShortLink.Api.Identity
{
    public static class ClaimsBuilderService
    {
        public static ClaimsPrincipal CreateClaims(string userId, string scheme)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userId)
            };
            var identity = new ClaimsIdentity(claims, scheme);
            
            return new ClaimsPrincipal(identity);
        }
    }
}