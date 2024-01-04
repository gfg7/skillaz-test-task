using MongoDB.Bson.Serialization.Attributes;
using ShortLink.Services;

namespace ShortLink.Database
{
    public record ShortenedLinkDocument
    {
        [BsonId]
        public string ShortLink { get; init; } = null!;
        public string OriginalLink { get; init; } = null!;
        public int Counter { get; init; } = 0;

        public static ShortenedLinkDocument Create(string shortLink, string originalLink)
        {
            return new ShortenedLinkDocument()
            {
                ShortLink = shortLink,
                OriginalLink = originalLink
            };
        }
    }

    public static class ShortenedLinkDocumentMapping
    {
        public static ShortenedLinkEntity ToDomain(this ShortenedLinkDocument document)
        {
            return new ShortenedLinkEntity(document.OriginalLink, document.ShortLink, document.Counter);
        }
    }
}