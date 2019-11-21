using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketStore.Api.Model.Pdf;

namespace TicketStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly ILogger<PdfController> _log;
        private readonly Preview _preview;


        public PdfController(ILogger<PdfController> log, IHttpClientFactory clientFactory)
        {
            _log = log;
            _preview = new FakePreview(clientFactory.CreateClient());
        }

        [HttpGet("preview")]
        public ContentResult Preview()
        {
            _log.LogWarning("This is pdf preview");
            var layout = _preview.Layout("111122223333");
            return new ContentResult
            {
                ContentType = "text/html",
                Content = layout
            };
        }

//        public void Simplify()
//        {
//            var list = new List<String>();
//            var 
//        }
    }
}