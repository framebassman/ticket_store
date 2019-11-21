using System;
using AspNetCore.Yandex.ObjectStorage;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicketStore.Api.Middlewares;
using TicketStore.Api.Model;
using TicketStore.Api.Model.Email;
using TicketStore.Api.Model.Poster;
using TicketStore.Api.Model.Validation;
using TicketStore.Data;

namespace TicketStore.Api
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(opt => opt.LowercaseUrls = true);
            services.AddTransient<ITicketFinder, TicketFinder>();
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            services.AddTransient<IGuidProvider, GuidProvider>();
            services.AddTransient<IPosterUpdater, PosterUpdater>();
            services.AddTransient<IPosterDbUpdater, PosterDbUpdater>();
            services.AddTransient<IPosterReader, PosterReader>();
            services.AddYandexObjectStorage(Configuration);
            services
                .AddDbContext<ApplicationContext>()
                .BuildServiceProvider();
            services.AddSingleton<IConverter>(new SynchronizedConverter(new PdfTools()));
            services.AddHealthChecks();
            services.AddControllers();
            services.AddHttpClient();
            if (Environment.IsEnvironment("Test"))
            {
                services.AddSingleton<EmailService, FakeSenderService>();
            }
            else
            {
                services.AddSingleton<EmailService, YandexService>();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseWhen(x => x.Request.Path.StartsWithSegments("/api/verify", StringComparison.OrdinalIgnoreCase),
                builder => builder.UseMiddleware<AuthorizationMiddleware>());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/healthcheck");
            });
        }
    }
}
