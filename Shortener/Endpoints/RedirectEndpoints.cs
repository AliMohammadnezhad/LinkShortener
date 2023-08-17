using Microsoft.VisualBasic;
using Shortener.Services;

namespace Shortener.Endpoints;

internal static class RedirectEndpoints
{
    internal static async Task<IResult> RedirectEndpoint(
        IUrlShortenerService urlShortenerService,
        CancellationToken cancellationToken,
        string shortCode)
    {
        var foundUrlResult = await urlShortenerService.TryGetLongUrlAsync(shortCode, cancellationToken);

        if (foundUrlResult.found)
        { 
            return Results.Redirect(foundUrlResult.value!); 
        }

        return Results.BadRequest(Constants.EndPointMessages.InvalidUrl);
    }
}
