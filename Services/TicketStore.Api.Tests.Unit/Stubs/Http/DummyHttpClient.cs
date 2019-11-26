using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TicketStore.Api.Tests.Unit.Stubs.Http
{
    public class DummyHttpClient : HttpClient
    {
        public new Task<Stream> GetStreamAsync(string requestUri)
        {
            var base64DataOfImage = "iVBORw0KGgoAAAANSUhEUgAAAQoAAACgAQAAAADcQB9YAAAAd0lEQVR4nO3OMQqDMBhA4SAZDclRlDrmxx5d+TMq6VESdAy0JyguHSy8N3/D695XLZ25DPKNJDESXJVprS+bgt0ljFXSerogTzvebRcCgUAgEAgEAoFAIBAIBAL5OYlqtBxet9kPLZb20JK9xrk/ii4t3233r8gHONxA4BBf4VcAAAAASUVORK5CYII=";
            return new Task<Stream>(() =>
            {
                byte[] byteArray = Encoding.ASCII.GetBytes(base64DataOfImage);
                return new MemoryStream(byteArray);                
            });
        }
    }
}
