using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using WebcomicNotify.Models;
using WebcomicNotify.Services;

namespace WebcomicNotify.Commands
{
    public class CheckCommand : CliCommand
    {
        public CliArgument<string?> Webcomic =
            new("webcomic") { Description = "Limit the check to only a single specific comic.", DefaultValueFactory = result => null };

        public CheckCommand()
            : base("check", "Force a check to run without the polling service.")
        {
            Aliases.Add("force");
            Arguments.Add(Webcomic);

            Action = CommandHandler.Create<ParseResult, IHost>(ExecuteAsync);
        }

        private async Task<int> ExecuteAsync(ParseResult result, IHost host)
        {
            var value = result.GetValue(Webcomic);

            if (value != null)
            {
                var data = host.Services.GetRequiredService<DataService>();

                var webcomic = data.Get<Webcomic>(x => x.Id == value || x.Name.ToLower() == value.ToLower()).SingleOrDefault();
                if (webcomic == null)
                {
                    await Console.Error.WriteLineAsync($"The value `{value}` does not reference a configured webcomic.");
                    return 1;
                }

                // Execute check here
                await Console.Out.WriteLineAsync($"Execute check for {webcomic.Name} ({webcomic.Id})");
                return 0;
            }

            await Console.Out.WriteLineAsync($"Execute checks for all configured comics.");
            return 0;
        }
    }
}
