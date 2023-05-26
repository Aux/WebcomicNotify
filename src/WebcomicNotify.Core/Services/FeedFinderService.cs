using CodeHollow.FeedReader;
using Microsoft.Extensions.Logging;

namespace WebcomicNotify.Services
{
    public class FeedFinderService
    {
        private readonly ILogger _logger;
        private readonly DataService _data;

        public FeedFinderService(ILogger<FeedFinderService> logger, DataService data)
        {
            _logger = logger;
            _data = data;
        }

        /// <summary>
        ///     Generic feeds have to be the direct url
        /// </summary>
        public async Task FindAsync(Uri url)
        {
            var feed = await FeedReader.ReadAsync(url.ToString());

            _logger.LogInformation("Found a feed for {title} with {amount} recent chapters, last updated at {datetime}.", 
                feed.Title, feed.Items.Count, feed.LastUpdatedDate?.ToString("G") ?? "unknown");

            _logger.LogInformation("Found {amount} results", feed.Items.Count);
        }

        /// <summary>
        ///     Scrapes results from https://www.webtoons.com/en/search?keyword= for matching comics and 
        ///     displays them to the user.
        ///     Follow link in result and scrape for rss feed page.
        /// </summary>
        public Task FindWithWebtoonAsync(string text)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Scrapes results from https://flamescans.org/?s= for matching comics and displays them to the user.
        ///     Pull title from result and regex match with global feed.
        /// </summary>
        public Task FindWithWordpressAsync(string text)
        {
            return Task.CompletedTask;
        }
    }
}
