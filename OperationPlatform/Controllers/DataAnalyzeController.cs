using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controls.DataAnalyze;
using Controls.IndexPanelInfo;

namespace OperationPlatform.Controllers
{
    public class DataAnalyzeController : Controller
    {
        // GET: DataAnalyze
        public ActionResult Index()
        {
            return View();
        }

        public string GetVennData(string type)
        {
            DateTime edDate = DateTime.Now;
            DateTime stDate = edDate.AddDays(-7);
            return DataAnalyze.GetVennData(type, stDate, edDate);
        }
    }
}
