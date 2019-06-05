using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TicketStore.Api.Model.Email
{
    public class FakeSenderService : EmailService
    {
        private readonly HttpClient _client;
        private readonly ILogger _log;
        private readonly Uri _uri;
        
        public FakeSenderService(IHostingEnvironment env, IConfiguration conf, ILogger log)
        {
            _log = log;
            _uri = new UriBuilder(
                "http",
                conf.GetSection("FakeSender").GetValue<string>("Host"),
                conf.GetSection("FakeSender").GetValue<int>("Port")
            ).Uri;
            _log.LogInformation("FakeSender uri: {0}", _uri.ToString());
            
            _client = HttpClientFactory.Create();
            _client.BaseAddress = _uri;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public override void SendTicket(String to, Pdf.Pdf ticket)
        {
            _log.LogInformation("[FakeSender] Sending ticket to {0}", to);
            IEnumerable<FakeEmail> json = new List<FakeEmail>
            {
                new FakeEmail
                {
                    to = to,
                    subject = "Билет The Cellophane Heads - X лет",
                    html = "",
                    attachment = ticket.toBytes().ToString()
                }
                
            };
            Task.Run(() => _client.PostAsJsonAsync("api/emails", json)).Wait();
            _log.LogInformation("[FakeSender] Successfully send ticket to {0}", to);
        }

        private void PingFakeSender()
        {
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();

            // Use the default Ttl value which is 128,
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            PingReply reply = pingSender.Send(_uri.ToString(), timeout, buffer, options);
            
            if (reply.Status == IPStatus.Success)
            {
                _log.LogInformation("Address: {0}", reply.Address.ToString());
                _log.LogInformation("RoundTrip time: {0}", reply.RoundtripTime);
                _log.LogInformation("Time to live: {0}", reply.Options.Ttl);
                _log.LogInformation("Don't fragment: {0}", reply.Options.DontFragment);
                _log.LogInformation("Buffer size: {0}", reply.Buffer.Length);
            }
            else
            {
                _log.LogError("Reply status is {0}", reply.Status);
            }
        }

        public override void Dispose()
        {
            _client.Dispose();
        }
    }
}
