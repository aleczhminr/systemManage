using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OperationPlatform.Controllers
{
    [OperationPlatform.App_Start.LoginAuthentication]
    public class AreaMapController : Controller
    {
        //
        // GET: /AreaMap/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DataDistributed()
        {
           ViewBag.StartTime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            //ViewBag.StartTime = "2013-06-05";
            return View();
        }
        public ActionResult SelectMap(string type,string gName)
        {
            ViewBag.type = type;
            ViewBag.gName = gName;
            return View();
        }

        public ActionResult GoodsMap(string gName)
        {
            ViewBag.gName = gName;
            return View();
        }

        public string DataDistributedChartJson(string key, string sClass, DateTime bgdate, DateTime? eddate = null,string order="")
        {
            DateTime endTime = DateTime.Now;
            if (sClass == "range")
            {
                endTime = Convert.ToDateTime(eddate);
            }
            var list = Controls.AreaMap.AreaMap.GetDataDistributedChart(key, sClass, bgdate, endTime, order);
            if (list.areaDataList != null)
            {
                return CommonLib.Helper.JsonSerializeObject(list, "MM-dd HH:mm");
            }
            else
            {
                return "none";
            }
        }

        public string DataDistributedList(string areaname, int page, DateTime bgdate, DateTime eddate)
        {
            var list = Controls.AreaMap.AreaMap.GetDataDistributedList(page, areaname, bgdate, eddate, 10);
            return CommonLib.Helper.JsonSerializeObject(list, "MM-dd HH:mm");
        }


        public string SaleMapJson(int oldvalue=0)
        {
            var list = Controls.AreaMap.AreaMap.GetMapSaleList(oldvalue);
            return CommonLib.Helper.JsonSerializeObject(list);
        }

        public string GoodsMapJson(string gName)
        {
            var list = Controls.AreaMap.AreaMap.GetGoodsList(gName);
            return CommonLib.Helper.JsonSerializeObject(list);
        }
    }
}