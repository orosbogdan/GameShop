namespace GameShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Transaction
    {
        public long id { get; set; }

        public long user_id { get; set; }

        public long game_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime date { get; set; }

        [Column(TypeName = "money")]
        public decimal price { get; set; }

        public virtual Game Game { get; set; }

        public virtual User User { get; set; }
    }
}
