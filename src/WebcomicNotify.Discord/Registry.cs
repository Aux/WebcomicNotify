using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using WebcomicNotify.Discord.Services;

namespace WebcomicNotify.Discord;

public static class Registry
{
    public static void AddDiscord(this IServiceCollection services)
    {
        services.AddSingleton(new DiscordSocketClient(new()
        {
            GatewayIntents = GatewayIntents.Guilds | GatewayIntents.GuildWebhooks,
            LogLevel = LogSeverity.Warning,
            AlwaysDownloadUsers = false,
            AlwaysResolveStickers = false,
            LogGatewayIntentWarnings = false,
            MessageCacheSize = 0,
            LargeThreshold = 0
        }));
        services.AddSingleton(services =>
        {
            var discord = services.GetRequiredService<DiscordSocketClient>();
            return new InteractionService(discord, new()
            {
                LogLevel = LogSeverity.Warning,
                DefaultRunMode = RunMode.Async,
                EnableAutocompleteHandlers = true
            });
        });

        services.AddHostedService<DiscordStartupService>();
        services.AddHostedService<InteractionHandlingService>();
    }
}
