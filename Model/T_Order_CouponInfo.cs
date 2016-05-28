using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //T_Order_CouponInfo
    /// <summary>
    /// T_Order_CouponInfo
    /// </summary>	
    [Serializable]
    public partial class T_Order_CouponInfo
    {

        /// <summary>
        /// 优惠券id
        /// </summary>		
        public int id { get; set; }
        /// <summary>
        /// 优惠券类型
        /// </summary>		
        public int couponType { get; set; }
        /// <summary>
        /// 绑定类型
        /// </summary>		
        public int bindType { get; set; }
        /// <summary>
        /// 绑定分类id
        /// </summary>		
        public int bindValue { get; set; }
        /// <summary>
        /// 绑定分类ID说明
        /// </summary>		
        public string bindName { get; set; }
        /// <summary>
        /// 限制类型
        /// </summary>		
        public int ruleType { get; set; }
        /// <summary>
        /// 限制金额
        /// </summary>		
        public int ruleValue { get; set; }
        /// <summary>
        /// 优惠券金额
        /// </summary>		
        public int couponValue { get; set; }
        /// <summary>
        /// 优惠券状态
        /// </summary>		
        public int couponStatus { get; set; }
        /// <summary>
        /// 优惠券描述
        /// </summary>		
        public string couponDesc { get; set; }
        /// <summary>
        /// 最大生成数量
        /// </summary>		
        public int maxLimitNum { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>		
        public DateTime createDate { get; set; }
        /// <summary>
        /// 截至日期
        /// </summary>		
        public DateTime endDate { get; set; }
        /// <summary>
        /// 操作员id
        /// </summary>		
        public int operatorId { get; set; }
        /// <summary>
        /// 操作ip
        /// </summary>		
        public string operarorIp { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>		
        public DateTime operatorTime { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>		
        public string remark { get; set; }
        /// <summary>
        /// prefixAgent
        /// </summary>		
        public int prefixAgent { get; set; }

    }

    /// <summary>
    /// 优惠券详情
    /// </summary>
    public partial class OrderCouponInfo
    {


        /// <summary>
        /// 优惠券id
        /// </summary>		
        public int id { get; set; }
        /// <summary>
        /// 优惠券类型
        /// </summary>		
        public int couponType { get; set; }
        /// <summary>
        /// 优惠券类型 名称
        /// </summary>
        public string couponTypeName { get; set; }

        /// <summary>
        /// 绑定类型
        /// </summary>		
        public int bindType { get; set; }
        /// <summary>
        /// 绑定类型
        /// </summary>
        public string bindTypeName { get; set; }

        /// <summary>
        /// 绑定分类id
        /// </summary>		
        public int bindValue { get; set; }
        /// <summary>
        /// 绑定分类ID说明
        /// </summary>		
        public string bindName { get; set; }
        /// <summary>
        /// 限制类型
        /// </summary>		
        public int ruleType { get; set; }
        /// <summary>
        /// 限制类型
        /// </summary>
        public string ruleTypeName { get; set; }

        /// <summary>
        /// 限制金额
        /// </summary>		
        public int ruleValue { get; set; }
        /// <summary>
        /// 优惠券金额
        /// </summary>		
        public int couponValue { get; set; }
        /// <summary>
        /// 优惠券状态
        /// </summary>		
        public int couponStatus { get; set; }
        /// <summary>
        /// 优惠券状态
        /// </summary>
        public string couponStatusName { get; set; }
        /// <summary>
        /// 优惠券描述
        /// </summary>		
        public string couponDesc { get; set; }

        /// <summary>
        /// 生成数量
        /// </summary>
        public int produceNum { get; set; }
        /// <summary>
        /// 绑定数量
        /// </summary>
        public int bindingNum { get; set; }
        /// <summary>
        /// 使用数量
        /// </summary>
        public int useNum { get; set; }
        /// <summary>
        /// 最大生成数量
        /// </summary>		
        public int maxLimitNum { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>		
        public DateTime createDate { get; set; }
        /// <summary>
        /// 截至日期
        /// </summary>		
        public DateTime endDate { get; set; }
        /// <summary>
        /// 操作员id
        /// </summary>		
        public int operatorId { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string operatorName { get; set; } 
        /// <summary>
        /// 备注信息
        /// </summary>		
        public string remark { get; set; } 
    }

    /// <summary>
    /// 优惠券信息
    /// </summary>
    public partial class OrderCouponInfoListItem
    {
        /// <summary>
        /// 优惠券id
        /// </summary>		
        public int id { get; set; }
        /// <summary>
        /// 优惠券描述
        /// </summary>		
        public string couponDesc { get; set; }
        /// <summary>
        /// 最大生成数量
        /// </summary>		
        public int maxLimitNum { get; set; }
        /// <summary>
        /// 生成数量
        /// </summary>
        public int produceNum { get; set; }
        /// <summary>
        /// 绑定数量
        /// </summary>
        public int bindingNum { get; set; }
        /// <summary>
        /// 使用数量
        /// </summary>
        public int useNum { get; set; }

        /// <summary>
        /// 截至日期
        /// </summary>		
        public DateTime endDate { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>		
        public DateTime createDate { get; set; }
        /// <summary>
        /// 操作员id
        /// </summary>		
        public int operatorId { get; set; }
        /// <summary>
        /// 操作人员
        /// </summary>
        public string opertorName { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>		
        public string remark { get; set; }
        /// <summary>
        /// 优惠券状态
        /// </summary>
        public int couponStatus { get; set; }
        /// <summary>
        /// 优惠券状态说明
        /// </summary>
        public string couponStatusName { get; set; }
    }
}

