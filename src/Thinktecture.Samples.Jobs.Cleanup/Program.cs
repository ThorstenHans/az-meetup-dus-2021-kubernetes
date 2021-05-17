using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Thinktecture.Samples.Configuration.Extensions;
using Thinktecture.Samples.Entities;

namespace Thinktecture.Samples.Jobs.Cleanup
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = GetConfiguration();
            try
            {
                var cfg = configuration.GetAPIConfig();
                var contextOptions = new DbContextOptionsBuilder<DemoContext>()
                    .UseSqlServer(cfg.DatabaseConnectionString)
                    .Options;

                using var ctx = new DemoContext(contextOptions);
                var oldLogs = ctx
                    .AuditLogs
                    .Where(al => al.TimeStamp < DateTime.UtcNow.AddDays(cfg.AuditLogRetentionDays));

                ctx.RemoveRange(oldLogs);
                ctx.SaveChanges();
                Console.WriteLine("Audit Log cleaned up.");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"ERROR while removing old AuditLogs.");
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
