using System;
using System.Collections.Generic;
using System.Linq;
using TicketStore.Api.Tests.Data;
using TicketStore.Api.Tests.Model.Db;

namespace TicketStore.Api.Tests.Tests.DevelopmentData
{
    public class DevelopmentData
    {
        private Merchant _merchant;
        private List<Event> _concerts;
        private List<Ticket> _tickets;
        private Payment _payment;
        private Boolean _areMerchantsExist;
        private Boolean _areEventsExist;
        private Boolean _areTicketsExist;
        private Boolean _arePaymentsExist;

        public DevelopmentData()
        {
            _merchant = new Merchant
            {
                YandexMoneyAccount = "987654321",
                Place = "Cherdak",
            };
            _concerts = new List<Event> {
                CreateEvent("Muse"),
                CreateEvent("Blur"),
                CreateEvent("The Queen"),
                CreateEvent("The Beatles"),
                CreateEvent("The Cellophane Heads"),
                CreateEvent("Rasmus"),
                CreateEvent("KS"),
                CreateEvent("O2"),
                CreateEvent("Hoobstank"),
                CreateEvent("Gorillazz"),
                CreateEvent("Safe"),
                CreateEvent("Weezer"),
                CreateEvent("NRKTK"),
            };
            _tickets = new List<Ticket>
            {
                CreateTicket("000", false),
                CreateTicket("111", false),
                CreateTicket("222", false),
                CreateTicket("333", false),
                CreateTicket("444", false),
                CreateTicket("555", false),
                CreateTicket("666", false),
                CreateTicket("666", false),
                CreateTicket("777", false),
                CreateTicket("888", false),
                CreateTicket("999", false),
            };
            _payment = new Payment
            {
                Amount = 1000,
                Email = "test@test.test",
                Tickets = _tickets
            };
        }

        public Boolean IsExistIn(ApplicationContext db)
        {
            return false;
        }

        public void InsertTo(ApplicationContext db)
        {
            db.Merchants.RemoveRange(db.Merchants);
            db.Events.RemoveRange(db.Events);
            db.Tickets.RemoveRange(db.Tickets);
            db.Payments.RemoveRange(db.Payments);

            db.Merchants.Add(_merchant);
            db.Events.AddRange(_concerts);
            db.Tickets.AddRange(_tickets);
            db.Payments.Add(_payment);

            db.SaveChanges();
        }

        private Event CreateEvent(string artist)
        {
            var newEvent = new Event
            {
                Artist = artist,
                Roubles = 200,
                PressRelease = "Not Bad",
                Time = DateTime.UtcNow + TimeSpan.FromHours(12),
                PosterUrl = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png",
                Merchant = _merchant
            };
            return newEvent;
        }

        private Ticket CreateTicket(String number, Boolean expired)
        {
            var newTicket = new Ticket
            {
                CreatedAt = new DateTime(2019, 7, 9, 16, 10, 0, DateTimeKind.Utc),
                Number = number,
                Expired = expired,
                Roubles = 100,
                Event = _concerts[0]
            };
            return newTicket;
        }
    }
}
