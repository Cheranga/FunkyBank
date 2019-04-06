using System;
using System.Linq;
using System.Reflection;
using DbUp;

namespace FunkyBank.DataAccess.Migrations.DbUp
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args == null || !args.Any())
            {
                throw new Exception("Please pass the connection string as an argument");
            }


            var connectionString = args.First();

            Console.WriteLine($"Connection String Passed: {connectionString}");

            Console.WriteLine($"Connection String: {connectionString}");

            EnsureDatabase.For.SqlDatabase(connectionString);

            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToAutodetectedLog()
                    .Build();

            var result = upgrader.PerformUpgrade();

            return result.Successful ? 0 : -1;
        }
    }
}
