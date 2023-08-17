namespace Shortener.Models.Domain;

public sealed class Link
{
    public const string TableName = "Links";

    public long Id { get; set; }
    public string ShortCode { get; set; }
    public string LongUrl { get; set; }

    public Link(string shortCode, string longUrl)
    {
        ShortCode = shortCode;
        LongUrl = longUrl;
    }

    public static Link Create(string shortCode, string longUrl)
        => new(shortCode, longUrl);
}
