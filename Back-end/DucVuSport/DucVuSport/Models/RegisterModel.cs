using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DucVuSport.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Nhập họ tên")]
        public string fullname { get; set; }

        [Required(ErrorMessage = "Nhập email")]
        public string email { get; set; }

        [Required(ErrorMessage = "Nhập số điện thoại")]
        public string phonenumber { get; set; }

        [Required(ErrorMessage = "Nhập mật khẩu")]
        public string pwd { get; set; }

        [Required(ErrorMessage = "Nhập lại mật khẩu")]
        [Compare("pwd", ErrorMessage = "Mật khẩu nhập lại không đúng")]
        public string repwd { get; set; }
    }
}