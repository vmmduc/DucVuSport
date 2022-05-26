namespace DucVuSport.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BlogCategory")]
    public partial class BlogCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BlogCategory()
        {
            Blogs = new HashSet<Blog>();
        }

        [Key]
        public int CatID { get; set; }

        [StringLength(255)]
        public string CateName { get; set; }

        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(255)]
        public string Alias { get; set; }

        [StringLength(255)]
        public string MetaDesc { get; set; }

        [StringLength(255)]
        public string MetaKey { get; set; }

        [StringLength(255)]
        public string Thumb { get; set; }

        public bool Published { get; set; }

        public int? Ordering { get; set; }

        public int? Parent { get; set; }

        public int? Levels { get; set; }

        [StringLength(255)]
        public string Cover { get; set; }

        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Blog> Blogs { get; set; }
    }
}
