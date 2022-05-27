namespace DucVuSport.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Bills = new HashSet<Bill>();
            tbOrders = new HashSet<tbOrder>();
        }

        public int CustomerID { get; set; }

        [StringLength(255)]
        public string FullName { get; set; }


        [StringLength(50)]
        public string PhoneNumber { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        public int? AccountID { get; set; }

        public int? Province { get; set; }

        public int? District { get; set; }

        public int? Ward { get; set; }

        [StringLength(255)]
        public string AddressDetail { get; set; }

        public virtual Account Account { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bill> Bills { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbOrder> tbOrders { get; set; }
    }
}
