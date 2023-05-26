using Microsoft.Extensions.Logging;

namespace WebcomicNotify.Services;

public class AsurascanScraper : WordpressScraper, IScraper
{
    public AsurascanScraper(HttpClient http, ILogger<AsurascanScraper> logger)
        : base(http, logger) { }
}
