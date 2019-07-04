using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketStore.Api.Model
{
    [Table("tickets")]
    public class Ticket
    {
        [Column("id")]
        public Int32 Id { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("number")]
        public String Number { get; set; }
        [Column("roubles")]
        public Decimal Roubles { get; set; }
        [Column("expired")]
        public Boolean Expired { get; set; }
        [Column("event_name")]
        public String EventName { get; set; }
        
        [Column("payment_id")]
        public Int32 PaymentId { get; set; }
        public Payment Payment { get; set; }
    }
}