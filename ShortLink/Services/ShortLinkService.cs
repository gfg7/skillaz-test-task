using ShortLink.Services.Args;

namespace ShortLink.Services
{
    public class ShortLinkService
    {
        public Task<ShortenedLinkEntity> CreateShortLink(GenerateShortLinkArgs args)
        {
            return null;
        }

        public Task<ShortenedLinkEntity> GetByShortLink(string shortLink)
        {
            return null;
        }

        public Task<ShortenedLinkEntity[]> GetShortenedLinks()
        {
            return null;
        }
    }
}