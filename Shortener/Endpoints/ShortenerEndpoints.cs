using Shortener.Services;

namespace Shortener.Endpoints;

internal static class ShortenerEndpoints
{
    internal static async Task<IResult> ShortenEndpoint(
        string url,
        IUrlShortenerService urlShortenerService, 
        CancellationToken cancellationToken)
    {
        var shortUrl = await urlShortenerService.ShortenUrlAsync(url, cancellationToken);
        return Results.Ok(new { ShortUrl = shortUrl });
    }

}
