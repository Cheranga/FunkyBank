using System.IO;
using System.Net.Http.Headers;
using Autofac;
using AzureFunctions.Autofac.Configuration;
using FunkyBank.DataAccess.Core;
using FunkyBank.Services;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Logging;

namespace FunkyBank.CustomersApi
{
    public class ApiBootstrapper
    {
        public ApiBootstrapper(string functionName)
        {
            DependencyInjection.Initialize(builder =>
            {
                RegisterLogging(builder);

                var config =  GetDatabaseConfig(builder);


                ServicesBootstrapper.Register(builder, config);
            }, functionName);
        }

        private DatabaseConfig GetDatabaseConfig(ContainerBuilder builder)
        {
            var configurationBuilder = new ConfigurationBuilder();
            var configuration = configurationBuilder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var connectionString = string.Empty;
            var keyVaultUrl = configuration.GetValue<string>("KeyVaultUrl");
            if (string.IsNullOrEmpty(keyVaultUrl))
            {
                connectionString = configuration.GetValue<string>("FunkyBankConnectionString");
                return new DatabaseConfig
                {
                    ConnectionString = connectionString
                };
            }
            //
            // Get the connection string from AKV
            //
            var akvUrl = configuration.GetValue<string>("FunkyBankConnectionString");
            var tokenProvider = new AzureServiceTokenProvider();
            var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(tokenProvider.KeyVaultTokenCallback));

            configurationBuilder.AddAzureKeyVault(akvUrl, keyVaultClient, new DefaultKeyVaultSecretManager());

            connectionString = configuration["FunkyBankConnectionString"];

            return new DatabaseConfig
            {
                ConnectionString = connectionString
            };
        }

        private void RegisterLogging(ContainerBuilder builder)
        {
            builder.RegisterInstance(new LoggerFactory()).As<ILoggerFactory>();
            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>)).SingleInstance();
        }
    }
}