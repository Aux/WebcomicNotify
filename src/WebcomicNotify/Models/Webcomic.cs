namespace WebcomicNotify.Models
{
    public enum WebcomicType
    {
        Other,
        Webtoons,
        Flamescans,
        Asurascans
    }

    public class Webcomic : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string RssUrl { get; set; }
        public WebcomicType Type { get; set; } = WebcomicType.Other;
        public Webhook? CustomWebhook { get; set; }

        public Webcomic(string id, string name, string rssUrl)
        {
            Id = id;
            Name = name;
            RssUrl = rssUrl;
        }
    }
}
