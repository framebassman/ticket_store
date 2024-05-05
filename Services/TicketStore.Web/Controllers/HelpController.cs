using Microsoft.AspNetCore.Mvc;

namespace TicketStore.Web.Controllers;

[Route("api/[controller]")]
public class HelpController
{
    [HttpGet]
    public IActionResult Get()
    {
        return new OkObjectResult("OK");
    }
}