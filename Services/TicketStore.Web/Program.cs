using System;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.Net.Http.Headers;
using Sentry;
using Sentry.Extensibility;
using Serilog;
using Log = Serilog.Log;

namespace TicketStore.Web
{
    public class ProgramOld
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(BuildConfiguration())
                .CreateLogger();
            try
            {
                Log.Logger.Information("Getting started...");
                Log.Logger.Information($"Environment: {CurrentEnv()}");
                var app = CreateHostBuilder(args).Build();
                // app.MapHealthChecks("/healthcheck");
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
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseStartup<StartupOld>()
                        .UseSentry(options =>
                            {
                                options.Environment = CurrentEnv();
                                options.MaxQueueItems = 100;
                                options.ShutdownTimeout = TimeSpan.FromSeconds(5);
                                options.DecompressionMethods = DecompressionMethods.None;
                                options.MaxRequestBodySize = RequestSize.Always;
                                options.Release = Environment.GetEnvironmentVariable("SENTRY_RELEASE");
                            }
                        );
                })
                .UseSerilog();
        
        private static IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{CurrentEnv()}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        private static string CurrentEnv()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
        }
    }
}
