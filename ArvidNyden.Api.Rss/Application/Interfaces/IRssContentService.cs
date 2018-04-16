using System.Collections.Generic;
using System.Threading.Tasks;
using ArvidNyden.Api.Rss.Application.Entities;

namespace ArvidNyden.Api.Rss.Application.Interfaces
{
    public interface IRssContentService
    {
        Task<List<RssItemTableDto>> ListLatest();
    }
}