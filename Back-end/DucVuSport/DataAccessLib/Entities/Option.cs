namespace DataAccessLib.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Option")]
    public partial class Option
    {
        public int id { get; set; }

        [StringLength(50)]
        public string optionName { get; set; }

        [Column(TypeName = "money")]
        public decimal? price { get; set; }

        public double? discount { get; set; }

        public int? amount { get; set; }

        [StringLength(50)]
        public string productID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? createDate { get; set; }

        public virtual Product Product { get; set; }
    }
}
