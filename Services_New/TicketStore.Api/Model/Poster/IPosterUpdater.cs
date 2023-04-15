using System.Threading.Tasks;

namespace TicketStore.Api.Model.Poster
{
    public interface IPosterUpdater
    {
        Task<string> Update(Poster poster);
    }
}
