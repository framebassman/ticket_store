using System;

namespace TicketStore.Api.Model.Validation.Exceptions
{
    public class TooEarly : FindException
    {
        public TooEarly(string verificationMethod, Double hoursDiff)
            : base(verificationMethod, $"Too early for concert, it will happen in {hoursDiff} hours")
        {
        }
    }
}
