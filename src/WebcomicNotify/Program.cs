using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.CommandLine.Hosting;
using WebcomicNotify;
using WebcomicNotify.Services;

var hostBuilder = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddSingleton<DataService>();
    });

if (args.Length != 0)
{
    var cli = new CliConfiguration(new RootCommand())
        .UseHost(_ => hostBuilder.ConfigureLogging(options =>
        {
            options.AddFilter("Microsoft.Hosting.Lifetime", LogLevel.None);
        }));

    await cli.InvokeAsync(args);
} 
else
{
    await hostBuilder
        .Build()
        .RunAsync();
}