namespace ShortLink.Services.Errors
{
    public class ShortLinkNotFoundError : Exception
    {
        public ShortLinkNotFoundError(string shortLink) : base($"Shortend link '{shortLink}' not found")
        {
            
        }
    }
}