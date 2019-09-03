using System;

namespace TicketStore.Api.Model.Http
{
    public class UsedTicketAnswer : VerifiedAnswer
    {
        public UsedTicketAnswer(string concLabel) : base(concLabel)
        {
            used = true;
        }
    }
}
