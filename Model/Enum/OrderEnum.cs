using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Enum
{
    /// <summary>
    /// 订单信息
    /// </summary>
    public static class OrderEnum
    {
        /// <summary>
        /// 订单支付方式
        /// </summary>
        public enum PayType : int
        {
            平台订单 = 0,
            线下支付 = 1,
            支付宝 = 2,
            财付通 = 3,
            快钱 = 4,
            微信支付 = 5
        }

        /// <summary>
        /// 业务类型
        /// </summary>
        public enum ItemType : int
        {
            短信 = 1,
            版本 = 2,
            产品 = 3,
            套餐 = 4
        }

        /// <summary>
        /// 订单状态
        /// </summary>
        public enum OrderStatus : int
        {
            等待支付 = 0,
            成功支付 = 1,
            交易成功 = 2,
            订单过期 = -1,
            退款成功 = -2
        }
    }
}
