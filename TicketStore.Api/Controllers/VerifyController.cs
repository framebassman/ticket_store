using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketStore.Api.Data;
using TicketStore.Api.Model;

namespace TicketStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerifyController : ControllerBase
    {
        private ApplicationContext _db;

        public VerifyController(ApplicationContext context)
        {
            _db = context;
        }

        [HttpPost]
        public IActionResult Post([FromBody] String code)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestActionResult("Code should have string type");
            }
            return new OkActionResult("OK");
        }
    }
}
