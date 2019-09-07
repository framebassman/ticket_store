using TicketStore.Data.Model;

namespace TicketStore.Api.Model.Validation
{
    public interface ITicketFinder
    {
        Ticket Find(Barcode barcode);
    }
}