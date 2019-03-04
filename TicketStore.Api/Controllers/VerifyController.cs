using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TicketStore.Api.Data;
using TicketStore.Api.Model;
using TicketStore.Api.Model.Http;

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
            if (!IsAuthorized(Request))
            {
                return new UnauthorizedObjectResult(new UnauthorizedAnswer());
            }
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(new BadRequestAnswer());
            }
            return new OkObjectResult(new Answer("OK"));
        }

        private Boolean IsAuthorized(HttpRequest request)
        {
            var authHeader = request.Headers["Authorization"].First();
            if (string.IsNullOrEmpty(authHeader))
            {
                return false;
            }
            return authHeader == "Bearer pkR9vfZ9QdER53mf";
        }
    }
}
