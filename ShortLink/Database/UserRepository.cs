using MongoDB.Bson;
using ShortLink.Services;

namespace ShortLink.Database
{
    public class UserRepository : IUserRepository
    {
        private readonly DbFactory _dbFactory;

        public UserRepository(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<UserEntity> CreateUser()
        {
            var db = _dbFactory.Create();
            var document = UserDocument.Create(ObjectId.GenerateNewId().ToString());
            await db.Users.InsertOneAsync(document);

            return document.ToDomain();
        }
    }
}