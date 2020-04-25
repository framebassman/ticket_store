using System;
using System.Collections.Generic;
using TicketStore.Api.Model.Email;
using TicketStore.Api.Model.PdfDocument;

namespace TicketStore.Api.Tests.Unit.Stubs
{
    public class DummyEmailService : EmailService
    {
        private readonly Dictionary<String, List<Pdf>> _storage;

        public DummyEmailService()
        {
            _storage = new Dictionary<string, List<Pdf>>();
        }
        
        public override void SendTicket(String to, Pdf ticket)
        {
            List<Pdf> items;
            if (_storage.TryGetValue(to, out items))
            {
                items.Add(ticket);
            }
            else
            {
                _storage.Add(to, new List<Pdf> { ticket });
            }
        }

        public override void Dispose()
        {
        }

        public List<Pdf> PdfList(String to)
        {
            return _storage[to];
        }

        public Boolean IsExist(String to)
        {
            return _storage.ContainsKey(to);
        }
    }
}