namespace DataAccessLib.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Feedback")]
    public partial class Feedback
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FeedbackID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(12)]
        public string Phone_Number { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public string Content { get; set; }

        public int? VoteNumber { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? ProductID { get; set; }

        public virtual Product Product { get; set; }
    }
}
