using Microsoft.EntityFrameworkCore;
using Shortener.Models.Domain;

namespace Shortener.Data;

public class ShortenerDbContext : DbContext
{
    public const string DefaultSchema = "shortener";

    public ShortenerDbContext(DbContextOptions<ShortenerDbContext> dbContextOptions)
        : base(dbContextOptions)
    {

    }

    public DbSet<Link> Links => Set<Link>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Link>(link =>
        {
            link.ToTable(Link.TableName, ShortenerDbContext.DefaultSchema);
            link.HasKey(x => x.Id);

            link.Property(x => x.ShortCode)
                .IsRequired();

            link.Property(x => x.LongUrl)
                .IsRequired();

            link.HasIndex(x => x.ShortCode)
                    .IsUnique(true);

            link.HasIndex(x => x.LongUrl)
                    .IsUnique(true);
        });
    }
}
