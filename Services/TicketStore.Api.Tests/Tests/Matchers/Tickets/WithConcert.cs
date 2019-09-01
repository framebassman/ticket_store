using System;
using System.Globalization;
using Newtonsoft.Json;
using TicketStore.Api.Model.Http;
using TicketStore.Api.Tests.Model.Db;

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
