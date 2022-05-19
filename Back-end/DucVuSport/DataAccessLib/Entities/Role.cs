namespace DataAccessLib.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Role
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RoleID { get; set; }

        [StringLength(50)]
        public string RoleName { get; set; }

        [StringLength(100)]
        public string Detail { get; set; }

        public virtual Account Account { get; set; }
    }
}
