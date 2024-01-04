using ShortLink.Services.Args;

namespace ShortLink.Services
{
    public interface IShortLinkGenerator
    {
        Task<string> GenerateShortLink(GenerateShortLinkArgs args);
    }
}