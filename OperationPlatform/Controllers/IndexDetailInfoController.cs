using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controls.IndexDetail;
using Model;

namespace OperationPlatform.Controllers
{
        [OperationPlatform.App_Start.LoginAuthentication]
    public class IndexDetailInfoController : Controller
    {
        // GET: IndexDetailInfo
        public ActionResult Index(int pageIndex, string type, int index,string cnt,string location,string verif,string date,string day,string keyword="")
        {
            ViewBag.Page = pageIndex;
            ViewBag.Type = type;
            ViewBag.Index = index;
            ViewBag.Cnt = cnt;
            ViewBag.Location = location;
            ViewBag.Verif = verif;

            ViewBag.Date = date;
            ViewBag.Day = day;

            ViewBag.Keyword = keyword;

            switch (type)
            {
                case "LoginUser":
                    ViewBag.Title = "当日登录用户";
                    break;
                case "ActiveUser":
                    ViewBag.Title = "当日用户活跃类型";
                    break;
                case "Sale":
                    ViewBag.Title = "当日产生销售用户";
                    break;
                case "Usr":
                    ViewBag.Title = "当日录入会员用户";
                    break;
                case "Goods":
                    ViewBag.Title = "当日录入商品用户";
                    break;
                case "Sms":
                    ViewBag.Title = "当日发送短信用户";
                    break;
                case "PaidAll":
                case "Order":
                    ViewBag.Title = "当日产生订单用户";
                    break;
                case "Location":
                    ViewBag.Title = location + "地区用户";
                    break;
                case "Filter":
                    ViewBag.Title = "筛选结果用户";
                    break;
                case "NowActiveByLogin":
                    int dayCount = Convert.ToInt32(day) + 1;
                    ViewBag.Title = Convert.ToDateTime(date).ToShortDateString() + "注册后第" + dayCount + "日留存用户";
                    break;
                case "FunnelDetail":
                    switch (keyword)
                    {
                        case "reg":
                            ViewBag.Title = "漏斗-注册用户";
                            break;
                        case "login":
                            ViewBag.Title = "漏斗-登录用户";
                            break;
                        case "paid":
                            ViewBag.Title = "漏斗-付费用户";
                            break;
                        case "active":
                            ViewBag.Title = "漏斗-活跃用户";
                            break;
                        case "datainput":
                            ViewBag.Title = "漏斗-录入数据用户";
                            break;
                        case "saleinput":
                            ViewBag.Title = "漏斗-录入销售用户";
                            break;
                        case "secretention":
                            ViewBag.Title = "漏斗-次日留存用户";
                            break;
                    }
                    
                    break;
            }
            return View();
        }

        public string GetDetailData(int pageIndex, string type, int index, string cnt, string location, string order, string verif, string date, string day,string keyword="")
        {
            string returnJson = "";
            ManageUserModel uM = (ManageUserModel)Session["logUser"];
                    int uid =uM.UserID;

            switch (type)
            {
                case "LoginUser"://登录用户数据
                    returnJson = IndexDetail.GetLoginAccountId(pageIndex, index, cnt, order);
                    break;
                case "ActiveUser"://活跃度用户数据
                    returnJson = IndexDetail.GetActiveAccountId(pageIndex, index, cnt, order);
                    break;
                case "PaidAll"://获取今日所有付费用户
                    returnJson = IndexDetail.GetPaidAllAccountId(pageIndex, index, cnt, order);
                    break;
                case "Location"://获取某个区域店铺列表
                    returnJson = IndexDetail.GetLocationUsr(pageIndex, location,order);
                    break;
                case "Filter"://获取筛选器结果用户
                    
                    returnJson = IndexDetail.GetFilterUsr(pageIndex, verif, order, uid);
                    break;
                case "NowActiveByLogin"://留存率数据集
                    returnJson = IndexDetail.GetRetentionUserDetail(pageIndex, index, cnt, date, day, order);
                    break;
                case "FunnelDetail"://漏斗详情数据
                    string UidList = Controls.Filtrate.Filtrate.GetAccountList(uid, verif);
                    string activeList = UidList.Substring(UidList.IndexOf(',') + 1);//去除首个0

                    returnJson = IndexDetail.GetFunnelDetail(pageIndex, keyword, activeList,cnt);
                    break;
                default://通用数据
                    returnJson = IndexDetail.GetGeneralAccountId(pageIndex, type, index, cnt, order);
                    break;
            }

            return returnJson;
        }

        public string GetLocationUsrStatus(string location)
        {
            return IndexDetail.GetLocationUsrStatus(location);
        }
    }
}
