namespace DucVuSport.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
        [Required(ErrorMessage = "Nhap ho ten")]
        public string FullName { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Nhap so dien thoai")]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Nhap email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Chua dung dinh dang email")]
        public string Email { get; set; }

        [StringLength(255)]
        public string PasswordHash { get; set; }

        public DateTime? LastActivity { get; set; }

        public int? RoleID { get; set; }

        public bool Unlock { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Nhap tinh/thanh pho")]
        public string Province { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Nhap quan/huyen")]
        public string District { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Nhap xa phuong")]
        public string Ward { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Nhap dia chi")]
        public string AddressDetail { get; set; }

        public bool? IsDeleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Blog> Blogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        public virtual Role Role { get; set; }
    }
}
