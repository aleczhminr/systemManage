using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controls.IndexPanelInfo;
using Model;

namespace OperationPlatform.Controllers
{
    [OperationPlatform.App_Start.LoginAuthentication]
    public class FunnelAnalyzeController : Controller
    {
        // GET: FunnelAnalyze
        public ActionResult Index(string verif)
        {
            if (!string.IsNullOrEmpty(verif))
            {
                ViewBag.List = verif;
                //ViewBag.Verification = verif;

                //string verification = verif;
                //ManageUserModel uM = (ManageUserModel)Session["logUser"];
                //int uid = uM.UserID;
                //string UidList = Controls.Filtrate.Filtrate.GetAccountList(uid, verification);

                //string activeList = UidList.Substring(UidList.IndexOf(',') + 1);//去除首个0
                //if (!string.IsNullOrEmpty(activeList))
                //{
                //    ViewBag.List = verif;
                //}
                //else
                //{
                //    ViewBag.List = "";
                //}

            }
            else
            {
                ViewBag.List = "";
            }

            return View();
        }

        /// <summary>
        /// 返回转化率漏斗图
        /// </summary>
        /// <param name="timeType"></param>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public string GetFunnelData(string timeType, string sourceType, DateTime? stDate, DateTime? edDate)
        {
            DateTime stTime = DateTime.Now;
            DateTime edTime = DateTime.Now;

            if (stDate != null && edDate != null)
            {
                stTime = stDate.Value;
                edTime = edDate.Value;
            }
            return PanelShow.GetFunnelData(timeType,sourceType, stTime, edTime);
        }

        public string GetFunnelDataEx(string verif)
        {
            string verification = verif;
            ManageUserModel uM = (ManageUserModel)Session["logUser"];
            int uid = uM.UserID;
            string UidList = Controls.Filtrate.Filtrate.GetAccountList(uid, verification);

            string activeList = UidList.Substring(UidList.IndexOf(',') + 1);//去除首个0
            return PanelShow.GetFunnelDataEx(activeList);
        }
    }
}
