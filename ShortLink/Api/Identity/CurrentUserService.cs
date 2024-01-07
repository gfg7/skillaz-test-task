using System.Security.Claims;
using ShortLink.Services;

namespace ShortLink.Api.Identity
{
    public class CurrentUserService : ICurrentUserService
    {
        public string UserId {get; init;}
        public CurrentUserService(ClaimsPrincipal claimsPrincipal)
        {
            UserId = claimsPrincipal.Identity!.Name!;
        }
    }
}