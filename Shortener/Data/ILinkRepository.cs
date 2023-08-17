using Shortener.Models.Domain;

namespace Shortener.Data;

public interface ILinkRepository
{
    Task AddAsync(Link link, CancellationToken cancellationToken);
    Task<(bool found, string? value)> TryGetLongUrlAsync(string shortCode, CancellationToken cancellationToken);
    Task<(bool found, string? value)> TryGetShortUrlAsync(string longUrl, CancellationToken cancellationToken);
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);

}
