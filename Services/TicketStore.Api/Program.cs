using System.Net;
using AspNetCore.Yandex.ObjectStorage.Extensions;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Sentry.Extensibility;
using Serilog;
using TicketStore.Api.Model;
using TicketStore.Api.Model.Email;
using TicketStore.Api.Model.Payment.YandexMoney;
using TicketStore.Api.Model.PdfDocument.Model.BarcodeConverters;
using TicketStore.Api.Model.Poster;
using TicketStore.Api.Model.Validation;
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
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Host.UseSerilog();
    builder.Services.AddControllers();
    builder.Services.AddResponseCompression();
    builder.Services.AddRouting(opt => opt.LowercaseUrls = true);
    builder.Services.AddTransient<ITicketFinder, TicketFinder>();
    builder.Services.AddTransient<IDateTimeProvider, DateTimeProvider>();
    builder.Services.AddTransient<IGuidProvider, GuidProvider>();
    builder.Services.AddTransient<IPosterUpdater, PosterUpdater>();
    builder.Services.AddTransient<IPosterDbUpdater, PosterDbUpdater>();
    builder.Services.AddTransient<IPosterReader, PosterReader>();
    builder.Services.AddYandexObjectStorage(options =>
    {
        options.Protocol = configuration.GetSection("YandexObjectStorage").GetSection("Protocol").Value;
        options.Endpoint = configuration.GetSection("YandexObjectStorage").GetSection("Endpoint").Value;
        options.Location = configuration.GetSection("YandexObjectStorage").GetSection("Location").Value;
        options.BucketName = configuration.GetSection("YandexObjectStorage").GetSection("BucketName").Value;
        options.AccessKey = configuration.GetSection("YandexObjectStorage").GetSection("AccessKey").Value;
        options.SecretKey = configuration.GetSection("YandexObjectStorage").GetSection("SecretKey").Value;
        
    });
    builder.Services.AddDbContext<ApplicationContext>();
    builder.Services.AddSingleton<PdfTools>();
    builder.Services.AddSingleton<IConverter, SynchronizedConverter>();
    builder.Services.AddHttpClient();
    builder.Services.AddTransient<Converter>();
    if (builder.Environment.IsEnvironment("Test"))
    {
        builder.Services.AddSingleton<EmailService, FakeSenderService>();
        builder.Services.AddTransient<IPaymentValidator, DummyValidator>();
    }
    else
    {
        builder.Services.AddSingleton<EmailService, YandexService>();
        builder.Services.AddTransient<IPaymentValidator, Validator>();
    }
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
    app.UseRouting();
    app.UseWhen(
        x => x.Request.Path.StartsWithSegments("/api/verify", StringComparison.OrdinalIgnoreCase),
        conf => conf.UseMiddleware<AuthorizationMiddleware>()
    );
    app.UseSentryTracing();
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");
    app.MapHealthChecks("/healthcheck");
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