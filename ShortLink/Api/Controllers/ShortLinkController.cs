using Microsoft.AspNetCore.Mvc;
using ShortLink.Api.Models;
using ShortLink.Services;
using ShortLink.Services.Args;

namespace ShortLink.Api.Controllers
{
    [ApiController]
    public class ShortLinkController
    {
        private readonly ShortLinkService _shortLinkService;
        public ShortLinkController(ShortLinkService shortLinkService)
        {
            _shortLinkService = shortLinkService;
        }

        [HttpPost]
        public async Task<ShortenedLink> CreateShortLink(OriginalLink input)
        {
            var args = new GenerateShortLinkArgs(input.Link);
            var result = await _shortLinkService.CreateShortLink(args);
            return ShortenedLinkMapping.FromDomain(result);
        }

        public async Task<string> GetOriginalLink([FromRoute] string shortLink)
        {
            var result = await _shortLinkService.GetByShortLink(shortLink);
            return result.OriginalLink;
        }

        public async Task<ShortenedLinkCounter[]> GetShortenedLinks()
        {
            var result = await _shortLinkService.GetShortenedLinks();
            return result.Select(x => ShortenedLinkCounterMapping.FromDomain(x)).ToArray();
        }
    }
}