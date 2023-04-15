using System;

namespace TicketStore.Api.Model.Http
{
    public class ValidTicketAnswer : VerifiedAnswer
    {
        public ValidTicketAnswer(String concLabel) : base(concLabel)
        {
            used = false;
        }
    }
}
