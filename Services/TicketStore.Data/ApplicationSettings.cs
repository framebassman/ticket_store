using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace TicketStore.Data
{
    public class ApplicationSettings
    {
        private readonly string _environmentName;

        public ApplicationSettings()
        {
            _environmentName = Environment.GetEnvironmentVariable(
                                   "ASPNETCORE_ENVIRONMENT",
                                   EnvironmentVariableTarget.Process)
                               ?? "Development";
        }

        public string ConnectionString()
        {
            return new ConnectionString(
                BuildConfiguration().GetConnectionString("DefaultConnection")
            ).Value();
        }

        private IConfiguration BuildConfiguration()
        {
            Console.WriteLine($"[TicketStore.Data] Environment: {_environmentName}");
            return new ConfigurationBuilder()
                .SetBasePath(CalculateBasePath())
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{_environmentName}.json", true)
                .AddEnvironmentVariables()
                .Build();
        }

        private string CalculateBasePath()
        {
            var dirname = Path.Combine(AbsolutePathOfProject(), "DbConfigs");
            var info = new DirectoryInfo(dirname);
            if (!info.Exists)
            {
                Console.WriteLine("There is no directory via address {0}", info.FullName);
                throw new FileNotFoundException("There is no file via address {0}", info.FullName);
            }

            return info.FullName;
        }

        private string AbsolutePathOfProject()
        {
            var prefix = "file:";
            var pathWithPrefix = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            return pathWithPrefix.TrimStart(prefix.ToCharArray());
        }
    }
}