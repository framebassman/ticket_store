using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TicketStore.Api.Model.Email
{
    public class FakeSenderService : EmailService
    {
        private HttpClient _client;
        private ILogger<FakeSenderService> _log;
        private Uri _uri;
        private JsonSerializerOptions _options;
        
        public FakeSenderService(ILogger<FakeSenderService> log, IConfiguration conf, IHttpClientFactory clientFactory)
        {
            _log = log;
            _uri = new UriBuilder(
                "http",
                conf.GetSection("FakeSender").GetValue<string>("Host"),
                conf.GetSection("FakeSender").GetValue<int>("Port")
            ).Uri;
            _log.LogInformation("FakeSender uri: {0}", _uri.ToString());
            
            _client = clientFactory.CreateClient();
            _client.BaseAddress = _uri;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _options = new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
        }

        public override void SendTicket(String to, PdfDocument.Pdf ticket)
        {
            _log.LogInformation("[FakeSender] Sending ticket to {0}", to);
            IEnumerable<FakeEmail> emails = new List<FakeEmail>
            {
                new FakeEmail
                {
                    to = to,
                    subject = "Билет The Cellophane Heads - X лет",
                    html = "",
                    attachment = ticket.ToBytes().ToString()
                }
                
            };
            Task
                .Run(() => _client.PostAsync(
                    "api/emails",
                    new StringContent(
                        JsonSerializer.Serialize(emails, _options),
                        Encoding.UTF8,
                        "application/json"
                    )))
                .Wait();
            _log.LogInformation("[FakeSender] Successfully send ticket to {0}", to);
        }

        public override void Dispose()
        {
            _client.Dispose();
        }
    }
}
