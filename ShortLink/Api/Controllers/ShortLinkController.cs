using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using ShortLink.Api.Models;

namespace ShortLink.Api.Controllers
{
    [ApiController]
    public class ShortLinkController
    {
        public ShortLinkController()
        {

        }

        [HttpPost]
        public Task<ShortenedLink> CreateShortLink(OriginalLink input)
        {
            return null;
        }

        public Task<string> GetOriginalLink([FromRoute] string shortLink)
        {
            return null;
        }

        public Task<ShortenedLinkCounter[]> GetShortenedLinks()
        {
            return null;
        }
    }
}