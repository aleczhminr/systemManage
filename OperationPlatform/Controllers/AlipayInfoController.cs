using Controls.AlipayInfo;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OperationPlatform.Controllers
{
    public class AlipayInfoController : Controller
    {
        //
        // GET: /AlipayInfo/
        public ActionResult Index()
        {
            return View();
        }
        public string getAlipayInfoRecord(int pageIndex = 1, int userType = -99, int displayType = -99, string accId = "", string start = "", string end = "")
        {
            return AlipayInfo.getAlipayInfoRecord(pageIndex, userType, displayType, accId, start, end);
        }
        public string getAlipayInfoModel(int alipayId)
        {
            return AlipayInfo.getAlipayInfoModel(alipayId);
        }
        public string updateStatus(int alipayId, int accid, int oldstatus = 0, int status = 0, bool isGoNextStep = false, string remark = "")
        {
            int operatorId = 0;
            string operatorIP = string.Empty;
            if (Session["logUser"] != null)
            {
                ManageUserModel uM = (ManageUserModel)Session["logUser"];
                operatorId = uM.UserID;
            }
            operatorIP = Request.UserHostAddress;
            return AlipayInfo.updateStatus(accid, oldstatus, status, isGoNextStep, remark, alipayId, operatorId, operatorIP);
        }
    }
}
