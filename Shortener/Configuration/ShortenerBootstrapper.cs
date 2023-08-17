using Microsoft.EntityFrameworkCore;
using Shortener.AppSettings;
using Shortener.Data;
using Shortener.Services;

namespace Shortener.Configuration;

public static class ShortenerBootstrapper
{
    private const string DefaultServiceConnectionString = "SvcDbContext";
    public static void Configure(IServiceCollection services,IConfiguration configuration)
    {
        
        services.Configure<UrlShortenerSetting>(configuration.GetSection(UrlShortenerSetting.SectionName));

        var connectionString = configuration?.GetConnectionString("DefaultServiceConnectionString");

        services.AddDbContext<ShortenerDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        
        services.AddScoped<ILinkRepository, LinkRepository>();
        services.AddScoped<IUrlShortenerService, UrlShortenerService>();
        
    }
}