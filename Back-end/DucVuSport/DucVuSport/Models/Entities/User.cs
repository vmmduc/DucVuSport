namespace DucVuSport.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Blogs = new HashSet<Blog>();
            Orders = new HashSet<Order>();
        }

        public int UserID { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Input full name")]
        public string FullName { get; set; }


        [StringLength(50)]
        [Required(ErrorMessage = "Input phone number")]
        public string PhoneNumber { get; set; }


        [StringLength(50)]
        [Required(ErrorMessage = "Input email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Input email")]
        public string Email { get; set; }


        [StringLength(255)]
        public string PasswordHash { get; set; }

        public DateTime? LastActivity { get; set; }

        public int? RoleID { get; set; }

        public bool Unlock { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Input Province")]
        public string Province { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Input distric")]
        public string District { get; set; }


        [StringLength(50)]
        [Required(ErrorMessage = "Input ward")]
        public string Ward { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Input address")]
        public string AddressDetail { get; set; }
        public bool? IsDeleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Blog> Blogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        public virtual Role Role { get; set; }
    }
}
