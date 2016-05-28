using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Web;
using Model;

namespace Controls.MobileRechargeAPI
{
    public static class MobileRecharge
    {
        public static string appkey = "ccc5dd1b917a5f439fe9a8ed877ee8eb"; //appkey
        public static string openId = "JH01334b46d0c362da33daa2c039e3035d"; //openId

        #region 聚合账户余额查询
        public static BalanceReturnModel GetBalance()
        {
            string timeStamp = CommonLib.Helper.GetTimeStamp();
            string signMd5 = CommonLib.Helper.Md5Hash(openId + appkey + timeStamp);
            string url = "http://op.juhe.cn/ofpay/mobile/yue";

            var parameters = new Dictionary<string, string>();

            parameters.Add("timestamp", timeStamp); //当前时间戳
            parameters.Add("key", appkey);//appkey
            parameters.Add("sign", signMd5); //校验值

            string result = sendPost(url, parameters, "get");

            var model = CommonLib.Helper.JsonDeserializeObject<BalanceReturnModel>(result);

            return model;
        }
        #endregion

        #region 检测手机号是否可以充值
        public static JuheReturnModel CheckPhoneNum(string phoneNum, string cardNum)
        {

            string url = "http://op.juhe.cn/ofpay/mobile/telcheck";

            var parameters = new Dictionary<string, string>();

            parameters.Add("phoneno", phoneNum); //手机号码
            parameters.Add("cardnum", cardNum); //充值金额,目前可选：5、10、20、30、50、100、300
            parameters.Add("key", appkey);//你申请的key

            string result = sendPost(url, parameters, "get");

            return Newtonsoft.Json.JsonConvert.DeserializeObject<JuheReturnModel>(result);
        }
        #endregion

        #region 根据手机号和面值查询商品信息
        public static string GetGoodsByPhoneNum(string phoneNum, string cardNum)
        {
            string url = "http://op.juhe.cn/ofpay/mobile/telquery";

            var parameters = new Dictionary<string, string>();

            parameters.Add("phoneno", phoneNum); //手机号码
            parameters.Add("cardnum", cardNum); //充值金额,目前可选：5、10、20、30、50、100、300
            parameters.Add("key", appkey);//你申请的key

            string result = sendPost(url, parameters, "get");

            return result;
        }
        #endregion

        #region 手机直充接口
        public static JuheReturnModel MobileCharge(string phoneNum, string cardNum, string orderId)
        {
            string url = "http://op.juhe.cn/ofpay/mobile/onlineorder";
            string timeStamp = CommonLib.Helper.GetTimeStamp();
            string signMd5 = CommonLib.Helper.Md5Hash(openId + appkey + phoneNum + cardNum + orderId);

            var parameters = new Dictionary<string, string>();

            parameters.Add("phoneno", phoneNum); //手机号码
            parameters.Add("cardnum", cardNum); //充值金额,目前可选：5、10、20、30、50、100、300
            parameters.Add("orderid", orderId); //商家订单号，8-32位字母数字组合
            parameters.Add("key", appkey);//你申请的key
            parameters.Add("sign", signMd5); //校验值

            string result = sendPost(url, parameters, "get");

            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<JuheReturnModel>(result);
            }
            catch (Exception ex)
            {
                return new JuheReturnModel(101, ex.ToString());
            }
        }
        #endregion

        #region 订单状态查询
        public static string GetOrderStatus(string orderId)
        {
            string url = "http://op.juhe.cn/ofpay/mobile/ordersta";

            var parameters = new Dictionary<string, string>();

            parameters.Add("orderid", orderId); //商家订单号，8-32位字母数字组合
            parameters.Add("key", appkey);//你申请的key

            string result = sendPost(url, parameters, "get");

            return result;
        }
        #endregion

        #region 历史订单列表
        public static RechargeOrderReturnModel GetHistoryOrder(int page, int pageSize, string mobilePhone, string orderId)
        {
            string url = "http://op.juhe.cn/ofpay/mobile/orderlist";

            var parameters = new Dictionary<string, string>();

            parameters.Add("page", page.ToString()); //当前页数，默认1
            parameters.Add("pagesize", pageSize.ToString()); //每页显示条数，默认50，最大100 
            parameters.Add("mobilephone", mobilePhone); //检索指定手机号码
            parameters.Add("orderid", orderId); //需要检索的商户订单号
            parameters.Add("key", appkey);//你申请的key

            string result = sendPost(url, parameters, "get");

            var model = CommonLib.Helper.JsonDeserializeObject<RechargeOrderReturnModel>(result);

            return model;
        }
        #endregion

        #region 状态回调配置
        public static string ConfigCallback()
        {
            return "";
        }
        #endregion

        //public static int DalAdd(Model.JuheApiModel.JuheApi apiModel)
        //{
        //    MobileRechargeDal dal = new MobileRechargeDal();
        //    return dal.AddRecord(apiModel);
        //}

        /// <summary>
        /// Http (GET/POST)
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="parameters">请求参数</param>
        /// <param name="method">请求方法</param>
        /// <returns>响应内容</returns>
        static string sendPost(string url, IDictionary<string, string> parameters, string method)
        {
            if (method.ToLower() == "post")
            {
                HttpWebRequest req = null;
                HttpWebResponse rsp = null;
                System.IO.Stream reqStream = null;
                try
                {
                    req = (HttpWebRequest)WebRequest.Create(url);
                    req.Method = method;
                    req.KeepAlive = false;
                    req.ProtocolVersion = HttpVersion.Version10;
                    req.Timeout = 5000;
                    req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                    byte[] postData = Encoding.UTF8.GetBytes(BuildQuery(parameters, "utf8"));
                    reqStream = req.GetRequestStream();
                    reqStream.Write(postData, 0, postData.Length);
                    rsp = (HttpWebResponse)req.GetResponse();
                    Encoding encoding = Encoding.GetEncoding(rsp.CharacterSet);
                    return GetResponseAsString(rsp, encoding);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    if (reqStream != null) reqStream.Close();
                    if (rsp != null) rsp.Close();
                }
            }
            else
            {
                //创建请求
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "?" + BuildQuery(parameters, "utf8"));

                //GET请求
                request.Method = "GET";
                request.ReadWriteTimeout = 5000;
                request.ContentType = "text/html;charset=UTF-8";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));

                //返回内容
                string retString = myStreamReader.ReadToEnd();
                return retString;
            }
        }

        /// <summary>
        /// 组装普通文本请求参数。
        /// </summary>
        /// <param name="parameters">Key-Value形式请求参数字典</param>
        /// <returns>URL编码后的请求数据</returns>
        static string BuildQuery(IDictionary<string, string> parameters, string encode)
        {
            StringBuilder postData = new StringBuilder();
            bool hasParam = false;
            IEnumerator<KeyValuePair<string, string>> dem = parameters.GetEnumerator();
            while (dem.MoveNext())
            {
                string name = dem.Current.Key;
                string value = dem.Current.Value;
                // 忽略参数名或参数值为空的参数
                if (!string.IsNullOrEmpty(name))//&& !string.IsNullOrEmpty(value)
                {
                    if (hasParam)
                    {
                        postData.Append("&");
                    }
                    postData.Append(name);
                    postData.Append("=");
                    if (encode == "gb2312")
                    {
                        postData.Append(HttpUtility.UrlEncode(value, Encoding.GetEncoding("gb2312")));
                    }
                    else if (encode == "utf8")
                    {
                        postData.Append(HttpUtility.UrlEncode(value, Encoding.UTF8));
                    }
                    else
                    {
                        postData.Append(value);
                    }
                    hasParam = true;
                }
            }
            return postData.ToString();
        }

        /// <summary>
        /// 把响应流转换为文本。
        /// </summary>
        /// <param name="rsp">响应流对象</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>响应文本</returns>
        static string GetResponseAsString(HttpWebResponse rsp, Encoding encoding)
        {
            System.IO.Stream stream = null;
            StreamReader reader = null;
            try
            {
                // 以字符流的方式读取HTTP响应
                stream = rsp.GetResponseStream();
                reader = new StreamReader(stream, encoding);
                return reader.ReadToEnd();
            }
            finally
            {
                // 释放资源
                if (reader != null) reader.Close();
                if (stream != null) stream.Close();
                if (rsp != null) rsp.Close();
            }
        }

        /// <summary>
        /// 充值返回的状态Model
        /// </summary>
        public class JuheReturnModel
        {
            public JuheReturnModel()
            {

            }

            public JuheReturnModel(int errCode, string reasonStr)
            {
                error_code = errCode;
                reason = reasonStr;
                result = null;
            }

            public int error_code { get; set; }
            public string reason { get; set; }
            public ReturnResultModel result { get; set; }
        }

        public class ReturnResultModel
        {
            public string cardid { get; set; }
            public string cardname { get; set; }
            //public decimal inprice{get;set;}
            //public string game_area{get;set;}
            public int cardnum { get; set; }
            public decimal ordercash { get; set; }
            public string sporder_id { get; set; }
            public string uorderid { get; set; }
            public string game_userid { get; set; }
            public int game_state { get; set; }
            //public string uid { get; set; }
            //public decimal money { get; set; }
            //public decimal uordercash{get;set;}
        }

        public class PhoneCheckReturnModel
        {
            public int error_code { get; set; }
            public string reason { get; set; }
            public string result { get; set; }
        }

        public class SingleOrder
        {
            public string reason { get; set; }
            public int error_code { get; set; }

            public SingleOrderResult result { get; set; }
        }

        public class SingleOrderResult
        {
            public string uordercash { get; set; }
            public string sporder_id { get; set; }
            public int game_state { get; set; }
        }

        #region 获取第三方充值订单列表Model
        public class RechargeOrderReturnModel
        {
            public string reason { get; set; }
            public int error_code { get; set; }
            public RechargeOrderReturnData result { get; set; }
        }

        public class RechargeOrderReturnData
        {
            public RechargeOrderReturnData()
            {
                data = new List<RechargeOrderDate>();
                Summary = new RecordSummary();
            }
            public int totalcount { get; set; }
            public int totalpage { get; set; }
            public int page { get; set; }
            public int pagesize { get; set; }
            public List<RechargeOrderDate> data { get; set; }

            public RecordSummary Summary { get; set; }
        }

        public class RechargeOrderDate
        {
            public RechargeOrderDate()
            {
                accModel = new T_AccountBasic();
            }
            public string sporder_id { get; set; }
            public string uorderid { get; set; }
            public string cardid { get; set; }
            public string cardnum { get; set; }
            public string uordercash { get; set; }
            public string cardname { get; set; }
            public int game_state { get; set; }
            public string game_userid { get; set; }
            public DateTime addtime { get; set; }

            public T_AccountBasic accModel { get; set; }
        }
        #endregion

        #region 聚合账号余额查询

        public class BalanceReturnModel
        {
            public string reason { get; set; }
            public int error_code { get; set; }
            public BalanceResult result { get; set; }

        }

        public class BalanceResult
        {
            public string uid { get; set; }
            public string money { get; set; }
        }

        #endregion
    }
}
