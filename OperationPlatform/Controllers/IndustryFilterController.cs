using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controls.IndustryFilter;

namespace OperationPlatform.Controllers
{
    public class IndustryFilterController : Controller
    {
        // GET: IndustryFilter
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 清洗新增用户
        /// </summary>
        /// <returns></returns>
        //public string FilterNewAccount()
        //{

        //}

        /// <summary>
        /// 清洗全部用户
        /// </summary>
        /// <returns></returns>
        public string FilterAllUser()
        {
            return IndustryFilter.FilterIndustry().ToString();
        }
    }
}
