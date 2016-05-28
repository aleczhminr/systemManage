using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TriggerTemplateModel
    {
        public int Id { get; set; }

        public string MissionTarget { get; set; }
        public string MissionName { get; set; }
        public string UserDesc { get; set; }
        public int SmsMark { get; set; }
        public int MobileMark { get; set; }
        public int WebMark { get; set; }
        public int EmailMark { get; set; }
        public string MobileTitle { get; set; }
        public string MobileContent { get; set; }
        public string MobileContentType { get; set; }
        public string MobileContentUrl { get; set; }
        public string EmailTitle { get; set; }
        public string EmailContent { get; set; }
        public string SmsContent { get; set; }
        public string WebTitle { get; set; }
        public string WebContent { get; set; }
        public DateTime CreateTime { get; set; }
        public int Operator { get; set; }
        public int EnableStatus { get; set; }
        public int EventId { get; set; }
    }

    /// <summary>
    /// 页面显示Model
    /// </summary>
    public class ListView
    {
        public ListView()
        {
            dataList = new List<TriggerTemplateModel>();
        }

        public int count { get; set; }
        public int maxPage { get; set; }
        public List<TriggerTemplateModel> dataList { get;set; }
    }

    /// <summary>
    /// Kafka事件定义Model
    /// </summary>
    public class Sys_KafkaEvent
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string Remark { get; set; }
        public int EventMark { get; set; }
    }

    #region 填充消息占位符内容的Model

    public class EventMessage
    {
        public int EventId { get; set; }
        public string SpecModel { get; set; }
    }

    public class GenericUserInfo
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public int EventId { get; set; }
        public int AccId { get; set; }
        public string ShopName { get; set; }        
    }

    /// <summary>
    /// 注册后推送
    /// </summary>
    public class AfterReg : GenericUserInfo
    {        
        
    }

    public class AfterFirstLogin : GenericUserInfo
    {
        
    }

    public class AfterSale : GenericUserInfo
    {
        public string GoodsName { get; set; }
        public DateTime SaleTime { get; set; }
        public string UserName { get; set; }
        public string OperatorName { get; set; }
    }

    /// <summary>
    /// 优惠券绑定后信息Model
    /// </summary>
    public class AfterBindCoupon : GenericUserInfo
    {
        public string CouponDesc { get; set; }
        public DateTime EndDate { get; set; }
    }

    /// <summary>
    /// 反馈转为需求时/需求完成时
    /// </summary>
    public class AfterImportReq : GenericUserInfo
    {
        public string RequirementDesc { get; set; }
    }

    public class AfterExpress : GenericUserInfo
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// 物流公司
        /// </summary>
        public string ExpressName { get; set; }
        /// <summary>
        /// 快递单号
        /// </summary>
        public string ExpressCode { get; set; }
    }

    public class AfterOrderSuccess : GenericUserInfo
    {
        /// <summary>
        /// 订单类型[1-业务 2-实物 3-第三方 4-充值]
        /// </summary>
        public int OrderType { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 订单内容
        /// </summary>
        public string OrderName { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal OrderMoney { get; set; }
        /// <summary>
        /// 快递单号
        /// </summary>
        public string OrderExpressId { get; set; }
        /// <summary>
        /// 教程Url
        /// </summary>
        public string HelpUrl { get; set; }
        /// <summary>
        /// 產品ID
        /// </summary>
        public int BusId { get; set; }
        /// <summary>
        /// 话费充值号码
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 店主姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 订单数量
        /// </summary>
        public int OrderNum { get; set; }
    }

    #endregion

    public class MessageRequestModel
    {
        //int id, string eventName, string specModel, int accid
        public int Id { get; set; }
        public string EventName { get; set; }
        public string SpecModel { get; set; }
        public int AccId { get; set; }
    }

}
