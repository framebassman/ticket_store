using System;
using Newtonsoft.Json;
using NHamcrest.Core;
using TicketStore.Api.Tests.Model.Services.Verify.Answers;

namespace TicketStore.Api.Tests.Tests.Matchers.Tickets
{
    public abstract class IsVerifiedAnswer : Matcher<String>
    {
        protected VerifiedAnswer Actual;
        protected TicketMatcher TicketMatcher;

        public IsVerifiedAnswer(TicketMatcher ticketMatcher)
        {
            TicketMatcher = ticketMatcher;
        }
        
        public override bool Matches(String json)
        {
            try
            {
                Actual = JsonConvert.DeserializeObject<VerifiedAnswer>(json);
                return Actual.message == "OK";
            }
            catch (JsonReaderException e)
            {
                return false;
            }
        }
    }
}
