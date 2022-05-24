using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DucVuSport.Models
{
    public class LoginModel
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }
        public bool remember { get; set; }
    }
}