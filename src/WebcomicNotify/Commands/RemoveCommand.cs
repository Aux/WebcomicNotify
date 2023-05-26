using Microsoft.Extensions.Logging;

namespace WebcomicNotify.Commands;

public class RemoveCommand : ConsoleAppBase
{
    private readonly ILogger _logger;

    public RemoveCommand(ILogger<RemoveCommand> logger)
    {
        _logger = logger;
    }

    [Command(new[] { "remove", "rem", "delete", "del" }, "Remove a configured webcomic from the polling service.")]
    public Task RemoveAsync(string name)
    {
        return Task.CompletedTask;
    }
}
