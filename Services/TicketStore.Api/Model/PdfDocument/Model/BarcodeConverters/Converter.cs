using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;

namespace TicketStore.Api.Model.PdfDocument.Model.BarcodeConverters
{
    public class Converter
    {
        private HttpClient _client;
        
        public Converter(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient();
        }

        public virtual String ToBase64(String origin)
        {
            return GenerateBarcodeTask(origin);
        }
        
        private String GenerateBarcodeTask(String ticketNumber)
        {
            var imageUrl = $"https://barcodeapi.org/api/{ticketNumber}";
            using (var inputStream = _client.GetStreamAsync(imageUrl).Result)
            {
                using (var memoryStream = new MemoryStream())
                {
                    var bitmap = new Bitmap(inputStream);
                    bitmap.Save(memoryStream, ImageFormat.Png);
                    byte[] byteImage = memoryStream.ToArray();
                    return Convert.ToBase64String(byteImage);
                }
            }
        }
    }
}
