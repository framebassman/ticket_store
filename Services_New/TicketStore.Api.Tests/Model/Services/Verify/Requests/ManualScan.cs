namespace TicketStore.Api.Tests.Model.Services.Verify.Requests
{
    public class ManualScan : TurnstileScan
    {
        public ManualScan(string barcode) : base(barcode)
        {
            method = "Manual";
        }
    }
}
