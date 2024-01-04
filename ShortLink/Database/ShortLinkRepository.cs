using MongoDB.Driver;
using ShortLink.Services;
using ShortLink.Services.Args;

namespace ShortLink.Database
{
    public class ShortLinkRepository : IShortLinkRepository
    {
        private readonly DbFactory _dbFactory;
        private readonly UpdateDefinitionBuilder<ShortenedLinkDocument> _u = Builders<ShortenedLinkDocument>.Update;
        private readonly FilterDefinitionBuilder<ShortenedLinkDocument> _f = Builders<ShortenedLinkDocument>.Filter;

        public ShortLinkRepository(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<ShortenedLinkEntity?> GetByShortLink(string shortLink)
        {
            var db = _dbFactory.Create();
            var document = await db.ShortLinks.Find(
                _f.Eq(x => x.ShortLink, shortLink)
            ).FirstOrDefaultAsync();

            return document?.ToDomain();
        }

        public async Task<ShortenedLinkEntity[]> GetShortLinks(ShortLinkFilterArgs args)
        {
            var db = _dbFactory.Create();
            var documents = await db.ShortLinks.Find(
                _f.Empty
            )
            .Skip(args.Take)
            .Limit(args.Page * args.Take - 1)
            .ToListAsync();

            return documents.Select(x => x.ToDomain()).ToArray();
        }

        public async Task IncrementCounter(string shortLink)
        {
            var db = _dbFactory.Create();
            await db.ShortLinks.UpdateOneAsync(
                _f.Eq(x => x.ShortLink, shortLink),
                _u.Inc(e => e.Counter, 1)
            );
        }

        public async Task<ShortenedLinkEntity> SaveShortLink(SaveShortLinkArgs args)
        {
            var db = _dbFactory.Create();
            var document = ShortenedLinkDocument.Create(args.ShortLink, args.OriginalLink);
            await db.ShortLinks.InsertOneAsync(document);

            return document.ToDomain();
        }
    }
}