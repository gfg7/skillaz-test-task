using ShortLink.Services;

namespace ShortLink.Api.Models
{
    public record ShortenedLinkCounter(string ShortLink, string OriginalLink, int Counter = 0);

    public static class ShortenedLinkCounterMapping
    {
        public static ShortenedLinkCounter FromDomain(ShortenedLinkEntity entity)
        {
            return new ShortenedLinkCounter(
                entity.ShortLink,
                entity.OriginalLink,
                entity.Counter
            );
        }
    }
}