namespace GameShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comment
    {
        public long user_id { get; set; }

        public long game_id { get; set; }

        [Column("comment")]
        [Required]
        [StringLength(255)]
        public string comment1 { get; set; }

        public long id { get; set; }

        public virtual Game Game { get; set; }

        public virtual User User { get; set; }
    }
}
