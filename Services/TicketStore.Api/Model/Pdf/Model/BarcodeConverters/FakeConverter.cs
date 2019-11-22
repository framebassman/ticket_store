namespace TicketStore.Api.Model.Pdf.Model.BarcodeConverters
{
    public class FakeConverter : Converter
    {
        public override string ToBase64(string origin)
        {
            return "iVBORw0KGgoAAAANSUhEUgAAAWIAAACgAQAAAAAecFvcAAAAjElEQVR4nO3OsQqDMBRA0VQyWpJPMaRjHvbTlZexop+SUMeA3btJoTjcO5/hdseJps6cCf1vncWIv1d5zHWz2dtFfKiS590HkbnuNi/+ti1PG671jUaj0Wg0Go1Go9FoNBqNRqPRaDQa/atOarS8nb5GN7RUWtSyOk1jX1bV0fUtxXIMcWrrtb7R330AylBW1fDTdkwAAAAASUVORK5CYII=";
        }
    }
}