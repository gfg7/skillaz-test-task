using MongoDB.Driver;
using ShortLink.Api.Support;
using ShortLink.Services;
using ShortLink.Services.Args;

namespace ShortLink.Database
{
    public class ShortLinkRepository : IShortLinkRepository
    {
        private readonly DbFactory _dbFactory;
        private readonly UpdateDefinitionBuilder<ShortenedLinkDocument> _u = Builders<ShortenedLinkDocument>.Update;
        private readonly FilterDefinitionBuilder<ShortenedLinkDocument> _f = Builders<ShortenedLinkDocument>.Filter;
        private readonly CancellationToken _cancellationToken;

        public ShortLinkRepository(DbFactory dbFactory, CancellationTokenProvider cancellationTokenProvider)
        {
            _dbFactory = dbFactory;
            _cancellationToken = cancellationTokenProvider.Token;
        }

        public async Task<ShortenedLinkEntity?> GetByShortLink(string shortLink)
        {
            var db = _dbFactory.Create();
            var document = await db.ShortLinks.FindOneAndUpdateAsync(
                _f.Eq(x => x.ShortLink, shortLink),
                _u.Inc(x => x.Counter, 1)
            );

            return document?.ToDomain();
        }

        public async Task<ShortenedLinkEntity[]> GetShortLinks(ShortLinkFilterArgs args)
        {
            var db = _dbFactory.Create();
            var documents = await db.ShortLinks.Find(
                _f.Eq(x => x.UserId, args.UserId)
            )
            .Skip(args.Page * args.Take)
            .Limit(args.Take)
            .ToListAsync(_cancellationToken);

            return documents.Select(x => x.ToDomain()).ToArray();
        }

        public async Task<ShortenedLinkEntity> SaveShortLink(SaveShortLinkArgs args)
        {
            var db = _dbFactory.Create();
            var document = ShortenedLinkDocument.Create(args.ShortLink, args.OriginalLink, args.UserId);
            await db.ShortLinks.InsertOneAsync(document);

            return document.ToDomain();
        }
    }
}