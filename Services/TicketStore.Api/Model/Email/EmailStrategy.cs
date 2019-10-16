using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TicketStore.Api.Model.Email
{
    public class EmailStrategy
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _conf;
        private readonly ILogger _log;
        
        public EmailStrategy(IWebHostEnvironment env, IConfiguration conf, ILogger log)
        {
            _env = env;
            _conf = conf;
            _log = log;
        }

        public EmailService Service()
        {
            _log.LogInformation("Environment: {0}", _env.EnvironmentName);
            if (_env.IsEnvironment("Test"))
            {
                return new FakeSenderService(_env, _conf);
            }
            else
            {
                return new YandexService(_conf, _log);
            }
        }
    }
}