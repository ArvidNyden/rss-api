using System;
using System.Linq;
using System.Threading.Tasks;
using ArvidNyden.Api.Rss.Application.Interfaces;
using ArvidNyden.Api.Rss.Infrastrucutre.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ArvidNyden.Api.Rss.Infrastrucutre
{
    public class RssImageService : IRssImageService
    {
        private readonly CloudBlobClient client;
        private readonly string containerName;

        public RssImageService(RssImageConfiguration configuration)
        {
            if (string.IsNullOrEmpty(configuration?.ContainerName))
                throw new ArgumentNullException(nameof(configuration.ContainerName));

            if (!CloudStorageAccount.TryParse(configuration.StorageConnectionString, out var storagAccount))
                throw new Exception($"Unable to parse connection string");

            client = storagAccount.CreateCloudBlobClient();
            containerName = configuration.ContainerName;
        }

        public async Task<string> GetImageUri(string contentLink)
        {
            var containerReferences = client.GetContainerReference(containerName);

            if(!await containerReferences.ExistsAsync())
                throw new ArgumentException($"Container {containerName} does not exists", nameof(RssImageConfiguration.ContainerName));

            var imageName = new Uri(contentLink).Segments.Last();

            var imageBlob = containerReferences.GetBlobReference(imageName);

            return imageBlob.Uri.ToString();
        }
    }
}

