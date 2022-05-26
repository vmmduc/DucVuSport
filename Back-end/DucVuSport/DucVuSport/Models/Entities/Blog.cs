namespace DucVuSport.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Blog")]
    public partial class Blog
    {
        [Key]
        public int NewID { get; set; }

        [StringLength(255)]
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime? Create_date { get; set; }

        public int? AccountID { get; set; }

        [StringLength(255)]
        public string ShotContent { get; set; }

        [StringLength(255)]
        public string Author { get; set; }

        public string Tag { get; set; }

        public int? BlogCatID { get; set; }

        public bool? NewFeed { get; set; }

        public virtual Account Account { get; set; }

        public virtual BlogCategory BlogCategory { get; set; }
    }
}
