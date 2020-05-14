using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace TicketStore.Data
{
    public class ApplicationSettings
    {
        private readonly String _environmentName;
        private readonly Host _host;
        
        public ApplicationSettings()
        {
            _environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT",
                                   EnvironmentVariableTarget.Process)
                               ?? "Development";
            _host = new Host();
        }

        public String ConnectionString()
        {
            return BuildConfiguration()
                .GetConnectionString("DefaultConnection")
                .Replace("$DOCKER_HOST", _host.Value());
        }
        
        private IConfiguration BuildConfiguration()
        {
            Console.WriteLine($"[TicketStore.Data] Environment: {_environmentName}");
            return new ConfigurationBuilder()
                .SetBasePath(CalculateBasePath())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{_environmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        private String CalculateBasePath()
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

        private String AbsolutePathOfProject()
        {
            var prefix = "file:";
            var pathWithPrefix = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            return pathWithPrefix.TrimStart(prefix.ToCharArray());            
        }
    }
}