namespace InstagramAnalyzer.Infrastructure.Models;

// Representa el objeto dentro
public class InstagramJsonItem
{
    public string href { get; set; } = string.Empty;
    public string value { get; set; } = string.Empty;
    public long timestamp { get; set; }
}