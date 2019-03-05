using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace TicketStore.Api.Model.Email
{
    public class SendGridService
    {
        private String _token;
        private ILogger _log;

        public SendGridService(String token, ILogger log)
        {
            _token = token;
            _log = log;
        }

        public async Task<Response> SendTicket(String recipient)
        {
            var client = new SendGridClient(_token);
            var from = new EmailAddress("framebassman@gmail.com", "Kolenka Inc Tickets");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress(recipient);
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            _log.LogInformation("Send ticket: {@0}", msg);
            return await client.SendEmailAsync(msg);
        }
    }
}
