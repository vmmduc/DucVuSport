using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DucVuSport.Models
{
    public class LoginModel
    {

        [Required(ErrorMessage = "Nhập email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Chưa đúng định dạng email")]
        public string email { get; set; }


        [Required(ErrorMessage = "Nhập mật khẩu")]
        public string password { get; set; }

        public bool remember { get; set; }
    }
}