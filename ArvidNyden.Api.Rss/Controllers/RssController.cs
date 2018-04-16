using System.Threading.Tasks;
using ArvidNyden.Api.Rss.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArvidNyden.Api.Rss.Controllers
{
    [Route("api/[controller]")]
    public class RssController : Controller
    {
        private readonly IRssFeedService rssFeedService;

        public RssController(IRssFeedService rssFeedService)
        {
            this.rssFeedService = rssFeedService;
        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await rssFeedService.List());
        }
    }
}
