using MongoDB.Bson.Serialization.Attributes;
using ShortLink.Services;

namespace ShortLink.Database
{
    public record UserDocument
    {
        [BsonId]
        public string UserId { get; init; } = null!;

        public static UserDocument Create(string userId)
        {
            return new UserDocument()
            {
                UserId = userId
            };
        }
    }

    public static class UserDocumentMapping
    {
        public static UserEntity ToDomain(this UserDocument document)
        {
            return new UserEntity(document.UserId);
        }
    }
}