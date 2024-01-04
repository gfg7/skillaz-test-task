namespace ShortLink.Api.Models
{
    public record ShortenedLinkCounter(string ShortLink, int Counter = 0);
}