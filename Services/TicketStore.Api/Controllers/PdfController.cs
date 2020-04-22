using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Mime;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TicketStore.Api.Model.Pdf;
using TicketStore.Api.Model.Pdf.Model.BarcodeConverters;
using TicketStore.Data.Model;

namespace TicketStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly ILogger<PdfController> _log;
        private readonly IWebHostEnvironment _environment;
        private readonly HttpClient _client;
        private readonly IConverter _pdfConverter;
        private readonly Converter _barcodeConverter;
        private readonly Event _concert;
        private readonly List<Ticket> _tickets;

        public PdfController(
            ILogger<PdfController> log,
            IHttpClientFactory clientFactory,
            IConverter pdfConverter,
            Converter barcodeConverter,
            IWebHostEnvironment environment
        )
        {
            _log = log;
            _environment = environment;
            _client = clientFactory.CreateClient();
            _pdfConverter = pdfConverter;
            _barcodeConverter = barcodeConverter;
            _tickets = new List<Ticket> 
            {
                new Ticket
                {
                    Number = "11112222"
                },
                new Ticket
                {
                    Number = "33334444"
                }
            };
            _concert = new Event
            {
                Artist = "Animal Джаз",
                Roubles = 1000,
                Time = DateTime.Today,
                Tickets = _tickets
            };
        }

        [HttpGet("preview")]
        public IActionResult Preview()
        {
            if (_environment.IsProduction() || _environment.IsStaging())
            {
                return new NotFoundResult();
            }
            else
            {
                _log.LogWarning("This is pdf preview");
                var preview = new Preview(_client, _concert, _tickets, _barcodeConverter);
                return new ContentResult
                {
                    ContentType = "text/html",
                    Content = preview.Layout()
                };                
            }
        }

        [HttpGet("example")]
        public IActionResult Example()
        {
            if (_environment.IsProduction() || _environment.IsStaging())
            {
                return new NotFoundResult();
            }
            else
            {
                _log.LogWarning("This is pdf example");
                var pdf = new Pdf(_concert, _tickets, _pdfConverter, _barcodeConverter, _client);
                using (var output = new MemoryStream(pdf.ToBytes()))
                {
                    return File(output.ToArray(), MediaTypeNames.Application.Pdf);
                }                
            }
        }
    }
}
