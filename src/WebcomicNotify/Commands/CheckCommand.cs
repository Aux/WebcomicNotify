using Microsoft.Extensions.Logging;

namespace WebcomicNotify.Commands;

public class CheckCommand : ConsoleAppBase
{
    private readonly ILogger _logger;

    public CheckCommand(ILogger<CheckCommand> logger)
    {
        _logger = logger;
    }

    [Command(new[] { "check", "force" }, "Skip the polling service's queue and force a poll to run.")]
    public Task CheckAsync()
    {
        return Task.CompletedTask;
    }

    [Command(new[] { "check", "force" }, "Skip the polling service's queue and force a poll to run for a specific webcomic.")]
    public Task CheckAsync(string name)
    {
        return Task.CompletedTask;
    }
}
