using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Shortener.AppSettings;
using Shortener.Data;
using Shortener.Models.Domain;

namespace Shortener.Services;

public class UrlShortenerService:IUrlShortenerService
{
    private readonly UrlShortenerSetting _shortenerSetting;
    private readonly ILinkRepository _linkRepository;
    
    public UrlShortenerService(
        IOptions<UrlShortenerSetting> shortenerSettingOptions,
        ILinkRepository linkRepository)
    {
        _shortenerSetting = shortenerSettingOptions.Value;
        _linkRepository = linkRepository;
    }
    
    public async Task<(bool found, string? value)> TryGetLongUrlAsync(string shortCode, CancellationToken cancellationToken)
    {
        var getLinkResult = await _linkRepository.TryGetLongUrlAsync(shortCode, cancellationToken);
        if (getLinkResult.found)
        {
            
            return (true, getLinkResult.value);
        }

        return (false, null);
    }

    public async Task<string> ShortenUrlAsync(string longUrl, CancellationToken cancellationToken)
    {
        var getUrlResult = await TryGetShortUrlAsync(longUrl, cancellationToken);
        if (getUrlResult.found)
        { return getUrlResult.value!; }

        var shortCode = GenerateShortCode(longUrl);

        var link = Link.Create(shortCode, longUrl);
        await _linkRepository.AddAsync(link, cancellationToken);
        await _linkRepository.SaveChangesAsync(cancellationToken);

        
        return  $"{_shortenerSetting.BaseServiceUrl}/{shortCode}";
    }
    
    
    private async Task<(bool found, string? value)> TryGetShortUrlAsync(string longUrl, CancellationToken cancellationToken)
    {
        var getShortLinkResult = await _linkRepository.TryGetShortUrlAsync(longUrl, cancellationToken);
        if (getShortLinkResult.found)
        {
            return (true, getShortLinkResult.value);
        }

        return (false, null);
    }
    
    private string GenerateShortCode(string longUrl)
    {
        using MD5 md5 = MD5.Create();
        var hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(longUrl));
        var hashCode = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

        for (var i = 0; i <= hashCode.Length - _shortenerSetting.ShortCodeLength; i++)
        {
            var candidateCode = hashCode.Substring(i, _shortenerSetting.ShortCodeLength);
            return candidateCode;
        }
             
        throw new Exception("Failed to generate a unique short code.");
    }
}