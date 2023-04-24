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
using DateTimeProvider = Elasticsearch.Net.DateTimeProvider;
using IDateTimeProvider = Elasticsearch.Net.IDateTimeProvider;

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
    builder.Services.AddYandexObjectStorage(configuration);
    builder.Services.AddDbContext<ApplicationContext>();
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    builder.Services.AddSingleton<IConverter>(new SynchronizedConverter(new PdfTools()));
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