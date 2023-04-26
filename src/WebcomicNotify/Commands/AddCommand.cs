using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using WebcomicNotify.Services;

namespace WebcomicNotify.Commands
{
    public class AddCommand : CliCommand
    {
        public CliOption<string> Url { get; }
            = new("--url", "-u") { Description = "A url that points to the webcomic's page, or directly to the rss feed." };
        public CliOption<string> Webhook { get; }
            = new("--webhook", "-w") { Description = "A custom webhook url if not using the globally specified one.", Required = false };

        public AddCommand()
            : base("add", "Add a webcomic to the polling service.")
        {
            Aliases.Add("new");
            Options.Add(Url);
            Options.Add(Webhook);

            Action = CommandHandler.Create<ParseResult, IHost>(ExecuteAsync);
        }

        private async Task<int> ExecuteAsync(ParseResult result, IHost host)
        {
            var data = host.Services.GetRequiredService<DataService>();
            var url = result.GetValue(Url);
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                await Console.Error.WriteLineAsync($"The value `{url}` is not a valid url.");
                return 0;
            }

            Uri.TryCreate(result.GetValue(Webhook), UriKind.Absolute, out var webhookUri);

            switch (uri.Host)
            {
                case WebcomicConstants.WebtoonsHost:
                    /*
                     * - Scrapes results from https://www.webtoons.com/en/search?keyword= for matching comics and displays them to the user.
                     * - Follow link in result and scrape for rss feed page.
                     */
                    break;
                case WebcomicConstants.FlamescansHost:
                    /*
                     * - Scrapes results from https://flamescans.org/?s= for matching comics and displays them to the user.
                     * - Pull title from result and regex match with global feed.
                     */
                    break;
                case WebcomicConstants.AsurascansHost:
                    /*
                     * - Scrapes results from https://www.asurascans.com/?s= for matching comics and displays them to the user.
                     * - Pull title from result and regex match with global feed.
                     */
                    break;
                default:
                    break;
            }


            await Console.Out.WriteLineAsync($"Ok I'll add {uri}");
            return 0;
        }
    }
}
