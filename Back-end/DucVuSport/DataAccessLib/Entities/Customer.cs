namespace DataAccessLib.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(100)]
        public string FullName { get; set; }

        public bool? Gender { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public int? UserID { get; set; }
    }
}
