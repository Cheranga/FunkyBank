using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Autofac;
using FunkyBank.Core;
using FunkyBank.DataAccess.Core;
using FunkyBank.DataAccess.Core.Interfaces;
using FunkyBank.DataAccess.Dapper.Repositories;
using Microsoft.Extensions.Configuration;

namespace FunkyBank.DataAccess.Dapper
{
    public class DataAccessBootstrapper
    {
        public static void Register(ContainerBuilder builder, DatabaseConfig config)
        {
            builder.RegisterType<DbConnectionFactory>().As<IDbConnectionFactory>();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>().WithParameter("config", config);
        }
    }
}
