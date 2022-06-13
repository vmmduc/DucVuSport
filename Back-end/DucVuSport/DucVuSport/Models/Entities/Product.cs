namespace DucVuSport.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Mvc;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductID { get; set; }

        [Required(ErrorMessage = "Nh?p tên s?n ph?m")]
        [StringLength(255)]
        public string ProductName { get; set; }

        [StringLength(255)]
        public string ShortDescription { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        [Required(ErrorMessage = "Ch?n ?nh")]
        [StringLength(255)]
        [DataType(DataType.Upload)]
        public string Image { get; set; }

        public int? CategoryID { get; set; }

        [Required(ErrorMessage = "Nh?p giá s?n ph?m")]
        public long? Price { get; set; }

        public double? Discount { get; set; }

        [Required(ErrorMessage = "Nh?p s? l??ng")]
        public int? Quantity { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? Sold { get; set; }

        public int? ViewCount { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
