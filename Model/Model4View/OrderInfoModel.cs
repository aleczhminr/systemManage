using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Model.Model4View
{
    public class OrderInfoModel
    {
        [Key]
        public int oid { get; set; }

        [Display(Name = "订单号")]
        public string orderNo { get; set; }

        [Display(Name = "商铺名称")]
        public string accountName { get; set; }
        public int accId { get; set; }

        [Display(Name = "业务名称")]
        public string OrderProjectName { get; set; }
        public int busId { get; set; }

        [Display(Name = "购买数量")]
        public int busQuantity { get; set; }

        [Display(Name = "支付类型")]
        public string orderPayDesc { get; set; }

        [Display(Name = "支付金额")]
        public decimal RealPayMoney { get; set; }

        [Display(Name = "购买时间")]
        public DateTime createDate { get; set; }

        [Display(Name = "购买时间")]
        public DateTime transactionDate { get; set; }

        [Display(Name = "订单状态")]
        public int orderStatus { get; set; }
        [Display(Name = "订单状态")]
        public string orderStatusName { get; set; }
        public int invoiceId { get; set; }

        public int orderPayType { get; set; }
        public decimal busPrice { get; set; }
        public decimal busSumMoney { get; set; }
        public decimal couponMoney { get; set; }
        public string couponId { get; set; }
        public string confirmRemark { get; set; }
        public string FirstFlag { get; set; }
        /// <summary>
        /// 使用积分数
        /// </summary>
        public string commuteIntegral { get; set; }
        /// <summary>
        /// 积分抵用金额
        /// </summary>
        public string commuteIntegralMoney { get; set; }

        #region 用户信息补全
        /// <summary>
        /// 用户版本
        /// </summary>
        public string Edit { get; set; }
        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string UserRealName { get; set; }
        /// <summary>
        /// 上次登录时间
        /// </summary>
        public string LoginLast { get; set; }
        #endregion

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

        public int oFlag { get; set; }

        #region 实物用户补充信息

        public string Address { get; set; }
        public string TelNumber { get; set; }
        public string ConsigneeName { get; set; }

        public string ExpressCompany { get; set; }
        public string ExpressCode { get; set; }
        //public string SendStatus { get; set; }

        #endregion
    }

    public class OrderInfoViewModel : DbContext
    {
        public DbSet<OrderInfoModel> OrderInfoView { get; set; }
    }
    /// <summary>
    /// 绑定订单和发票信息的视图模型
    /// </summary>
    public class OrderListViewModel:DbContext
    {
        public DbSet<OrderInfoModel> orderInfo { get; set; }
    }

    public class OrderInvoiceViewModel : DbContext
    {
        public DbSet<OrderInvoiceModel> invoiceList { get; set; }
    }

    /// <summary>
    /// 退款相关的订单信息
    /// </summary>
    public class DrawbackInfoModel
    {
        /// <summary>
        /// 订单Id
        /// </summary>
        public int oid { get; set; }
        /// <summary>
        /// 订单创建时间
        /// </summary>
        public DateTime createDate { get; set; }
        /// <summary>
        /// 产品Id
        /// </summary>
        public int busId { get; set; }
        /// <summary>
        /// 产品类型
        /// </summary>
        public int orderTypeId { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public int accid { get; set; }

        public string pName { get; set; }
        public decimal RealPayMoney { get; set; }
        public int busQuantity { get; set; }
    }

    public class OrderProjectItem
    {
        /// <summary>
        /// 订单Id
        /// </summary>
        public int oid { get; set; }

        /// <summary>
        /// 产品Id
        /// </summary>
        public int itemId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string itemName { get; set; }
        /// <summary>
        /// 产品数量
        /// </summary>
        public int itemQuantity { get; set; }
    }

    public class RechargeLogModel
    {
        //int accid,int uid,int oid,int cardNum,int status,string desc,string newOrdNo
        public int Id { get; set; }
        public int Accid { get; set; }
        public int Uid { get; set; }
        public int Oid { get; set; }
        public int CardNum { get; set; }
        public int Status { get; set; }
        public string Descrip { get;set;}
        public string NewOrdNo { get; set; }
    }
}
