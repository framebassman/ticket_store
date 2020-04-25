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
        private readonly ILogger<YandexService> _log;
        private readonly String _smtpUsername;
        private readonly String _smtpPassword;

        public YandexService(IConfiguration conf, ILogger<YandexService> log)
        {
            _log = log;
            _smtpUsername = "no-reply@romashov.tech";
            _smtpPassword = conf.GetValue<String>("EmailSenderPassword");
        }

        public override void SendTicket(String to, PdfDocument.Pdf ticket)
        {
            var builder = new BodyBuilder
            {
                TextBody = "Билеты во вложении",
            };
            builder.Attachments.Add($"Ticket-{DateTime.Now}.pdf", ticket.ToBytes(), new ContentType("application", "pdf"));

            var message = new MimeMessage();
			message.From.Add (new MailboxAddress ("no-reply", "no-reply@romashov.tech"));
			message.To.Add (new MailboxAddress (to));
			message.Subject = "Билеты от Чертополоха";
            message.Body = builder.ToMessageBody();
            
            _log.LogInformation("Send ticket to @{0}", to);

            using (var smtpClient = new SmtpClient())
            {
                _log.LogInformation("Create new SmtpClient");
                smtpClient.ServerCertificateValidationCallback = (s,c,h,e) => true;
                smtpClient.Connect("smtp.yandex.ru", 465, true);
                _log.LogInformation("Authenticate via SmtpClient");
                smtpClient.Authenticate(_smtpUsername, _smtpPassword);
                _log.LogInformation("Send email via Yandex account");
                smtpClient.Send(message);
                _log.LogInformation("Disconnect and dispose");
                smtpClient.Disconnect(true);
            }
            
        }

        public override void Dispose()
        {
        }
    }
}
