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
        public int id { get; set; }

        [StringLength(100)]
        public string fullname { get; set; }

        public bool? gender { get; set; }

        [StringLength(50)]
        public string phoneNumber { get; set; }

        public string address { get; set; }

        public int? userID { get; set; }
    }
}
