using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using CommonLib;
using Model;

namespace OperationPlatform.App_Start
{
    public class LoginAuthenticationAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        /// <summary>
        /// API接口密钥
        /// </summary>
        public readonly string AuthCode = "YH_B8C29A60A8E2D8670CCA3CE15C3E6486";
        public readonly string SecretKey = "YH_82ABE0DCF6670473D3A532B43A413E18";

        public void OnAuthentication(AuthenticationContext filterContext)
        {
                        
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var user = filterContext.HttpContext.Session["logUser"];
            if (user == null)
            {
                if (filterContext.HttpContext.Request.Headers["signature"] != null)
                {
                    var proxyRequest = new ProxyRequest();
                    if (filterContext.HttpContext.Request.Headers["signature"] != null)
                    {
                        proxyRequest.Signature = filterContext.HttpContext.Request.Headers["signature"].Trim();
                    }
                    if (filterContext.HttpContext.Request.Headers["timestamp"] != null)
                    {
                        proxyRequest.Timestamp = filterContext.HttpContext.Request.Headers["timestamp"].Trim();
                    }
                    if (filterContext.HttpContext.Request.Headers["nonce"] != null)
                    {
                        proxyRequest.Nonce = filterContext.HttpContext.Request.Headers["nonce"].Trim();
                    }
                    if (filterContext.HttpContext.Request.Headers["accid"] != null && filterContext.HttpContext.Request.Headers["accid"] != "")
                    {
                        proxyRequest.AccId = Convert.ToInt32(filterContext.HttpContext.Request.Headers["accid"].Trim());
                    }
                    if (filterContext.HttpContext.Request.Headers["token"] != null)
                    {
                        proxyRequest.Token = filterContext.HttpContext.Request.Headers["token"].Trim();
                    }
                    if (filterContext.HttpContext.Request.Headers["appkey"] != null)
                    {
                        proxyRequest.AppKey = filterContext.HttpContext.Request.Headers["appkey"].Trim();
                    }
                    if (filterContext.HttpContext.Request.Headers["requestname"] != null)
                    {
                        proxyRequest.RequestName = filterContext.HttpContext.Request.Headers["requestname"].Trim();
                    }
                    if (filterContext.HttpContext.Request.Headers["requestjson"] != null)
                    {
                        proxyRequest.RequestJson = filterContext.HttpContext.Request.Headers["requestjson"].Trim();
                    }

                    if (!CheckCode(proxyRequest))
                    {
                        filterContext.Result = new HttpUnauthorizedResult();
                        //filterContext.HttpContext.Response.Redirect("/Account/Login", false);
                        //filterContext.HttpContext.Response.Write("Authentication Error");
                    }
                }
                else
                {
                    filterContext.HttpContext.Response.Redirect("/Account/Login", false);
                }
            }
            //ManageUserModel uM = (ManageUserModel)Session["logUser"];
            //var user = filterContext.HttpContext.Session["logUser"];
            //if (user == null)
            //{
            //    filterContext.HttpContext.Response.Redirect("/Account/Login", false);
            //}
        }

        #region CheckCode 验证签名真实性
        /// <summary>
        /// 验证签名真实性
        /// </summary>
        /// <param name="requestModel">请求消息</param>
        /// <returns></returns>
        public bool CheckCode(ProxyRequest requestModel)
        {
            bool bResult = false;
            var strSign = new StringBuilder();

            //签名生成
            strSign.Append(AuthCode);
            strSign.Append(requestModel.Timestamp);
            strSign.Append(requestModel.Nonce);
            strSign.Append(SecretKey);
            
            long timeSpan = Convert.ToInt64(Helper.GetTimeStamp()) - Convert.ToInt64(requestModel.Timestamp.Trim());
            string strAuthCode = Helper.Md5Hash(strSign.ToString()).ToUpper();
            if (strAuthCode == requestModel.Signature && timeSpan <= 1500*100000)
            {
                //签名一致并且时间距离5分钟之内
                bResult = true;
            }
            return bResult;
        }
        #endregion
    }
}