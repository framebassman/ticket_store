using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using NHamcrest;
using TicketStore.Api.Tests.Data;
using TicketStore.Api.Tests.Model.Db;
using TicketStore.Api.Tests.Model.Services;
using TicketStore.Api.Tests.Tests.Fixtures;
using Xunit;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;
using Assert = NHamcrest.XUnit.Assert;

namespace TicketStore.Api.Tests.Tests.Features;

public class EventsTests : TestBed<ApiDIFixture>
{
    private ITestOutputHelper _logger;
    private ApplicationContext _db;
    private WebService _web;
    private ApiService _api;

    private Merchant _merchant;
    private List<Event> _events;

    public EventsTests(ITestOutputHelper logger, ApiDIFixture fixture) : base(logger, fixture)
    {
        _logger = logger;
        _web = fixture.GetService<WebService>(logger);
        _api = fixture.GetService<ApiService>(logger);
        _db = fixture.GetService<ApplicationContext>(logger);
        _logger.WriteLine("Seed database");
        SeedData();
    }

    [Fact]
    public void GetEvents_WithoutMerchantId_ShouldReturnBadRequest()
    {
        _logger.WriteLine("Make a request");
        var response = _web.GetEvents();

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Fact]
    public void GetEvents_ShouldReturnEvents()
    {
        _logger.WriteLine("Make a request with merchant id: " + _merchant.Id);
        var response = _web.GetEvents(_merchant.Id);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    private void SeedData()
    {
        // _db.Merchants.RemoveRange(_db.Merchants.ToList());
        // _db.Events.RemoveRange(_db.Events.ToList());
        //_db.Payments.RemoveRange(_db.Payments.ToList());
        // _db.Tickets.RemoveRange(_db.Tickets.ToList());
        // _db.SaveChanges();
        _merchant = new Merchant
        {
            Place = "Test1",
            YandexMoneyAccount = Generator.YandexMoneyAccount()
        };
        _events = new List<Event>
        {
            new Event
            {
                Artist = "First Test Artist",
                Merchant = _merchant,
                PosterUrl = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png",
                PressRelease = "First test press",
                Roubles = 2.00m,
                Time = DateTime.Parse("Sun, 9 Jul 2119 17:00:00Z").ToUniversalTime(),
            },
            new Event
            {
                Artist = "Second Test Artist",
                Merchant = _merchant,
                PosterUrl = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png",
                PressRelease = "Second test press",
                Roubles = 3.00m,
                Time = DateTime.Parse("Sat, 8 Jul 2119 18:00:00Z").ToUniversalTime(),
            }
        };

        _db.Merchants.Add(_merchant);
        _db.Events.AddRange(_events);
        _db.SaveChanges();
        var count = _db.Merchants.Count();
        _logger.WriteLine("Merchants count: " + count);
        _logger.WriteLine("Try to get the merchant from valid merchant id from DataBase");
        _merchant = _db.Merchants
            .First(m => m.Place == "Test1")
        _logger.WriteLine("Merchant ID from the database is " + _merchant.Id);
    }
}
