using System;

namespace TicketStore.Api.Model.Validation.Exceptions
{
    public class TicketNotFound : FindException
    {
        public TicketNotFound(String verificationMethod)
            : base(verificationMethod, "Ticket not found in Database")
        {
        }
    }
}
