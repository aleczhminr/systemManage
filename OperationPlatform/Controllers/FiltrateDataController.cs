using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controls.Filtrate;
using Controls.RuleManage;

namespace OperationPlatform.Controllers
{
    [OperationPlatform.App_Start.LoginAuthentication]
    public class FiltrateDataController : Controller
    {
        //
        // GET: /FiltrateData/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AccountFiltrate()
        {

            FiltrateData.AllMax MaxModel = Filtrate.GetAllMax();
            ViewBag.MaxJson = Newtonsoft.Json.JsonConvert.SerializeObject(MaxModel);
            return View();
        }

        public ActionResult FilterEx()
        {
            FiltrateData.AllMax MaxModel = Filtrate.GetAllMax();
            ViewBag.MaxJson = Newtonsoft.Json.JsonConvert.SerializeObject(MaxModel);
            return View();
        }

        public ActionResult Download(string verif)
        {
            string verification = verif;
            ManageUserModel uM = (ManageUserModel)Session["logUser"];
            int uid =uM.UserID;

            string UidList = Controls.Filtrate.Filtrate.GetAccountList(uid, verification);
            

            //string TopList = UidList.Substring(0, index);

            int starting = -1;
            int finish = 0;
            int size = 0;
            string nowStr = "";
            string itemList = "";

            while (size <= 100)
            {
                starting = UidList.IndexOf(',', finish);
                if (starting > -1)
                {
                    finish = UidList.IndexOf(',', starting + 1);
                    if (finish > 0)
                    {
                        nowStr = UidList.Substring(starting + 1, finish - starting - 1);
                    }
                    else
                    {
                        nowStr = UidList.Substring(starting + 1);
                        finish = starting + nowStr.Length + 1;
                    }
                    if (nowStr != null && nowStr != "")
                    {
                        if (size > 0)
                        {
                            itemList += ",";
                        }
                        itemList += "" + nowStr;
                        size++;
                    }
                }
                else
                {
                    break;
                }
            } 
            List<dynamic> ds = Controls.Filtrate.Filtrate.GetSummarizingData(itemList);

            Dictionary<string, string> colName = new Dictionary<string, string>();

            colName["ID"] = "店铺ID";
            colName["CompanyName"] = "店铺名称";
            colName["UserRealName"] = "店主名称";
            colName["PhoneNumber"] = "电话号码";
            colName["UserEmail"] = "电子邮件";
            colName["RegTime"] = "注册时间";
            colName["aotjb"] = "版本";
            colName["aotjbEndtime"] = "版本结束时间";
            colName["userNum"] = "会员数";
            colName["goodsNum"] = "商品名称";
            colName["saleNum"] = "销售数";
            colName["smsNum"] = "发送短信数";
            colName["dxunity"] = "剩余短信数";
            colName["outlayNum"] = "支出数";
            colName["returnInsertTime"] = "最后回访人";
            colName["allCount"] = "总优惠券";
            colName["userCount"] = "使用优惠券";
            colName["LoginTimeWeb"] = "登录次数";
            colName["LoginTimeLast"] = "最后登录时间";
            colName["active"] = "活跃状态";
            colName["orderMoney"] = "订单金额";
            colName["AgentName"] = "代理商";



            HelperEx.ExportExcel export = new HelperEx.ExportExcel("筛选数据", "synthesie", colName);
            string webPath = System.Web.HttpContext.Current.Server.MapPath("/");
            string strFileName = export.ExportFile(uM.UserName, ds, webPath);


            ViewBag.NavigateUrl = strFileName;

            Response.Redirect(strFileName);
            return View();
        }

        #region 得到基本信息

        public string TagList()
        {
            var list = Controls.Filtrate.Filtrate.GetTagList();
            return CommonLib.Helper.JsonSerializeObject(list);
        }

        public string OrderProjectList()
        {
            var list = Controls.Filtrate.Filtrate.GetOrderProjectList();
            return CommonLib.Helper.JsonSerializeObject(list);
        }

        public string AnentList()
        {
            var list = Controls.Filtrate.Filtrate.GetAnentList();
            return CommonLib.Helper.JsonSerializeObject(list);
        }

        #endregion

        public string SelectData(string postJson)
        {
            ManageUserModel uM = (ManageUserModel)Session["logUser"];
            var list = Controls.Filtrate.Filtrate.GetSelectData(postJson, uM.UserID, uM.Name);
            return CommonLib.Helper.JsonSerializeObject(list,"yyyy-MM-dd HH:mm:ss");
        }

        #region 优化筛选器之后的筛选器方法
        /// <summary>
        /// 优化规则的筛选器方法
        /// </summary>
        /// <param name="postJson"></param>
        /// <returns></returns>
        public string GetFilterDataSet(string postJson)
        {
            ManageUserModel uM = (ManageUserModel)Session["logUser"];
            var list = Controls.Filtrate.Filtrate.GetFilterData(postJson, uM.UserID, uM.Name);
            return CommonLib.Helper.JsonSerializeObject(list, "yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 获取规则条件列表
        /// </summary>
        /// <returns></returns>
        public string GetRuleCondition()
        {
            ConditionList list = new ConditionList();
            list.DataList = RuleManage.GetRuleConditionList("");
            return CommonLib.Helper.JsonSerializeObject(list);
        }

        /// <summary>
        /// 获取所选范围的枚举项
        /// </summary>
        /// <param name="colName"></param>
        /// <returns></returns>
        public string GetRangeItem(string colName)
        {
            return RuleManage.GetEnumItem(colName);
        }

        #endregion
    }
}