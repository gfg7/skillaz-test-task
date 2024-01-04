using MongoDB.Driver;

namespace ShortLink.Database
{
    public class DbFactory
    {
        private readonly IMongoClient _client;

        public DbFactory(string connectionString)
        {
            _client = new MongoClient(connectionString);
        }

        public Db Create()
        {
            var database = _client.GetDatabase("ShortLink");
            return new Db(database);
        }
    }
}