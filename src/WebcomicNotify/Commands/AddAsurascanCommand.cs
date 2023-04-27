using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using WebcomicNotify.Services;

namespace WebcomicNotify.Commands
{
    public class AddAsurascanCommand : CliCommand
    {
        public CliOption<string> Query { get; }
            = new("--query", "-q") { Description = "Search for a comic's rss feed by Title.", Required = false };
        public CliOption<string> Url { get; }
            = new("--url", "-u") { Description = "A url that points to the webcomic's page, or directly to the rss feed.", Required = false };
        public CliOption<string> Webhook { get; }
            = new("--webhook", "-w") { Description = "A custom webhook url if not using the globally specified one.", Required = false };

        public AddAsurascanCommand()
            : base("asurascan", "Add an asurascans comic to the service.")
        {
            Aliases.Add("asurascans");
            Aliases.Add("asura");
            Options.Add(Query);
            Options.Add(Url);
            Options.Add(Webhook);

            Action = CommandHandler.Create<ParseResult, IHost>(ExecuteAsync);
        }

        private Task<int> ExecuteAsync(ParseResult result, IHost host)
        {
            var logger = host.Services.GetRequiredService<ILogger<AddCommand>>();
            var finder = host.Services.GetRequiredService<FeedFinderService>();

            logger.LogInformation("Executed add asurascans command");
            return Task.FromResult(0);
        }
    }
}