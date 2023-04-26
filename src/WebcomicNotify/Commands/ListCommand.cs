using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using WebcomicNotify.Models;
using WebcomicNotify.Services;

namespace WebcomicNotify.Commands
{
    public class ListCommand : CliCommand
    {
        public CliOption<string> Query =
            new("--query", "-q") { Description = "Limit results to comics related to the query text." };

        public ListCommand()
            : base("list", "Show a list of all configured webcomics.") 
        {
            Aliases.Add("ls");
            Options.Add(Query);

            Action = CommandHandler.Create<ParseResult, IHost>(ExecuteAsync);
        }

        private async Task<int> ExecuteAsync(ParseResult result, IHost host)
        {
            var data = host.Services.GetRequiredService<DataService>();
            var query = result.GetValue(Query);

            string? queryPart = null;
            IEnumerable<Webcomic> webcomics;
            if (query != null)
            {
                webcomics = data.GetSimilar(query);
                queryPart = $" like `{query}`";
            }
            else
                webcomics = data.Get<Webcomic>(x => true, limit: 25);

            await Console.Out.WriteLineAsync($"Found {webcomics.Count()} comics{queryPart}:");

            foreach (var comic in webcomics)
                await Console.Out.WriteLineAsync($"({comic.Id}) {comic.Name}");

            return 0;
        }
    }
}
