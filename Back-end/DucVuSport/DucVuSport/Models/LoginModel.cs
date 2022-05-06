using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DucVuSport.Models
{
    public class LoginModel
    {
        public string email { get; set; }
        public string passwd { get; set; }
        public bool remember { get; set; }
    }
}