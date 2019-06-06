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
        private readonly String _yandexMoneyAccount;

        public EventsController(ILogger<EventsController> log)
        {
            _log = log;
            _yandexMoneyAccount = "410011021763706";
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
                        Artist = "Oxxxymiron",
                        PressRelease = PressReleaseTemplate(),
                        Price = 5m,
                        YandexMoneyAccount = _yandexMoneyAccount,
                        Time = DateTime.Now
                    },
                    new Event
                    {
                        Artist = "Face",
                        PressRelease = PressReleaseTemplate(),
                        Price = 4m,
                        YandexMoneyAccount = _yandexMoneyAccount,
                        Time = DateTime.Now
                    },
                    new Event
                    {
                        Artist = "XXXTenacion",
                        PressRelease = PressReleaseTemplate(),
                        Price = 1m,
                        YandexMoneyAccount = _yandexMoneyAccount,
                        Time = DateTime.Now
                    },
                    new Event
                    {
                        Artist = "Виктор Цой",
                        PressRelease = PressReleaseTemplate(),
                        Price = 3m,
                        YandexMoneyAccount = _yandexMoneyAccount,
                        Time = DateTime.Now
                    },
                    new Event
                    {
                        Artist = "Филипп Киркоров",
                        PressRelease = PressReleaseTemplate(),
                        Price = 2m,
                        YandexMoneyAccount = _yandexMoneyAccount,
                        Time = DateTime.Now
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
