namespace ShortLink.Services.Errors
{
    public class ShortLinkNotFoundError : Exception
    {
        public ShortLinkNotFoundError(string shortLink) : base($"Shortened link '{shortLink}' not found")
        {
            
        }
    }
}