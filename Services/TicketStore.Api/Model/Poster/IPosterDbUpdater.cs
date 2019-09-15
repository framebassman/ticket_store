namespace TicketStore.Api.Model.Poster
{
    public interface IPosterDbUpdater
    {
        void Update(Poster poster, string imageUri);
    }
}
