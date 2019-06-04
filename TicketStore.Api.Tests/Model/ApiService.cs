using System;
using RestSharp;

namespace TicketStore.Api.Tests.Model
{
    public class ApiService : TicketStoreService
    {
        protected override int Port()
        {
            return 3000;
        }

        public IRestResponse SendTestPayment()
        {
            var request = new RestRequest("api/payments", Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("test_notification", true);
            return Client.Execute(request);
        }

        public IRestResponse SendPayment(String email, Decimal withdraw_amount, Decimal amount)
        {
            var request = new RestRequest("api/payments", Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("withdraw_amount", withdraw_amount);
            request.AddParameter("amount", amount);
            request.AddParameter("email", email);
            return Client.Execute(request);
        }
    }
}
