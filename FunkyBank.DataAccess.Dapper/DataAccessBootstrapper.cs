using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Autofac;
using FunkyBank.DataAccess.Core;
using FunkyBank.DataAccess.Core.Interfaces;
using FunkyBank.DataAccess.Dapper.Repositories;
using Microsoft.Extensions.Configuration;

namespace FunkyBank.DataAccess.Dapper
{
    public class DataAccessBootstrapper
    {
        public static void Register(ContainerBuilder builder)
        {
            //
            // Load the configuration information and, get the connection string
            //
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetValue<string>("DatabaseConnection");

            if (string.IsNullOrWhiteSpace(
                connectionString))
            {
                throw new Exception("Error: The connection string property is not set in the configuration");
            }

            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>().WithParameter("config", new DatabaseConfig
            {
                ConnectionString = connectionString
            });

        }
    }
}
