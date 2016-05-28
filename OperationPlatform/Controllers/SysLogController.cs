using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace OperationPlatform.Controllers
{
    public class SysLogController : Controller
    {
        // GET: SysLog
        public ActionResult Index()
        {
            return View();
        }

        #region 运营/客服记录的事件日志
        /// <summary>
        /// 获取日志
        /// </summary>
        /// <returns></returns>
        public string GetDailyTipsText()
        {
            string strResult = CommonLib.Helper.SendHttpGet("http://log.i2oo.cn/api/WebUpdateLog/next", null);
            return strResult;
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <returns></returns>
        public string SetDailyTipsText()
        {
            var accInfo = (ManageUserModel)Session["logUser"];
            Dictionary<string, string> formData = new Dictionary<string, string>();
            formData["Content"] = Request["context"].ToString().Trim();
            formData["insertTime"] = CommonLib.Helper.GetTimeStamp();
            formData["insertName"] = accInfo.Name;
            string postStr = CommonLib.Helper.SendHttpPost("http://log.i2oo.cn/api/WebUpdateLog", formData);
            return postStr;
        }
        #endregion
    }
}
