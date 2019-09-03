using System;
using Newtonsoft.Json;
using NHamcrest;
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
            var actual = Deserialize(json);
            return actual.concertLabel == _concertLabel;
        }

        public override void DescribeMismatch(String item, IDescription mismatchDescription)
        {
            VerifiedAnswer answer = Deserialize(item);
            mismatchDescription.AppendText($"concertLabel equals {answer.concertLabel}");
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendText($"concertLabel equals {_concertLabel}");
        }
    }
}
