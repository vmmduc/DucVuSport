using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DucVuSport.Models
{
    public class ChangePasswordModel
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string confirm { get; set; }
    }
}