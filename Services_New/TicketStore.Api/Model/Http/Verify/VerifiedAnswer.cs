using System;

namespace TicketStore.Api.Model.Http
{
    public class VerifiedAnswer : Answer
    {
        public String concertLabel { get; set; }
        public Boolean used { get; set; }

        public VerifiedAnswer(string concLabel) : base("OK")
        {
            concertLabel = concLabel;
        }
    }
}
