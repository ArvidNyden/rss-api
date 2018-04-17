using System.Linq;
using System.Threading.Tasks;
using ArvidNyden.Api.Rss.Application.Interfaces;
using ArvidNyden.Api.Rss.Domain;

namespace ArvidNyden.Api.Rss.Application
{
    public class RssFeedService : IRssFeedService
    {
        private readonly IRssContentService tableService;
        private readonly IRssImageService imageService;

        public RssFeedService(IRssContentService tableService, IRssImageService imageService)
        {
            this.tableService = tableService;
            this.imageService = imageService;
        }

        public async Task<RssList> List()
        {
            var rssItems = new RssList();
            var result = await tableService.ListLatest();

            rssItems.RssItems = result.Select(item => new RssItem()
            {
                Title = item.Title,
                FeedName = item.PartitionKey,
                SourceUri = item.Link,
                PublishedDate = item.RowKey,
                Summary = item.Summary,
                ImageUri = imageService.GetImageUri(item.Link).Result
            }).ToList();

            return rssItems;
        }
    }
}
