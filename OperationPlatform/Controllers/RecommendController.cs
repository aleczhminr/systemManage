using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controls.Recommend;

namespace OperationPlatform.Controllers
{
    [OperationPlatform.App_Start.LoginAuthentication]
    public class RecommendController : Controller
    {
        // GET: Recommend
        public ActionResult Index()
        {
            return View();
        }

        public string GetRecList(int page, int type, string date, string accid)
        {
            DateTime recDate = DateTime.MinValue;
            int accId = -1;

            if (!string.IsNullOrEmpty(date))
            {
                recDate = Convert.ToDateTime(date);
            }

            if (!string.IsNullOrEmpty(accid))
            {
                accId = int.Parse(accid);
            }

            return Recommend.GetRecList(page, type, recDate, accId);

        }
    }
}
