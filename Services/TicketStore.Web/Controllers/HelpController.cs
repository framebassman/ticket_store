using Microsoft.AspNetCore.Mvc;

namespace TicketStore.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HelpController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return new OkObjectResult("OK");
    }
}