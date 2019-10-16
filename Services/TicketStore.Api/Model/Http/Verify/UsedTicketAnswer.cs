using System;

namespace TicketStore.Api.Model.Http
{
    public class UsedTicketAnswer : VerifiedAnswer
    {
        public UsedTicketAnswer(String concLabel) : base(concLabel)
        {
            used = true;
        }
    }
}
