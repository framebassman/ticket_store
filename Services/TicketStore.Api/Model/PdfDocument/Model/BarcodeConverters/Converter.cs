using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;

namespace TicketStore.Api.Model.PdfDocument.Model.BarcodeConverters
{
    public class Converter
    {
        private readonly HttpClient _client;
        
        public Converter(HttpClient client)
        {
            _client = client;
        }

        public virtual String ToBase64(String origin)
        {
            return GenerateBarcodeTask(origin);
        }
        
        private String GenerateBarcodeTask(String ticketNumber)
        {
            var imageUrl = $"https://www.scandit.com/wp-content/themes/scandit/barcode-generator.php?symbology=code128&value={ticketNumber}&size=200&ec=L";
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
