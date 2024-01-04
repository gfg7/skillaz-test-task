using ShortLink.Services;

namespace ShortLink.Api.Models
{
    public record ShortenedLink(string ShortLink, string OriginalLink);

    public static class ShortenedLinkMapping
    {
        public static ShortenedLink FromDomain(ShortenedLinkEntity entity)
        {
            return new ShortenedLink(
                entity.ShortLink,
                entity.OriginalLink
            );
        }
    }
}