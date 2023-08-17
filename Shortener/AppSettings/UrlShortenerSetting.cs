﻿namespace Shortener.AppSettings;

public class UrlShortenerSetting
{
    public const string SectionName = "UrlShortener";

    public required string BaseServiceUrl { get; set; }

    public required int ShortCodeLength { get; set; }

    public int DefaultAbsoluteExpirationRelativeToNowOnDays { get; set; }
}
