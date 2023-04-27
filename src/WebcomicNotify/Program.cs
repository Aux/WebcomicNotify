using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.CommandLine.Hosting;
using WebcomicNotify;
using WebcomicNotify.Services;

var hostBuilder = Host.CreateDefaultBuilder()
    .ConfigureLogging(options =>
    {
        options.AddSimpleConsole(options => options.SingleLine = true);
    })
    .ConfigureServices(services =>
    {
        services.AddSingleton<DataService>();
        services.AddTransient<FeedFinderService>();
    });

var cli = new CliConfiguration(new RootCommand())
{
    ProcessTerminationTimeout = null
}.UseHost(_ => hostBuilder);

// There's no way to disable cli.ProcessTerminationTimeout so we
// have to do this mess to run the host service.
var result = cli.Parse(args);

if (result.CommandResult.Command is not RootCommand root)
    await result.InvokeAsync();
else
{
    // Executing the root command directly disables help, so we
    // return 2 if it detects the help command arguments
    int r = await root.ExecuteAsync(result, hostBuilder.Build());
    if (r == 2) cli.Invoke(args);
}