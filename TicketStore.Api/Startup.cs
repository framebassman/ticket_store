using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using DinkToPdf;
using DinkToPdf.Contracts;
using TicketStore.Api.Data;
using TicketStore.Api.Middlewares;
using TicketStore.Api.Model.Email;

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
            _log = log;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(opt => opt.LowercaseUrls = true);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<ApplicationContext>(
                    options => options.UseNpgsql(
                        Configuration.GetConnectionString("DefaultConnection")
                    )
                )
                .BuildServiceProvider();
            services.AddSingleton(Configuration);
            services.AddSingleton<IConverter>(new SynchronizedConverter(new PdfTools()));
            services.AddSingleton(
                new YandexService(Configuration.GetValue<String>("EmailSenderPassword"), _log));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<HealthCheckMiddleware>();
            // app.Map("/api/verify", branchedApp => {
            //     branchedApp.UseMiddleware<AuthorizationMiddleware>();
            // });
            app.UseMvc();
        }
    }
}
