namespace TicketStore.Api.Model.Pdf.Model.BarcodeConverters
{
    public class FakeConverter : Converter
    {
        public override string ToBase64(string origin)
        {
            return "iVBORw0KGgoAAAANSUhEUgAAAQoAAACgAQAAAADcQB9YAAAAd0lEQVR4nO3OMQqDMBhA4SAZDclRlDrmxx5d+TMq6VESdAy0JyguHSy8N3/D695XLZ25DPKNJDESXJVprS+bgt0ljFXSerogTzvebRcCgUAgEAgEAoFAIBAIBAL5OYlqtBxet9kPLZb20JK9xrk/ii4t3233r8gHONxA4BBf4VcAAAAASUVORK5CYII=";
        }
    }
}