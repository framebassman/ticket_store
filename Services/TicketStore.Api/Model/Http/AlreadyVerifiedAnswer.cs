using System;

namespace TicketStore.Api.Model.Http
{
    public class AlreadyVerifiedAnswer : Answer
    {
        public AlreadyVerifiedAnswer()
            : base("ticket has already verified") { }
    }
}
