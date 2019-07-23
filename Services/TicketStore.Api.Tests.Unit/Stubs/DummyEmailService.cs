using System;
using System.Collections.Generic;
using TicketStore.Api.Model.Email;
using TicketStore.Api.Model.Pdf;

namespace TicketStore.Api.Tests.Unit.Stubs
{
    public class DummyEmailService : EmailService
    {
        private readonly Dictionary<String, Pdf> _storage;

        public DummyEmailService()
        {
            _storage = new Dictionary<string, Pdf>();
        }
        
        public override void SendTicket(String to, Pdf ticket)
        {
            _storage.Add(to, ticket);
        }

        public override void Dispose()
        {
        }

        public Pdf Pdf(String to)
        {
            return _storage[to];
        }

        public Boolean IsExist(String to)
        {
            return _storage.ContainsKey(to);
        }
    }
}