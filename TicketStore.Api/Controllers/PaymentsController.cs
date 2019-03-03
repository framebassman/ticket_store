using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TicketStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        // POST api/values
        [HttpPost]
        public void Post(
            [FromForm] bool test_notification,
            [FromForm] string notification_type,
            [FromForm] string operation_id,
            [FromForm] Decimal amount,
            [FromForm] Decimal withdraw_amount,
            [FromForm] string currency,
            [FromForm] DateTime datetime,
            [FromForm] bool unaccepted,
            [FromForm] string email,
            [FromForm] string lastname
        )
        {
            var a = 1;
        }
    }
}