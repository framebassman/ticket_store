using System;

namespace TicketStore.Api.Model.Validation.Exceptions
{
    public class TooLate : FindException
    {
        public TooLate(string verificationMethod, Double hoursDiff)
            : base(verificationMethod, $"Too late for concert, it's happened {hoursDiff} hours ago")
        {
        }
    }
}
