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

        public void SendTicket(String to, byte[] ticket)
        {
            var message = new MimeMessage ();
			message.From.Add (new MailboxAddress ("no-reply", "no-reply@romashov.tech"));
			message.To.Add (new MailboxAddress (to));
			message.Subject = "Билет The Cellophane Heads - X лет";
			
            var builder = new BodyBuilder ();
            // Set the plain-text version of the message text
            builder.TextBody = "Билет";
            // We may also want to attach a calendar event for Monica's party...
            builder.Attachments.Add($"Ticket-{DateTime.Now}.pdf", ticket, new ContentType("application", "pdf"));
            message.Body = builder.ToMessageBody();

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
