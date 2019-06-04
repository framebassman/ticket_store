using System;

namespace TicketStore.Api.Model.Email
{
    public abstract class EmailService : IDisposable
    {
        public abstract void SendTicket(String to, Pdf.Pdf ticket);
        public abstract void Dispose();
    }
}
