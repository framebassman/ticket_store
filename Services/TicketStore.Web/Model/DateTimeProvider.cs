using System;

namespace TicketStore.Web.Model
{
    public class DateTimeProvider : AbstractDateTimeProvider
    {
        public override DateTime Now => DateTime.Now;
    }
}
