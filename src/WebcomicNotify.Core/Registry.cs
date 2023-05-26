using Microsoft.Extensions.DependencyInjection;
using WebcomicNotify.Services;

namespace WebcomicNotify;

public static class Registry
{
    public static void AddWebcomicServices(this IServiceCollection services)
    {
        AddWebcomicScrapers(services);
        services.AddSingleton<DataService>();
        services.AddTransient<FeedFinderService>();
    }

    public static void AddWebcomicScrapers(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddHttpClient<IScraper, WordpressScraper>();
        services.AddHttpClient<IScraper, WebtoonScraper>(client => client.BaseAddress = new Uri(WebcomicConstants.WebtoonsHost));
        services.AddHttpClient<IScraper, FlamescanScraper>(client => client.BaseAddress = new Uri(WebcomicConstants.FlamescansHost));
        services.AddHttpClient<IScraper, AsurascanScraper>(client => client.BaseAddress = new Uri(WebcomicConstants.AsurascansHost));
    }
}
