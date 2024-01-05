namespace ShortLink.Services.Errors
{
    public class GenerateShortLinkAttemptExceededError : Exception
    {
        public GenerateShortLinkAttemptExceededError(string originalLink) : base($"Run out of attempts to create short link for '{originalLink}'")
        {

        }
    }
}