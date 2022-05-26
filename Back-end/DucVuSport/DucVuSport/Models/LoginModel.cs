using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DucVuSport.Models
{
    public class LoginModel
    {
        public string email { get; set; }
        public string password { get; set; }
        public bool remember { get; set; }
    }
}