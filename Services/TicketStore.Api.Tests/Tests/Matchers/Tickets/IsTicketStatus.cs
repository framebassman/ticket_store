using System;
using Newtonsoft.Json;
using NHamcrest.Core;
using TicketStore.Api.Model.Http;

namespace TicketStore.Api.Tests.Tests.Matchers.Tickets
{
    public class IsTicketStatus : Matcher<String>
    {
        public override bool Matches(String json)
        {
            try
            {
                JsonConvert.DeserializeObject<VerifiedAnswer>(json);
                return true;
            }
            catch (JsonReaderException e)
            {
                return false;
            }
        }
    }
}
