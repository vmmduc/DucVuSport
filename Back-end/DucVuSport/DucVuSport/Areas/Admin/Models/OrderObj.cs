using System;

namespace DucVuSport.Areas.Admin.Models
{
    public class OrderObj
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? Status { get; set; }
    }
}