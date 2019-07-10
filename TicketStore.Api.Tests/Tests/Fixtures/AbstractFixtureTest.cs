using System;
using System.Linq;
using System.Collections.Generic;
using TicketStore.Api.Tests.Model.Db;
using Xunit;

namespace TicketStore.Api.Tests.Tests.Fixtures
{
    public class AbstractFixtureTest : IClassFixture<ApiFixture>
    {
        protected ApiFixture Fixture;
        protected Merchant Merchant;
        protected List<Event> Events;
        

        public AbstractFixtureTest(ApiFixture fixture)
        {
            this.Fixture = fixture;
            CleanUpDatabase();
            SeedTestData();
            Fixture.Db.SaveChanges();
        }

        public void CleanUpDatabase()
        {
            if (Fixture.Db.Events.Count() != 0)
            {
                Fixture.Db.Events.RemoveRange(Fixture.Db.Events);
            }
            if (Fixture.Db.Merchants.Count() != 0)
            {
                Fixture.Db.Merchants.RemoveRange(Fixture.Db.Merchants);
            }
        }

        public void SeedTestData()
        {
            Fixture.Db.Events.RemoveRange(Events);
            Fixture.Db.Merchants.RemoveRange(Merchant);
            Merchant = new Merchant
            {
                Place = "Test Place",
                YandexMoneyAccount = "1234567890"
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
                    Time = DateTime.Parse("Tue, 9 Jul 2019 17:00:00Z"),
                },
                new Event
                {
                    Artist = "Second Test Artist",
                    Merchant = Merchant,
                    PosterUrl = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png",
                    PressRelease = "Second test press",
                    Roubles = 3.00m,
                    Time = DateTime.Parse("Mon, 8 Jul 2019 18:00:00Z"),
                }
            };

            Fixture.Db.Merchants.Add(Merchant);
            Fixture.Db.Events.AddRange(Events);
        }
    }
}
