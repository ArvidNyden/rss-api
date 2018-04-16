using System;
using System.Threading.Tasks;
using ArvidNyden.Api.Rss.Infrastrucutre.Configuration;
using ArvidNyden.Api.Rss.Infrastrucutre.Interfaces;
using Microsoft.Azure.KeyVault;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.Azure.Services.AppAuthentication;

namespace ArvidNyden.Api.Rss.Infrastrucutre
{
    public sealed class StorageSecurityService : IStorageSecurityService
    {
        private const string KeyVaultStorageConnectionStringName = "StorageConnectionString";
        private static string keyVaultUri;

        public StorageSecurityService(KeyVaultConfiguration config)
        {
            keyVaultUri = config.KeyVaultUri;
        }

        public CloudBlobClient GetBlobClient()
        {
            return StorageAccount.CreateCloudBlobClient();
        }

        public CloudTableClient GetTableClient()
        {
            return StorageAccount.CreateCloudTableClient();
        }

        private CloudStorageAccount storageAccount;
        private CloudStorageAccount StorageAccount
        {
            get
            {
                if (storageAccount != null) return storageAccount;

                using (var kvClient = GetKeyVaultClient())
                {
                    storageAccount = GetCloudStorageAccount(kvClient).Result;
                }
                return storageAccount;
            }
        }

        private static async Task<CloudStorageAccount> GetCloudStorageAccount(IKeyVaultClient kvClient)
        {
            var connectionString = await kvClient.GetSecretAsync(keyVaultUri, KeyVaultStorageConnectionStringName);

            if (!CloudStorageAccount.TryParse(connectionString.Value, out var storagAccount))
                throw new Exception($"Unable to parse connection string {connectionString}");

            return storagAccount;
        }

        private static KeyVaultClient GetKeyVaultClient()
        {
            return new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(
                new AzureServiceTokenProvider().KeyVaultTokenCallback));
        }
    }
}
