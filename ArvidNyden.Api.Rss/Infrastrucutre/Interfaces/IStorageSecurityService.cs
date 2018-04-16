using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;

namespace ArvidNyden.Api.Rss.Infrastrucutre.Interfaces
{
    public interface IStorageSecurityService
    {
        CloudBlobClient GetBlobClient();
        CloudTableClient GetTableClient();
    }
}