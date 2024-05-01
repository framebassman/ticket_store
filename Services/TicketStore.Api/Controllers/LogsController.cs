using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DinkToPdf.Contracts;
using TicketStore.Api.Model;
using TicketStore.Api.Model.Email;
using TicketStore.Api.Model.Payment.YandexMoney;
using TicketStore.Api.Model.PdfDocument;
using TicketStore.Api.Model.PdfDocument.Model.BarcodeConverters;
using TicketStore.Data;
using TicketStore.Data.Model;

namespace TicketStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private ILogger<LogsController> _log;

        public LogsController(
            ILogger<LogsController> log
        )
        {
            _log = log;
        }

        [HttpGet]
        public IActionResult Get() {
            _log.LogInformation("It is not bad");
            return new OkObjectResult("Hi!");
        }
    }
}
