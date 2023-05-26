namespace WebcomicNotify.Services;

public interface IScraper
{
    Task<IEnumerable<SearchResult>> SearchAsync(string text);
}
