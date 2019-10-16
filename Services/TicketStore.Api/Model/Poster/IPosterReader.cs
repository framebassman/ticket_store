namespace TicketStore.Api.Model.Poster
{
    public interface IPosterReader
    {
        byte[] GetImage(Poster poster);
    }
}