using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TicketStore.Data.Model;

namespace TicketStore.Data.Model
{
    [Table("events")]
    public class Event
    {
        [Column("id")]
        public Int32 Id { get; set; }
        [Column("artist")]
        public String Artist { get; set; }
        [Column("roubles")]
        public Decimal Roubles { get; set; }
        [Column("press_release")]
        public String PressRelease { get; set; }
        [Column("time")]
        public DateTime Time { get; set; }
        [Column("poster_url")]
        public String PosterUrl { get; set; }
        
        [Column("merchant_id")]
        public Int32 MerchantId { get; set; }
        [JsonIgnore]
        public Merchant Merchant { get; set; }
        
        [JsonIgnore]
        public List<Ticket> Tickets { get; set; }
    }
}
