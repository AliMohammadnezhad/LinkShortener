using Microsoft.EntityFrameworkCore;
using Shortener.Models.Domain;

namespace Shortener.Data;

public class LinkRepository : ILinkRepository
{
    private readonly ShortenerDbContext _shortenerDbContext;

    public LinkRepository(ShortenerDbContext shortenerDbContext)
        => _shortenerDbContext = shortenerDbContext;

    public async Task AddAsync(Link link, CancellationToken cancellationToken)
        => await _shortenerDbContext.Links.AddAsync(link, cancellationToken);

    public async Task<(bool found, string? value)> TryGetLongUrlAsync(string shortCode, CancellationToken cancellationToken)
    {
        var link = await _shortenerDbContext.Links.FirstOrDefaultAsync(x => x.ShortCode == shortCode, cancellationToken);

        if (link == null)
            return (false, null);

        link.IncreaseCount();

        _shortenerDbContext.Links.Update(link);
        await _shortenerDbContext.SaveChangesAsync(cancellationToken);

        return (true, link.LongUrl);
    }

    public async Task<(bool found, string? value)> TryGetShortUrlAsync(string longUrl, CancellationToken cancellationToken)
    {
        var link = await _shortenerDbContext.Links.FirstOrDefaultAsync(x => x.LongUrl == longUrl, cancellationToken);

        if (link != null)
        {
            return (true, link.ShortCode);
        }

        return (false, null);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
        => await _shortenerDbContext.SaveChangesAsync(cancellationToken) > 0;
}