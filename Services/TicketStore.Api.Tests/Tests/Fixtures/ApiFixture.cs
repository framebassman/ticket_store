using System;
using System.Collections.Generic;
using System.Linq;
using TicketStore.Api.Tests.Model.Services;
using TicketStore.Api.Tests.Data;
using TicketStore.Api.Tests.Model.Db;

namespace TicketStore.Api.Tests.Tests.Fixtures
{
    public class ApiFixture : IDisposable
    {
        public ApiService Api;
        public FakeSenderService FakeSender;
        public ApplicationContext Db;
        
        public Merchant Merchant;
        public List<Event> Events;

        public ApiFixture()
        {
            Api = new ApiService();
            FakeSender = new FakeSenderService();
            Db = new ApplicationContext();
            SeedTestData();
            Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
        }

        public void SeedTestData()
        {
            Merchant = new Merchant
            {
                Place = "Test Place",
                YandexMoneyAccount = Generator.YandexMoneyAccount()
            };
            Events = new List<Event>
            {
                new Event
                {
                    Artist = "First Test Artist",
                    Merchant = Merchant,
                    PosterUrl = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png",
                    PressRelease = "First test press",
                    Roubles = 2.00m,
                    Time = DateTime.Parse("Sun, 9 Jul 2119 17:00:00Z").ToUniversalTime(),
                },
                new Event
                {
                    Artist = "Second Test Artist",
                    Merchant = Merchant,
                    PosterUrl = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png",
                    PressRelease = "Second test press",
                    Roubles = 3.00m,
                    Time = DateTime.Parse("Sat, 8 Jul 2119 18:00:00Z").ToUniversalTime(),
                }
            };

            Db.Merchants.Add(Merchant);
            Db.Events.AddRange(Events);
        }
    }
}
