using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //T_Account_User
    /// <summary>
    /// 店铺登录账号信息
    /// </summary>	
    [Serializable]
    public partial class T_Account_User
    {

        /// <summary>
        /// id
        /// </summary>		
        public int id { get; set; }
        /// <summary>
        /// 账号
        /// </summary>		
        public string account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>		
        public string passwd { get; set; }
        /// <summary>
        /// 名称
        /// </summary>		
        public string name { get; set; }
        /// <summary>
        /// sex
        /// </summary>		
        public string sex { get; set; }
        /// <summary>
        /// 店铺ID
        /// </summary>		
        public int accountid { get; set; }
        /// <summary>
        /// 权限
        /// </summary>		
        public string grade { get; set; }
        /// <summary>
        /// integral
        /// </summary>		
        public int integral { get; set; }
        /// <summary>
        /// recharge
        /// </summary>		
        public int recharge { get; set; }
        /// <summary>
        /// dele_user
        /// </summary>		
        public int dele_user { get; set; }
        /// <summary>
        /// dele_sell
        /// </summary>		
        public int dele_sell { get; set; }
        /// <summary>
        /// zc_insert
        /// </summary>		
        public int zc_insert { get; set; }
        /// <summary>
        /// shop_insert
        /// </summary>		
        public int shop_insert { get; set; }
        /// <summary>
        /// shop_class
        /// </summary>		
        public int shop_class { get; set; }
        /// <summary>
        /// regtime
        /// </summary>		
        public DateTime regtime { get; set; }
        /// <summary>
        /// 电话
        /// </summary>		
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 邮件
        /// </summary>		
        public string UserEmail { get; set; }
        /// <summary>
        /// Perssion
        /// </summary>		
        public string Perssion { get; set; }
        /// <summary>
        /// PhoneConty
        /// </summary>		
        public string PhoneConty { get; set; }
        /// <summary>
        /// PhoneCity
        /// </summary>		
        public string PhoneCity { get; set; }
        /// <summary>
        /// TrueConty
        /// </summary>		
        public string TrueConty { get; set; }
        /// <summary>
        /// TrueCity
        /// </summary>		
        public string TrueCity { get; set; }
        /// <summary>
        /// UserPower
        /// </summary>		
        public int UserPower { get; set; }

    }
    /// <summary>
    /// 店铺登录账号基本信息
    /// </summary>
    public partial class T_Account_UserBasic
    {
        public int id { get; set; }
        /// <summary>
        /// 店铺ID
        /// </summary>
        public int accountid { get; set; }
        /// <summary>
        /// 登录账号
        /// </summary>
        public string account { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public string grade { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string UserEmail { get; set; }
    }
}

