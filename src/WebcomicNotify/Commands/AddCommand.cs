using Microsoft.Extensions.Logging;

namespace WebcomicNotify.Commands;

[Command(new[] { "add", "new" }, "Add a webcomic to the polling service.")]
public class AddCommand : ConsoleAppBase
{
    private readonly ILogger _logger;

    public AddCommand(ILogger<AddCommand> logger)
    {
        _logger = logger;
    }

    public Task BaseAsync(string url, string? webhookUrl = null)
    {
        return Task.CompletedTask;
    }

    [Command("webtoons")]
    public Task QueryWebtoonsAsync(string query)
    {
        return Task.CompletedTask;
    }

    [Command("asurascans")]
    public Task QueryAsurascansAsync(string query)
    {
        return Task.CompletedTask;
    }

    [Command("flamescans")]
    public Task QueryFlamescansAsync(string query)
    {
        return Task.CompletedTask;
    }
}
