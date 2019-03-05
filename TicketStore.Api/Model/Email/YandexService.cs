using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using Microsoft.Extensions.Logging;

namespace TicketStore.Api.Model.Email
{
    public class YandexService
    {
        private String _password;
        private ILogger _log;

        public YandexService(String password, ILogger log)
        {
            _password = password;
            _log = log;
        }

        public void SendTicket(String to)
        {
            var message = new MimeMessage ();
			message.From.Add (new MailboxAddress ("no-reply", "no-reply@romashov.tech"));
			message.To.Add (new MailboxAddress (to));
			message.Subject = "Билет The Cellophane Heads - X лет";
			message.Body = new TextPart ("plain") {
				Text = @"Hey Chandler"
			};

			using (var client = new SmtpClient ()) {
				client.ServerCertificateValidationCallback = (s,c,h,e) => true;
				client.Connect ("smtp.yandex.ru", 465, true);
				client.Authenticate ("no-reply@romashov.tech", _password);
                _log.LogInformation("Send ticket to @{0}", to);
				client.Send(message);
				client.Disconnect(true);
			}
        }
    }
}
