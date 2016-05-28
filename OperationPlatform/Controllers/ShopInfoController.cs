using Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Controls.PlatformVisit;
using Controls.Shop;
using Model;
namespace OperationPlatform.Controllers
{
    [OperationPlatform.App_Start.LoginAuthentication]
    public class ShopInfoController : Controller
    {
        //
        // GET: /ShopInfo/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">店铺ID</param>
        /// <param name="td">回访任务ID</param>
        /// <returns></returns>
        public ActionResult Index(int id, int td = 0, int vd = 0)
        {
            ViewBag.AccId = id;
            ViewBag.TaskDailyId = td;
            ViewBag.VisitId = vd;
            return View();
        }
        public ActionResult ShopInfo(int id)
        {
            ViewBag.AccId = id;

            ManageUserModel uM = (ManageUserModel)Session["logUser"];
            if (uM.Name == "田震")
            {
                ViewBag.permission = 1;
            }
            else
            {
                ViewBag.permission = 0;
            }
            Model.T_AccountBasic accountModel = ShopDetails.GetAccountBasic(id);

            return View(accountModel);
        }
        public ActionResult ShopSale()
        {
            return View();
        }
        public ActionResult ShopGoodsList()
        {
            return View();
        }
        public ActionResult ShopSmsList()
        {
            return View();
        }
        public ActionResult ShopPayList()
        {
            return View();
        }
        public ActionResult ShopGrowUp()
        {
            return View();
        }
        public ActionResult ShopVisit()
        {
            return View();
        }
        public ActionResult MsgCenterList()
        {
            return View();
        }

        public ActionResult UserMessageList()
        {
            return View();
        }

        #region 店铺详情首页
        public string GetShopSummarize(int id)
        {
            T_AccountSummarize.Summarize summarize = ShopDetails.GetAccountAllSummarize(id);
            var orders = ShopDetails.GetAccountOrder(id);
            //订单笔数和金额实时计算，不从每日店铺信息汇总表获取,保证数据的及时性
            if (orders == null) return CommonLib.Helper.JsonSerializeObject(summarize);
            summarize.orderNum = orders.DataList.Count;
            summarize.orderMoney = Convert.ToDecimal(orders.SumInfo.Profit);
            return CommonLib.Helper.JsonSerializeObject(summarize);
        }

        public string GetLogDetail(int id)
        {
            return ShopDetails.GetLogDetail(id);
        }

        public string GetLatestLogClient(int id)
        {
            return ShopDetails.GetLatestLogClient(id);
        }

        public string GetVisitStatus(int id)
        {
            VisitPeriodModel vm = new VisitPeriodModel();
            vm = ShopDetails.GetVisitPeriod(id);

            if (vm != null)
            {
                return CommonLib.Helper.JsonSerializeObject(ShopDetails.GetVisitPeriod(id), "yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                return "";
            }

        }

        public string GetAppList(int id)
        {
            AppList list = ShopDetails.GetAccountAppList(id);
            return CommonLib.Helper.JsonSerializeObject(list, "yyyy'-'MM'-'dd");
        }


        public string SetAppActive(int accid, int appkey)
        {

            ManageUserModel uM = (ManageUserModel)Session["logUser"];
            int r = ShopDetails.AddAccountApp(accid, appkey, uM.UserID);

            return r.ToString();
        }

        public string SetAlipay(int accId, string alipayAccount, string alipayPid, string alipayKey)
        {
            ManageUserModel uM = (ManageUserModel)Session["logUser"];
            int r = ShopDetails.AddAccountAlipay(accId, alipayAccount, alipayPid, alipayKey);

            return r.ToString();
        }

        /// <summary>
        /// 获取店铺未处理回访
        /// </summary>
        /// <param name="accId"></param>
        /// <returns></returns>
        public string GetUnfinishedTask(int accId)
        {
            return TaskInfo.GetShopSimpleVisitTasks(accId);
        }

        /// <summary>
        /// 获取店铺相关的论坛反馈
        /// </summary>
        /// <param name="accId"></param>
        /// <returns></returns>
        public string GetForumFeedBack(int accId)
        {
            return TaskInfo.GetForumFeedBack(accId);
        }

        public string RemoveAccountApp(int accid, int appkey)
        {
            if (ShopDetails.RemoveAccountApp(accid, appkey))
            {
                return "1";
            }
            else
            {
                return "2";
            }
        }
        public string SubbranchList(int id)
        {
            Dictionary<string, object> list = ShopDetails.GetSubbranchList(id);
            return CommonLib.Helper.JsonSerializeObject(list);
        }
        public string UserList(int id)
        {
            List<T_Account_UserBasic> userBasic = ShopDetails.GetAccountUserBasic(id);
            return CommonLib.Helper.JsonSerializeObject(userBasic);
        }
        /// <summary>
        /// 得到一个店铺的信息
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public string SysAccountInfo(int id)
        {
            return CommonLib.Helper.JsonSerializeObject(ShopDetails.GetSysAccountInfo(id));
        }

        public string UpdateSysAccount(int accid, string col, string val)
        {
            int r = ShopDetails.UpdateAccountCollectInfo(accid, col, val);
            if (r > 0)
            {
                return "1";
            }
            else
            {
                return "-1";
            }
        }

        public string GetOrder(int id)
        {
            var list = ShopDetails.GetAccountOrder(id);
            return CommonLib.Helper.JsonSerializeObject(list, "yyyy-MM-dd HH:mm:ss");
        }
        public string GetAccountCoupon(int id)
        {
            var list = ShopDetails.GetAccountCoupon(id);
            return CommonLib.Helper.JsonSerializeObject(list, "yyyy-MM-dd HH:mm:ss");
        }

        public string UpdateHtmlTemes(int accid, int val)
        {
            if (ShopDetails.UpdateHtmlTemes(accid, val))
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }


        public string SysTagQuestions(int accid, string tagType = "")
        {
            List<int> excludTypeList = new List<int>();
            excludTypeList.Add(0);
            foreach (string item in tagType.Split(','))
            {
                int tid = 0;
                if (int.TryParse(item, out tid))
                {
                    excludTypeList.Add(tid);
                }
            }


            var list = ShopDetails.GetTagQuestions(accid, excludTypeList.ToArray());
            return CommonLib.Helper.JsonSerializeObject(list);
        }
        #endregion


        #region 店铺销售页
        /// <summary>
        /// 得到 单个店铺的销售信息
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public string GetSaleChartData(int accid, DateTime startTime, DateTime endTime, int dataType)
        {
            endTime = endTime.Date.Add(new TimeSpan(23, 59, 59));
            var chart = ShopDetails.GetSaleChartData(accid, startTime, endTime, dataType);
            return CommonLib.Helper.JsonSerializeObject(chart);
        }
        #endregion

        #region 店铺短信页面
        public string SmsList(int accid, int pageIndex, int? status = null, string phone = "", DateTime? starttime = null, DateTime? endtime = null)
        {
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            var smsList = ShopDetails.GetSmsList(pageIndex, 15, accid, status, phone, starttime, endtime);


            return CommonLib.Helper.JsonSerializeObject(smsList, "yy-MM-dd HH:mm:ss");
        }
        #endregion

        #region 店铺商品页面

        public string GoodsList(int accid, int pageIndex, string goodsname = "", DateTime? starttime = null, DateTime? endtime = null)
        {
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            var goodsList = ShopDetails.GetGoodsinfo(pageIndex, 15, accid, goodsname, starttime, endtime);
            return CommonLib.Helper.JsonSerializeObject(goodsList, "yy-MM-dd HH:mm:ss");
        }


        #endregion


        #region 店铺支出信息

        public string GetPayList(int accid, int pageIndex, string name = "", DateTime? starttime = null, DateTime? endtime = null)
        {
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            var outPayList = ShopDetails.GetPayRecord(pageIndex, 15, accid, name, starttime, endtime);
            return CommonLib.Helper.JsonSerializeObject(outPayList, "yy-MM-dd HH:mm:ss");
        }

        #endregion


        #region 成长图表

        public string GetGrowUpChartData(int accid, string dataType, DateTime? startTime = null, DateTime? endTime = null)
        {

            Dictionary<string, string> columnList = new Dictionary<string, string>();
            columnList.Add("login", "loginNum");
            columnList.Add("user", "userNum");
            columnList.Add("sale", "saleNum");
            columnList.Add("money", "saleMoney");
            columnList.Add("sms", "smsNum");
            columnList.Add("goods", "goodsNum");
            columnList.Add("order", "orderMoney");
            columnList.Add("rep", "acc_Rep");
            columnList.Add("mood", "moodNum");

            List<string> dataTypeList = new List<string>();

            foreach (string item in dataType.Split(','))
            {
                if (columnList.ContainsKey(item))
                {
                    dataTypeList.Add(columnList[item]);
                }
            }

            var chartData = ShopDetails.GetGrowingChart(accid, dataTypeList.ToArray(), startTime, endTime);

            return CommonLib.Helper.JsonSerializeObject(chartData);
        }

        #endregion


        #region 店铺回访

        public string VisitTagList()
        {
            var list = Controls.PlatformVisit.VisitInfo.GetVisitTagList();
            return CommonLib.Helper.JsonSerializeObject(list);
        }
        public string TaskDailyInfo(int id)
        {
            var model = Controls.PlatformVisit.TaskInfo.GetSimplifyModel(id);
            return CommonLib.Helper.JsonSerializeObject(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accid">店铺ID</param>
        /// <param name="recordType">记录类别</param>
        /// <param name="vmanner">回访方式</param>
        /// <param name="content">内容</param>
        /// <param name="stat">状态</param>
        /// <param name="insertName">回访人</param>
        /// <param name="tags">标签</param>
        /// <param name="contact">回访方式值</param>
        /// <param name="taskid">任务ID</param>
        /// <returns></returns>
        public string addVisit(int accid, int recordType, int vrOne, int vrTwo, int vrThree, int vmanner, string content, int stat, string tags = "", string contact = "", int taskid = 0, int feedBack = 0)
        {
            //登录人名

            ManageUserModel uM = (ManageUserModel)Session["logUser"];


            content = Server.UrlDecode(content);
            return Controls.PlatformVisit.VisitInfo.AddVisitInfo(accid, recordType, vrOne, vrTwo, vrThree, vmanner, content, stat, uM.Name, tags, contact, taskid, feedBack).ToString();
        }

        public string VisitReason(int id)
        {
            var list = Controls.PlatformVisit.VisitInfo.GetVisitReasonList(id);

            return CommonLib.Helper.JsonSerializeObject(list, "MM-dd HH:mm:dd");
        }
        public string VisitManner()
        {
            var list = Controls.PlatformVisit.VisitInfo.GetVisitManner();
            return CommonLib.Helper.JsonSerializeObject(list);
        }
        #endregion


        #region 店铺标签

        public string ShopTagList(int id)
        {
            var list = ShopDetails.GetAccountTag(id);
            return CommonLib.Helper.JsonSerializeObject(list);
        }
        public string SysTagList()
        {
            var list = ShopDetails.GetSysTagList();
            return CommonLib.Helper.JsonSerializeObject(list);
        }
        public string AddShopSysTag(int accid, int tagid)
        {
            ManageUserModel uM = (ManageUserModel)Session["logUser"];
            var list = ShopDetails.AddAccountTag(accid, tagid, uM.Name);
            if (list > 0)
            {
                return "0";
            }
            else
            {
                return "1";
            }
        }

        public string AddShopSysNewTag(int accid, string tagName)
        {
            ManageUserModel uM = (ManageUserModel)Session["logUser"];
            return ShopDetails.AddShopSysNewTag(accid, tagName, uM.Name).ToString();
        }
        public string RemoveShopTag(int accid, int tagid)
        {
            var list = ShopDetails.RemoveAccountTag(accid, tagid);
            if (list)
            {
                return "0";
            }
            else
            {
                return "1";
            }
        }
        /// <summary>
        /// 得到需要回访的列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string NeedVisitList(int id)
        {
            var list = ShopDetails.GetNeedVisitList(id);
            return CommonLib.Helper.JsonSerializeObject(list, "MM-dd HH:mm");
        }

        public string AddVisitReply(int vi_id, int accid, string vr_Content, int vr_Stat)
        {
            vr_Content = Server.UrlDecode(vr_Content);

            ManageUserModel uM = (ManageUserModel)Session["logUser"];
            string insertName = uM.Name;

            int l = Controls.PlatformVisit.VisitInfo.AddVisitReply(vi_id, accid, vr_Content, vr_Stat, insertName);
            return l.ToString();
        }
        #endregion


        #region 得到一个店铺最后登录信息

        public string LastLogInfo(int id)
        {
            var list = ShopDetails.GetAccountLastLogSource(id);
            return CommonLib.Helper.JsonSerializeObject(list, "MM-dd hh:mm");
        }

        #endregion

        #region 获取店铺消息列表
        public string GetMessageList(int accid, int pageIndex, string start, string end, int msgType, string content, string title)
        {
            Sys_UserMessageBLL.UpdateUserMessage(accid);
            var list = Sys_UserMessageBLL.GetUserList(accid, pageIndex, start, end, msgType, content, title);
            return CommonLib.Helper.JsonSerializeObject(list, "yyyy-MM-dd HH:mm:ss");
        }

        #endregion

    }
}
