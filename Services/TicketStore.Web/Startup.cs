using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Robotify.AspNetCore;
using TicketStore.Data;
using TicketStore.Web.Middlewares;
using TicketStore.Web.Model;

namespace TicketStore.Web
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
            services.AddResponseCompression();
//            services.AddRobotify(c => c.AddRobotGroupsFromAppSettings());
            services.AddHealthChecks();
            services.AddControllers();
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            services
                .AddDbContext<ApplicationContext>()
                .BuildServiceProvider();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "Client/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseResponseCompression();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseRewriter(new RewriteOptions()
                .AddRedirect("index.html", "/"));
//            app.UseRobotify(c => c
//                .WithSitemap(new Uri("https://chertopolokh.ru/sitemap"))
//                .WithCrawlDelay(10)
//            );
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapHealthChecks("/healthcheck");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "Client";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
