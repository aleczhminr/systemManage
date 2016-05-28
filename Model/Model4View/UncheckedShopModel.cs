using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Model.Model4View
{
    /// <summary>
    /// 审核店铺Model
    /// </summary>
    public class UncheckedShopModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 店铺名称
        /// </summary>
        [Display(Name = "店铺名称")]
        public string CompanyName { get; set; }
        /// <summary>
        /// 店铺地址
        /// </summary>
        public string CompanyAddress { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [Display(Name = "手机号码")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 邮件地址
        /// </summary>
        [Display(Name = "邮件")]
        public string UserEmail { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        [Display(Name = "注册时间")]
        public DateTime RegTime { get; set; }
        /// <summary>
        /// 登录信息
        /// </summary>
        public string Loginbrslast { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserRealName { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [Display(Name = "验证码")]
        public string Reg_Code { get; set; }

        /// <summary>
        /// 是否注册成功
        /// </summary>
        public int regOk { get; set; }

    }

    public class UncheckedShopContext : DbContext
    {
        public DbSet<UncheckedShopModel> UncheckedShop { get; set; }

        public System.Data.Entity.DbSet<Model.T_AccountBasic> T_AccountBasic { get; set; }
    }
}
