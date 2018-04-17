using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArvidNyden.Api.Rss.Application.Entities;
using ArvidNyden.Api.Rss.Application.Interfaces;
using ArvidNyden.Api.Rss.Infrastrucutre.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace ArvidNyden.Api.Rss.Infrastrucutre
{
    public class RssTableService : IRssContentService
    {
        private readonly CloudTableClient client;
        private readonly string tableName;

        public RssTableService(RssTableConfiguration configuration)
        {
            if(string.IsNullOrEmpty(configuration?.TableName))
                throw new ArgumentNullException(nameof(configuration.TableName));

            if (!CloudStorageAccount.TryParse(configuration.StorageConnectionString, out var storagAccount))
                throw new Exception($"Unable to parse connection string");

            client = storagAccount.CreateCloudTableClient();
            tableName = configuration.TableName;
        }

        public async Task<List<RssItemTableDto>> ListLatest()
        {
            var tableClient = client.GetTableReference(tableName);
            var query = new TableQuery<RssItemTableDto>();

            var result = await tableClient.ExecuteQuerySegmentedAsync(query, new TableContinuationToken());

            return result.Results;
        }
    }
}
