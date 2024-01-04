namespace ShortLink.Services
{
    public record ShortenedLinkEntity(string OriginalLink, string ShortLink, int Counter = 0);
}