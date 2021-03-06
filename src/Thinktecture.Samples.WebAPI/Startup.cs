using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Thinktecture.Samples.Configuration.Extensions;
using Thinktecture.Samples.Entities;
using Thinktecture.Samples.WebAPI.Repositories;
using Thinktecture.Samples.WebAPI.Services;

namespace Thinktecture.Samples.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var config = Configuration.GetAPIConfig();
            services.AddSingleton(config);
            
            services.AddAutoMapper(GetType().Assembly);
            
            services
                .AddScoped<IAudiencesRepository, AudiencesRepository>()
                .AddScoped<ISpeakersRepository, SpeakersRepository>()
                .AddScoped<ISessionsRepository, SessionsRepository>()
                .AddScoped<IAuditLogRepository, AuditLogRepository>()
                .AddScoped<IAudiencesService, AudiencesService>()
                .AddScoped<ISpeakersService, SpeakersService>()
                .AddScoped<ISessionsService, SessionsService>();

            services.AddDbContext<DemoContext>(setup => 
                setup.UseSqlServer(config.DatabaseConnectionString));
            
            services.AddResponseCompression(setup =>
            {
                var options = new GzipCompressionProviderOptions()
                {
                    Level = CompressionLevel.Fastest
                };
                setup.Providers.Clear();
                setup.Providers.Add(new GzipCompressionProvider(options));
            });
            
            services.AddHealthChecks();
            services.AddControllers();
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo {Title = "Azure Meetup DUS", Version = "v1"});
                setup.EnableAnnotations();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/error-development");
            }
            else
            {
                app.UseExceptionHandler("/error");
            }
            
            // swagger exposure: expose swagger UI only for live demo
            app.UseSwagger();
            app.UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Thinktecture.Samples.WebAPI v1"));
            // end swagger exposure

            app.UseResponseCompression();
            
            // .NET is not responsible for configuring HTTPS 
            // app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { 
                endpoints.MapControllers(); 
                // add support for Kubernetes probes (liveness and readiness)
                endpoints.MapHealthChecks("/readiness");
                endpoints.MapHealthChecks("/liveness");
            });
        }
    }
}
