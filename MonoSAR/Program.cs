using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Identity;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MonoSAR
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(ConfigConfiguration)
                .UseStartup<Startup>()
                .Build();

        static void ConfigConfiguration(WebHostBuilderContext webHostBuilderContext, IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("azurekeyvault.json", false, true)
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables();

            var config = configurationBuilder.Build();

            TokenCredential tokenCredential = new ClientSecretCredential(
               config["azureKeyVault:tenantId"],
               config["azureKeyVault:clientId"],
               config["azureKeyVault:clientSecret"]);

            configurationBuilder.AddAzureKeyVault(
                new Uri($"https://{config["azureKeyVault:vault"]}.vault.azure.net/"),
                tokenCredential);
        }
    }

    
}
