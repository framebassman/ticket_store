using System;
using TicketStore.Data.Model;

namespace TicketStore.Api.Tests.Unit.TestData
{
    public class EventsProvider
    {
        private Merchant _merchant;
        private Int32 _idCounter;

        public EventsProvider(Merchant merchant)
        {
            _merchant = merchant;
            _idCounter = 0;
        }

        public Event WithDate(DateTime date)
        {
            _idCounter += 1;
            return new Event
            {
                Id = _idCounter,
                Artist = "Test artist",
                Roubles = 1.00m,
                PressRelease = "Test press release",
                Time = date,
                PosterUrl = "https://ya.ru/logo.png",
                MerchantId = _merchant.Id,
                Merchant = _merchant
            };
        }
    }
}