using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //T_SaleInfo
    /// <summary>
    /// T_SaleInfo
    /// </summary>	
    [Serializable]
    public partial class T_SaleInfo
    {

        /// <summary>
        /// 销售概要ID
        /// </summary>		
        public int saleID { get; set; }
        /// <summary>
        /// 销售流水号
        /// </summary>		
        public string saleNo { get; set; }
        /// <summary>
        /// 店铺ID
        /// </summary>		
        public int accID { get; set; }
        /// <summary>
        /// 是否零售
        /// </summary>		
        public int isRetail { get; set; }
        /// <summary>
        /// 会员ID
        /// </summary>		
        public int uid { get; set; }
        /// <summary>
        /// 销售时间
        /// </summary>		
        public DateTime saleTime { get; set; }
        /// <summary>
        /// 记录插入时间
        /// </summary>		
        public DateTime insertTime { get; set; }
        /// <summary>
        /// 商品种类
        /// </summary>		
        public int saleKind { get; set; }
        /// <summary>
        /// 销售数量
        /// </summary>		
        public decimal saleNum { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>		
        public int payType { get; set; }
        /// <summary>
        /// 应付金额
        /// </summary>		
        public decimal AbleMoney { get; set; }
        /// <summary>
        /// 实付金额
        /// </summary>		
        public decimal RealMoney { get; set; }
        /// <summary>
        /// 应付与实付金额差值
        /// </summary>		
        public decimal DiffMoney { get; set; }
        /// <summary>
        /// StoreTimes
        /// </summary>		
        public int StoreTimes { get; set; }
        /// <summary>
        /// 储值支付金额
        /// </summary>		
        public decimal StoreMoney { get; set; }
        /// <summary>
        /// 现金支付金额
        /// </summary>		
        public decimal CashMoney { get; set; }
        /// <summary>
        /// CardMoney
        /// </summary>		
        public decimal CardMoney { get; set; }
        /// <summary>
        /// 未支付金额
        /// </summary>		
        public decimal UnpaidMoney { get; set; }
        /// <summary>
        /// ReturnBackMoney
        /// </summary>		
        public decimal ReturnBackMoney { get; set; }
        /// <summary>
        /// 是否发送短信账单
        /// </summary>		
        public int isSendSMS { get; set; }
        /// <summary>
        /// 是否打印小票
        /// </summary>		
        public int isPrintTicket { get; set; }
        /// <summary>
        /// 操作员ID
        /// </summary>		
        public int operatorID { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>		
        public DateTime operatorTime { get; set; }
        /// <summary>
        /// 操作IP
        /// </summary>		
        public string operatorIP { get; set; }
        /// <summary>
        /// exJson
        /// </summary>		
        public string exJson { get; set; }
        /// <summary>
        /// zt
        /// </summary>		
        public int zt { get; set; }
        /// <summary>
        /// CouponMoney
        /// </summary>		
        public int CouponMoney { get; set; }
        /// <summary>
        /// CouponID
        /// </summary>		
        public int CouponID { get; set; }
        /// <summary>
        /// CouponCode
        /// </summary>		
        public string CouponCode { get; set; }
        /// <summary>
        /// SerialNum
        /// </summary>		
        public long SerialNum { get; set; }
        /// <summary>
        /// AlipayMoney
        /// </summary>		
        public decimal AlipayMoney { get; set; }

    }
}

