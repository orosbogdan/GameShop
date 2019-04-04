namespace GameShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Comments = new HashSet<Comment>();
            Ratings = new HashSet<Rating>();
            Transactions = new HashSet<Transaction>();
        }

        public long id { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z]\w+|[0-9][0-9_]*[a-zA-Z]+\w*$",ErrorMessage ="Username allows only alphanumeric characters.")]
        [Index(IsUnique = true)]
        [Remote("IsUserExists", "Home", ErrorMessage = "User Name already in use")]
        public string username { get; set; }

        [Required]
        [StringLength(250)]
        [EmailAddress(ErrorMessage = "Invalid Email Address Format")]
        [Index(IsUnique = true)]
        public string email { get; set; }

        [Required]
        [StringLength(32)]
 
        public string password { get; set; }

        [Column(TypeName = "money")]
        public decimal balance { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rating> Ratings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
