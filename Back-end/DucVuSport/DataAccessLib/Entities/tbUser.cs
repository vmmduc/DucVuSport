namespace DataAccessLib.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbUser()
        {
            Carts = new HashSet<Cart>();
        }

        public int id { get; set; }

        [StringLength(100)]
        public string fullName { get; set; }

        [StringLength(100)]
        public string email { get; set; }

        [StringLength(50)]
        public string phoneNumber { get; set; }

        public string passwordHash { get; set; }

        public DateTime? lastActivity { get; set; }

        public bool? unlock { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Carts { get; set; }
    }
}
