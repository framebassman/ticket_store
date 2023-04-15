namespace TicketStore.Api.Model.Validation.Exceptions
{
    public class ConcertNotFound : FindException
    {
        public ConcertNotFound(string verificationMethod)
            : base(verificationMethod, "Concert is not found for ticket")
        {
        }
    }
}
