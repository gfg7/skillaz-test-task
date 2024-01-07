using ShortLink.Services.Args;
using ShortLink.Services.Errors;
using ShortLink.Utils;

namespace ShortLink.Services
{
    public class ShortLinkService
    {
        private readonly IShortLinkRepository _shortLinkRepository;
        private readonly IShortLinkGenerator _shortLinkGenerator;
        private readonly ICurrentUserService _currentUserService;

        public ShortLinkService(ICurrentUserService currentUserService,
                                IShortLinkRepository shortLinkRepository,
                                IShortLinkGenerator shortLinkGenerator)
        {
            _shortLinkRepository = shortLinkRepository;
            _shortLinkGenerator = shortLinkGenerator;
            _currentUserService = currentUserService;
        }

        public async Task<ShortenedLinkEntity> CreateShortLink(GenerateShortLinkArgs args)
        {
            var attempts = Convert.ToInt32(EnvService.GetVariable("RETRY_COUNT"));
            int attemptCount = 0;
            do
            {
                try
                {
                    attemptCount++;
                    var shortLink = await _shortLinkGenerator.GenerateShortLink(args);
                    var saveArgs = new SaveShortLinkArgs(args.OriginalLink, shortLink, _currentUserService.UserId);

                    return await _shortLinkRepository.SaveShortLink(saveArgs);
                }
                catch { }
            } while (attemptCount < attempts);

            throw new GenerateShortLinkAttemptExceededError(args.OriginalLink);
        }

        public async Task<ShortenedLinkEntity> GetByShortLink(string shortLink)
        {
            var result = await _shortLinkRepository.GetByShortLink(shortLink) ?? throw new ShortLinkNotFoundError(shortLink);

            await _shortLinkRepository.IncrementCounter(shortLink);
            return result;
        }

        public async Task<ShortenedLinkEntity[]> GetShortenedLinks(int take, int page)
        {
            var filterArgs = new ShortLinkFilterArgs(_currentUserService.UserId, take, page);
            return await _shortLinkRepository.GetShortLinks(filterArgs);
        }
    }
}