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

                var dbConfig = GetDatabaseConfig(builder);
                builder.RegisterInstance(dbConfig);

                //var someSetting = GetSomeSetting();
                //builder.RegisterInstance(someSetting);


                ServicesBootstrapper.Register(builder, dbConfig);
            }, functionName);
        }

        private SomeSetting GetSomeSetting()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var configuration = configurationBuilder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("somesetting.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            var setting = configuration.Get<SomeSetting>();

            return setting;
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
                    .AddAzureKeyVault(keyVaultUrl, keyVaultClient, new DefaultKeyVaultSecretManager())
                    .AddEnvironmentVariables()
                    .Build();

                var connectionStringKey = configuration["FunkyBankConnectionStringKey"];
                var connectionString = connectionStringKey;//configuration[connectionStringKey];

                return new DatabaseConfig {ConnectionString = connectionString};

            }
            else
            {
                var connectionString = Environment.GetEnvironmentVariable("FunkyBankConnectionStringKey");
                return new DatabaseConfig {ConnectionString = connectionString};
            }
        }

        private void RegisterLogging(ContainerBuilder builder)
        {
            builder.RegisterInstance(new LoggerFactory()).As<ILoggerFactory>();
            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>)).SingleInstance();
        }
    }

    public class SomeSetting
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}