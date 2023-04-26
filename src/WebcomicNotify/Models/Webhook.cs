namespace WebcomicNotify.Models
{
    public class Webhook : IEntity
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public List<Webcomic>? Webcomics { get; set; }

        public Webhook(string id, string url)
        {
            Id = id;
            Url = url;
        }
    }
}
