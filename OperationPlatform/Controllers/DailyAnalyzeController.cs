using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controls.DailyAnalyze;

namespace OperationPlatform.Controllers
{
    [OperationPlatform.App_Start.LoginAuthentication]
    public class DailyAnalyzeController : Controller
    {
        // GET: DailyAnalyze
        public ActionResult Index()
        {
            return View();
        }

        public string GetDailyInfo(int page, int source, int newReg, int noAction, DateTime dateTime,string orderWhere)
        {
            return DailyAnalyze.GetDailyAnalyzeList(page, source, newReg, noAction, dateTime, orderWhere);
        }
    }
}
