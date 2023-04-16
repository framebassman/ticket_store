using System;

namespace TicketStore.Api.Tests.Model.Services.Verify.Answers
{
    public class VerifiedAnswer : Answer
    {
        public Boolean used { get; set; }
        public String concertLabel { get; set; }
    }
}
