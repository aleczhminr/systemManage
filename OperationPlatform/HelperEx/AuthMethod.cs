using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using Model;

namespace OperationPlatform.HelperEx
{
    public static class AuthMethod
    {
        #region 接口信息
        /// <summary>
        /// API接口密钥
        /// </summary>
        public static string AuthCode = "W20E15C06H29A15T";

        /// <summary>
        /// 微信接口地址
        /// </summary>
        public static string ProxyUrl = string.Format("http://{0}/open.ashx", ConfigurationManager.AppSettings["ApiWeixinService"].ToString());
        
        #endregion

        #region CreateAuthCode 生成验证信息
        /// <summary>
        /// 生成验证信息
        /// </summary>
        /// <returns></returns>
        public static ProxyRequestModel.ProxyRequest CreateAuthCode()
        {
            var requestMd = new ProxyRequestModel.ProxyRequest { Timestamp = CommonLib.Helper.GetTimeStamp(), Nonce = CommonLib.Helper.GetRandomNum() };

            var strSign = new StringBuilder();
            strSign.Append(AuthCode);
            strSign.Append(requestMd.Timestamp);
            strSign.Append(requestMd.Nonce);
            requestMd.Signature = CommonLib.Helper.SHA1_Encrypt(strSign.ToString());

            return requestMd;
        }
        #endregion

        #region SendRequest 发送代理请求(Post)
        /// <summary>
        /// 发送代理请求(Post)
        /// </summary>
        /// <param name="proxyRequest">请求参数</param>
        /// <param name="requestName">请求名称</param>
        /// <param name="requestJson">请求参数(Json)</param>
        /// <returns></returns>
        public static ProxyRequestModel.WeixinResponseModel SendRequest(ProxyRequestModel.ProxyRequest proxyRequest)
        {
            var requestMd = CreateAuthCode();
            requestMd.AccId = proxyRequest.AccId;
            requestMd.RequestName = proxyRequest.RequestName;
            requestMd.RequestJson = proxyRequest.RequestJson;

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("signature", requestMd.Signature);
            parameters.Add("timestamp", requestMd.Timestamp);
            parameters.Add("nonce", requestMd.Nonce);
            parameters.Add("accid", requestMd.AccId.ToString());
            parameters.Add("requestname", requestMd.RequestName);
            parameters.Add("requestjson", requestMd.RequestJson);

            string strResult = string.Empty;
            strResult = CommonLib.Helper.SendHttpPost(ProxyUrl, parameters);
            var objResponse = CommonLib.Helper.JsonDeserializeObject<ProxyRequestModel.WeixinResponseModel>(strResult);

            return objResponse;
        }
        #endregion

    }
}