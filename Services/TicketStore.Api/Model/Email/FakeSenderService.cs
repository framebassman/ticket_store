using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TicketStore.Api.Model.Email
{
    public class FakeSenderService : EmailService
    {
        private readonly HttpClient _client;
        private readonly Uri _uri;
        
        public FakeSenderService(IWebHostEnvironment env, IConfiguration conf)
        {
//            _log = log;
//            _uri = new UriBuilder(
//                "http",
//                conf.GetSection("FakeSender").GetValue<string>("Host"),
//                conf.GetSection("FakeSender").GetValue<int>("Port")
//            ).Uri;
//            _log.LogInformation("FakeSender uri: {0}", _uri.ToString());
//            
//            _client = HttpClientFactory.Create();
//            _client.BaseAddress = _uri;
//            _client.DefaultRequestHeaders.Accept.Clear();
//            _client.DefaultRequestHeaders.Accept.Add(
//                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public override void SendTicket(String to, Pdf.Pdf ticket)
        {
//            _log.LogInformation("[FakeSender] Sending ticket to {0}", to);
//            IEnumerable<FakeEmail> json = new List<FakeEmail>
//            {
//                new FakeEmail
//                {
//                    to = to,
//                    subject = "Билет The Cellophane Heads - X лет",
//                    html = "",
//                    attachment = ticket.ToBytes().ToString()
//                }
//                
//            };
//            Task.Run(() => _client.PostAsJsonAsync("api/emails", json)).Wait();
//            _log.LogInformation("[FakeSender] Successfully send ticket to {0}", to);
        }

        public override void Dispose()
        {
            _client.Dispose();
        }
    }
}
