using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using FunkyBank.DataAccess.Core;
using FunkyBank.DataAccess.Dapper;

namespace FunkyBank.Services
{
    public class ServicesBootstrapper
    {
        public static void Register(ContainerBuilder builder, DatabaseConfig config)
        {
            builder.RegisterType<CustomerService>().As<ICustomerService>();

            DataAccessBootstrapper.Register(builder, config);
        }
    }
}
