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
        private readonly List<String> _artists;
        private readonly Random _random;

        public EventsController(ILogger<EventsController> log)
        {
            _log = log;
            _random = new Random();
        }

        [HttpGet]
        public IActionResult Get(Int32 merchantId)
        {
            if (merchantId == 0)
            {
                _log.LogWarning("Request without merchantId parameter");
                return new BadRequestObjectResult("Request should contains merchantId parameter");
            }
            var result = DistemperAndAgata();
            _log.LogInformation("Return hardcoded events: {@result}", result);
            return new OkObjectResult(result);
        }

        private IEnumerable<Event> DistemperAndAgata()
        {
            var result = new List<Event>();
            result.Add(new Event
            {
                Artist = "Distemper",
                Roubles = new decimal(2),
                Time = new DateTime(2019, 10, 4, 19, 00, 00),
                PosterUrl = new Uri("https://pp.userapi.com/c858028/v858028662/7c5d/r0sdXNhQXW0.jpg")
            });
            result.Add(new Event
            {
                Artist = "Глеб Самойлов и The\u00A0Matrixxx",
                Roubles = new decimal(3),
                Time = new DateTime(2019, 09, 14, 19, 00, 00),
                PosterUrl = new Uri("https://pp.userapi.com/c852132/v852132662/15e4b7/zN4K2Pe9NKk.jpg")
            });
            return result;
        }

        private IEnumerable<Event> HardcodedEvents(Int32 counter)
        {
            var result = new List<Event>();
            for (int i = 0; i < counter; i++)
            {
                result.Add(HardcodedEvent(i));
            }
            return result;
        }

        private Event HardcodedEvent(Int32 index)
        {
            return new Event
            {
                Artist = _artists[index],
                PressRelease = PressReleaseTemplate(),
                Roubles = new Decimal(2),
                Time = DateTime.Now,
                PosterUrl = new Uri("https://pp.userapi.com/c849224/v849224484/14cde2/0GUw8PewP58.jpg")
            };
        }

        private String PressReleaseTemplate()
        {
            return @"Товарищи! сложившаяся структура организации способствует подготовки и реализации новых предложений. Разнообразный и богатый опыт рамки и место обучения кадров позволяет оценить значение позиций, занимаемых участниками в отношении поставленных задач. Повседневная практика показывает, что новая модель организационной деятельности обеспечивает широкому кругу (специалистов) участие в формировании соответствующий условий активизации.";
        }
    }
}
