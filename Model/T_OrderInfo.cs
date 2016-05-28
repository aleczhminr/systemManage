using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //T_OrderInfo
    /// <summary>
    /// T_OrderInfo
    /// </summary>	
    [Serializable]
    public partial class T_OrderInfo
    {

        /// <summary>
        /// 订单id
        /// </summary>		
        public int oid { get; set; }
        /// <summary>
        /// 店铺id
        /// </summary>		
        public int accId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>		
        public string orderNo { get; set; }
        /// <summary>
        /// 交易号
        /// </summary>		
        public string transactionNo { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>		
        public DateTime createDate { get; set; }
        /// <summary>
        /// 支付日期
        /// </summary>		
        public DateTime transactionDate { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>		
        public int orderStatus { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>		
        public int orderPayType { get; set; }
        /// <summary>
        /// 支付方式描述
        /// </summary>		
        public string orderPayDesc { get; set; }
        /// <summary>
        /// 支付银行id
        /// </summary>		
        public string orderPayBank { get; set; }
        /// <summary>
        /// 支付银行描述
        /// </summary>		
        public string orderPayBankDesc { get; set; }
        /// <summary>
        /// 业务Id
        /// </summary>		
        public int busId { get; set; }
        /// <summary>
        /// 业务数量
        /// </summary>		
        public int busQuantity { get; set; }
        /// <summary>
        /// 业务单价
        /// </summary>		
        public decimal busPrice { get; set; }
        /// <summary>
        /// 应付金额
        /// </summary>		
        public decimal busSumMoney { get; set; }
        /// <summary>
        /// 优惠券id
        /// </summary>		
        public int couponId { get; set; }
        /// <summary>
        /// 优惠券编码
        /// </summary>		
        public string couponCode { get; set; }
        /// <summary>
        /// 优惠券金额
        /// </summary>		
        public decimal couponMoney { get; set; }
        /// <summary>
        /// 实际支付金额
        /// </summary>		
        public decimal RealPayMoney { get; set; }
        /// <summary>
        /// 发票信息id
        /// </summary>		
        public int invoiceId { get; set; }
        /// <summary>
        /// 操作人员id
        /// </summary>		
        public int operatorId { get; set; }
        /// <summary>
        /// 操作人员ip
        /// </summary>		
        public string operatorIp { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>		
        public DateTime operatorTime { get; set; }
        /// <summary>
        /// 订单备注信息
        /// </summary>		
        public string remark { get; set; }
        /// <summary>
        /// 附加信息
        /// </summary>		
        public string flag { get; set; }
        /// <summary>
        /// 线下订单确认人员id
        /// </summary>		
        public int confirmOpId { get; set; }
        /// <summary>
        /// 线下订单确认人员ip
        /// </summary>		
        public string confirmOpIp { get; set; }
        /// <summary>
        /// 线下订单确认时间
        /// </summary>		
        public DateTime confirmOpTime { get; set; }
        /// <summary>
        /// 线下订单确认金额
        /// </summary>		
        public decimal confirmMoney { get; set; }
        /// <summary>
        /// 线下订单确认备注
        /// </summary>		
        public string confirmRemark { get; set; }
        /// <summary>
        /// 退款金额
        /// </summary>		
        public decimal returnMoney { get; set; }
        /// <summary>
        /// 退款时间
        /// </summary>		
        public DateTime returnDate { get; set; }
        /// <summary>
        /// paymentType
        /// </summary>		
        public int paymentType { get; set; }
        /// <summary>
        /// payerId
        /// </summary>		
        public int payerId { get; set; }
        public string ThirdPartOrderId { get; set; }
        /// <summary>
        /// 第三方订单状态 orderEnum枚举 新增主要用于京东订单
        /// </summary>
        public int ThirdPartOrderStatus { get; set; }
        /// <summary>
        /// 订单类型 orderEnum枚举 新增主要用于区分 业务 自营实体 第三方订单类型
        /// </summary>
        public int OrderTypeId { get; set; }
        /// <summary>
        /// 订单收获地址
        /// </summary>
        public int ReceivingAddressId { get; set; }

        #region 实物用户补充信息

        public string Address { get; set; }
        public string TelNumber { get; set; }
        public string ConsigneeName { get; set; }

        #endregion

    }

    public class MaterialAddressInfo
    {
        public string Address { get; set; }
        public string TelNumber { get; set; }
        public string ConsigneeName { get; set; }
        public string ExpressCode { get; set; }
        public string ExpressCompany { get; set; }
    }

    /// <summary>
    /// 订单分析类
    /// </summary>
    public partial class OrderAnalysis
    {
        public OrderAnalysis()
        {
            itemList = new List<OrderAnalysisItem>();
        }

        /// <summary>
        /// 订单数
        /// </summary>
        public int num { get; set; }
        /// <summary>
        /// 购买数量
        /// </summary>
        public int quantity { get; set; }
        /// <summary>
        /// 短信条数
        /// </summary>
        public int smsNum { get; set; }
        /// <summary>
        /// 店铺月份
        /// </summary>
        public int accNum { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public int money { get; set; }

        public List<OrderAnalysisItem> itemList;
    }
    public partial class OrderAnalysisItem
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        public string busName { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string bus_mclass { get; set; }
        /// <summary>
        /// 订单数
        /// </summary>
        public int num { get; set; }
        /// <summary>
        /// 购买数量
        /// </summary>
        public int quantity { get; set; }
        /// <summary>
        /// 短信条数
        /// </summary>
        public int smsNum { get; set; }
        /// <summary>
        /// 店铺月份
        /// </summary>
        public int accNum { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public int money { get; set; }
        /// <summary>
        /// 白分比
        /// </summary>
        public decimal baifen { get; set; }
    }

    public class DetailOrder
    {
        public DetailOrder()
        {
            DataList = new List<dynamic>();
            SumInfo = new OrderItemProp();
        }

        public OrderItemProp SumInfo { get; set; }

        public List<dynamic> DataList { get; set; }
    }

    public class OrderItemProp
    {
        public OrderItemProp()
        {
            Profit = 0;
            Cost = 0;
            SumMoney = 0;
            isFreeSms = 0;
        }

        /// <summary>
        /// 利润额
        /// </summary>
        public double Profit { get; set; }
        /// <summary>
        /// 成本价
        /// </summary>
        public double Cost { get; set; }
        /// <summary>
        /// 差价额
        /// </summary>
        public double SumMoney { get; set; }
        /// <summary>
        /// 维客短信标记
        /// </summary>
        public int isFreeSms { get; set; }
        /// <summary>
        /// 纯短信标记
        /// </summary>
        public string PureSms { get; set; }
        /// <summary>
        /// 自营标记
        /// </summary>
        public string SelfMark { get; set; }
    }
}

