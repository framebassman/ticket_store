using System;

namespace TicketStore.Web.Model
{
    public class CustomStuff : AbstractCustomStuff
    {
        public override DateTime Now => DateTime.Now;
    }
}
