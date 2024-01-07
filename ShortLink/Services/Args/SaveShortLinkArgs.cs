namespace ShortLink.Services.Args
{
    public record SaveShortLinkArgs(string OriginalLink, string ShortLink, string UserId);
}