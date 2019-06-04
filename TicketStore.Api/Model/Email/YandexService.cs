using System;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TicketStore.Api.Model.Email
{
    public class YandexService : EmailService
    {
        private readonly ILogger _log;
        private readonly SmtpClient _smtpClient;

        public YandexService(IHostingEnvironment env, IConfiguration conf, ILogger log) : base(env, conf, log)
        {
            _log = log;
            _smtpClient = new SmtpClient();
            _smtpClient.ServerCertificateValidationCallback = (s,c,h,e) => true;
            _smtpClient.Connect ("smtp.yandex.ru", 465, true);
            _smtpClient.Authenticate (
                "no-reply@romashov.tech", 
                conf.GetValue<String>("EmailSenderPassword")
                );
        }

        public override void SendTicket(String to, Pdf.Pdf ticket)
        {
            var builder = new BodyBuilder
            {
                TextBody = "Билет",
            };
            builder.Attachments.Add($"Ticket-{DateTime.Now}.pdf", ticket.toBytes(), new ContentType("application", "pdf"));

            var message = new MimeMessage();
			message.From.Add (new MailboxAddress ("no-reply", "no-reply@romashov.tech"));
			message.To.Add (new MailboxAddress (to));
			message.Subject = "Билет The Cellophane Heads - X лет";
            message.Body = builder.ToMessageBody();
            
            _log.LogInformation("Send ticket to @{0}", to);
            _smtpClient.Send(message);
        }

        public override void Dispose()
        {
            _smtpClient.Disconnect(true);
	        _smtpClient.Dispose();
        }
    }
}
