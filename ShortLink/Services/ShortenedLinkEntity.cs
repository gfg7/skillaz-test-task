namespace ShortLink.Services
{
    public record ShortenedLinkEntity(string OriginalLink, string ShortLink, string UserId, int Counter = 0);
}