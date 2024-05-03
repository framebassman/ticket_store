namespace TicketStore.Web.Model;

public abstract class AbstractDateTimeProvider
{
    public abstract DateTime Now { get; }
}
