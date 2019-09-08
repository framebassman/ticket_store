using System;

namespace TicketStore.Api.Model.Validation.Exceptions
{
    public class CodeToShort : FindException
    {
        public CodeToShort(string verificationMethod, Double codeLength)
            : base(verificationMethod, $"Searchable part of code is shorter than {codeLength} characters")
        {
        }
    }
}
