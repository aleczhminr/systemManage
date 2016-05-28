using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controls.IndexDetail;

namespace OperationPlatform.Controllers
{
    [OperationPlatform.App_Start.LoginAuthentication]
    public class ActiveDetailController : Controller
    {
        // GET: ActiveDetail
        //public ActionResult Main()
        //{
        //    return View("~/Views/ActiveDetail/Index.cshtml");
        //}
        [HttpPost]
        public ActionResult Index()
        {
            
            string type = Request.Form["type"];
            //List<int> accids = CommonLib.Helper.JsonDeserializeObject<List<int>>(Request.Form["accids"]);

            ViewBag.Type = type;
            ViewBag.DayAccids = Request.Form["accids"];
            ViewBag.Page = 1;

            if (type=="NowActiveByLogin")
            {
                ViewBag.Title = "留存用户详情";
            }

            return View();
        }


        public string GetActiveUsrDetail(int pageIndex, string type, string dayAccids)
        {
            string returnJson = "";
            List<int> accids = CommonLib.Helper.JsonDeserializeObject<List<int>>(dayAccids);
            returnJson = IndexDetail.GetGeneralAccountId(pageIndex, type, accids);
            return returnJson;
        }

    }
}
