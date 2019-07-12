using System;
using RestSharp;
using TicketStore.Api.Tests.Environment;
using TicketStore.Api.Tests.Model.Services.Verify;

namespace TicketStore.Api.Tests.Model.Services
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

        public IRestResponse SendPayment(String sender, YandexPaymentLabel label, String email, Decimal withdraw_amount, Decimal amount)
        {
            var request = new RestRequest("api/payments", Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("sender", sender);
            request.AddParameter("label", label.Value());
            request.AddParameter("email", email);
            request.AddParameter("withdraw_amount", withdraw_amount);
            request.AddParameter("amount", amount);
            return Client.Execute(request);
        }

        public IRestResponse VerifyBarcode(string ticketNumber)
        {
            var request = CreateVerifyRequest(ticketNumber);
            request.AddHeader("Authorization", "Bearer pkR9vfZ9QdER53mf");
            return Client.Execute(request);
        }

        public IRestResponse VerifyBarcodeWithoutAuth(string ticketNumber)
        {
            var request = CreateVerifyRequest(ticketNumber);
            return Client.Execute(request);
        }

        private RestRequest CreateVerifyRequest(string ticketNumber)
        {
            var request = new RestRequest("api/verify", Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new Barcode(ticketNumber));
            return request;
        }
    }
}
