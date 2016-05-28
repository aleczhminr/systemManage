using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controls.SingleUserAnalyze;

namespace OperationPlatform.Controllers
{
    [OperationPlatform.App_Start.LoginAuthentication]
    public class UserBoardController : Controller
    {
        // GET: UserBoard
        public ActionResult Index(int id)
        {
            ViewBag.AccId = id;
            return View();
        }

        public string GetPageData(int accId)
        {
            try
            {
                return SingleUserAnalyze.GetSingleUsrAnalyze(accId);
            }
            catch (Exception ex)
            {
                Utility.Logger.Error("UserBoard", ex);
                return "";

            }
        }

        public string GetMostSaleList(int accId)
        {
            return SingleUserAnalyze.GetMostSaleList(accId);
        }

        public string GetMostProfitList(int accId)
        {
            return SingleUserAnalyze.GetMostProfitList(accId);
        }
    }
}
