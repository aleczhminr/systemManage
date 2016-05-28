using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controls.SysAccount;
using Controls.IndexPanelInfo;
using Controls.Order;
using Controls.PlatformVisit;
using Controls.Sms;
using Model.Model4View;

namespace OperationPlatform.Controllers
{
    [OperationPlatform.App_Start.LoginAuthentication]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            Model.ManageUserModel uM = (Model.ManageUserModel)Session["logUser"];

            if (uM!=null)
            {
                switch (uM.PowerSession)
                {
                    case 0:
                    case 4:
                    case 6:
                    case 7:
                        return View();
                    case 9:
                        return RedirectToAction("UsrFeedback", "PlatformVisit");
                    default:
                        return RedirectToAction("Servcie", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        public ActionResult Exit()
        {
            Session.RemoveAll();
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        public string TodayYesterdayContrast()
        {
            var list = PanelShow.GetTodayYesterdayContrast();
            return CommonLib.Helper.JsonSerializeObject(list, "MM-dd");
        }
        public string TodayYesterdayContrastMore()
        {
            var list = PanelShow.GetTodayYesterdayContrastMore();
            return CommonLib.Helper.JsonSerializeObject(list);
        }


        public string GetTableData(string gType, string source="all")
        {
            DateTime dt = DateTime.Now;
            return PanelShow.getTableData(gType, dt,source);
        }

        public string GetYesterdayData()
        {
            return PanelShow.GetYesterdayNowData();
        }

        public string GetBalance()
        {
            return OrderInfoList.GetJuheAccountBalance();
        }

        #region KPI预设值相关操作
        public ActionResult KPI_Input()
        {
            return View();
        }

        public ActionResult KPI_View(int page=1,string msg="")
        {
            List<MonthlyKPI> list = PanelShow.GetKpiList(page);

            ViewData["Msg"] = msg;
            return View(list);
        }

        [HttpPost]
        public ActionResult AddKpi(MonthlyKPI model)
        {
            string msg = PanelShow.AddKpi(model);
            //return RedirectToAction("KPI_View", new { msg = msg });
            //return KPI_View(1, msg);
            return RedirectToAction("MonthlyReview", "Home");
        }

        public ActionResult DeleteKpi(int id)
        {
            string msg = PanelShow.DeleteKpi(id);
            //return RedirectToAction("KPI_View", new { msg = msg });
            return RedirectToAction("KPI_View", "Home");
        }

        public string GetCompleteProp()
        {
            DateTime dt = DateTime.Now;
            return PanelShow.GetMonthlyData(dt);
        }

        #endregion

        #region DashBoard数据面板

        public string GetLoginBoard()
        {
            DateTime dt = DateTime.Now;
            return PanelShow.GetLoginDashBoardData(dt);
        }

        public string GetActiveBoard()
        
        {
            DateTime dt = DateTime.Now;
            return PanelShow.GetActiveDashBoardData(dt);
        }

        public string GetNewUsrBoard()
        {
            DateTime dt = DateTime.Now;
            return PanelShow.GetNewUsrDashBoardData(dt);
        }

        public string GetNewUsrBoardDetail()
        {
            DateTime dt = DateTime.Now;
            return PanelShow.GetNewUsrDashBoardDataDetail(dt);
        }

        public string GetRegUsr()
        {
            DateTime dt = DateTime.Now;
            return PanelShow.GetRegUsr(dt);
        }

        public string GetLoginType()
        {
            DateTime dt = DateTime.Now;
            return PanelShow.GetLoginType(dt);
        }

        public string GetStatusDataBoard()
        {
            DateTime dt = DateTime.Now;
            return PanelShow.GetStatusType(dt);
        }
        #endregion

        #region 每月数据汇总

        public ActionResult MonthlyReview()
        {   
            return View();
        }

        public string GetMonthlyReview()
        {
            string returnJson = "";

            returnJson = PanelShow.GetMonthlyReview();

            return returnJson;
        }

        public string GetAllData()
        {
            string returnJson = "";

            ManageUserModel uM = (ManageUserModel)Session["logUser"];
            if (uM.UserName == "guorui" || uM.UserName == "yanhao")
            {
                returnJson = PanelShow.GetAllData();
            }
            
            return returnJson;
        }

        #endregion

        public string GetActiveUsrVenn()
        {
            DateTime edDate = DateTime.Now;
            DateTime stDate = edDate.AddDays(-7);
            return PanelShow.GetActiveVenn(stDate, edDate);
        }

        #region 客服首页
        public ActionResult Servcie()
        {
            return View();
        }

        public ActionResult _Index_TaskInfo()
        {
            List<Sys_TaskDailyInfo> list = TaskInfo.IndexDailyTaskInfo();

            return PartialView(list);
        }
        public ActionResult _Index_SmsReview()
        {
            ReviewModel rwModel = new ReviewModel();

            rwModel = SmsList.GetReviewList(1, 1, 0, DateTime.Now.AddDays(-6), DateTime.Now);

            return PartialView(rwModel.Data);
        }


        public string IndexReview(int notifyId, int reviewType, int channelType, int mobile, int unicom,
            int telecom, string reviewDesc = "")
        {
            ManageUserModel uM = (ManageUserModel)Session["logUser"];

            int status = SmsList.SetReviewSms(notifyId, reviewType, channelType, mobile, unicom, telecom, uM.UserID,
                reviewDesc);

            if (status != 0)
            {
                return "1";
            }
            else
            {
                return "2";
            }
        }

        /// <summary>
        /// 执行审核通过的动作
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexShopChecked(int id, string phone)
        {
            return Content(CheckShopInfo(id, phone), "text/html");
        }

        /// <summary>
        /// 执行店铺删除的动作
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexShopDelete(int id)
        {
            return Content(DeleteUncheckedShop(id), "text/html");
        }
        /// <summary>
        /// 审核店铺信息
        /// </summary>
        /// <returns></returns>
        public string CheckShopInfo(int id, string phone)
        {
            int type = string.IsNullOrEmpty(phone) ? 0 : 1;
            int status = Account.CheckShop(id, type);

            if (status > 0)
            {
                return "1";
            }
            else
            {
                return "2";
            }
        }

        /// <summary>
        /// 删除审核店铺
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public string DeleteUncheckedShop(int accid)
        {
            int status = Account.DeleteShopDuringChecking(accid);
            if (status > 0)
            {
                return "<script>alert('删除成功！');window.location='/HomePage/ShopCheck'; </script>";
            }
            else
            {
                return "<script>alert('删除失败！');window.location='/HomePage/ShopCheck'; </script>";
            }
        }
        #endregion
    }
}