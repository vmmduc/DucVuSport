namespace DataAccessLib.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Image")]
    public partial class Image
    {
        [Key]
        public int imgID { get; set; }

        [StringLength(50)]
        public string productID { get; set; }

        public string url { get; set; }

        public virtual Product Product { get; set; }
    }
}
