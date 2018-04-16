using Microsoft.WindowsAzure.Storage.Table;

namespace ArvidNyden.Api.Rss.Application.Entities
{
    public class RssItemTableDto : TableEntity
    {
        public RssItemTableDto()
        {
        }

        public RssItemTableDto(string feedName, string publishedDates)
        {
            PartitionKey = feedName;
            RowKey = publishedDates;
        }

        public string Link { get; set; }
        public string Summary { get; set; }
        public string Title { get; set; }

    }
}
