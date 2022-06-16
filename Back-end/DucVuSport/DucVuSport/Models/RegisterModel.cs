using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DucVuSport.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Nhập họ và tên")]
        public string fullName { get; set; }

        [Required(ErrorMessage = "Nhập số điện thoại")]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "Nhập email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Chưa đúng định dạng email")]
        public string email { get; set; }


        [Required(ErrorMessage = "Nhập mật khẩu")]
        public string password { get; set; }


        [Required(ErrorMessage = "Nhập lại mật khẩu")]
        [Compare("password", ErrorMessage = "Mật khẩu nhập lại không đúng")]
        public string repassword { get; set; }
    }
}