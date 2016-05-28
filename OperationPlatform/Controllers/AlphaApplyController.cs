using Controls.AlphaApply;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OperationPlatform.Controllers
{
    public class AlphaApplyController : Controller
    {
        //
        // GET: /AlphaApply/
        public ActionResult Index()
        {
            return View();
        }
        public string getAlphaApplyRecord(int pageIndex = 1, int alphaStatus = -99, string userPhone = "", string accId = "", string start = "", string end = "", string alphaVersion = "")
        {
            return AlphaApply.GetAlphaApplyRecord(pageIndex, alphaStatus, userPhone, accId, start, end, alphaVersion);
        }
        public string updateAlphaApplyRecord(int id, int status)
        {
            string ip = HttpContext.Request.UserHostAddress;
            int opid = 0;
            if (Session["logUser"] != null)
            {
                ManageUserModel uM = (ManageUserModel)Session["logUser"];
                opid = uM.UserID;
            }
            return AlphaApply.UpdateWithdrawalStatus(id, status, ip, opid);
        }
    }
}