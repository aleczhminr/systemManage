using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Dapper
{
    /// <summary>
    /// 主站API 请求
    /// </summary>
    public class AppApi
    {
        public readonly string ProxyUrl = "http://" + ConfigurationManager.AppSettings["AppHost"] + "/API/Sys/SysApi.ashx";
        public readonly string AuthCode = "MR_bQjSwJopeNtyReT7s";                       //API接口密钥
        #region CreateAuthCode 生成验证信息
        /// <summary>
        /// 生成验证信息
        /// </summary>
        /// <returns></returns>
        public ProxyModel.ProxyRequestModel CreateAuthCode()
        {
            var requestMd = new ProxyModel.ProxyRequestModel { Timestamp = CommonLib.Helper.GetTimeStamp(), Nonce = CommonLib.Helper.GetRandomNum() };

            var strSign = new StringBuilder();
            strSign.Append(AuthCode);
            strSign.Append(requestMd.Timestamp);
            strSign.Append(requestMd.Nonce);
            requestMd.Signature = CommonLib.Helper.SHA1_Encrypt(strSign.ToString());

            return requestMd;
        }
        #endregion
        #region SendRequest 发送命令请求(Post)
        /// <summary>
        /// 发送命令请求(Post)
        /// </summary>
        /// <param name="openid">Openid</param>
        /// <param name="requestName">请求名称</param>
        /// <param name="requestJson">请求参数(Json)</param>
        /// <returns></returns>
        public ProxyModel.ProxyResponseModel SendRequest(string requestName, string requestJson = "")
        {


            var requestMd = CreateAuthCode();
            requestMd.RequestName = requestName;
            requestMd.RequestJson = requestJson;

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("signature", requestMd.Signature);
            parameters.Add("timestamp", requestMd.Timestamp);
            parameters.Add("nonce", requestMd.Nonce);
            parameters.Add("requestname", requestMd.RequestName);
            parameters.Add("requestjson", requestMd.RequestJson);


            string strResult = CommonLib.Helper.SendHttpPost(ProxyUrl, parameters);
            var objResponse = CommonLib.Helper.JsonDeserializeObject<ProxyModel.ProxyResponseModel>(strResult);


            return objResponse;

        }
        #endregion


		#region TaskProvideIntegral 新手任务审核完成 发放积分
		/// <summary>
		/// 新手任务审核完成 发放积分
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public int TaskProvideIntegral(int taskId)
        {
            var iResult = 0;
            var response = SendRequest("taskauditok", taskId.ToString());
            if (response.Status == true)
            {
                iResult = int.Parse(response.ObjectStr);
            }
            else
            {
                iResult = -100;
            }
            return iResult;
        }
        #endregion
    }
}
