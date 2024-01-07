using MongoDB.Driver;

namespace ShortLink.Database
{
    public class Db
    {
        private readonly IMongoDatabase _mongoDatabase;

        public Db(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public IMongoCollection<ShortenedLinkDocument> ShortLinks => _mongoDatabase.GetCollection<ShortenedLinkDocument>(nameof(ShortLinks));
        public IMongoCollection<UserDocument> Users => _mongoDatabase.GetCollection<UserDocument>(nameof(Users));
    }
}