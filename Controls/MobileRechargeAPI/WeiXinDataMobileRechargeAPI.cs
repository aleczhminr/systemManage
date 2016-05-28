using CommonLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Web;
using Model;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using CommonLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Controls.MobileRechargeAPI
{
    public static class WeiXinDataMobileRechargeAPI
    {
        public static string apiHost = "http://localhost:25081" + "/api/";
        /// <summary>
        /// API接口密钥
        /// </summary>
        public static readonly string AuthCode = "wXb7jdg6RfN7zrlsBT";
        /// <summary>
        /// signature 验证
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> ApiSignature()
        {
            String _Secret = "WSKXQ896OMDCS7YMWVZFbwPYzlQp33zg";
            String _AppKey = "I200LD02WSUQIRRGYM";
            Random r = new Random();
            String i1 = (r.Next(10000) + 1000).ToString();
            String _timestamp = Helper.GetTimeStamp();
            String sign = "";
            sign = Helper.Md5Hash(_AppKey + _timestamp + i1 + _Secret);
            String _Signature = sign; 
            Dictionary<string, string> head = new Dictionary<string, string>();
            head.Add("Signature", _Signature);
            head.Add("Timestamp", _timestamp);
            head.Add("Nonce", i1);
            head.Add("AppKey", _AppKey);
            return head;
        }
        public static string SendMobileRechargeAmounts(string money)
        {
            string iResult = "-1";
            try
            {
                Dictionary<string, object> postData = new Dictionary<string, object>();
                var objData = new Dictionary<string, string>();
                objData["money"] = money.ToString();
                var retJSON = HttpPost(apiHost + "/weixinpushmethod?method=rechargebalancemoney",ApiSignature(), objData);
                RetrunResponseModel rM = CommonLib.Helper.JsonDeserializeObject<RetrunResponseModel>(retJSON);
                if (retJSON == "success")
                {
                    iResult = "1";
                }
            }
            catch (Exception ex)
            {

            }
            return iResult;
        }
        public static string HttpPost(string sHttpUrl, IDictionary<string, string> Headers, IDictionary<string, string> parameters)
        {
            string UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0)";
            HttpWebResponse response = CreatePostHttpResponse(sHttpUrl, Headers, parameters, null, UserAgent, Encoding.UTF8, null);
            var reader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
            return reader.ReadToEnd();
        }
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }
        private static HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, string> Headers, IDictionary<string, string> parameters, int? timeout, string userAgent, Encoding requestEncoding, CookieCollection cookies)
        {
            string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (requestEncoding == null)
            {
                throw new ArgumentNullException("requestEncoding");
            }
            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            if (!(Headers == null || Headers.Count == 0))
            {
                foreach (string key in Headers.Keys)
                {
                    request.Headers.Add(key, Headers[key]);
                }
            }
            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            else
            {
                request.UserAgent = DefaultUserAgent;
            }

            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value;
            }
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            //如果需要POST数据  
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    }
                    i++;
                }
                byte[] data = requestEncoding.GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            return request.GetResponse() as HttpWebResponse;
        }
    }
}
