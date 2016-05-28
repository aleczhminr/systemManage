using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;
using Controls;
using Controls.PlatformVisit;
using OperationPlatform.HelperEx;

namespace OperationPlatform.Controllers
{
    [OperationPlatform.App_Start.LoginAuthentication]
    public class PlatformVisitController : Controller
    {
        //
        // GET: /PlatformVisit/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult VisitList()
        {
            return View();
        }

        public ActionResult UsrFeedback()
        {
            return View();
        }

        public ActionResult WeixinFeedBack()
        {
            return View();
        }

        public ActionResult VisitFigureOut(int type = 1)
        {
            ViewBag.DataType = type;
            ViewBag.BgDate = DateTime.Now.AddDays(-6).ToShortDateString();
            ViewBag.EdDate = DateTime.Now.ToShortDateString();
            return View();
        }
        public ActionResult CaseAnalyze()
        {
            return View();
        }
        #region 日常任务

        public string TaskList(int pageIndex, int status, int startlevel = 0, int endlevel = 0, string source = "")
        {
            var list = Controls.PlatformVisit.TaskInfo.GetList(pageIndex, 15, status, startlevel, endlevel, source);
            return CommonLib.Helper.JsonSerializeObject(list, "yyyy-MM-dd HH:mm:ss");
        }

        #endregion

        #region 回访信息

        public string GetVisitList(int pageIndex, int status = -1, int manner = 0, DateTime? starttime = null, DateTime? endtime = null, int accid = 0, string insertname = "", string tag = "", int recordType = 0, string keyword = "")
        {
            var list = Controls.PlatformVisit.VisitInfo.GetList(pageIndex, 15, status, manner, starttime, endtime, accid, insertname, tag, recordType, keyword);

            return CommonLib.Helper.JsonSerializeObject(list, "yyyy-MM-dd HH:mm:ss");
        }

        #endregion

        public string GetVisitInfo(int id)
        {
            var model = Controls.PlatformVisit.VisitInfo.GetParticularModel(id);
            return CommonLib.Helper.JsonSerializeObject(model, "yyyy-MM-dd HH:mm:ss");
        }
        #region 未关闭事件

        public string GetCaseList(int pageIndex, int dataType, int classType, string revisitName, DateTime? statTime = null, DateTime? endTime = null)
        {
            var list = Controls.PlatformVisit.VisitInfo.GetCaseList(pageIndex, dataType, classType, revisitName, statTime, endTime);
            return CommonLib.Helper.JsonSerializeObject(list, "MM-dd HH:mm");
        }


        #endregion

        #region 事件分析
        public string CaseClassAnalyze(int rank, DateTime statTime, DateTime endTime)
        {

            return null;
        }
        #endregion
        #region 用户反馈

        public string GetFeedbackList(int pageIndex, int accId, DateTime start, DateTime end, int visitStatus = -99, int feedType = -99, string content = "")
        {
            return
                CommonLib.Helper.JsonSerializeObject(TaskInfo.GetFeedbackList(pageIndex, accId, start, end, visitStatus, feedType, content));
        }

        public string GetFeedbackSummary(DateTime stDate, DateTime edDate)
        {
            return CommonLib.Helper.JsonSerializeObject(TaskInfo.GetFeedbackSummary(stDate, edDate));
        }
        #endregion

        #region 微信公众平台反馈
        /// <summary>
        /// 调用接口返回数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="accId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public string GetWeixinFeedBack(int pageIndex, string accId, string start, string end)
        {
            ProxyRequestModel.ProxyRequest request = new ProxyRequestModel.ProxyRequest();
            ProxyRequestModel.UserMessage usrMsg = new ProxyRequestModel.UserMessage();

            Dictionary<string, string> dic = new Dictionary<string, string>()
            {
                {"listData",""},
                {"rowCount",""},
                {"maxPage",""}
            };

            usrMsg.pageIndex = pageIndex;
            usrMsg.pageSize = 15;

            if (!string.IsNullOrEmpty(start))
            {
                usrMsg.startTime = Convert.ToDateTime(start);
            }
            if (!string.IsNullOrEmpty(end))
            {
                usrMsg.endTime = Convert.ToDateTime(end);
            }
            if (int.Parse(accId) != 0)
            {
                usrMsg.accId = Convert.ToInt32(accId);
                request.RequestName = "getMessageByAccId";
            }
            else
            {
                usrMsg.accId = 0;
                request.RequestName = "getMessage";
            }

            request.RequestJson = CommonLib.Helper.JsonSerializeObject(usrMsg);
            ProxyRequestModel.WeixinResponseModel response = new ProxyRequestModel.WeixinResponseModel();
            response = AuthMethod.SendRequest(request);

            if (response.Status)
            {
                OpenResponseModel.ResponseMessage responseModel =
                    CommonLib.Helper.JsonDeserializeObject<OpenResponseModel.ResponseMessage>(response.ObjectStr);

                foreach (OpenResponseModel.T_UserMessageModel item in responseModel.userMessage)
                {
                    item.CompanyName = T_AccountBLL.GetSingleCompanyName(item.accId);
                }

                dic["listData"] = CommonLib.Helper.JsonSerializeObject(responseModel.userMessage, "yyyy-MM-dd HH:mm:ss");
                dic["rowCount"] = responseModel.pageInfo.totalCount.ToString();
                dic["maxPage"] = responseModel.pageInfo.pageCount.ToString();

                return CommonLib.Helper.JsonSerializeObject(dic, "yyyy-MM-dd HH:mm:ss");

            }
            else
            {
                return "API接口调用出错！";
            }

        }

        #endregion

        public string GetForumFeedBack(string url, string remark)
        {
            ManageUserModel uM = (ManageUserModel)Session["logUser"];
            string uName = uM.Name;
            return VisitInfo.GetForumFeedBack(url, uName, remark);
        }

        public string GetForumContent(string url)
        {
            return CommonLib.Helper.JsonSerializeObject(VisitInfo.GetForumContent(url));
        }

        /// <summary>
        /// 获取用户问题
        /// </summary>
        /// <param name="accId"></param>
        /// <returns></returns>
        public string GetUserQuestion(int accId)
        {
            var model = Sys_Visit_QuestionBLL.GetUserQuestion(accId);

            if (model == null)
            {
                return "";
            }
            else
            {
                return CommonLib.Helper.JsonSerializeObject(model);
            }

        }

        /// <summary>
        /// 新增客服问题的回复
        /// </summary>
        /// <param name="accId"></param>
        /// <param name="qid"></param>
        /// <param name="reply"></param>
        /// <returns></returns>
        public string AddReply(int accId, int qid, string reply)
        {
            return Sys_Visit_QuestionBLL.AddQuestionReply(accId, qid, reply).ToString();
        }

        /// <summary>
        /// 关闭回访
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public string CloseVisit(int visitId,string reason,int accid)
        {
            ManageUserModel uM = (ManageUserModel)Session["logUser"];
            //return "0";
            return Controls.PlatformVisit.VisitInfo.AddVisitInfo(accid, 1, 505, 553, 759, 5, reason, 1, uM.Name, "", "", visitId, 0).ToString();
        }
    }
}