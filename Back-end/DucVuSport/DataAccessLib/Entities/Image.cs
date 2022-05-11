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

        public int? productID { get; set; }

        public string url { get; set; }

        public DateTime? create_at { get; set; }

        public bool? status { get; set; }

        public virtual Product Product { get; set; }
    }
}
