using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using WebcomicNotify.Services;

namespace WebcomicNotify.Commands
{
    public class RemoveCommand : CliCommand
    {
        public CliOption<string> Webcomic =
            new("--name", "-n") { Description = "The name of the webcomic to remove from the polling service." };

        public RemoveCommand()
            : base("remove", "Remove a webcomic from the polling service.")
        {
            Aliases.Add("delete");
            Aliases.Add("del");
            Options.Add(Webcomic);

            Action = CommandHandler.Create<ParseResult, IHost>(ExecuteAsync);
        }

        private async Task<int> ExecuteAsync(ParseResult result, IHost host)
        {
            var data = host.Services.GetRequiredService<DataService>();
            var name = result.GetValue(Webcomic);

            await Console.Out.WriteLineAsync($"Ok I'll remove {name}");
            return 0;
        }
    }
}
