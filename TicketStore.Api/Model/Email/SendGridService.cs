using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace TicketStore.Api.Model.Email
{
    public class SendGridService
    {
        private String _token;

        public SendGridService(String token)
        {
            _token = token;
        }

        public async Task<Response> SendTicket(String recipient)
        {
            var client = new SendGridClient(_token);
            var from = new EmailAddress("framebassman@gmail.com", "Romashov Tickets");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress(recipient);
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            return await client.SendEmailAsync(msg);
        }
    }
}
