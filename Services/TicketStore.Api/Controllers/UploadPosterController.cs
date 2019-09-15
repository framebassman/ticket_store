using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AspNetCore.Yandex.ObjectStorage;
using ImageMagick;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketStore.Api.Model.Poster;
using TicketStore.Data;

namespace TicketStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadPosterController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly ILogger<VerifyController> _log;
        private YandexStorageService _storage;

        public UploadPosterController(ApplicationContext context, ILogger<VerifyController> log, YandexStorageService storage)
        {
            _db = context;
            _log = log;
            _storage = storage;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Poster poster)
        {
            var concert = _db.Events.FirstOrDefault(e => e.Id == poster.eventId);

            var outputImage = GetImage(poster);

            // await _storage.PutObjectAsync(outputImage, "next-obj.png");

            return new OkObjectResult($"{outputImage}");
        }

        private byte[] GetImage(Poster poster)
        {
            var imageUrl = poster.imageUrl;
            using (var webClient = new WebClient())
            {
                using (var inputStream = webClient.OpenRead(imageUrl))
                {
                    using (MagickImage outputImage = new MagickImage(inputStream))
                    {
                        outputImage.Format = MagickFormat.Jpeg;
                        MagickGeometry size = new MagickGeometry(1000, 1000);
                        outputImage.Resize(size);

                        var byteArray = outputImage.ToByteArray();
                        var compressedStream = new MemoryStream(byteArray);
                        ImageOptimizer optimizer = new ImageOptimizer();
                        optimizer.Compress(compressedStream);

                        using (MagickImage compressedImage = new MagickImage(compressedStream))
                        {
                            return compressedImage.ToByteArray();
                        }
                    }
                }
            }
        }
    }
}
