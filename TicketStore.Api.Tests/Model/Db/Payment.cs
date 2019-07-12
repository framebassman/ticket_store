using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketStore.Api.Tests.Model.Db
{
    [Table("payments")]
    public class Payment
    {
        [Column("id")]
        public Int32 Id { get; set; }
        [Column("email")]
        public String Email { get; set; }
        [Column("amount")]
        public Decimal Amount { get; set; }

        public List<Ticket> Tickets { get; set; }
    }
}
