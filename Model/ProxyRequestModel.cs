using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class RetrunResponseModel
    {
        /// <summary>
        /// 执行状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Ver { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrCode { get; set; }

        /// <summary>
        /// 对象数据Json
        /// </summary>
        public object Data { get; set; }
    }
    public class ProxyRequestModel
    {
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
            /// 请求名称
            /// </summary>
            public string RequestName { get; set; }

            /// <summary>
            /// 请求参数
            /// </summary>
            public string RequestJson { get; set; }
        }

        #region ProxyResponse 通用 响应返回格式
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

        public class WeixinResponseModel
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

        public class UserMessage
        {
            public int accId { get; set; }

            /// <summary>
            /// 页数 从1开始
            /// </summary>
            public int pageIndex { get; set; }

            /// <summary>
            /// 每页条数 默认15条
            /// </summary>
            public int pageSize { get; set; }
            public DateTime startTime { get; set; }
            public DateTime endTime { get; set; }
        }


    }

    /// <summary>
    /// 响应结果
    /// </summary>
    public class OpenResponseModel
    {
        /// <summary>
        /// 响应信息结果集
        /// </summary>
        public class ResponseMessage
        {
            /// <summary>
            /// 服务器总数
            /// </summary>
            public OpenResponseModel.PageInfo pageInfo { get; set; }
            /// <summary>
            /// 用户消息
            /// </summary>
            public List<T_UserMessageModel> userMessage { get; set; }
        }

        /// <summary>
        /// 用户微信信息
        /// </summary>
        public class T_UserMessageModel
        {
            public int id { get; set; }
            /// <summary>
            /// 店铺Id 
            /// </summary>
            public int accId { get; set; }

            /// <summary>
            /// 店铺名称
            /// </summary>
            public string CompanyName { get; set; }

            /// <summary>
            /// 微信用户opendId
            /// </summary>
            public string wxOpenId { get; set; }
            /// <summary>
            /// 信息内容
            /// </summary>
            public string msgContent { get; set; }
            /// <summary>
            /// 信息类型
            /// </summary>
            public string msgType { get; set; }
            /// <summary>
            /// 关键字 （链接 按钮 事件等记录）
            /// </summary>
            public string msgKey { get; set; }
            /// <summary>
            /// 发送时间
            /// </summary>
            public DateTime createTime { get; set; }
        }

        /// <summary>
        /// 分页信息
        /// </summary>
        public class PageInfo
        {
            /// <summary>
            /// 总行数
            /// </summary>
            public int totalCount { get; set; }
            /// <summary>
            /// 当前页数（从零开始）
            /// </summary>
            public int pageIndex { get; set; }
            /// <summary>
            /// 每页行数（默认50）
            /// </summary>
            public int pageSize { get; set; }
            /// <summary>
            /// 总页数
            /// </summary>
            public int pageCount { get; set; }
        }
    }
}
