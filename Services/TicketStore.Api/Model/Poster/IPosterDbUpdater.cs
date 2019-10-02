namespace TicketStore.Api.Model.Poster
{
    public interface IPosterDbUpdater
    {
        bool CanUpdate(Poster poster);
        void Update(Poster poster, string imageUri);
    }
}
