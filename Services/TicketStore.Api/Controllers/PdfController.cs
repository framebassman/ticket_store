using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketStore.Api.Model.Pdf;
using TicketStore.Data.Model;

namespace TicketStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly ILogger<PdfController> _log;
        private readonly HttpClient _client;

        public PdfController(ILogger<PdfController> log, IHttpClientFactory clientFactory)
        {
            _log = log;
            _client = clientFactory.CreateClient();
        }

        [HttpGet("preview")]
        public ContentResult Preview()
        {
            _log.LogWarning("This is pdf preview");
            var tickets = new List<Ticket>
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
            var concert = new Event
            {
                Artist = "Animal Джаз",
                Roubles = 1000,
                Time = DateTime.Today,
                Tickets = tickets
            };
            var preview = new Preview(_client, concert);
            return new ContentResult
            {
                ContentType = "text/html",
                Content = preview.Layout()
            };
        }
    }
}