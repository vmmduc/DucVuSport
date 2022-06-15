namespace DucVuSport.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderID { get; set; }

        public int? CustomerID { get; set; }

        public DateTime? OrderDate { get; set; }

        public bool? Paid { get; set; }

        public DateTime? PaymentDate { get; set; }

        public int? PaymentID { get; set; }
        public long? Total { get; set ;} 

        [StringLength(255)]
        public string Note { get; set; }

        [StringLength(255)]
        public string ShipID { get; set; }

        public int? Status { get; set; }

        public virtual OrderStatu OrderStatu { get; set; }

        public virtual Payment Payment { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
