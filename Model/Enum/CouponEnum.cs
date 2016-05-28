using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Enum
{
    /// <summary>
    /// 优惠券
    /// </summary>
    public static class CouponEnum
    {
        /// <summary>
        /// 优惠券类别
        /// </summary>
        public enum CouponType : int
        {
            抵值券 = 1,
            功能券 = 2
        }
        /// <summary>
        /// 优惠券类型限制
        /// </summary>
        public enum BindType : int
        {
            无限制 = 0, 限制类型 = 1, 限制产品 = 2
        }
        /// <summary>
        /// 优惠券使用限制
        /// </summary>
        public enum RuleType : int
        {
            无限制 = 0, 满减抵值 = 1
        }
        /// <summary>
        /// 优惠券状态
        /// </summary>
        public enum CouponStatus : int
        {
            正常 = 0,
            已过期 = 1,
            已作废 = 3
        }
        /// <summary>
        /// 优惠券使用状态
        /// </summary>
        public enum CouponListStatus : int
        {
            未使用 = 0,
            已经使用 = 1,
            已绑定 = 2,
            已作废 = 3
        }
    }
}
