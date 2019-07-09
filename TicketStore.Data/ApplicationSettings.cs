using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace TicketStore.Data
{
    public class ApplicationSettings
    {
        private readonly String _environmentName;
        private readonly Boolean _isInsideDocker; 
        
        public ApplicationSettings()
        {
            _environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT",
                                   EnvironmentVariableTarget.Process)
                               ?? "Development";
            _isInsideDocker =
                Convert.ToBoolean(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER",
                    EnvironmentVariableTarget.Process));
            Console.WriteLine("Environment: {0}", _environmentName);
            Console.WriteLine("Is application inside docker container: {0}", _isInsideDocker);
        }

        public String ConnectionString()
        {
            return BuildConfiguration().GetConnectionString("DefaultConnection");
        }
        
        private IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(CalculateBasePath())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{_environmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        private String CalculateBasePath()
        {
            var executionPathWithFile = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            var executionPath = executionPathWithFile.TrimStart("file:".ToCharArray());
            Console.WriteLine("Execution path: {0}", executionPath);
            var dirname = Path.Combine(executionPath, "DbConfigs");
            var info = new DirectoryInfo(dirname);
            if (!info.Exists)
            {
                Console.WriteLine("There is no directory via address {0}", info.FullName);
                throw new FileNotFoundException("There is no file via address {0}", info.FullName);
            }
            Console.WriteLine("Base path for configs: {0}", info.FullName);
            return info.FullName;
        }
    }
}