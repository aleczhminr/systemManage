using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controls;
using Controls.OrderAnalyze;

namespace OperationPlatform.Controllers
{
    [OperationPlatform.App_Start.LoginAuthentication]
    public class OrderAnalyzeController : Controller
    {
        // GET: OrderAnalyze
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取订单类型信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="dataType"></param>
        /// <param name="BgTime"></param>
        /// <param name="EdTime"></param>
        /// <returns></returns>
        public string getOrderTypeData(string type, string dataType, DateTime BgTime, DateTime EdTime)
        {
            return OrderAnalyze.getOrderAnalyzeData(type, dataType, BgTime, EdTime);
        }

        /// <summary>
        /// 获取订单详情列表
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="keyword"></param>
        /// <param name="bgTime"></param>
        /// <param name="edTime"></param>
        /// <returns></returns>
        public string getOrderTrend(string dataType, string keyword, DateTime bgTime, DateTime edTime)
        {
            return OrderAnalyze.getOrderTrend(dataType,keyword, bgTime, edTime);
        }

        public ActionResult NewUsrOrderAnalyze()
        {
            return View();
        }

        public string GetNewOrdChart(DateTime stTime, DateTime edTime)
        {
            return OrderAnalyze.GetNewUsrOrder(stTime, edTime);
        }

        public string GetOrderType(string dataType, DateTime BgTime, DateTime EdTime)
        {
            return OrderAnalyze.GetOrderType(dataType, BgTime, EdTime);
        }
    }
}
