using System;
using System.IO;
using System.Linq;
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

                builder.RegisterInstance(config);


                ServicesBootstrapper.Register(builder, config);
            }, functionName);
        }

        private DatabaseConfig GetDatabaseConfig(ContainerBuilder builder)
        {
            var environment =  Environment.GetEnvironmentVariable("FunctionAppEnvironment");
            
            if (string.Equals(environment, "Production"))
            {
                var keyVaultUrl = Environment.GetEnvironmentVariable("KeyVaultUrl");
                var tokenProvider = new AzureServiceTokenProvider();
                var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(tokenProvider.KeyVaultTokenCallback));

                var configurationBuilder = new ConfigurationBuilder();
                var configuration = configurationBuilder
                    //.SetBasePath(Directory.GetCurrentDirectory())
                    //.AddJsonFile("appsettings.json")
                    .AddAzureKeyVault(keyVaultUrl, keyVaultClient, new DefaultKeyVaultSecretManager())
                    .AddEnvironmentVariables()
                    .Build();

                var connectionStringKey = configuration["FunkyBankConnectionStringKey"];
                var connectionString = configuration[connectionStringKey];

                return new DatabaseConfig {ConnectionString = connectionString};

            }
            else
            {
                var connectionString = Environment.GetEnvironmentVariable("FunkyBankConnectionStringKey");
                return new DatabaseConfig {ConnectionString = connectionString};
            }

           

            //var connectionString = string.Empty;
            //var keyVaultUrl = configuration.GetValue<string>("KeyVaultUrl");
            //if (string.IsNullOrEmpty(keyVaultUrl))
            //{
            //    connectionString = configuration.GetValue<string>("FunkyBankConnectionStringKey");

            //    return new DatabaseConfig
            //    {
            //        ConnectionString = connectionString
            //    };
            //}
            ////
            //// Get the connection string from AKV
            ////
            //var tokenProvider = new AzureServiceTokenProvider();
            //var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(tokenProvider.KeyVaultTokenCallback));

            //var secret = keyVaultClient.GetSecretAsync($"{keyVaultUrl}secrets/FunkyBankConnectionString").Result;
            //if (secret == null || string.IsNullOrEmpty(secret.Value))
            //{
            //    throw new Exception("Cannot get the secret value");
            //}

            ////configurationBuilder.AddAzureKeyVault(keyVaultUrl, keyVaultClient, new DefaultKeyVaultSecretManager());

            ////connectionString = configuration["FunkyBankConnectionStringKey"];

            //connectionString = secret.Value;

            //if (string.IsNullOrEmpty(connectionString))
            //{
            //    throw new ArgumentNullException("Connection string is empty or null");
            //}

            //return new DatabaseConfig
            //{
            //    ConnectionString = connectionString
            //};
        }

        private void RegisterLogging(ContainerBuilder builder)
        {
            builder.RegisterInstance(new LoggerFactory()).As<ILoggerFactory>();
            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>)).SingleInstance();
        }
    }
}