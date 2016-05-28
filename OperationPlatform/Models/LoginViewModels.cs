using System.ComponentModel.DataAnnotations;

namespace OperationPlatform.Models
{

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "验证码")]
        public string VerifyCode { get; set; }
    }

}