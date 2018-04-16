using System.Threading.Tasks;
using ArvidNyden.Api.Rss.Domain;

namespace ArvidNyden.Api.Rss.Application.Interfaces
{
    public interface IRssFeedService
    {
        Task<RssList> List();
    }
}