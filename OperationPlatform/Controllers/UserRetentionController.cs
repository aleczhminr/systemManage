using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controls;
using Controls.UserRetention;

namespace OperationPlatform.Controllers
{
    [OperationPlatform.App_Start.LoginAuthentication]
    public class UserRetentionController : Controller
    {
        // GET: UserRetention
        public ActionResult Index()
        {
            ViewBag.edTime = DateTime.Now.ToShortDateString();
            ViewBag.bgTime = DateTime.Now.AddDays(-7).ToShortDateString();

            return View();
        }

        #region 平均留存率测试方法

        public ActionResult MethodTest()
        {
            ViewBag.edTime = DateTime.Now.ToShortDateString();
            ViewBag.bgTime = DateTime.Now.AddDays(-7).ToShortDateString();
            return View();
        }

        public string GetRetentionTest(string dateType, DateTime bgTime, DateTime edTime, string usrType, string regSource)
        {
            return UserRetention.GetUserRetentionTest(dateType, bgTime, edTime, usrType, regSource);
        }

        #endregion 

        public string GetUserRetention(string dateType, DateTime bgTime, DateTime edTime, string usrType, string regSource, string agent="")
        {
            return UserRetention.GetUserRetention(dateType, bgTime, edTime, usrType, regSource,agent);
        }

        public ActionResult ActiveUsrDetail()
        {
            return View();
        }

        public ActionResult ActiveStatus()
        {
            return View();
        }

        public string GetActiveStatus(DateTime stTime,DateTime edTime)
        {
            string returnJson = "";
            edTime = edTime.AddDays(1);
            returnJson = UserRetention.GetActiveStatus(stTime, edTime);
            return returnJson;
        }

        public ActionResult NewUsrRetention()
        {
            ViewBag.edTime = DateTime.Now.ToShortDateString();
            ViewBag.bgTime = DateTime.Now.AddDays(-7).ToShortDateString();
            return View();
        }

        public string GetUserRetentionEx(string dateType, DateTime bgTime, DateTime edTime, string usrType,
            string regSource)
        {
            return UserRetention.GetUserRetentionEx(dateType, bgTime, edTime, usrType, regSource);
        }

    }
}
