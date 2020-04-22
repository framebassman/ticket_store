using System;
using System.Text.RegularExpressions;
using NHamcrest;
using NHamcrest.Core;

namespace TicketStore.Api.Tests.Unit.Matchers
{
    public class ContainsNTimes : Matcher<String>
    {
        private readonly String _substring;
        private readonly Int32 _expectedCount;
        private Int32 _actialCount;

        public ContainsNTimes(String substring, Int32 expectedCount)
        {
            _substring = substring;
            _expectedCount = expectedCount;
        }

        public override bool Matches(String origin)
        {
            _actialCount = Regex.Matches(origin, _substring).Count;
            return _expectedCount == _actialCount;
        }

        public override void DescribeTo(IDescription description)
        {
            description
                .AppendText("origin String should contains ")
                .AppendValue(_substring)
                .AppendText($" {_expectedCount} times");
        }
        
        
        public override void DescribeMismatch(String item, IDescription description)
        {
            description.AppendText($"but contains {_actialCount} times");
        }
    }
}