using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using FunkyBank.DataAccess.Dapper;

namespace FunkyBank.Services
{
    public class ServicesBootstrapper
    {
        public static void Register(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerService>().As<ICustomerService>();

            DataAccessBootstrapper.Register(builder);
        }
    }
}
