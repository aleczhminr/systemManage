using Controls.MobileShow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OperationPlatform.Controllers
{
    public class MobileShowController : Controller
    {
        //
        // GET: /MobileShow/
        public ActionResult Index()
        {
            return View();
        }
        public string GetSouceData(DateTime startTime, DateTime endTime,string dType="orderNum")
        {
            return MobileShow.GetDataSource(startTime, endTime,dType);
        }
        public string GetSourceSummary(DateTime startTime, DateTime endTime, string dType = "orderNum")
        {
            return MobileShow.GetSourceSummary(startTime, endTime, dType);
        }
	}
}