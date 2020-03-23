namespace AwesomeBank.Migrations
{
    using System;
    using System.Reflection;
    using DbUp;
    using Microsoft.Extensions.Configuration;

    public class Program
    {
        private const string AppSettingsJsonFilePath = "appsettings.json";
        private const string AwesomeBankDatabaseName = "AwesomeBankDatabase";

        public static int Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile(AppSettingsJsonFilePath);
            var configurationRoot = builder.Build();
            var connectionString = configurationRoot.GetConnectionString(AwesomeBankDatabaseName);

            EnsureDatabase.For.SqlDatabase(connectionString);

            var upgradeEngine = DeployChanges.To
                .SqlDatabase(connectionString)
                .WithoutTransaction()
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToConsole()
                .Build();

            var result = upgradeEngine.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();

#if DEBUG
                Console.ReadLine();
#endif
                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            Console.ReadLine();
            return 0;
        }
    }
}