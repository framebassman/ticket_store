using System;

namespace TicketStore.Api.Model.Email
{
    public class FakeSenderService : EmailService
    {
        public override void SendTicket(String to, Pdf.Pdf ticket)
        {

        }

        public override void Dispose()
        {

        }
    }
}
