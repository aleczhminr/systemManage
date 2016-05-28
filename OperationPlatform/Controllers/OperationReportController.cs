using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controls.Helper4Control;
using Controls.OperationReport;
using Model;
using Utility;

namespace OperationPlatform.Controllers
{
    public class OperationReportController : Controller
    {
        // GET: OperationReport
        public ActionResult Index()
        {                       
            return View();
        }

        public ActionResult NewAccount()
        {
            return View();
        }

        public ActionResult Conversion()
        {
            return View();
        }

        public ActionResult Income()
        {
            return View();
        }

        public ActionResult Active()
        {
            return View();
        }

        public ActionResult Retention()
        {
            return View();
        }

        public string GetNewAccountModel(DateTime stDate, DateTime edDate, DateTime lstDate, DateTime ledDate)
        {
            return
                CommonLib.Helper.JsonSerializeObject(ReportControl.GetNewAccountModel(stDate, edDate, lstDate, ledDate));
        }

        public string GetConversionModel(DateTime stDate, DateTime edDate)
        {
            edDate = ControlHelper.GetEndDate(edDate);            
            return
                CommonLib.Helper.JsonSerializeObject(ReportControl.GetConversionModel(stDate, edDate));
        }

        public string GetRetentionModel(DateTime stDate, DateTime edDate, DateTime lstDate, DateTime ledDate)
        {
            UgcList list = new UgcList();
            try
            {
                list.DataList = ReportControl.GetRetentionModel(stDate, edDate, lstDate, ledDate);
            }
            catch (Exception ex)
            {
                Logger.Error("获取留存相关报表出错！", ex);
                return "";
            }
            

            return
                CommonLib.Helper.JsonSerializeObject(list);
        }

        public string GetAvgDataModel(DateTime stDate, DateTime edDate, DateTime lstDate, DateTime ledDate)
        {
            UgcList list = new UgcList();
            try
            {
                list.DataList = ReportControl.GetAvgDateModel(stDate, edDate, lstDate, ledDate);
            }
            catch (Exception ex)
            {
                Logger.Error("获取促活相关报表错误！", ex);
                return "";
            }


            return
                CommonLib.Helper.JsonSerializeObject(list);
        }

        public string GetIncomeModel(DateTime stDate, DateTime edDate, DateTime lstDate, DateTime ledDate)
        {
            UgcList list = new UgcList();
            try
            {
                list.DataList = ReportControl.GetIncomeModel(stDate, edDate, lstDate, ledDate);
            }
            catch (Exception ex)
            {
                Logger.Error("获取付费相关报表错误！", ex);
                return "";
            }


            return
                CommonLib.Helper.JsonSerializeObject(list);
        }
    }
}
