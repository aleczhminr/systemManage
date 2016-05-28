using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controls.MessageCenter;
using Model;
namespace OperationPlatform.Controllers
{
    /// <summary>
    /// 消息中心
    /// </summary>
    [OperationPlatform.App_Start.LoginAuthentication]
    public class MsgCenterController : Controller
    {
        //
        // GET: /MsgCenter/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PCMessageList()
        {
            return View();
        }
        public ActionResult MobileMessageList()
        {
            return View();
        }
        public ActionResult SendMessage()
        {
            return View();
        }
        /// <summary>
        /// 得到系统消息 
        /// </summary>
        public string GetMessageNotice(int pagesize, int page, int showType)
        {
            return MessageCenterControls.GetMessageNotice(pagesize, page, showType);
        }
        public string GetMessage(int pagesize, int page, string boardcastid="", int accid=0)
        {
            return MessageCenterControls.GetMessage(accid, boardcastid, pagesize, page);
        }

        public string GetMessageAnalysis(int pagesize, int page, int showType)
        {
            return MessageCenterControls.GetMessageAnalysis(pagesize, page, showType);
        }
        public string MobileMessage(string accids, string title, string content,  DateTime? timing = null)
        {
            ManageUserModel uM = (ManageUserModel)Session["logUser"];

            content = Server.UrlDecode(content);

            int operatorId = uM.UserID;
            string operstorName = uM.UserName;
            return MessageCenterControls.PostMobileMessage(accids, title, content, operatorId, operstorName, timing);
        }

        public string MobileGlobal(string title, string content, DateTime? expire, DateTime? timing = null)
        {
            ManageUserModel uM = (ManageUserModel)Session["logUser"];

            content = Server.UrlDecode(content);

            int operatorId = uM.UserID;
            string operstorName = uM.UserName;
            return MessageCenterControls.PostMobileGlobal(uM.PowerSession, title, content, operatorId, operstorName, timing);
        }
        public string GetMobileMessage(int pagesize, int page, int accid = 0, string boardcastid = "")
        {
            return MessageCenterControls.GetMobileMessage(accid, boardcastid, pagesize, page);
        }
        public string GetMobileMessageNotice(int pagesize, int page, int showType)
        {
            return MessageCenterControls.GetMobileMessageNotice(pagesize, page, showType);
        }
        public string GetMobileMessageAnalysis(int pagesize, int page, int showType)
        {
            return MessageCenterControls.GetMobileMessageAnalysis(pagesize, page, showType);
        }
        public string PostMessage(string accids, string title, string content, DateTime? timing=null)
        {
            ManageUserModel uM = (ManageUserModel)Session["logUser"];

            content = Server.UrlDecode(content);

            int operatorId = uM.UserID;
            string operstorName = uM.UserName;
            return MessageCenterControls.PostMessage(accids, title, content, operatorId, operstorName, timing);
        }
        public string PostGlobal(string title, string content, DateTime expire, DateTime? timing=null)
        {
            ManageUserModel uM = (ManageUserModel)Session["logUser"];

            content = Server.UrlDecode(content);

            int operatorId = uM.UserID;
            string operstorName = uM.UserName;
            return MessageCenterControls.PostGlobal(title, content, uM.PowerSession, operatorId, operstorName, timing, expire);
        }
        public string GetStatus(int accid)
        {
            return MessageCenterControls.GetStatus(accid);
        }
        public string GetOnline()
        {
            return MessageCenterControls.GetOnline();
        }
	}
}