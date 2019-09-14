using System.Linq;
using System.Net;
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

        public UploadPosterController(ApplicationContext context, ILogger<VerifyController> log)
        {
            _db = context;
            _log = log;
            MagickNET.SetTempDirectory(@"/Users/igorgolopolosov/Documents/learning/ticket_store");
        }

        [HttpPost]
        public IActionResult Post([FromBody] Poster poster)
        {
            var concert = _db.Events.FirstOrDefault(e => e.Id == poster.eventId);

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
                        return new OkObjectResult($"{outputImage.ToBase64()}");
                    }
                }
            }
        }
    }
}
