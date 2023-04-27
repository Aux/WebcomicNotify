using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
            Subcommands.Add(new AddWebtoonCommand());
            Subcommands.Add(new AddFlamescanCommand());
            Subcommands.Add(new AddAsurascanCommand());

            Action = CommandHandler.Create<ParseResult, IHost>(ExecuteAsync);
        }

        protected virtual async Task<int> ExecuteAsync(ParseResult result, IHost host)
        {
            var logger = host.Services.GetRequiredService<ILogger<AddCommand>>();
            var finder = host.Services.GetRequiredService<FeedFinderService>();

            var rss = result.GetValue(Url);
            if (!Uri.TryCreate(rss, UriKind.Absolute, out var rssUrl))
            {
                logger.LogError("The value `{url}` is not a valid url.", rssUrl);
                return 1;
            }

            var customWebhook = result.GetValue(Webhook);
            if (customWebhook != null && !Uri.TryCreate(customWebhook, UriKind.Absolute, out var webhookUri))
            {
                logger.LogError("The value `{customWebhook}` is not a valid url.", customWebhook);
                return 1;
            }

            await finder.FindAsync(rssUrl);
            return 0;
        }
    }
}
