using Microsoft.Extensions.Logging;

namespace WebcomicNotify.Services;

public class WordpressScraper : IScraper
{
    private readonly HttpClient _http;
    private readonly ILogger _logger;

    public WordpressScraper(HttpClient http, ILogger<WordpressScraper> logger)
    {
        _http = http;
        _logger = logger;
    }

    public Task<IEnumerable<SearchResult>> SearchAsync(string text)
    {
        throw new NotImplementedException();
    }
}
