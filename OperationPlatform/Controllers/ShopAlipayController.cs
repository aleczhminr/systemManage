using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controls.ShopAlipay;

namespace OperationPlatform.Controllers
{
    public class ShopAlipayController : Controller
    {
        // GET: ShopAlipay
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取汇总Json
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public string GetAlipaySummary(DateTime stDate, DateTime edDate)
        {
            return "";
        }

        /// <summary>
        /// 获取图表详情Json
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public string GetAlipayChart(DateTime stDate, DateTime edDate, string columns)
        {
            string[] cs = columns.Split(',');
            return CommonLib.Helper.JsonSerializeObject(ShopAlipay.GetShopAlipay(stDate, edDate, cs));
        }
    }
}
