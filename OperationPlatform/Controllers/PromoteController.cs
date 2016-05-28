using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BLL;
using Controls.PlatformVisit;
using Controls.Promotion;
using Model;
using Utility;

namespace OperationPlatform.Controllers
{
    [OperationPlatform.App_Start.LoginAuthentication]
    public class PromoteController : Controller
    {
        // GET: Promote
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PromoteList()
        {
            return View();
        }

        public ActionResult AddPromotion()
        {
            return View();
        }

        public string AddNewPromotion(int linkType,string linkUrl,string remark,string linkName,string shortUrl)
        {
            T_OutLink model = new T_OutLink();

            model.CreateTime = DateTime.Now;
            model.PV = 0;
            model.ClickCount = 0;
            model.EndTime = Convert.ToDateTime("1900-01-01");
            if (!string.IsNullOrEmpty(shortUrl))
            {
                model.ShortUrl = shortUrl;
            }
            else
            {
                return "短链不可为空！";
            }
            model.linkClass = 0;
            model.linktype = linkType;
            if (!string.IsNullOrEmpty(linkName))
            {
                model.linkname = linkName;
            }
            else
            {
                return "名称不可为空！";
                
            }
            model.state = 1;
            model.remark = remark;
            if (!string.IsNullOrEmpty(linkUrl))
            {
                model.linkurl = linkUrl;
            }
            else
            {
                return "地址不可为空！";
            }


            ManageUserModel uM = (ManageUserModel)System.Web.HttpContext.Current.Session["logUser"];
            model.managerid = uM.UserID;

            int msg = OutLink.Add(model);

            if (msg != 0)
            {
                return "1";
            }
            else
                return "添加出错！";
        }

        public int ChangeStatus(string status,string id)
        {
            return OutLink.ChangeStatus(status,id);
        }

        public string GetBaiduUrl(string url)
        {
            string str = string.Empty;
            try
            {
                string postData = "url=" + url;
                byte[] bytes = Encoding.UTF8.GetBytes(postData);
                WebClient wc = new WebClient();
                wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                wc.Headers.Add("ContentLength", postData.Length.ToString());
                byte[] responseData = wc.UploadData("http://dwz.cn/create.php", "POST", bytes);
                wc.Dispose();
                str = System.Text.Encoding.GetEncoding("UTF-8").GetString(responseData);
            }
            catch (Exception ex)
            {
                str = "";
                Logger.Error("百度", ex);
            }
            if (str.IndexOf("\"status\":0") > 0)
            {
                str = str.Replace("{\"tinyurl\":\"", "");
                return str.Substring(0, str.IndexOf("\",")).Replace("\\", "");
            }
            else
            {
                return "";
            }
        }

        public ActionResult PromotionAnalyze()
        {
            return View();
        }

        public string GetPromoteList(int pageIndex, int majorType, int minorType, string adder, string linkName, int status, string stDate, string edDate)
        {
            DateTime edTime = DateTime.Now;
            DateTime stTime = DateTime.Now;
            if (!string.IsNullOrEmpty(stDate))
            {
                stTime = Convert.ToDateTime(stDate);
            }
            if (!string.IsNullOrEmpty(edDate))
            {
                edTime = Convert.ToDateTime(edDate);
            }

            //List<T_OutLinkInfo> outLinkList = OutLink.GetList(pageIndex, majorType, minorType, adder, linkName, status, stTime,
            //    edTime);

            return CommonLib.Helper.JsonSerializeObject(OutLink.GetList(pageIndex, majorType, minorType, adder, linkName, status, stTime,
                edTime), "");
        }

        public string GetMinorType(int id)
        {
            List<OutLinkType> typeList = OutLink.GetTypeList();
            string html = string.Empty;

            foreach (OutLinkType list in typeList)
            {
                if (list.id == id)
                {
                    foreach (OutLinkType item in list.itemList)
                    {
                        html += "{\"id\":\"" + item.id + "\",\"name\":\"" + item.ot_name + "\"},";
                    }
                    html="[" + html.TrimEnd(',') + "]";

                    return html;
                }
            }
            return "[{\"id\":\"0\",\"name\":\"无相关分类\"}]";
        }

    }
}
