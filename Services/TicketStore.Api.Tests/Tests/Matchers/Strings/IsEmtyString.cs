using System;
using NHamcrest;
using NHamcrest.Core;

namespace TicketStore.Api.Tests.Tests.Matchers.Strings
{
    public class IsEmptyString : Matcher<String>
    {
        public override bool Matches(String actual)
        {
            return string.IsNullOrEmpty(actual);
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendText("an empty string");
        }
    }
}
