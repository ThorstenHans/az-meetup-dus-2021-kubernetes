using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Thinktecture.Samples.Configuration.Extensions;

namespace Thinktecture.Samples.Entities
{
    // ReSharper disable once UnusedType.Global
    public class DesignTimeContextFactory: IDesignTimeDbContextFactory<DemoContext>
    {
        public DemoContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.AddEnvironmentVariables();
            var configuration = builder.Build();

            var cfg = configuration.GetAPIConfig();
            
            var optionsBuilder = new DbContextOptionsBuilder<DemoContext>();
            optionsBuilder.UseSqlServer(cfg.DatabaseConnectionString);

            return new DemoContext(optionsBuilder.Options);
        }
    }
}
