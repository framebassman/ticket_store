namespace TicketStore.Api.Tests.Model.Services.Verify.Answers
{
    public class NotFoundAnswer : Answer
    {
        public NotFoundAnswer()
        {
            message = "cannot find code in database";
        }
    }
}
