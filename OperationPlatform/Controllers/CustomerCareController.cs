using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controls;
using Controls.CustomerCare;
using Model;

namespace OperationPlatform.Controllers
{
    [OperationPlatform.App_Start.LoginAuthentication]
    public class CustomerCareController : Controller
    {
        // GET: CustomerCare
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ServiceReview()
        {
            return View();
        }

        public ActionResult ServiceOrder()
        {
            return View();
        }

        public ActionResult VisitAnalyze()
        {
            return View();
        }
        public ActionResult WithdrawalInfo()
        {
            return View();
        }

        /// <summary>
        /// 获取客服汇总数据
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public string getServiceInfo(DateTime startTime, DateTime endTime)
        {
            return CustomerCare.getServiceInfo(startTime, endTime);
        }
        public string UpdateWithdrawalRecord(int withdrawalRecordId, int accid=0,int status=5)
        {
            string ip = HttpContext.Request.UserHostAddress;
            int operatorId = 0;
            string operatorName = string.Empty;
            if (Session["logUser"] != null)
            {
                ManageUserModel uM = (ManageUserModel)Session["logUser"];
                operatorId = uM.UserID;
                operatorName = uM.UserName;
            }
            return CustomerCare.UpdateWithdrawalStatus(withdrawalRecordId, status, ip, operatorId, accid, operatorName);
        }

        /// <summary>
        /// 获取订单数据 
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="dayCnt"></param>
        /// <param name="type"></param>
        /// <param name="person"></param>
        /// <returns></returns>
        public string getOrderInfo(DateTime startTime, DateTime endTime, int dayCnt, int type,string person)
        {
            return CustomerCare.getOrderInfo(startTime, endTime, dayCnt, type, person);
        }

        /// <summary>
        /// 获取回访信息
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public string getVisitInfo()
        {
            return CustomerCare.getVisitinfo();
        }
        public string getWithdrawRecord(int pageIndex = 1, int withdrawStatus = -99, int payType = -99, string withdrawalNo = "", string accId = "", string start = "", string end = "")
        {
            return CustomerCare.GetWithdrawalRecord(pageIndex, withdrawStatus, payType, withdrawalNo, accId, start, end);
        }
        /// <summary>
        /// 获取回访信息详情
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string getVisitDetail(int type)
        {
            return CustomerCare.getVisitDetail(type);
        }

        public ActionResult CareRetention()
        {
            ViewBag.edTime = DateTime.Now.ToShortDateString();
            ViewBag.bgTime = DateTime.Now.AddDays(-7).ToShortDateString();
            return View();
        }

        public string GetCareRetention(DateTime stDate, DateTime edDate, string dateType, string usrName)
        {
            return CustomerCare.GetCareRetention(stDate, edDate, dateType, usrName);
        }

        public ActionResult CarePartition()
        {
            return View();
        }

        public string GetCarePartitionPer(DateTime startTime, DateTime endTime,string usrName,int index)
        {
            TimeSpan ts = endTime - startTime;
            if (ts.TotalDays<=1)
            {
                startTime = startTime.AddDays(-1);
            }
            if (ts.TotalDays>31)
            {
                return "";
            }
            string returnJson = "";

            returnJson = CustomerCare.GetCarePartitionPer(startTime, endTime, usrName, index);

            return returnJson;
        }

        public ActionResult SmsCheckPerform()
        {
            return View();
        }

        #region 订单续费率
        public ActionResult OrderRenewal()
        {
            return View();
        }

        /// <summary>
        /// 返回订单续费数据Json
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetOrderRenewal(DateTime stDate,DateTime edDate,string type)
        {
            return CustomerCare.GetOrderRenewal(stDate, edDate, type);
        }

        #endregion 
    }
}
