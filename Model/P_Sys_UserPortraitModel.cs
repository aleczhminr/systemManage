using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class P_Sys_UserPortraitModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 插入时间 
        /// </summary>
        public DateTime InsertTime { get; set; }
        /// <summary>
        /// 店铺Id
        /// </summary>
        public int AccId { get; set; }
        /// <summary>
        /// 用户所在行业
        /// </summary>
        public string Industry { get; set; }
        /// <summary>
        /// 用户地址
        /// </summary>
        public int Location { get; set; }
        /// <summary>
        /// 用户来源
        /// </summary>
        public string UserSourcePortrait { get; set; }
        /// <summary>
        /// 用户选择我们的理由
        /// </summary>
        public string ChoiceReason { get; set; }
        /// <summary>
        /// 竞品
        /// </summary>
        public string CompetingProduct { get; set; }
        /// <summary>
        /// 主要问题       
        /// </summary>
        public string MainQuestion { get; set; }

        /// <summary>
        /// 潜在需求
        /// </summary>
        public string PotentialNeed { get; set; }
        /// <summary>
        /// 用户态度
        /// </summary>
        public int Attitude { get; set; }
        /// <summary>
        /// 经商经验
        /// </summary>
        public int BusinessExp { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNum { get; set; }
        /// <summary>
        /// QQ号码
        /// </summary>
        public string QQNum { get; set; }
        /// <summary>
        /// 年龄段
        /// </summary>
        public int AgeGrade { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// 用户的互联网使用水平
        /// </summary>
        public int WebExpGrade { get; set; }
        /// <summary>
        /// 店员人数
        /// </summary>
        public int SalesmanNum { get; set; }

        /// <summary>
        /// 用户补充备注Id
        /// </summary>
        public string RemarkId { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 邮件地址
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        public string Weinxin { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCardNo { get; set; }
        /// <summary>
        /// 发票抬头
        /// </summary>
        public string InvoiceTitle { get; set; }
        /// <summary>
        /// 店铺地址
        /// </summary>
        public string ShopAddress { get; set; }
        /// <summary>
        /// 分店数量
        /// </summary>
        public int BranchNum { get; set; }

        /// <summary>
        /// 是否解决问题
        /// </summary>
        //public int IsSolveProblem { get; set; }

        #region 用于接收临时参数的字段
        public int RemarkType1 { get; set; }
        public int RemarkType2 { get; set; }
        public string RemarkContent1 { get; set; }
        public string RemarkContent2 { get; set; }
        #endregion
    }
}

