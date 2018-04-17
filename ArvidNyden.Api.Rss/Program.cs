using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace ArvidNyden.Api.Rss
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseApplicationInsights()
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    var configuration = config.Build();
                    var keyVault = configuration.GetValue<string>("KeyVaultUri");
                    config.AddAzureKeyVault(keyVault, GetKeyVaultClient(), new DefaultKeyVaultSecretManager());
                })
                .Build();

        private static KeyVaultClient GetKeyVaultClient()
        {
            return new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(
                new AzureServiceTokenProvider().KeyVaultTokenCallback));
        }
    }
}
