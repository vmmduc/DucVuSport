namespace DataAccessLib.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbRole
    {
        [Key]
        [StringLength(100)]
        public string roleID { get; set; }

        public string Name { get; set; }
    }
}
