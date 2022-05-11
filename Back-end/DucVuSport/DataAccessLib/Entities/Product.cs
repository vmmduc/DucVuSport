namespace DataAccessLib.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Carts = new HashSet<Cart>();
            Images = new HashSet<Image>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int productID { get; set; }

        [StringLength(50)]
        public string productName { get; set; }

        public string shotDescribe { get; set; }

        public string describe { get; set; }

        [StringLength(100)]
        public string img { get; set; }

        public int? categoryID { get; set; }

        public int? supplierID { get; set; }

        public long? price { get; set; }

        public double? discount { get; set; }

        public int? quantity { get; set; }

        [Column(TypeName = "date")]
        public DateTime? createDate { get; set; }

        public int? view_count { get; set; }

        public bool? status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Carts { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Image> Images { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
