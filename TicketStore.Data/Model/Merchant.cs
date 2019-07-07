using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace TicketStore.Data.Model
{
    [Table("merchants")]
    public class Merchant
    {
        [Column("id")]
        public Int32 Id { get; set; }
        [Column("yandex_money_account")]
        public String YandexMoneyAccount { get; set; }
        [Column("place")]
        public String Place { get; set; }
        
        public List<Event> Events { get; set; }
    }
}