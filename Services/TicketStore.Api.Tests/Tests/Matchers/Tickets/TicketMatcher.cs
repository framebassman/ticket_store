using System;
using Newtonsoft.Json;
using NHamcrest.Core;
using TicketStore.Api.Tests.Model.Services.Verify.Answers;

namespace TicketStore.Api.Tests.Tests.Matchers.Tickets
{
    public abstract class TicketMatcher : Matcher<String>
    {
        protected VerifiedAnswer Deserialize(String json)
        {
            return JsonConvert.DeserializeObject<VerifiedAnswer>(json);
        }
    }
}
