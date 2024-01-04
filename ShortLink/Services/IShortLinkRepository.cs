using ShortLink.Services.Args;

namespace ShortLink.Services
{
    public interface IShortLinkRepository
    {
        Task<ShortenedLinkEntity> SaveShortLink(SaveShortLinkArgs args);
        Task<ShortenedLinkEntity?> GetByShortLink(string shortLink);
        Task IncrementCounter(string shortLink);
        Task<ShortenedLinkEntity[]> GetShortLinks();
    }
}