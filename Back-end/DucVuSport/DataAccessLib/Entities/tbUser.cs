namespace DataAccessLib.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbUser
    {
        [Key]
        [StringLength(100)]
        public string userID { get; set; }

        [StringLength(100)]
        public string fullName { get; set; }

        [StringLength(100)]
        public string email { get; set; }

        [StringLength(100)]
        public string phoneNumber { get; set; }

        public string address { get; set; }

        public DateTime? lockOutEndDate { get; set; }

        public string passwordHash { get; set; }

        public bool? unlock { get; set; }
    }
}
