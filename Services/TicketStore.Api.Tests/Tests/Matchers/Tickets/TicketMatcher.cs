using System;
using Newtonsoft.Json;
using NHamcrest.Core;

namespace TicketStore.Api.Tests.Tests.Matchers.Tickets
{
    public abstract class TicketMatcher : Matcher<String>
    {
    }
}