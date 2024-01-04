using System.Security.Cryptography;
using System.Text;
using Base62;
using ShortLink.Services.Args;

namespace ShortLink.Services
{
    public class ShortLinkGenerator : IShortLinkGenerator
    {
        public async Task<string> GenerateShortLink(GenerateShortLinkArgs args)
        {
            var rand = new Random();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(args.OriginalLink + rand.NextInt64(100000, 999999)));
            var hashed = await MD5.HashDataAsync(stream);
            var stringified = hashed.ToBase62();

            return new string(stringified.Take(6).ToArray())!;
        }
    }
}