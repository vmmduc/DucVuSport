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
        public int ImgID { get; set; }

        public int? ProductID { get; set; }

        public string URL { get; set; }

        public DateTime? Create_date { get; set; }

        public bool? Status { get; set; }

        public virtual Product Product { get; set; }
    }
}
