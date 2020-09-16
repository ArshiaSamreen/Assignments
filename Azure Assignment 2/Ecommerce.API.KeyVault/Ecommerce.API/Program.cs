using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Azure.Identity;

namespace Ecommerce.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            //BuildWebHost(args).Run();
        }

        //public static IWebHost BuildWebHost(string[] args) =>
        //WebHost.CreateDefaultBuilder(args)
        //.UseStartup<Startup>()
        //.ConfigureAppConfiguration((ctx, builder) =>
        //{
        //     //Build the config from sources we have
        //     var config = builder.Build();
        //     //Create Managed Service Identity token provider
        //     var tokenProvider = new AzureServiceTokenProvider();
        //     //Create the Key Vault client
        //     var kvClient = new KeyVaultClient((authority, resource, scope) => tokenProvider.KeyVaultTokenCallback(authority, resource, scope));
        //     //Add Key Vault to configuration pipeline
        //     builder.AddAzureKeyVault(config["KeyVault:BaseUrl"], kvClient, new DefaultKeyVaultSecretManager());
        //})
        //.Build();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
.ConfigureAppConfiguration((context, config) =>
{
var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
config.AddAzureKeyVault(
keyVaultEndpoint,
new DefaultAzureCredential());
})
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
