using System.Threading.Tasks;

namespace ArvidNyden.Api.Rss.Application.Interfaces
{
    public interface IRssImageService
    {
        Task<string> GetImageUri(string contentPath);
    }
}