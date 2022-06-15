namespace DucVuSport.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        [Key]
        public int OrderDetail_ID { get; set; }

        public int? OrderID { get; set; }

        public int ProductID { get; set; }

        public int? Quantity { get; set; }

        public double? Discount { get; set; }

        public long? Total { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
