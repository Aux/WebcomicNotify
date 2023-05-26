using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebcomicNotify;
using ZLogger;

var app = ConsoleApp.CreateBuilder(args, options =>
    {
        options.ApplicationName = "Webcomic Notify";
        options.NameConverter = x => x.ToLower();
    })
    .ConfigureAppConfiguration(config =>
    {
        config.AddYamlFile("_config.yml", false);
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddZLoggerConsole();
        logging.AddZLoggerRollingFile((dt, x) => $"logs/console/{dt.ToLocalTime():yyyy-MM-dd}_{x:000}.log", x => x.ToLocalTime().Date, 1024);
    })
    .ConfigureServices(services =>
    {
        services.AddWebcomicServices();
    })
    .Build();

app.AddAllCommandType();

await app.RunAsync();
