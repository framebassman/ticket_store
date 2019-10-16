using System;

namespace TicketStore.Api.Model.Http
{
    public class VerifiedAnswer : Answer
    {
        public String concertLabel;
        public Boolean used;

        public VerifiedAnswer(string concLabel) : base("OK")
        {
            concertLabel = concLabel;
        }
    }
}
