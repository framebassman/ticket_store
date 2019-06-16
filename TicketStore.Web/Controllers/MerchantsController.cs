using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketStore.Web.Model;

namespace TicketStore.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantsController : ControllerBase
    {
        private readonly ILogger<MerchantsController> _log;

        public MerchantsController(ILogger<MerchantsController> log)
        {
            _log = log;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            _log.LogInformation("Return hardcoded merchants");
            return new OkObjectResult(
                new List<Merchant>
                {
                    new Merchant
                    {
                        Id = 1,
                        YandexMoneyAccount = "410011021763706",
                        Place = "Чердак"
                    }
                }
            );
        }

    }
}
