using Microsoft.AspNetCore.Mvc;

namespace TicketStore.Web.Controllers
{
    [Route("[controller]")]
    public class HelpController
    {
        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult("OK");
        }
    }
}