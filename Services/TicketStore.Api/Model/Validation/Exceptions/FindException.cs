using System;

namespace TicketStore.Api.Model.Validation.Exceptions
{
    public abstract class FindException : Exception
    {
        public FindException(String verificationMethod, String message)
            : base($"Method: {verificationMethod}. {message}") {}
    }
}
