using Microsoft.AspNetCore.Mvc;
using ShortLink.Api.Models;
using ShortLink.Services;
using ShortLink.Services.Args;

namespace ShortLink.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShortLinkController
    {
        private readonly ShortLinkService _shortLinkService;
        public ShortLinkController(ShortLinkService shortLinkService)
        {
            _shortLinkService = shortLinkService;
        }

        [HttpPost("create")]
        public async Task<ShortenedLink> CreateShortLink([FromBody] OriginalLink input)
        {
            var args = new GenerateShortLinkArgs(input.Link);
            var result = await _shortLinkService.CreateShortLink(args);
            return ShortenedLinkMapping.FromDomain(result);
        }

        [HttpGet("{link}")]
        public async Task<IActionResult> GetOriginalLink([FromRoute] string link)
        {
            var result = await _shortLinkService.GetByShortLink(link);
            return new RedirectResult(result.OriginalLink);
        }

        [HttpGet("list")]
        public async Task<ShortenedLinkCounter[]> GetShortenedLinks(int take = 10, int page = 0)
        {
            var result = await _shortLinkService.GetShortenedLinks(take, page);
            return result.Select(x => ShortenedLinkCounterMapping.FromDomain(x)).ToArray();
        }
    }
}