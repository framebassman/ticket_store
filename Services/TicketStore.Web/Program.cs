using System.Net;
using Elasticsearch.Net;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.Net.Http.Headers;
using Sentry.Extensibility;
using Serilog;
using TicketStore.Data;

var currentEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{currentEnv}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();
try
{
    Log.Logger.Information("Getting started...");
    Log.Logger.Information("Environment: {env}", currentEnv);
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Host.UseSerilog();
    builder.Services.AddControllers();
    builder.Services.AddSpaStaticFiles(config => {
        config.RootPath = "Client/build";
    });
    builder.Services.AddResponseCompression();
    builder.Services.AddTransient<IDateTimeProvider, DateTimeProvider>();
    builder.Services.AddDbContext<ApplicationContext>();
    builder.Services.AddHealthChecks();
    builder.WebHost.UseSentry(options =>
    {
        options.Environment = currentEnv;
        options.MaxQueueItems = 100;
        options.ShutdownTimeout = TimeSpan.FromSeconds(5);
        options.DecompressionMethods = DecompressionMethods.None;
        options.MaxRequestBodySize = RequestSize.Always;
        options.Release = Environment.GetEnvironmentVariable("SENTRY_RELEASE");
    });

    var app = builder.Build();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseSentryTracing();
    app.MapFallbackToFile("index.html");
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");
    app.MapHealthChecks("/healthcheck");


    var spaPath = "/";
    if (app.Environment.IsDevelopment())
    {
        app.MapWhen(y => y.Request.Path.StartsWithSegments(spaPath), client =>
        {
            client.UseSpa(spa =>
            {
                spa.UseProxyToSpaDevelopmentServer("https://localhost:3000");
            });
        });
    }
    else
    {
        app.Map(new PathString(spaPath), client =>
        {
            client.UseSpaStaticFiles();
            client.UseSpa(spa => {
                spa.Options.SourcePath = "Client";

                // adds no-store header to index page to prevent deployment issues (prevent linking to old .js files)
                // .js and other static resources are still cached by the browser
                spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
                {
                    OnPrepareResponse = ctx =>
                    {
                        ResponseHeaders headers = ctx.Context.Response.GetTypedHeaders();
                        headers.CacheControl = new CacheControlHeaderValue
                        {
                            NoCache = true,
                            NoStore = true,
                            MustRevalidate = true
                        };
                    }
                };
            });
        });
    }
    app.Run();
}
catch (Exception ex)
{
    Log.Logger.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
