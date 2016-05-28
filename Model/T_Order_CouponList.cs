using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //T_Order_CouponList
    /// <summary>
    /// 店铺优惠券
    /// </summary>	
    [Serializable]
    public partial class T_Order_CouponList
    {

        /// <summary>
        /// 优惠券Id
        /// </summary>		
        public int id { get; set; }
        /// <summary>
        /// 优惠券批次ID
        /// </summary>		
        public int groupId { get; set; }
        /// <summary>
        /// 优惠券Code
        /// </summary>		
        public string couponId { get; set; }
        /// <summary>
        /// 优惠券Val
        /// </summary>		
        public int couponValue { get; set; }
        /// <summary>
        /// 优惠券状态
        /// </summary>		
        public int couponStatus { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>		
        public DateTime createDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>		
        public DateTime endDate { get; set; }
        /// <summary>
        /// 绑定日期
        /// </summary>		
        public DateTime receiveDate { get; set; }
        /// <summary>
        /// 使用日期
        /// </summary>		
        public DateTime usedDate { get; set; }
        /// <summary>
        /// 绑定店铺Id
        /// </summary>		
        public int toAccId { get; set; }
        /// <summary>
        /// 使用店铺Id
        /// </summary>		
        public int useAccId { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>		
        public string remark { get; set; }
        /// <summary>
        /// 绑定方式
        /// </summary>		
        public int bindWay { get; set; }
        /// <summary>
        /// 附加信息
        /// </summary>		
        public string flag { get; set; }

    }
    /// <summary>
    /// 店铺订单优惠券
    /// </summary>
    public partial class ShopOrderCoupon
    {
        /// <summary>
        /// 优惠券详情ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 店铺ID
        /// </summary>
        public int accid { get; set; }
        /// <summary>
        /// 优惠券ID
        /// </summary>
        public int groupId { get; set; }
        /// <summary>
        /// 优惠券类别ID
        /// </summary>
        public int couponType { get; set; }
        /// <summary>
        /// 优惠券类别
        /// </summary>
        public string couponTypeName { get; set; }
        /// <summary>
        /// 优惠券说明
        /// </summary>
        public string couponDesc { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public int couponValue { get; set; }
        /// <summary>
        /// 优惠券状态
        /// </summary>
        public int couponStatus { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string couponStatusName { get; set; }
        /// <summary>
        /// 过期
        /// </summary>
        public DateTime endDate { get; set; }
        /// <summary>
        /// 绑定时间
        /// </summary>
        public DateTime createDate { get; set; }
        /// <summary>
        /// 使用时间
        /// </summary>
        public DateTime usedDate { get; set; }
    }

    /// <summary>
    /// 优惠券列表信息
    /// </summary>
    public partial class OrderCouponListInfo
    {
        public int id { get; set; }
        /// <summary>
        /// 优惠券
        /// </summary>
        public string couponId { get; set; }
        /// <summary>
        /// 优惠券状态
        /// </summary>
        public int couponStatus { get; set; }
        /// <summary>
        /// 优惠券状态
        /// </summary>
        public string couponStatusName { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime endTime { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime receiveDate { get; set; }
        /// <summary>
        /// 使用时间
        /// </summary>
        public DateTime usedDate { get; set; }
        /// <summary>
        /// 绑定店铺ID
        /// </summary>
        public int toAccId { get; set; }
        /// <summary>
        /// 绑定店铺
        /// </summary>
        public string toAccName { get; set; }
        /// <summary>
        /// 使用店铺id
        /// </summary>
        public int useAccId { get; set; }
        /// <summary>
        /// 使用店铺
        /// </summary>
        public string useAccName { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string remark { get; set; }

    }
}

