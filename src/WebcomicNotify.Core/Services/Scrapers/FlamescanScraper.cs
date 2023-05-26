using Microsoft.Extensions.Logging;

namespace WebcomicNotify.Services;

public class FlamescanScraper : WordpressScraper, IScraper
{
    public FlamescanScraper(HttpClient http, ILogger<FlamescanScraper> logger)
        : base(http, logger) { }
}
