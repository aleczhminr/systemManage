using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ApiModel
    {
    }

    #region ProxyRequest 请求参数
    /// <summary>
    /// 请求参数
    /// </summary>
    public class ProxyRequest
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
        public int AccId { get; set; }

        /// <summary>
        /// 事件ID
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// 店铺令牌Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// AppKey
        /// </summary>
        public string AppKey { get; set; }

        /// <summary>
        /// 请求名称
        /// </summary>
        public string RequestName { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        public string RequestJson { get; set; }
    }
    #endregion

    #region ProxyResponse 响应返回格式
    /// <summary>
    /// 响应返回格式
    /// </summary>
    public class ProxyResponse
    {
        /// <summary>
        /// 执行状态
        /// </summary>
        public int Status { get; set; }

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
        public string StrObj { get; set; }
    }
    #endregion
}
