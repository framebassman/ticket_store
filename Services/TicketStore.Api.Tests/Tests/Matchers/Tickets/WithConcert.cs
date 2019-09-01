using System;
using Newtonsoft.Json;
using TicketStore.Api.Tests.Model.Services.Verify.Answers;

namespace TicketStore.Api.Tests.Tests.Matchers.Tickets
{
    public class WithConcert : TicketMatcher
    {
        private String _concertLabel;
        
        public WithConcert(String concertLabel)
        {
            _concertLabel = concertLabel;
        }

        public override bool Matches(String json)
        {
            var actual = JsonConvert.DeserializeObject<VerifiedAnswer>(json);
            return actual.concertLabel == _concertLabel;
        }
    }
}
