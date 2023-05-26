namespace WebcomicNotify;

public readonly struct SearchResult
{
    public string Name { get; }
    public string Url { get; }
    public string? Rating { get; }

    public SearchResult(string name, string url, string? rating)
    {
        Name = name;
        Url = url;
        Rating = rating;
    }
}
