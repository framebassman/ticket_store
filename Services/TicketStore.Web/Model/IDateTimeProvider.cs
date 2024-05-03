namespace TicketStore.Web.Model;

public interface IDateTimeProvider
{
    public DateTime Now { get; }
}
