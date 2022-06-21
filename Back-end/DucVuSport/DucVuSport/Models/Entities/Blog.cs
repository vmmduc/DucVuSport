namespace DucVuSport.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Blog")]
    public partial class Blog
    {
        public int BlogID { get; set; }

        [StringLength(255)]
        public string Title { get; set; }

        [AllowHtml]
        public string Content { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? CreateBy { get; set; }

        [StringLength(255)]
        public string ShortContent { get; set; }

        [StringLength(255)]
        public string Tag { get; set; }

        public int? BlogCatID { get; set; }

        public virtual User User { get; set; }

        public virtual BlogCategory BlogCategory { get; set; }
    }
}
