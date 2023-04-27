using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.CommandLine;
using System.CommandLine.Help;
using System.CommandLine.NamingConventionBinder;
using System.Runtime.CompilerServices;
using WebcomicNotify.Commands;
using WebcomicNotify.Services;

namespace WebcomicNotify
{
    public class RootCommand : CliRootCommand
    {
        public const int DefaultPollingRateMinutes = 60;

        public CliOption<string> DefaultWebhook { get; } =
            new("--webhook", "-w") { Description = "The default webhook to post notifications to." };
        public CliOption<int> PollRateMinutes { get; } =
            new("--pollrate", "-p") 
            { 
                Description = "How often in minutes the service will poll for new chapters.",
                DefaultValueFactory = (result) => DefaultPollingRateMinutes
            };

        public RootCommand()
            : base("A cli app for polling and notifying of when new webcomic chapters are released.")
        {
            Aliases.Add("run");
            Aliases.Add("start");
            Options.Add(DefaultWebhook);
            Options.Add(PollRateMinutes);

            Subcommands.Add(new AddCommand());
            Subcommands.Add(new CheckCommand());
            Subcommands.Add(new ListCommand());
            Subcommands.Add(new RemoveCommand());

            Action = CommandHandler.Create<ParseResult, IHost>(ExecuteAsync);
        }

        public async Task<int> ExecuteAsync(ParseResult result, IHost host)
        {
            if (result.Tokens.Any(x => new[] {"--help", "-h", "/h", "-?", "/?" }.Contains(x.Value)))
                return 2;

            var logger = host.Services.GetRequiredService<ILogger<RootCommand>>();
            var url = result.GetValue(DefaultWebhook);
            if (!Uri.TryCreate(url, UriKind.Absolute, out var defaultWebhookUrl))
            {
                logger.LogError("The value `{webhook}` is not a valid url.", defaultWebhookUrl);
                return 1;
            }

            var pollRateMinutes = result.GetValue(PollRateMinutes);
            if (pollRateMinutes < 1)
            {
                logger.LogError("The value `{pollrate}` is not a valid poll rate", pollRateMinutes);
                return 1;
            }

            logger.LogInformation("Running service with default webhook {webhook} and a polling rate of {pollrate} minute(s)", defaultWebhookUrl, pollRateMinutes);

            await host.RunAsync();
            host.Dispose();
            return 0;
        }
    }
}
