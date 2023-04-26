using System.CommandLine;
using WebcomicNotify.Commands;

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
            Options.Add(DefaultWebhook);
            Options.Add(PollRateMinutes);

            Subcommands.Add(new AddCommand());
            Subcommands.Add(new CheckCommand());
            Subcommands.Add(new ListCommand());
            Subcommands.Add(new RemoveCommand());
        }
    }
}
