using CodeHollow.FeedReader;
using Microsoft.Extensions.Logging;

namespace WebcomicNotify.Services;

public class WebtoonScraper : IScraper
{
    private readonly HttpClient _http;
    private readonly ILogger _logger;

    public WebtoonScraper(HttpClient http, ILogger<WebtoonScraper> logger)
    {
        _http = http;
        _logger = logger;
    }

    public Task<IEnumerable<SearchResult>> SearchAsync(string text)
    {
        throw new NotImplementedException();
    }
}
