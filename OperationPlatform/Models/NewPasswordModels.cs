using System.ComponentModel.DataAnnotations;

namespace OperationPlatform.Models
{

    public class NewPwdModels
    {
        [Required]
        [Display(Name = "旧密码")]
        public string OldPwd { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string NewPwd { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "重复密码")]
        public string RepeatPwd { get; set; }

        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }
    }

}