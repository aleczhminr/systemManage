using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controls;
using Model;

namespace OperationPlatform.Controllers
{
    public class RegTimeReportController : Controller
    {
        // GET: RegTimeReport
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取数据对比汇总
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="source"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public string RegisterSourceJson(int timeType, string type)
        {
            if (type.Length > 0)
            {
                List<string> sourceList = new List<string>();
                foreach (string item in type.Split(','))
                {
                    sourceList.Add(item);
                }
                var list = Controls.RegTimeReport.RegTimeReport.RegTimeReportSource(timeType, sourceList.ToArray());
                return CommonLib.Helper.JsonSerializeObject(list);
            }
            else
            {
                return "null";
            }
        }
    }
}
