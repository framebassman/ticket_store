using System;
using DinkToPdf.Contracts;
using DinkToPdf.EventDefinitions;

namespace TicketStore.Api.Tests.Unit.Stubs
{
    public class DummyConverter : IConverter
    {
        public byte[] Convert(IDocument document)
        {
            var result = new byte[1];
            result[0] = 0;
            return result;
        }

        public event EventHandler<PhaseChangedArgs> PhaseChanged;
        public event EventHandler<ProgressChangedArgs> ProgressChanged;
        public event EventHandler<FinishedArgs> Finished;
        public event EventHandler<ErrorArgs> Error;
        public event EventHandler<WarningArgs> Warning;
    }
}