using System;

namespace TicketStore.Api.Model.Validation.Exceptions
{
    public class MultipleTicketsFound : FindException
    {
        public MultipleTicketsFound(string verificationMethod, Double ticketsCount)
            : base(verificationMethod, $"Multiple tickets found: {ticketsCount} tickets")
        {
        }
    }
}
