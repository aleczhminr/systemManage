using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Model
{
    /// <summary>
    ///订单发票信息	
    ///</summary>
    [Serializable]
    public partial class T_Order_Invoice
    {
        private int _id;
        private int _accid;
        private int _oid;
        private DateTime _createdate;
        private decimal _invoicemoney;
        private string _invoicename;
        private string _invoicedesc;
        private string _invoicephone;
        private string _invoiceaddress;
        private int _invoicestatus;
        private string _invoiceno;
        private string _invoiceremark;
        private int _invoiceoperatorid;
        private string _invoiceAddressee;
        private DateTime _invoiceoperatortime;

        /// <summary>
        /// 发票Id
        /// </summary>		
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 店铺Id
        /// </summary>		
        public int accId
        {
            get { return _accid; }
            set { _accid = value; }
        }
        /// <summary>
        /// 订单id
        /// </summary>		
        public int oid
        {
            get { return _oid; }
            set { _oid = value; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>		
        public DateTime createDate
        {
            get { return _createdate; }
            set { _createdate = value; }
        }
        /// <summary>
        /// 发票金额
        /// </summary>		
        public decimal invoiceMoney
        {
            get { return _invoicemoney; }
            set { _invoicemoney = value; }
        }
        /// <summary>
        /// 发票抬头
        /// </summary>		
        public string invoiceName
        {
            get { return _invoicename; }
            set { _invoicename = value; }
        }
        /// <summary>
        /// 发票内容
        /// </summary>		
        public string invoiceDesc
        {
            get { return _invoicedesc; }
            set { _invoicedesc = value; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>		
        public string invoicePhone
        {
            get { return _invoicephone; }
            set { _invoicephone = value; }
        }
        /// <summary>
        /// 发票地址
        /// </summary>		
        public string invoiceAddress
        {
            get { return _invoiceaddress; }
            set { _invoiceaddress = value; }
        }
        /// <summary>
        /// 发票状态
        /// </summary>		
        public int invoiceStatus
        {
            get { return _invoicestatus; }
            set { _invoicestatus = value; }
        }
        /// <summary>
        /// 发票编号
        /// </summary>		
        public string invoiceNo
        {
            get { return _invoiceno; }
            set { _invoiceno = value; }
        }
        /// <summary>
        /// 备注信息
        /// </summary>		
        public string invoiceRemark
        {
            get { return _invoiceremark; }
            set { _invoiceremark = value; }
        }
        /// <summary>
        /// 操作人员id
        /// </summary>		
        public int invoiceOperatorId
        {
            get { return _invoiceoperatorid; }
            set { _invoiceoperatorid = value; }
        }
        /// <summary>
        /// 操作时间
        /// </summary>		
        public DateTime invoiceOPeratorTime
        {
            get { return _invoiceoperatortime; }
            set { _invoiceoperatortime = value; }
        }

        /// <summary>
        /// 发票收件人
        /// </summary>
        public string invoiceAddressee
        {
            get { return _invoiceAddressee; }
            set { _invoiceAddressee = value; }
        }

    }

    /// <summary>
    /// 发票信息（包含部分店铺，订单信息）
    /// </summary>
    public partial class OrderInvoiceModel
    {
        private int _id;
        private int _accid;
        private string _CompanyName;
        private string _UserRealName;
        private int _oid;
        private int _orderStat;
        private string _orderStatName;
        private string _bus_name;
        private DateTime _createdate;
        private decimal _invoicemoney;
        private string _invoicename;
        private string _invoicedesc;
        private string _invoicephone;
        private string _invoiceaddress;
        private int _invoicestatus;
        private string _invoicestatusname = "未开发票";
        private string _invoiceno;
        private string _invoiceremark;
        private int _invoiceoperatorid;
        private string _invoiceoperatorname;
        private DateTime? _invoiceoperatortime;
        private string _invoiceAddressee;

        /// <summary>
        /// 发票Id
        /// </summary>		
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 店铺Id
        /// </summary>		
        public int accId
        {
            get { return _accid; }
            set { _accid = value; }
        }
        /// <summary>
        /// 店铺名称
        /// </summary>
        [Display(Name = "店铺名称")]
        public string CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }
        /// <summary>
        /// 店主名称
        /// </summary>
        [Display(Name = "店主名称")]
        public string UserRealName
        {
            get { return _UserRealName; }
            set { _UserRealName = value; }
        }
        /// <summary>
        /// 订单id
        /// </summary>		
        public int oid
        {
            get { return _oid; }
            set { _oid = value; }
        }
        /// <summary>
        /// 订单状态
        /// </summary>		
        [Display(Name = "订单状态")]
        public int orderStat
        {
            get { return _orderStat; }
            set { _orderStat = value; }
        }
        /// <summary>
        /// 订单状态名称
        /// </summary>
        [Display(Name = "订单状态")]
        public string orderStatName
        {
            get { return _orderStatName; }
            set { _orderStatName = value; }
        }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string bus_name
        {
            get { return _bus_name; }
            set { _bus_name = value; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>		
        [Display(Name = "购买时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
        public DateTime createDate
        {
            get { return _createdate; }
            set { _createdate = value; }
        }
        /// <summary>
        /// 发票金额
        /// </summary>		
        [Display(Name = "发票金额")]
        [DisplayFormat(DataFormatString = "{0:￥#,##0.##}")]
        public decimal invoiceMoney
        {
            get { return _invoicemoney; }
            set { _invoicemoney = value; }
        }
        /// <summary>
        /// 发票抬头
        /// </summary>		
        [Display(Name = "发票抬头")]
        public string invoiceName
        {
            get { return _invoicename; }
            set { _invoicename = value; }
        }

        /// <summary>
        /// 发票收件人
        /// </summary>		
        [Display(Name = "发票收件人")]
        public string invoiceAddressee
        {
            get { return _invoiceAddressee; }
            set { _invoiceAddressee = value; }
        }

        /// <summary>
        /// 发票内容
        /// </summary>		
        public string invoiceDesc
        {
            get { return _invoicedesc; }
            set { _invoicedesc = value; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>		
        [Display(Name = "联系电话")]
        public string invoicePhone
        {
            get { return _invoicephone; }
            set { _invoicephone = value; }
        }
        /// <summary>
        /// 发票地址
        /// </summary>		
        [Display(Name = "发票地址")]
        public string invoiceAddress
        {
            get { return _invoiceaddress; }
            set { _invoiceaddress = value; }
        }
        /// <summary>
        /// 发票状态
        /// </summary>		
        [Display(Name = "发票状态")]
        public int invoiceStatus
        {
            get { return _invoicestatus; }
            set { _invoicestatus = value; }
        }
        /// <summary>
        /// 发票状态
        /// </summary>		
        [Display(Name = "发票状态")]
        public string invoiceStatusName
        {
            get { return _invoicestatusname; }
            set { _invoicestatusname = value; }
        }
        /// <summary>
        /// 发票编号
        /// </summary>		
        public string invoiceNo
        {
            get { return _invoiceno; }
            set { _invoiceno = value; }
        }
        /// <summary>
        /// 备注信息
        /// </summary>		
        public string invoiceRemark
        {
            get { return _invoiceremark; }
            set { _invoiceremark = value; }
        }
        /// <summary>
        /// 操作人员id
        /// </summary>		
        public int invoiceOperatorId
        {
            get { return _invoiceoperatorid; }
            set { _invoiceoperatorid = value; }
        }
        /// <summary>
        /// 操作人员
        /// </summary>		
        public string invoiceOperatorName
        {
            get { return _invoiceoperatorname; }
            set { _invoiceoperatorname = value; }
        }
        /// <summary>
        /// 操作时间
        /// </summary>		
        public DateTime? invoiceOPeratorTime
        {
            get { return _invoiceoperatortime; }
            set { _invoiceoperatortime = value; }
        }

    }
    /// <summary>
    /// 发票信息列表
    /// </summary>
    public partial class OrderInvoiceList
    {
        /// <summary>
        /// 当前页数
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页显示个数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }
        /// <summary>
        /// 总行数
        /// </summary>
        public int RowCount { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public List<OrderInvoiceModel> Data { get; set; }
        /// <summary>
        /// 分页代码，（用于后台输出分页代码）
        /// </summary>
        public string PageHtml { get; set; }
    }

    public class InvoiceSimple
    {
        public string invoiceNo { get; set; }
        public string invoiceExpress { get; set; }
        public string invoiceRemark { get; set; }
    }
}
