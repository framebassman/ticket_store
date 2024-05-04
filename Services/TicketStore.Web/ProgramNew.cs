using System.Net;
using Sentry.Extensibility;
using Serilog;
using TicketStore.Data;
using TicketStore.Web.Model;

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
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Host.UseSerilog();
    builder.Services.AddControllers();
    builder.Services.AddSpaStaticFiles(config => {
        config.RootPath = "Client/build";
    });
    builder.Services.AddResponseCompression();
    builder.Services.AddTransient<AbstractCustomStuff, CustomStuff>();
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
    app.UseSpa(spa =>
    {
        spa.Options.SourcePath = "Client";
        if (app.Environment.IsDevelopment())
        {
            spa.UseProxyToSpaDevelopmentServer("http://localhost:3000/");
        }
    });
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
