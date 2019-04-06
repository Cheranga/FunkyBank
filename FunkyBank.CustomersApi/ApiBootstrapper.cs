using Autofac;
using AzureFunctions.Autofac.Configuration;
using FunkyBank.Services;
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

                ServicesBootstrapper.Register(builder);
            }, functionName);
        }

        private void RegisterLogging(ContainerBuilder builder)
        {
            builder.RegisterInstance(new LoggerFactory()).As<ILoggerFactory>();
            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>)).SingleInstance();
        }
    }
}