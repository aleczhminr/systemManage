using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OperationPlatform.Controllers
{
    [OperationPlatform.App_Start.LoginAuthentication]
    public class OperationController : Controller
    {
        //
        // GET: /Operation/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Tendency()
        {
            return View();
        }
        public ActionResult MonthComparison()
        {
            return View();
        }
        public ActionResult RegisterSource()
        {
            return View();
        }
        public ActionResult ActiveAnalysis()
        {
            return View();
        }

        public ActionResult SourceData()
        {
            return View();
        }

        public ActionResult DetailSource()
        {
            return View();
        }

        public string GetSouceData(DateTime startTime, DateTime endTime,string source,string conditions)
        {
            string[] usrSource = source.Split(',');
            string[] conds = conditions.Split(',');

            return Controls.Operation.Operation.GetDiffSourceData(startTime, endTime, usrSource, conds);
        }

        public ActionResult AccountActiveList()
        {
            ViewBag.DateNowString = DateTime.Now.ToString("yyyy-MM-dd");
           // ViewBag.DateNowString = ("2012-01-1");

            return View();
        }
        public string TendencyJson(DateTime startTime,DateTime endTime,string columns)
        {
            string[] cs=columns.Split(',');
            var model = Controls.Operation.Operation.Tendency(startTime, endTime, cs);
            return CommonLib.Helper.JsonSerializeObject(model);
        }

        public string MonthComparisonJson(int timeType, string startMonth, string endMonth, string dataType, string sourceType, string sourceCnt)
        {
            if (dataType == "shop")
            {
                var model = Controls.Operation.Operation.MonthComparisonAboutNewAccount(timeType, startMonth, endMonth);
                //var peerData = Controls.Operation.Operation.GetPeerMonthData(DateTime.Now, DateTime.Now.AddMonths(-1));
                model.SeriesCount = sourceCnt;
                model.SourceType = sourceType;
                return CommonLib.Helper.JsonSerializeObject(model);
            }
            else
            {
                //分Android、IOS、PC端进行数据对比
                //if (dataType == "order"&&(!string.IsNullOrWhiteSpace(sourceType)))
                //{
                //    var resultModel = Controls.Operation.Operation.MonthOrderComparison(timeType, startMonth, endMonth,sourceType, dataType);
                //    return CommonLib.Helper.JsonSerializeObject(resultModel);
                //}

                if (!string.IsNullOrWhiteSpace(sourceType))
                {
                    var resultModel = Controls.Operation.Operation.MonthSectionComparison(timeType, startMonth, endMonth, sourceType, dataType);
                    return CommonLib.Helper.JsonSerializeObject(resultModel);
                }

                var model = Controls.Operation.Operation.MonthComparison(timeType, startMonth, endMonth, dataType);
                return CommonLib.Helper.JsonSerializeObject(model);
            }
        }

        public string GetPeerMonthData()
        {
            return Controls.Operation.Operation.GetPeerMonthData(DateTime.Now, DateTime.Now.AddMonths(-1));
        }

        /// <summary>
        /// 获取数据对比汇总
        /// </summary>
        /// <param name="timeType"></param>
        /// <param name="startMonth"></param>
        /// <param name="endMonth"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public string MonthSummary(int timeType, string startMonth, string endMonth, string dataType)
        {
            return CommonLib.Helper.JsonSerializeObject(Controls.Operation.Operation.GetDataSummary(timeType, startMonth, endMonth, dataType));
        }


        /// <summary>
        /// 获取会员、销售、商品、订单、支出数据对比分类汇总
        /// </summary>
        /// <param name="timeType"></param>
        /// <param name="startMonth"></param>
        /// <param name="endMonth"></param>
        /// <param name="dataType"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public string SectionMonthSummary(int timeType, string startMonth, string endMonth, string dataType,string sourceType)
        {
            return CommonLib.Helper.JsonSerializeObject(Controls.Operation.Operation.GetSectionDataSummary(timeType, startMonth, endMonth, dataType,sourceType));
        }

        public string TendencySummary(DateTime startTime, DateTime endTime, string columns)
        {
            string[] cs = columns.Split(',');
            return Controls.Operation.Operation.GetTendencySummary(startTime, endTime, cs);
        }

        public string RegisterSourceJson(DateTime startTime, DateTime endTime, string source, string dataType)
        {
            if (source.Length > 0)
            {
                //List<int> sourceList = new List<int>();
                //foreach (string item in source.Split(','))
                //{
                //    int i = 0;
                //    if (int.TryParse(item, out i))
                //    {
                //        sourceList.Add(i);
                //    }
                //}

                var list = Controls.Operation.Operation.RegisterSource(startTime, endTime, source.Split(','), dataType);
                return CommonLib.Helper.JsonSerializeObject(list);
            }
            else
            {
                return "null";
            }
        }

        public string ActiveAnalysisJson(DateTime startTime, DateTime endTime, string columns)
        {
            string[] column = columns.Split(',');
            var model = Controls.Operation.Operation.ActiveAnalysisJson(startTime, endTime, column);
            return CommonLib.Helper.JsonSerializeObject(model);
        }

        public string ActiveSummary(DateTime startTime, DateTime endTime, string columns)
        {
            string[] column = columns.Split(',');
            return Controls.Operation.Operation.GetActiveSummary(startTime, endTime, column);
            //return CommonLib.Helper.JsonSerializeObject(model);
        }

        public string ActiveAnalysisList(DateTime startTime, DateTime endTime, int page)
        {
            var list = Controls.Operation.Operation.ActiveAnalysisList(startTime, endTime, page, 10);
            return CommonLib.Helper.JsonSerializeObject(list, "MM月dd日");
        }

        public string ShopActiveList(int pageIndex, DateTime mainstart, DateTime mainend, string mainactive="", int maincontinue = 0)
        {
            List<int> activeList = new List<int>();
            foreach (string item in mainactive.Split(','))
            {
                int active = 0;
                if (int.TryParse(item, out active))
                {
                    activeList.Add(active);
                }
            }


            var list = Controls.Operation.Operation.ShopActiveList(pageIndex, mainstart, mainend, activeList.ToArray(), maincontinue,10);

            return CommonLib.Helper.JsonSerializeObject(list,"MM-dd hh:mm");
        }
        public string GroupShopActiveList(int pageIndex, DateTime mainstart, DateTime mainend, DateTime followstart, DateTime followend, string mainactive = "", int maincontinue = 0, string followactive = "", int followcontinue = 0)
        {
            List<int> mainActiveList = new List<int>();
            foreach (string item in mainactive.Split(','))
            {
                int active = 0;
                if (int.TryParse(item, out active))
                {
                    mainActiveList.Add(active);
                }
            }

            List<int> followActiveList = new List<int>();
            foreach (string item in followactive.Split(','))
            {
                int active = 0;
                if (int.TryParse(item, out active))
                {
                    followActiveList.Add(active);
                }
            }

            var list = Controls.Operation.Operation.GroupShopActiveList(pageIndex, mainstart, mainend, followstart, followend, mainActiveList.ToArray(), followActiveList.ToArray(), maincontinue, followcontinue, 15);

            return CommonLib.Helper.JsonSerializeObject(list, "MM-dd hh:mm");
        }
	}
}