using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketStore.Api.Model
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
    }
}