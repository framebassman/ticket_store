using System;

namespace TicketStore.Api.Model.Http
{
    public class ValidTicketAnswer : VerifiedAnswer
    {
        public ValidTicketAnswer(string concLabel) : base(concLabel)
        {
            used = false;
        }
    }
}
