using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DucVuSport.Models
{
    public class RegisterModel
    {
        public string fullname { get; set; }
        public string email { get; set; }
        public string phonenumber { get; set; }
        public string pwd { get; set; }
        public string repwd { get; set; }
    }
}