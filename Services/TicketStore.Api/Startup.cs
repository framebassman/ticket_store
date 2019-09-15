﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DinkToPdf;
using DinkToPdf.Contracts;
using TicketStore.Api.Middlewares;
using TicketStore.Api.Model.Email;
using TicketStore.Data;
using TicketStore.Api.Model.Validation;
using TicketStore.Api.Model;
using AspNetCore.Yandex.ObjectStorage;
using TicketStore.Api.Model.Poster;

namespace TicketStore.Api
{
    public class Startup
    {
        private readonly ILogger _log;
        
        public Startup(IHostingEnvironment env, ILogger<Startup> log)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            Environment = env;
            _log = log;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(opt => opt.LowercaseUrls = true);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddTransient<ITicketFinder, TicketFinder>();
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            services.AddTransient<PosterUpdater, PosterUpdater>();
            services.AddTransient<PosterReader, PosterReader>();
            services.AddYandexObjectStorage(options =>
            {
                options.Protocol = "https";
                options.BucketName = "igor-test";
                options.Location = "ru-central1";
                options.Endpoint = "storage.yandexcloud.net";
                options.AccessKey = "grkCpJmlPZxBpysw-D5H";
                options.SecretKey = "ar50IbK41nNvW4_QaCsyh_8Fd9AZsO2nvKWf6Fp9";
            });
            services
                .AddDbContext<ApplicationContext>()
                .BuildServiceProvider();
            services.AddSingleton(Configuration);
            services.AddSingleton<IConverter>(new SynchronizedConverter(new PdfTools()));
            services.AddSingleton(new EmailStrategy(Environment, Configuration, _log).Service());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<HealthCheckMiddleware>();
            app.UseWhen(x => x.Request.Path.StartsWithSegments("/api/verify", StringComparison.OrdinalIgnoreCase),
                builder => builder.UseMiddleware<AuthorizationMiddleware>());
            app.UseMvc();
        }
    }
}
