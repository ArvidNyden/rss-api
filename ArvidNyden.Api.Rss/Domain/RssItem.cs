namespace ArvidNyden.Api.Rss.Domain
{
    public class RssItem
    {
        public string FeedName { get; set; }
        public string PublishedDate { get; set; }
        public string SourceUri { get; set; }
        public string Summary { get; set; }
        public string Title { get; set; }
        public string ImageUri { get; set; }
    }
}
