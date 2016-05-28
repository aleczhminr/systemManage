using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel.DataAnnotations;
namespace Model
{
    //T_Account
    /// <summary>
    /// T_Account
    /// </summary>	
    [Serializable]
    public partial class T_Account
    {

        /// <summary>
        /// ID
        /// </summary>		
        public long ID { get; set; }
        /// <summary>
        /// UserRealName
        /// </summary>		
        public string UserRealName { get; set; }
        /// <summary>
        /// userpasswd
        /// </summary>		
        public string userpasswd { get; set; }
        /// <summary>
        /// PhoneNumber
        /// </summary>		
        public string PhoneNumber { get; set; }
        /// <summary>
        /// useremail
        /// </summary>		
        public string useremail { get; set; }
        /// <summary>
        /// RandomNumber
        /// </summary>		
        public string RandomNumber { get; set; }
        /// <summary>
        /// CompanyName
        /// </summary>		
        public string CompanyName { get; set; }
        /// <summary>
        /// CompanyAddress
        /// </summary>		
        public string CompanyAddress { get; set; }
        /// <summary>
        /// ServiceManager
        /// </summary>		
        public string ServiceManager { get; set; }
        /// <summary>
        /// RegTime
        /// </summary>		
        public DateTime RegTime { get; set; }
        /// <summary>
        /// LoginTimeWeb
        /// </summary>		
        public int LoginTimeWeb { get; set; }
        /// <summary>
        /// LoginTimeLast
        /// </summary>		
        public DateTime LoginTimeLast { get; set; }
        /// <summary>
        /// State
        /// </summary>		
        public int State { get; set; }
        /// <summary>
        /// SFZNumber
        /// </summary>		
        public string SFZNumber { get; set; }
        /// <summary>
        /// dxstatus
        /// </summary>		
        public int dxstatus { get; set; }
        /// <summary>
        /// proportion
        /// </summary>		
        public string proportion { get; set; }
        /// <summary>
        /// nexttotal
        /// </summary>		
        public string nexttotal { get; set; }
        /// <summary>
        /// reguserid
        /// </summary>		
        public int reguserid { get; set; }
        /// <summary>
        /// shotName
        /// </summary>		
        public string shotName { get; set; }
        /// <summary>
        /// subjection
        /// </summary>		
        public string subjection { get; set; }
        /// <summary>
        /// max_shop
        /// </summary>		
        public int max_shop { get; set; }
        /// <summary>
        /// logintimebreak
        /// </summary>		
        public DateTime logintimebreak { get; set; }
        /// <summary>
        /// loginbrslast
        /// </summary>		
        public string loginbrslast { get; set; }
        /// <summary>
        /// BBSusername
        /// </summary>		
        public string BBSusername { get; set; }
        /// <summary>
        /// emailChk
        /// </summary>		
        public int emailChk { get; set; }
        /// <summary>
        /// phoneChk
        /// </summary>		
        public int phoneChk { get; set; }
        /// <summary>
        /// Parent_AccountId
        /// </summary>		
        public int Parent_AccountId { get; set; }
        /// <summary>
        /// Reg_Code
        /// </summary>		
        public string Reg_Code { get; set; }
        /// <summary>
        /// Type
        /// </summary>		
        public int Type { get; set; }
        /// <summary>
        /// Imgurl
        /// </summary>		
        public string Imgurl { get; set; }
        /// <summary>
        /// manager_name
        /// </summary>		
        public string manager_name { get; set; }
        /// <summary>
        /// AgentId
        /// </summary>		
        public string AgentId { get; set; }
        /// <summary>
        /// Remark
        /// </summary>		
        public string Remark { get; set; }
        /// <summary>
        /// ShopType
        /// </summary>		
        public string ShopType { get; set; }
        /// <summary>
        /// py_full
        /// </summary>		
        public string py_full { get; set; }
        /// <summary>
        /// py_code
        /// </summary>		
        public string py_code { get; set; }
        /// <summary>
        /// Guider_Step
        /// </summary>		
        public string Guider_Step { get; set; }
        /// <summary>
        /// BBS_Uid
        /// </summary>		
        public int BBS_Uid { get; set; }
        /// <summary>
        /// weixin_openid
        /// </summary>		
        public string weixin_openid { get; set; }
        /// <summary>
        /// RecommendId
        /// </summary>		
        public int RecommendId { get; set; }
        /// <summary>
        /// Flag
        /// </summary>		
        public string Flag { get; set; }

    }


    /// <summary>
    /// 店铺
    /// </summary>
    public partial class T_AccountBasic
    {
        /// <summary>
        /// ID
        /// </summary>	
        public long ID { get; set; }
        /// <summary>
        /// 店铺名称
        /// </summary>		
        public string CompanyName { get; set; }
        /// <summary>
        /// 店主
        /// </summary>		
        public string UserRealName { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// 简称
        /// </summary>		
        public string shotName { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>		
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime RegTime { get; set; }
        /// <summary>
        /// 店铺地址
        /// </summary>		
        public string CompanyAddress { get; set; }
        /// <summary>
        /// 客户经理
        /// </summary>		
        public string ServiceManager { get; set; }
        /// <summary>
        /// 登录次数
        /// </summary>		
        public int LoginTimeWeb { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>		
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
        public DateTime LoginTimeLast { get; set; }
        /// <summary>
        /// 代理商
        /// </summary>		
        public string AgentId { get; set; }
        /// <summary>
        /// 代理商
        /// </summary>
        public string AgentName { get; set; }
        /// <summary>
        /// 版本
        /// </summary>		
        public int aotjb { get; set; }
        /// <summary>
        /// 版本名称
        /// </summary>
        public string aotjbName { get; set; }
        /// <summary>
        /// 版本到期时间
        /// </summary>		
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime endtime { get; set; }
        /// <summary>
        /// 总会员数
        /// </summary>		
        public int gsreguser { get; set; }
        /// <summary>
        /// 积分
        /// </summary>		
        public int integral { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>		
        public int active { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        public string activeName { get; set; }
        /// <summary>
        /// 短信配置信息
        /// </summary>		
        public string SmsBilling { get; set; }
        /// <summary>
        /// 短信剩余数
        /// </summary>
        public int smsSurplusNum { get; set; }
        /// <summary>
        /// 登录来源标记
        /// </summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 店铺汇总
    /// </summary>
    public partial class T_AccountSummarize
    {
        /// <summary>
        /// 今天汇总
        /// </summary>
        public partial class TodaySummarize
        {
            /// <summary>
            /// 销售数
            /// </summary>
            public int saleNum { get; set; }
            /// <summary>
            /// 销售金额
            /// </summary>
            public decimal saleMoney { get; set; }
            /// <summary>
            /// 会员数
            /// </summary>
            public int userNum { get; set; }
            /// <summary>
            /// 商品数
            /// </summary>
            public int goodsNum { get; set; }
            /// <summary>
            /// 订单数
            /// </summary>
            public int orderNum { get; set; }
            /// <summary>
            /// 订单金额
            /// </summary>
            public decimal orderMoney { get; set; }
            /// <summary>
            /// 发送短信数
            /// </summary>
            public int smsNum { get; set; }
            /// <summary>
            /// 支出笔数
            /// </summary>
            public int outlayNum { get; set; }
            /// <summary>
            /// 支出金额
            /// </summary>
            public decimal outlayMoney { get; set; }
            public int couponNum { get; set; }
            public int useCouponNum { get; set; }
        }

        /// <summary>
        /// 汇总信息  
        /// </summary>
        public partial class Summarize
        {
            public int accid { get; set; }
            /// <summary>
            /// 销售数
            /// </summary>
            public int saleNum { get; set; }
            /// <summary>
            /// 销售金额
            /// </summary>
            public decimal saleMoney { get; set; }
            /// <summary>
            /// 会员数
            /// </summary>
            public int userNum { get; set; }
            /// <summary>
            /// 商品数
            /// </summary>
            public int goodsNum { get; set; }
            /// <summary>
            /// 订单数
            /// </summary>
            public int orderNum { get; set; }
            /// <summary>
            /// 订单金额
            /// </summary>
            public decimal orderMoney { get; set; }
            /// <summary>
            /// 商铺积分
            /// </summary>
            public int shopIntegral { get; set; }
            /// <summary>
            /// 优惠券数量
            /// </summary>
            public int couponNum { get; set; }
            /// <summary>
            /// 使用优惠券
            /// </summary>
            public int useCouponNum { get; set; }
            /// <summary>
            /// 在线时长
            /// </summary>
            public int loginLong { get; set; }

            /// <summary>
            /// 发送短信数
            /// </summary>
            public int smsNum { get; set; }
            /// <summary>
            /// 免费短信
            /// </summary>
            public int freeSmsNum { get; set; }
            /// <summary>
            /// 支出笔数
            /// </summary>
            public int outlayNum { get; set; }
            /// <summary>
            /// 支出金额
            /// </summary>
            public decimal outlayMoney { get; set; }
            /// <summary>
            /// 行为积分
            /// </summary>
            public int behaveIndex { get; set; }
        }
    }

    public partial class T_AccountConfig
    {

        #region SmsBillingConfig 内部短信配置
        /// <summary>
        /// 短信计费配置
        /// </summary>
        [Serializable]
        public class SmsBillingConfig
        {

            public SmsBillingConfig()
            {
                RechargeRemind = 0;
                SmsBill = 0;
                ShopManage = 0;
                UserReg = 0;
                Birthday = 0;
                Coupon = 0;
            }


            /// <summary>
            /// 充值提醒 （2）
            /// </summary>
            public int RechargeRemind { get; set; }
            /// <summary>
            /// 电子账单（短信账单）（3）
            /// </summary>
            public int SmsBill { get; set; }
            /// <summary>
            /// 店铺经营（4）
            /// </summary>
            public int ShopManage { get; set; }
            /// <summary>
            /// 会员注册（6）
            /// </summary>
            public int UserReg { get; set; }
            /// <summary>
            /// 生日祝福（7）
            /// </summary>
            public int Birthday { get; set; }
            /// <summary>
            /// 电子优惠券（15）
            /// </summary>
            public int Coupon { get; set; }
        }

        #endregion
    }

}

