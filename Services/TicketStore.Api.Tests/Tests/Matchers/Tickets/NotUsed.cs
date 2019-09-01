using System;

namespace TicketStore.Api.Tests.Tests.Matchers.Tickets
{
    public class NotUsed : IsVerifiedAnswer
    {
        public NotUsed(TicketMatcher ticketMatcher) : base(ticketMatcher) { }

        public override bool Matches(String json)
        {
            var result = base.Matches(json);
            return result
                   && Actual.used == false;
        }
    }
}