using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DucVuSport.Models
{
    public class ChangePasswordModel
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }

        [Compare("newPassword")]
        public string confirm { get; set; }
    }
}