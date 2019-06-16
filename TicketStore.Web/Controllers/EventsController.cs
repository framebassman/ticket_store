using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketStore.Web.Model;

namespace TicketStore.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _log;

        public EventsController(ILogger<EventsController> log)
        {
            _log = log;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _log.LogInformation("Return hardcoded events");
            return new OkObjectResult(
                new List<Event>
                {
                    new Event
                    {
                        MerchantId = 1,
                        Artist = "Oxxxymiron",
                        PressRelease = PressReleaseTemplate(),
                        Roubles = 5m,
                        Time = DateTime.Now,
                        PosterUrl = new Uri("https://pp.userapi.com/c849224/v849224484/14cde2/0GUw8PewP58.jpg")
                    },
                    new Event
                    {
                        MerchantId = 1,
                        Artist = "Face",
                        PressRelease = PressReleaseTemplate(),
                        Roubles = 4m,
                        Time = DateTime.Now,
                        PosterUrl = new Uri("https://pp.userapi.com/c849224/v849224484/14cde2/0GUw8PewP58.jpg")
                    },
                    new Event
                    {
                        MerchantId = 1,
                        Artist = "XXXTenacion",
                        PressRelease = PressReleaseTemplate(),
                        Roubles = 3m,
                        Time = DateTime.Now,
                        PosterUrl = new Uri("https://pp.userapi.com/c849224/v849224484/14cde2/0GUw8PewP58.jpg")
                    },
                    new Event
                    {
                        MerchantId = 1,
                        Artist = "Виктор Цой",
                        PressRelease = PressReleaseTemplate(),
                        Roubles = 3m,
                        Time = DateTime.Now,
                        PosterUrl = new Uri("https://pp.userapi.com/c849224/v849224484/14cde2/0GUw8PewP58.jpg")
                    },
                    new Event
                    {
                        MerchantId = 1,
                        Artist = "Филипп Киркоров",
                        PressRelease = PressReleaseTemplate(),
                        Roubles = 2m,
                        Time = DateTime.Now,
                        PosterUrl = new Uri("https://pp.userapi.com/c849224/v849224484/14cde2/0GUw8PewP58.jpg")
                    }
                }
            );
        }

        private String PressReleaseTemplate()
        {
            return @"Товарищи! сложившаяся структура организации способствует подготовки и реализации новых предложений. Разнообразный и богатый опыт рамки и место обучения кадров позволяет оценить значение позиций, занимаемых участниками в отношении поставленных задач. Повседневная практика показывает, что новая модель организационной деятельности обеспечивает широкому кругу (специалистов) участие в формировании соответствующий условий активизации.";
        }
    }
}
