using System;
using Newtonsoft.Json;
using NHamcrest;
using NHamcrest.Core;
using TicketStore.Api.Tests.Model.Services.Verify.Answers;

namespace TicketStore.Api.Tests.Tests.Matchers.Tickets
{
    public abstract class IsVerifiedAnswer : Matcher<String>
    {
        protected VerifiedAnswer Actual;
        private String ExpectedMessage = "OK";
        
        public override bool Matches(String json)
        {
            try
            {
                Actual = Deserialize(json);
                return Actual.message == ExpectedMessage;
            }
            catch (JsonReaderException)
            {
                return false;
            }
        }

        public override void DescribeMismatch(String item, IDescription mismatchDescription)
        {
            VerifiedAnswer answer = Deserialize(item);
            mismatchDescription.AppendText($"message equals {answer.message}");
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendText($"message equals {ExpectedMessage}");
        }

        protected VerifiedAnswer Deserialize(String json)
        {
            return JsonConvert.DeserializeObject<VerifiedAnswer>(json);
        }
    }
}
