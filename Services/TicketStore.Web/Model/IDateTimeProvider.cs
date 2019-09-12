using System;

namespace TicketStore.Web.Model
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
