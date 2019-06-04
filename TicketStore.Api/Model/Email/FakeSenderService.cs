using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TicketStore.Api.Model.Email
{
    public class FakeSenderService : EmailService
    {
        public FakeSenderService(IHostingEnvironment env, IConfiguration conf, ILogger log)
        {
        }

        public override void SendTicket(String to, Pdf.Pdf ticket)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
