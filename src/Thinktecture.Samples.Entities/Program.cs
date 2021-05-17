using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Thinktecture.Samples.Configuration.Extensions;

namespace Thinktecture.Samples.Entities
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = GetConfiguration();
            try
            {
                var cfg = configuration.GetAPIConfig();
                var contextOptions = new DbContextOptionsBuilder<DemoContext>()
                    .UseSqlServer(cfg.DatabaseConnectionString)
                    .Options;

                await using var ctx = new DemoContext(contextOptions);
                
                var migrationTags = ctx.Database.MigrateAsync();
                Console.WriteLine("Migrating Database...");
                while (!migrationTags.IsCompleted)
                {
                    Console.Write(".");
                    Thread.Sleep(50);
                }
                Console.WriteLine("Migration finished");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"ERROR while executing database migrations");
                Console.WriteLine(exception.Message);
                Console.WriteLine(exception.StackTrace);
            }
        }

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder();
            builder.AddEnvironmentVariables();
            return builder.Build();
        }
    }
}
