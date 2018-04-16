using System.Threading.Tasks;

namespace ArvidNyden.Api.Rss.Infrastrucutre.Interfaces
{
    public interface IRssImageService
    {
        Task<string> GetImageUri(string contentPath);
    }
}