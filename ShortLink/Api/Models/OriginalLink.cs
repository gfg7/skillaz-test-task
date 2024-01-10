using System.ComponentModel.DataAnnotations;

namespace ShortLink.Api.Models
{
    public record OriginalLink(
        [Url]
        string Link
    );
}