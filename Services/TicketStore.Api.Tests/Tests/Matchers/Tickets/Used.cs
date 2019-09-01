using System;
using NHamcrest;
using TicketStore.Api.Tests.Model.Services.Verify.Answers;

namespace TicketStore.Api.Tests.Tests.Matchers.Tickets
{
    public class Used : IsVerifiedAnswer
    {
        private Boolean ExpectedUsed = true;
        
        public override bool Matches(String json)
        {
            var result = base.Matches(json);
            return result
                   && Actual.used == true;
        }

        public override void DescribeMismatch(String item, IDescription mismatchDescription)
        {
            VerifiedAnswer answer = Deserialize(item);
            mismatchDescription.AppendText($"used equals {answer.used}");
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendText($"used equals {ExpectedUsed}");
        }
        
    }
}