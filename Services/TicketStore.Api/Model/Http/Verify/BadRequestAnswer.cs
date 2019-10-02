namespace TicketStore.Api.Model.Http
{
    public class BadRequestAnswer : Answer
    {
        public BadRequestAnswer()
            : base("code should have string type") { }
    }
}
