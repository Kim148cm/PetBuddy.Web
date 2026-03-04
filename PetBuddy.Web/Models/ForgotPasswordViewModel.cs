using System.ComponentModel.DataAnnotations;

namespace PetBuddy.Web.Models
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu mới không được để trống")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }
    }
}
