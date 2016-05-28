using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Controls;
using Controls.Sms;
using BLL;

namespace OperationPlatform.Controllers
{
    [OperationPlatform.App_Start.LoginAuthentication]
    public class SmsController : Controller
    {
        #region 短信列表

        public ActionResult Index(int pageNo=1)
        {
            ViewBag.pageNo = 1;
            return View();
        }
        public ActionResult NoticeText()
        {
            var model = Controls.Sms.SmsList.GetNoticeText(1);
            if (model.Display == 1)
            {
                ViewBag.chValue = "checked='checked'";
            }
            else
            {
                ViewBag.chValue = "";
            }
            
            ViewBag.noticeId = model.Id.ToString();
            ViewBag.strContent = model.Content;

            return View();
        }

        public string GetSmsList(int iPage = 1, int accId = 0, int priorityType = -99, int sendType = -99, int smsType = -99,
            string timePeriod = "7", string start = "", string end = "")
        { 

            DateTime st = DateTime.Now.AddDays(-6);
            DateTime ed = DateTime.Now;

            switch (timePeriod)
            {
                case "7":
                    st = DateTime.Now.AddDays(-6);
                    break;
                case "15":
                    st = DateTime.Now.AddDays(-14);
                    break;
                case "30":
                    st = DateTime.Now.AddDays(-29);
                    break;
                case "other":
                    ed = Convert.ToDateTime(end);
                    st = Convert.ToDateTime(start);
                    break;
                default:
                    break;
            }

            
            SmsListModel smsList = SmsList.GetSmsList(iPage, accId, smsType, sendType, priorityType, st, ed);
            smsList.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(smsList.rowCount) / 20));

            return CommonLib.Helper.JsonSerializeObject(smsList,"yyyy-MM-dd HH:mm");
        }

        #endregion

        #region 短信测试

        public ActionResult ChannelTest(string context = "", string signTxt = "", int flag = 1, int smsCnt = 0, string smsAccList = "", int channelId = 0)
        {
            return View();
        }
        public string SetChannelTest(string context = "", string signTxt = "", int flag = 1, int smsCnt = 0, string smsAccList = "", int channelId = 0)
        { 
            if (context != "")
            {
                string content = context + "【" + signTxt + "】";
                string result = SmsChannelTest.GetChannelTest(content, smsAccList, smsCnt, channelId, flag);
                return result;
            }
            else
            {
                return "nocontext";
            }
        }

        #endregion

        #region 短信审核

        public ActionResult SmsReview(int pageIndex = 1, int pageSize = 15, string timePeriod = "7", string start = "", string end = "")
        {
            ViewBag.page = pageIndex;
            ReviewModel rModel = new ReviewModel();

            DateTime st = DateTime.Now.AddDays(-6);
            DateTime ed = DateTime.Now;

            if (pageIndex != 1)
            {
                st = TempData["st"] == null ? st : Convert.ToDateTime(TempData["st"]);
                ed = TempData["ed"] == null ? st : Convert.ToDateTime(TempData["ed"]);
            }
            else
            {
                switch (timePeriod)
                {
                    case "7":
                        st = DateTime.Now.AddDays(-6);
                        break;
                    case "15":
                        st = DateTime.Now.AddDays(-14);
                        break;
                    case "30":
                        st = DateTime.Now.AddDays(-29);
                        break;
                    case "other":
                        ed = Convert.ToDateTime(end);
                        st = Convert.ToDateTime(start);
                        break;
                    default:
                        break;
                }
                TempData["st"] = st;
                TempData["ed"] = ed;

            }

            rModel = SmsList.GetReviewList(pageIndex, 0, 0, st, ed);
            ViewBag.AllPage = rModel.PageSize / 20 + 1;

            return View(rModel.Data);
        }

        public ActionResult Review(int notifyId, int reviewType, int channelType, int mobile, int unicom,
            int telecom, int operatorId = 0, string reviewDesc = "")
        {
            int status = SmsList.SetReviewSms(notifyId, reviewType, channelType, mobile, unicom, telecom, operatorId,
                reviewDesc);

            if (status != 0)
            {
                Response.Write("<script>alert('审核成功！')</script>");
                return RedirectToAction("SmsReview");
            }
            else
            {
                Response.Write("<script>alert('审核失败！')</script>");
                return RedirectToAction("SmsReview");
            }
        }

        #endregion

        #region 修改短信内容

        public string UpdateSmsContent(int smsid, string smscontent)
        {
            if (Controls.Sms.SmsList.UpdateSmsContent(smsid, smscontent))
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        #endregion

        #region 获取短信详情列表
        /// <summary>
        /// 获取短信详情列表
        /// </summary>
        /// <param name="noticeid"></param>
        /// <param name="accid"></param>
        /// <param name="page"></param>
        /// <param name="pageSiz"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public string GetSmsDetailList(int noticeid, int accid = 0, int page = 1,
            string phone = "")
        {
            var list = Controls.Sms.SmsList.GetSmsDetailList(noticeid, accid, page, 15, phone);
            return CommonLib.Helper.JsonSerializeObject(list,"yyyy-MM-dd HH:mm:ss");
        }

        #endregion

        #region 短信通道设置

        public ActionResult ChannelOption()
        {
            return View();
        }

        /// <summary>
        /// 获得短信通道列表
        /// </summary>
        /// <returns></returns>
        public string GetSmsChannel()
        {
            var smsChannel = new SmsChannelControl();
            var oList = smsChannel.GetChannel();
            return CommonLib.Helper.JsonSerializeObject(oList);
        }

        /// <summary>
        /// 获得通道设置信息
        /// </summary>
        /// <returns></returns>
        public string GetChannelInfo()
        {
            var smsChannel = new SmsChannelBLL();
            var oInfo = smsChannel.GetChannelInfo();
            return CommonLib.Helper.JsonSerializeObject(oInfo);
        }

        /// <summary>
        /// 获得通道余额信息
        /// </summary>
        /// <returns></returns>
        public string GetChannelBalance()
        {
            var sResult = "-1";
            var channelId = Request["channelid"];
            if (channelId != null && channelId.ToString() != "")
            {
                var smsChannel = new SmsChannelControl();
                sResult = smsChannel.GetChannelBalance(Convert.ToInt32(channelId));
            }
            return sResult;
        }

        /// <summary>
        /// 更新运营商绑定通道设置
        /// </summary>
        /// <returns></returns>
        public string SetChannel()
        {
            string sResult = "-1";
            string chName = Request["chname"].ToString().Trim();
            int chId = Convert.ToInt32(Request["chid"].ToString().Trim());
            if (chId > 0)
            {
                var smsChannel = new SmsChannelControl();
                var iResult = smsChannel.SetChannel(chName, chId);
                if (iResult == 1)
                {
                    sResult = "1";
                }
            }
            return sResult;
        }

        /// <summary>
        /// 获得短信通道发送条数
        /// </summary>
        /// <returns></returns>
        public int GetChannelSum()
        {
            int chId = Convert.ToInt32(Request["chid"].ToString().Trim());
            var smsBll = new SmsChannelBLL();
            var iResult = smsBll.GetChannelSum(chId);
            return iResult;
        }

        /// <summary>
        /// 获得回访短信通道设置信息
        /// </summary>
        /// <returns></returns>
        public string GetRevisitChannel()
        {
            var smsChannel = new SmsChannelBLL();
            var oInfo = smsChannel.GetSysChannelInfo();
            return CommonLib.Helper.JsonSerializeObject(oInfo);
        }

        /// <summary>
        /// 更新后台回访通道设置
        /// </summary>
        /// <returns></returns>
        public string SetRevisitChannel()
        {
            string sResult = "-1";
            string chName = Request["chname"].ToString().Trim();
            int chId = Convert.ToInt32(Request["chid"].ToString().Trim());
            if (chId > 0)
            {
                var smsChannel = new SmsChannelControl();
                var iResult = smsChannel.SetRevisitChannel(chName, chId);
                if (iResult == 1)
                {
                    sResult = "1";
                }
            }
            return sResult;
        }

        #endregion

        #region 短信预告
        
        /// <summary>
        /// 保存短信通告信息
        /// </summary>
        /// <param name="noticeType">通告类型</param>
        /// <param name="noticeId">Id</param>
        /// <param name="flagEdit">修改标记</param>
        /// <param name="display">显示设置</param>
        /// <param name="noticeText">通告内容</param>
        /// <returns></returns>
        public string SaveNoticeText(int flagEdit, int noticeId, int noticeChk, string noticeText)
        {
            ManageUserModel uM = (ManageUserModel)Session["logUser"];
            noticeText = Server.UrlDecode(noticeText.Trim());
            var opName = uM.Name;
            var opIp = Request.UserHostAddress;

            if (noticeId == 0)
            {
                flagEdit = 1;
            }
            var objInfo = SmsList.SaveNoticeText(1, noticeId, flagEdit, noticeChk, noticeText, opName, opIp);
            return objInfo.ToString();
        }

        #endregion

        #region 短信日志记录
        /// <summary>
        /// 短信发送情况日志
        /// </summary>
        /// <returns></returns>
        public string GetSmsTipsText()
        {
            string strResult = CommonLib.Helper.SendHttpGet("http://log.i2oo.cn/api/SmsLog/next", null);
            return strResult;
        }

        /// <summary>
        /// 设置短信发送情况日志
        /// </summary>
        /// <returns></returns>
        public string SetSmsTipsText()
        {
            var accInfo = (ManageUserModel)Session["logUser"];
            Dictionary<string, string> formData = new Dictionary<string, string>();
            formData["Content"] = Request["context"].ToString().Trim();
            formData["insertTime"] = CommonLib.Helper.GetTimeStamp();
            formData["insertName"] = accInfo.Name;
            string postStr = CommonLib.Helper.SendHttpPost("http://log.i2oo.cn/api/SmsLog", formData);
            return postStr;
        }
        #endregion

        #region 短信内容管理

        public ActionResult SmsContentMaster()
        {
            return View();
        }

        public string GetContentList(int page,string type,string subType="")
        {
            return CommonLib.Helper.JsonSerializeObject(SmsList.GetSmsContent(page, type,subType));
        }

        public ActionResult FestivalSms()
        {
            ViewBag.type = "节日祝福";
            return View();
        }

        public ActionResult MarketingSms()
        {
            ViewBag.type = "营销模板";
            return View();
        }

        public ActionResult SmsTips()
        {
            ViewBag.type = "短信秘籍";
            return View();
        }

        public string AddSms()
        {
            return "";
        }

        public string UpdateCommonSmsContent(int smsid,string maxCate,string minCate, string smscontent)
        {
            return Controls.Sms.SmsList.UpdateCommonSmsContent(smsid, maxCate, minCate, smscontent);
        }

        public string GetMinCate(string maxCate)
        {
            List<string> str = SmsList.GetMinCate(maxCate);
            return CommonLib.Helper.JsonSerializeObject(str, "");
        }

        public string AddCate(string addCate)
        {
            return SmsList.AddCate(addCate);
        }

        public string AddCommonSms(string maxCate, string minCate, string smscontent)
        {
            return SmsList.AddCommonSms(maxCate, minCate, smscontent);
        }

        public string DeleteCommonSms(int id)
        {
            return SmsList.DeleteSms(id);
        }

        public string GetCateList(string type)
        {
            return SmsList.GetCateList(type);
        }

        public string UpdateCateName(string maxName, string minName, string updateName)
        {
            return SmsList.UpdateCateName(maxName, minName, updateName);
        }

        public string DeleteCate(string maxName, string minName)
        {
            return SmsList.DeleteCate(maxName, minName);
        }

        public string ChangeStatus(string maxName, string minName)
        {
            return SmsList.ChangeStatus(maxName, minName);
        }

        public string ChangeRanking(string maxName, string minName, int rank)
        {
            return SmsList.ChangeRanking(maxName, minName, rank);
        }

        #endregion
        #region 短信审核时效
        /// <summary>
        /// 获取短信审核时效
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public string GetSmsCheckPerform(DateTime stDate,DateTime edDate)
        {
            return CommonLib.Helper.JsonSerializeObject(SmsList.GetSmsPerformance(stDate, edDate)); 
        }

        #endregion
    }
}
