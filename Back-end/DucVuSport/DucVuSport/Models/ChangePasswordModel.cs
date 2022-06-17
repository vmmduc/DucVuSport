using System.ComponentModel.DataAnnotations;

namespace DucVuSport.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Nhập mật khẩu cũ")]
        public string oldPassword { get; set; }

        [Required(ErrorMessage = "Nhập mật khẩu mới")]
        public string newPassword { get; set; }

        [Required(ErrorMessage = "Nhập lại mật khẩu")]
        [Compare("newPassword", ErrorMessage = "Mật khẩu nhập lại không đúng")]
        public string confirm { get; set; }
    }
}