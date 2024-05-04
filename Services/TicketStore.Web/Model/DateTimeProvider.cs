namespace TicketStore.Web.Model
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now.ToUniversalTime();
    }
}
