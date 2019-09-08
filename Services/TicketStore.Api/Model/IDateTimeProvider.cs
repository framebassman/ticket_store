using System;

namespace TicketStore.Api.Model
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
