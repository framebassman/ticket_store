using System;

namespace TicketStore.Web.Model
{
    public class DateTimeProvider : AbstractDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
