namespace ShortLink.Api.Support
{
    public class CancellationTokenProvider
    {
        public CancellationToken Token {get;}
        
        public CancellationTokenProvider(IHttpContextAccessor contextAccessor)
        {
            Token = contextAccessor.HttpContext?.RequestAborted ?? CancellationToken.None;
        }
    }
}