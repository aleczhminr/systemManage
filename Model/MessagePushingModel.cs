using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MessagePushingModel
    {
        /// <summary>
        /// 用户ID列表
        /// </summary>
        public List<string> AccidList { get; set; }
        /// <summary>
        /// 发送备注
        /// </summary>
        public string Remark { get; set; }         
    }

    /// <summary>
    /// 用于消息发送的单个店铺信息
    /// </summary>
    public class SingleShopInfoForMsg
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string Accid { get; set; }
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime OverDueTime { get; set; }
        /// <summary>
        /// 业务名称 optional
        /// </summary>
        public string BusName { get; set; }
        /// <summary>
        /// 优惠券名称 optional
        /// </summary>
        public string CouponName { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public string RegTime { get; set; }
        /// <summary>
        /// 注册年限
        /// </summary>
        public string Interval { get; set; }
    }
}
