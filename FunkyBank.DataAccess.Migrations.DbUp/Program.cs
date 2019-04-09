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

            EnsureDatabase.For.SqlDatabase(connectionString);

            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToAutodetectedLog()
                    .Build();

            if (upgrader.IsUpgradeRequired())
            {
                Console.WriteLine("Database migrations are available. Applying the necessary changes now...");
                var result = upgrader.PerformUpgrade();

                if (result.Successful)
                {
                    Console.WriteLine("Database migration was successful");
                    return 0;
                }

                Console.WriteLine("Database migration was unsuccessful");
                return -1;
            }

            return 0;
        }
    }
}
