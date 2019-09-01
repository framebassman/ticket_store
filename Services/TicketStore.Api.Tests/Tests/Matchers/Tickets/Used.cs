using System;
using TicketStore.Api.Model.Http;

namespace TicketStore.Api.Tests.Tests.Matchers.Tickets
{
    public class Used : IsVerifiedAnswer
    {
        public override bool Matches(String json)
        {
            var result = base.Matches(json);
            return result
                   && Actual.used == true;
        }
    }
}