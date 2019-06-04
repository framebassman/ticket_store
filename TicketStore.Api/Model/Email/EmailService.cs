using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TicketStore.Api.Model.Email
{
    public class EmailService : IDisposable
    {
        private readonly EmailService _worker;

        public EmailService(IHostingEnvironment env, IConfiguration conf, ILogger log)
        {
            if (env.IsEnvironment("Test"))
            {
                _worker = new FakeSenderService(env, conf, log);
            }
            else
            {
                _worker = new YandexService(env, conf, log);
            }
        }

        public virtual void SendTicket(String to, Pdf.Pdf ticket)
        {
            _worker.SendTicket(to, ticket);
        }

        public virtual void Dispose()
        {
            _worker.Dispose();
        }
    }
}
