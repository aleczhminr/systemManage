using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controls.PlatformVisit;

namespace OperationPlatform.Controllers
{
    [OperationPlatform.App_Start.LoginAuthentication]
    public class IntegralManageController : Controller
    {
        //
        // GET: /IntegralManage/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IntegralStore()
        {
            return View();
        }
        public ActionResult TaskControls()
        {
            return View();
        }

        public ActionResult IntegralStat()
        {
            return View();
        }

        public ActionResult IntegralInput()
        {
            return View();
        }

        public string GetExchangeLog(int pageIndex, int pageSize, int accid = 0, string projectName = "", DateTime? startTime = null, DateTime? endTime = null, int stateValue = -1)
        {
            var list = Controls.IntegralManage.IntegralStore.GetExchangeLogList(pageIndex, pageSize, accid, projectName, startTime, endTime, stateValue);
            return CommonLib.Helper.JsonSerializeObject(list, "yyyy-MM-dd HH:mm:ss");
        }

        public string SetLogistics(int id, string loginstics, string loginsticsnumber)
        {
            ManageUserModel uM = (ManageUserModel)Session["logUser"];

            if (id > 0)
            {
                if (Controls.IntegralManage.IntegralStore.UpdateLogistics(id, loginstics, loginsticsnumber, uM.Name))
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "-1";
            }

        }

        public string GetTaskJourna(int pageIndex, int pageSize, DateTime? statTime = null, DateTime? endTime = null, int state = -1, int accid = 0, string type = "")
        {
            var list = Controls.PlatformVisit.TaskJourna.GetList(pageIndex, pageSize, statTime, endTime, state, accid, type);
            return CommonLib.Helper.JsonSerializeObject(list, "yyyy-MM-dd HH:mm:ss");
        }

        public string UpdateTaskStatus(int id, string auditCon)
        {
            auditCon = Server.UrlDecode(auditCon);

            var r = Controls.PlatformVisit.TaskJourna.TaskAuditOk(id, auditCon);
            return r.ToString();
        }

        public string GetIntegralProduct(string dayDate)
        {
            DateTime dt = Convert.ToDateTime(dayDate);
            var model = Controls.IntegralManage.IntegralStore.GetDailyExchangeModel(dt);
            return CommonLib.Helper.JsonSerializeObject(model);
        }

        public string AddStat(DateTime dateTime, string dataDic)
        {
            return Controls.IntegralManage.IntegralStore.UpdateExchangeModel(dateTime, dataDic);
        }

        public string GetIntegralStatChart(DateTime startTime, DateTime endTime)
        {
            return
                CommonLib.Helper.JsonSerializeObject(Controls.IntegralManage.IntegralStore.GetIntegralChart(startTime,
                    endTime));
        }

        public string GetIntegralPieStat(DateTime startTime, DateTime endTime)
        {
            return
                CommonLib.Helper.JsonSerializeObject(Controls.IntegralManage.IntegralStore.GetIntegralPieModel(
                    startTime, endTime));
        }

        public string ImportFeedBack(int accid, string content)
        {
            ManageUserModel uM = (ManageUserModel)Session["logUser"];

            return VisitInfo.AddCustomCareFeedBack(uM.Name, content, accid, 1).ToString();
        }

    }
}