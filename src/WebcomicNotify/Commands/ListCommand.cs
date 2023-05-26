using Microsoft.Extensions.Logging;

namespace WebcomicNotify.Commands;

public class ListCommand : ConsoleAppBase
{
    private readonly ILogger _logger;

    public ListCommand(ILogger<ListCommand> logger)
    {
        _logger = logger;
    }

    [Command(new[] { "list", "ls" }, "Show a list of all configured webcomics.")]
    public Task ListAsync()
    {
        return Task.CompletedTask;
    }

    [Command(new[] { "list", "ls" }, "Show information about a specific configured webcomic.")]
    public Task ListAsync(string name)
    {
        return Task.CompletedTask;
    }
}
