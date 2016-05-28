using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ProxyModel
    {

        /// <summary>
        /// 请求参数
        /// </summary>
        public class ProxyRequestModel
        {
            /// <summary>
            /// 加密签名
            /// </summary>
            public string Signature { get; set; }

            /// <summary>
            /// 时间戳
            /// </summary>
            public string Timestamp { get; set; }

            /// <summary>
            /// 随机数
            /// </summary>
            public string Nonce { get; set; }

            /// <summary>
            /// 店铺ID
            /// </summary>
            public int? AccId { get; set; }

            /// <summary>
            /// 店铺ID编码
            /// </summary>
            public string ShopOpenId { get; set; }

            /// <summary>
            /// 请求名称
            /// </summary>
            public string RequestName { get; set; }

            /// <summary>
            /// 请求参数
            /// </summary>
            public string RequestJson { get; set; }
        }

        /// <summary>
        /// 响应返回格式
        /// </summary>
        public class ProxyResponseModel
        {
            /// <summary>
            /// 执行状态
            /// </summary>
            public bool Status { get; set; }

            /// <summary>
            /// 错误描述
            /// </summary>
            public string ErrDesc { get; set; }

            /// <summary>
            /// 店铺ID
            /// </summary>
            public int AccId { get; set; }

            /// <summary>
            /// 对象数据Json
            /// </summary>
            public string ObjectStr { get; set; }
        }

        public class SmsLiteModel
        {
            /// <summary>
            /// 手机号码
            /// </summary>
            public string PhoneNum { get; set; }

            /// <summary>
            /// 短信内容
            /// </summary>
            public string SmsContent { get; set; }
        }
    }
}
