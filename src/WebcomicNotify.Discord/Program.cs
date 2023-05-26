using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebcomicNotify;
using WebcomicNotify.Discord;
using ZLogger;

var app = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config =>
    {
        config.AddYamlFile("_config.yml", false);
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddZLoggerConsole();
        logging.AddZLoggerRollingFile((dt, x) => $"logs/discord/{dt.ToLocalTime():yyyy-MM-dd}_{x:000}.log", x => x.ToLocalTime().Date, 1024);
    })
    .ConfigureServices(services =>
    {
        services.AddWebcomicServices();
        services.AddDiscord();
    })
    .Build();

await app.RunAsync();