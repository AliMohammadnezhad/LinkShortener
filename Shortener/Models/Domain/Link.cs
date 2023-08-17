namespace Shortener.Models.Domain;

public sealed class Link
{
    public const string TableName = "Links";
    private const int DefaultRequestCount = 0;
    public long Id { get; set; }
    public string ShortCode { get; set; }
    public string LongUrl { get; set; }
    public int RequestCount { get; set; }

    public Link(string shortCode, string longUrl)
    {
        ShortCode = shortCode;
        LongUrl = longUrl;

        RequestCount = DefaultRequestCount;
    }

    public static Link Create(string shortCode, string longUrl)
        => new(shortCode, longUrl);

    public void IncreaseCount()
        => RequestCount++;
}