using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace TicketStore.Api.Model.Pdf.Model.BarcodeConverters
{
    public class RealConverter : Converter
    {
        private HttpClient _client;
        
        public RealConverter(HttpClient client) : base()
        {
            _client = client;
        }

        public override string ToBase64(String origin)
        {
            var task = GenerateBarcodeTask(origin);
            task.Start();
            return task.Result;
        }
        
        private async Task<String> GenerateBarcodeTask(String ticketNumber)
        {
            var imageUrl = $"https://www.scandit.com/wp-content/themes/scandit/barcode-generator.php?symbology=code128&value={ticketNumber}&size=200&ec=L";
            using (var inputStream = await _client.GetStreamAsync(imageUrl))
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