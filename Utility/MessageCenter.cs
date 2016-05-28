using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using RestSharp;
using System.Net; 
using System.Threading;
namespace Utility
{
    public static class MessageCenter
    {
        private static string AppKey = "sys_mtvlt191ismndx99ksht";
        private static string Secret = "j4qspxy3itammjazaql4d4rsth4lh5mi";
        private static string MsgHost = System.Configuration.ConfigurationManager.AppSettings["MessageCenter"];

        private delegate void PostMobileMessageDelegate(string accIdList, string msgTitle, string msgContent, int operatorId, string operatorName, DateTime? timingTime);

        #region SendMessage 消息发送

        #region PostMessage 消息发送(Obj)
        /// <summary>
        /// 消息发送
        /// </summary>
        /// <param name="message"></param>
        public static MsgItem PostMessage(MsgModel message)
        {
            var model = new MsgItem();

            var authDict = GetAuth();

            var client = new RestClient(MsgHost);
            var request = new RestRequest("api/notice", Method.POST);
            request.AddHeader("appkey", authDict["appkey"]);
            request.AddHeader("timestamp", authDict["timestamp"]);
            request.AddHeader("nonce", authDict["nonce"]);
            request.AddHeader("signature", authDict["signature"]);

            request.AddParameter("accIdList", message.accIdList);
            request.AddParameter("fromAccId", message.fromAccId);
            request.AddParameter("priority", message.priority);
            request.AddParameter("msgType", message.msgType);
            request.AddParameter("msgClass", message.msgClass);
            request.AddParameter("msgTitle", message.msgTitle);
            request.AddParameter("msgContent", message.msgContent);
            request.AddParameter("operatorId", message.operatorId);
            request.AddParameter("operatorName", message.operatorName);

            if (message.timingTime != null)
            {
                request.AddParameter("timingTime", message.timingTime);
            }

            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = response.Content;
                //model = JsonConvert.DeserializeObject<MsgItem>(content);
                if (content == "ok")
                {
                    model.Status = true;
                }
                else
                {
                    model.Status = false;
                }
            }
            else
            {
                model.Status = false;
            }

            return model;
        }
        #endregion

        #region PostMessage 消息发送(客服)
        /// <summary>
        /// 消息发送
        /// </summary>
        /// <param name="accIdList">店铺Id(逗号分隔)</param>
        /// <param name="msgTitle">消息标题</param>
        /// <param name="msgContent">消息内容</param>
        /// <param name="timingTime">定时发送日期</param>
        /// <param name="operatorId">操作Id</param>
        /// <param name="operatorName">操作人员</param>
        /// <returns></returns>
        public static int PostMessage(string accIdList, string msgTitle, string msgContent, int operatorId, string operatorName, DateTime? timingTime = null)
        {
            int code = -1;
            var model = new MsgModel();
            model.accIdList = accIdList;
            model.fromAccId = 0;
            model.priority = 1;
            model.msgType = 1;
            model.msgClass = 2;
            model.msgTitle = msgTitle;
            model.msgContent = msgContent;
            model.operatorId = operatorId;
            model.operatorName = operatorName;
            model.timingTime = timingTime;

            var oResult = PostMessage(model);
            if (oResult.Status)
            {
                code = 1;
            }

            return code;
        }
        #endregion

        #region PostMessage 消息发送(系统)
        /// <summary>
        /// 消息发送
        /// </summary>
        /// <param name="accIdList">店铺Id(逗号分隔)</param>
        /// <param name="msgTitle">消息标题</param>
        /// <param name="msgContent">消息内容</param>
        /// <param name="timingTime">定时发送日期</param>
        /// <returns></returns>
        public static int PostMessage(string accIdList, string msgTitle, string msgContent, DateTime? timingTime = null)
        {
            int code = -1;
            var model = new MsgModel();
            model.accIdList = accIdList;
            model.fromAccId = 0;
            model.priority = 1;
            model.msgType = 1;
            model.msgClass = 2;
            model.msgTitle = msgTitle;
            model.msgContent = msgContent;
            model.operatorId = 0;
            model.operatorName = "系统";
            model.timingTime = timingTime;

            var oResult = PostMessage(model);
            if (oResult.Status)
            {
                code = 1;
            }

            return code;
        }
        #endregion

        #region PostMessage 消息发送(API)
        /// <summary>
        /// 消息发送(API)
        /// </summary>
        /// <param name="accIdList">店铺Id(逗号分隔)</param>
        /// <param name="msgTitle">消息标题</param>
        /// <param name="msgContent">消息内容</param>
        /// <param name="msgClass">消息分类</param>
        /// <param name="timingTime">定时发送日期</param>
        /// <returns></returns>
        public static int PostMessage(string accIdList, string msgTitle, string msgContent, int msgClass, DateTime? timingTime = null)
        {
            int code = -1;
            var model = new MsgModel();
            model.accIdList = accIdList;
            model.fromAccId = 0;
            model.priority = 1;
            model.msgType = 3;
            model.msgClass = msgClass;
            model.msgTitle = msgTitle;
            model.msgContent = msgContent;
            model.operatorId = 0;
            model.operatorName = "系统";
            model.timingTime = timingTime;

            var oResult = PostMessage(model);
            if (oResult.Status)
            {
                code = 1;
            }

            return code;
        }
        #endregion

        #endregion

        #region PostGlobal 全局公告发送(obj)
        /// <summary>
        /// 全局公告发送
        /// </summary>
        /// <param name="message"></param>
        public static MsgItem PostGlobal(MsgModel message)
        {
            var model = new MsgItem();

            var authDict = GetAuth();

            var client = new RestClient(MsgHost);
            var request = new RestRequest("api/global", Method.POST);
            request.AddHeader("appkey", authDict["appkey"]);
            request.AddHeader("timestamp", authDict["timestamp"]);
            request.AddHeader("nonce", authDict["nonce"]);
            request.AddHeader("signature", authDict["signature"]);

            request.AddParameter("fromAccId", message.fromAccId);
            request.AddParameter("priority", message.priority);
            request.AddParameter("msgType", message.msgType);
            request.AddParameter("msgClass", message.msgClass);
            request.AddParameter("msgTitle", message.msgTitle);
            request.AddParameter("msgContent", message.msgContent);
            request.AddParameter("operatorId", message.operatorId);
            request.AddParameter("operatorName", message.operatorName);
            request.AddParameter("expireTime", message.expireTime);
            request.AddParameter("global", "1");

            if (message.timingTime != null)
            {
                request.AddParameter("timingTime", message.timingTime);
            }

            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = response.Content;
                //model = JsonConvert.DeserializeObject<MsgItem>(content);
                if (content == "ok")
                {
                    model.Status = true;
                }
                else
                {
                    model.Status = false;
                }
            }
            else
            {
                model.Status = false;
            }

            return model;
        }
        #endregion

        #region PostGlobal 全局公告发送(客服)
        /// <summary>
        /// 全局公告发送
        /// </summary>
        /// <param name="msgTitle">消息标题</param>
        /// <param name="msgContent">消息内容</param>
        /// <param name="operatorId">操作人员Id</param>
        /// <param name="operatorName">操作人员</param>
        /// <param name="expireTime">过期时间</param>
        /// <param name="timingTime">定时日期</param>
        /// <returns></returns>
        public static int PostGlobal(string msgTitle, string msgContent, int operatorId, string operatorName, DateTime expireTime, DateTime? timingTime = null)
        {
            int code = -1;
            var model = new MsgModel();
            model.fromAccId = 0;
            model.priority = 1;
            model.msgType = 1;
            model.msgClass = 3;
            model.msgTitle = msgTitle;
            model.msgContent = msgContent;
            model.operatorId = operatorId;
            model.operatorName = operatorName;
            model.timingTime = timingTime;
            model.expireTime = expireTime;

            var oResult = PostGlobal(model);
            if (oResult.Status)
            {
                code = 1;
            }

            return code;
        }
        #endregion


        #region GetStatus 检测店铺在线状态
        /// <summary>
        /// 检测店铺在线状态
        /// </summary>
        /// <param name="message"></param>
        public static string GetStatus(int accId)
        {
            var strResult = "-1";

            var authDict = GetAuth();

            var client = new RestClient(MsgHost);
            var request = new RestRequest("api/status", Method.GET);
            request.AddHeader("appkey", authDict["appkey"]);
            request.AddHeader("timestamp", authDict["timestamp"]);
            request.AddHeader("nonce", authDict["nonce"]);
            request.AddHeader("signature", authDict["signature"]);

            request.AddParameter("accid", accId.ToString().Trim());

            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                strResult = response.Content;
            }

            return strResult;
        }
        #endregion

        #region GetOnline 当前在线统计
        /// <summary>
        /// 当前在线统计
        /// </summary>
        /// <param name="message"></param>
        public static OnlineModel GetOnline()
        {
            var model = new OnlineModel();

            var authDict = GetAuth();

            var client = new RestClient(MsgHost);
            var request = new RestRequest("api/online", Method.GET);
            request.AddHeader("appkey", authDict["appkey"]);
            request.AddHeader("timestamp", authDict["timestamp"]);
            request.AddHeader("nonce", authDict["nonce"]);
            request.AddHeader("signature", authDict["signature"]);

            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                model = CommonLib.Helper.JsonDeserializeObject<OnlineModel>(response.Content);
            }

            return model;
        }
        #endregion

        #region GetMessage 店铺消息列表
        /// <summary>
        /// 店铺消息列表
        /// </summary>
        /// <param name="accId">店铺Id</param>
        /// <param name="boardCastId">消息Id</param>
        /// <param name="pageSize"></param>
        /// <param name="nowPage"></param>
        /// <returns></returns>
        public static MessageModel GetMessage(int accId, string boardCastId, int pageSize, int nowPage)
        {
            var model = new MessageModel();

            var authDict = GetAuth();

            var client = new RestClient(MsgHost);
            var request = new RestRequest("api/message", Method.GET);
            request.AddHeader("appkey", authDict["appkey"]);
            request.AddHeader("timestamp", authDict["timestamp"]);
            request.AddHeader("nonce", authDict["nonce"]);
            request.AddHeader("signature", authDict["signature"]);

            if (accId > 0)
            {
                request.AddParameter("accid", accId.ToString().Trim());
            }
            if (boardCastId != "")
            {
                request.AddParameter("boardcastid", boardCastId.ToString().Trim());
            }
            request.AddParameter("pagesize", pageSize.ToString().Trim());
            request.AddParameter("nowpage", nowPage.ToString().Trim());

            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Content != "error")
                {
                    model = CommonLib.Helper.JsonDeserializeObject<MessageModel>(response.Content);
                }
            }

            return model;
        }
        #endregion

        #region GetMessageEx 用于更新店铺消息列表的消息获取
        /// <summary>
        /// 店铺消息列表
        /// </summary>
        /// <param name="accId">店铺Id</param>
        /// <param name="boardCastId">消息Id</param>
        /// <param name="pageSize"></param>
        /// <param name="nowPage"></param>
        /// <returns></returns>
        public static MessageModel GetMessageEx(int accId, string boardCastId, DateTime bgDate,DateTime edDate)
        {
            var model = new MessageModel();

            var authDict = GetAuth();

            var client = new RestClient(MsgHost);
            var request = new RestRequest("api/message", Method.GET);
            request.AddHeader("appkey", authDict["appkey"]);
            request.AddHeader("timestamp", authDict["timestamp"]);
            request.AddHeader("nonce", authDict["nonce"]);
            request.AddHeader("signature", authDict["signature"]);

            if (accId > 0)
            {
                request.AddParameter("accid", accId.ToString().Trim());
            }
            if (boardCastId != "")
            {
                request.AddParameter("boardcastid", boardCastId.ToString().Trim());
            }
            request.AddParameter("bgdate", bgDate);
            request.AddParameter("eddate", edDate);

            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Content != "error")
                {
                    model = CommonLib.Helper.JsonDeserializeObject<MessageModel>(response.Content);
                }
            }

            return model;
        }
        #endregion

        #region GetMessageNotice 系统消息列表
        /// <summary>
        /// 系统消息列表
        /// </summary>
        /// <param name="pageSize">分页</param>
        /// <param name="nowPage">当前页</param>
        /// <param name="msgShowType">消息类型 1-普通消息 2-公告消息 3-API消息</param>
        /// <returns></returns>
        public static MessageNoticeModel GetMessageNotice(int pageSize, int nowPage, int msgShowType)
        {
            var model = new MessageNoticeModel();

            var authDict = GetAuth();

            var client = new RestClient(MsgHost);
            var request = new RestRequest("api/messageNotice", Method.GET);
            request.AddHeader("appkey", authDict["appkey"]);
            request.AddHeader("timestamp", authDict["timestamp"]);
            request.AddHeader("nonce", authDict["nonce"]);
            request.AddHeader("signature", authDict["signature"]);

            request.AddParameter("pagesize", pageSize.ToString().Trim());
            request.AddParameter("nowpage", nowPage.ToString().Trim());
            request.AddParameter("msgShowType", msgShowType.ToString().Trim());

            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Content != "error")
                {
                    model = CommonLib.Helper.JsonDeserializeObject<MessageNoticeModel>(response.Content);
                }
            }

            return model;
        }
        #endregion

        #region GetMessageAnalysis 获得简单统计信息
        /// <summary>
        /// 获得简单统计信息
        /// </summary>
        /// <param name="pageSize">分页</param>
        /// <param name="nowPage">当前页</param>
        /// <param name="msgShowType">消息类型 1-普通消息 2-公告消息 3-API消息</param>
        /// <returns></returns>
        public static MessageAnalysis GetMessageAnalysis(int pageSize, int nowPage, int msgShowType)
        {
            var model = new MessageAnalysis();

            var authDict = GetAuth();

            var client = new RestClient(MsgHost);
            var request = new RestRequest("api/messageAnalysis", Method.GET);
            request.AddHeader("appkey", authDict["appkey"]);
            request.AddHeader("timestamp", authDict["timestamp"]);
            request.AddHeader("nonce", authDict["nonce"]);
            request.AddHeader("signature", authDict["signature"]);

            request.AddParameter("pagesize", pageSize.ToString().Trim());
            request.AddParameter("nowpage", nowPage.ToString().Trim());
            request.AddParameter("msgShowType", msgShowType.ToString().Trim());

            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Content != "error")
                {
                    model = CommonLib.Helper.JsonDeserializeObject<MessageAnalysis>(response.Content);
                }
            }

            return model;
        }
        #endregion


        #region PostMobile 移动端消息推送
        /// <summary>
        /// 手机端消息推送(Obj)
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static MsgItem PostMobileMessage(MobileMsgModel message)
        {
            var model = new MsgItem();

            var authDict = GetAuth();

            var client = new RestClient(MsgHost);
            var request = new RestRequest("push/single", Method.POST);
            request.AddHeader("appkey", authDict["appkey"]);
            request.AddHeader("timestamp", authDict["timestamp"]);
            request.AddHeader("nonce", authDict["nonce"]);
            request.AddHeader("signature", authDict["signature"]);

            request.AddParameter("toAccId", message.accIdList);
            request.AddParameter("fromAccId", message.fromAccId);
            request.AddParameter("priority", message.priority);
            request.AddParameter("msgType", message.msgType);
            request.AddParameter("msgClass", message.msgClass);
            request.AddParameter("msgTitle", message.msgTitle);
            request.AddParameter("msgContent", message.msgContent);
            request.AddParameter("operatorId", message.operatorId);
            request.AddParameter("operatorName", message.operatorName);
            request.AddParameter("contentType", message.contentType);
            request.AddParameter("openType", message.openType);

            if (message.timingTime != null)
            {
                request.AddParameter("timingTime", message.timingTime);
            }

            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = response.Content;
                //model = JsonConvert.DeserializeObject<MsgItem>(content);
                if (content == "ok")
                {
                    model.Status = true;
                }
                else
                {
                    model.Status = false;
                }
            }
            else
            {
                model.Status = false;
            }

            return model;
        }



        /// <summary>
        /// 手机端消息推送
        /// </summary>
        /// <param name="accIdList">店铺Id</param>
        /// <param name="msgTitle">消息标题</param>
        /// <param name="msgContent">消息内容</param>
        /// <param name="timingTime">定时发送日期</param>
        /// <param name="operatorId">操作Id</param>
        /// <param name="operatorName">操作人员</param>
        /// <returns></returns>
        public static int PostMobileMessage(string accIdList, string msgTitle, string msgContent, int operatorId, string operatorName, DateTime? timingTime = null)
        {
            int code = -1;

            if (accIdList.IndexOf(',') > -1)
            {

                var tokenDelegate = new PostMobileMessageDelegate(PostMobileMessageByAccidList);
                tokenDelegate.BeginInvoke(accIdList, msgTitle, msgContent, operatorId, operatorName, timingTime, null, null);
                code = 1;
            }
            else
            {
                code = PostMobileMessageByAccid(Convert.ToInt32(accIdList), msgTitle, msgContent, operatorId, operatorName, timingTime);
            }

            return code;
        }

        /// <summary>
        /// 手机端消息推送
        /// </summary>
        /// <param name="accIdList">店铺Id</param>
        /// <param name="msgTitle">消息标题</param>
        /// <param name="msgContent">消息内容</param>
        /// <param name="timingTime">定时发送日期</param>
        /// <param name="operatorId">操作Id</param>
        /// <param name="operatorName">操作人员</param>
        /// <returns></returns>
        public static void PostMobileMessageByAccidList(string accIdList, string msgTitle, string msgContent, int operatorId, string operatorName, DateTime? timingTime = null)
        {
            int code = -1;

            List<int> accids = new List<int>();
            foreach (string accid in accIdList.Split(','))
            {
                int accitem = 0;
                if (int.TryParse(accid, out accitem))
                {
                    accids.Add(accitem);
                }
            }

            foreach (int accid in accids)
            {
                code = PostMobileMessageByAccid(accid, msgTitle, msgContent, operatorId, operatorName, timingTime);
                Thread.Sleep(100);
            }
        }


        /// <summary>
        /// 手机端消息推送
        /// </summary>
        /// <param name="accIdList">店铺Id</param>
        /// <param name="msgTitle">消息标题</param>
        /// <param name="msgContent">消息内容</param>
        /// <param name="timingTime">定时发送日期</param>
        /// <param name="operatorId">操作Id</param>
        /// <param name="operatorName">操作人员</param>
        /// <returns></returns>
        public static int PostMobileMessageByAccid(int accid, string msgTitle, string msgContent, int operatorId, string operatorName, DateTime? timingTime = null)
        {
            int code = -1;
            var model = new MobileMsgModel();
            model.accIdList = accid.ToString();
            model.fromAccId = 0;
            model.priority = 1;
            model.msgType = 1;
            model.msgClass = 2;
            model.msgTitle = msgTitle;
            model.msgContent = msgContent;
            model.operatorId = operatorId;
            model.operatorName = operatorName;
            model.timingTime = timingTime;
            model.contentType = "text";
            model.openType = 2;

            var oResult = PostMobileMessage(model);
            if (oResult.Status)
            {
                code = 1;
            }

            return code;
        }


        /// <summary>
        /// 手机端公告发送
        /// </summary>
        /// <param name="message"></param>
        public static MsgItem PostMobileGlobal(MobileMsgModel message)
        {
            var model = new MsgItem();

            var authDict = GetAuth();

            var client = new RestClient(MsgHost);
            var request = new RestRequest("push/all", Method.POST);
            request.AddHeader("appkey", authDict["appkey"]);
            request.AddHeader("timestamp", authDict["timestamp"]);
            request.AddHeader("nonce", authDict["nonce"]);
            request.AddHeader("signature", authDict["signature"]);

            request.AddParameter("fromAccId", message.fromAccId);
            request.AddParameter("priority", message.priority);
            request.AddParameter("msgType", message.msgType);
            request.AddParameter("msgClass", message.msgClass);
            request.AddParameter("msgTitle", message.msgTitle);
            request.AddParameter("msgContent", message.msgContent);
            request.AddParameter("operatorId", message.operatorId);
            request.AddParameter("operatorName", message.operatorName);
            request.AddParameter("expireTime", message.expireTime);
            request.AddParameter("contentType", message.contentType);
            request.AddParameter("openType", message.openType);

            if (message.timingTime != null)
            {
                request.AddParameter("timingTime", message.timingTime);
            }

            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = response.Content;
                //model = JsonConvert.DeserializeObject<MsgItem>(content);
                if (content == "ok")
                {
                    model.Status = true;
                }
                else
                {
                    model.Status = false;
                }
            }
            else
            {
                model.Status = false;
            }

            return model;
        }

        /// <summary>
        /// 全局公告发送
        /// </summary>
        /// <param name="msgTitle">消息标题</param>
        /// <param name="msgContent">消息内容</param>
        /// <param name="operatorId">操作人员Id</param>
        /// <param name="operatorName">操作人员</param>
        /// <param name="expireTime">过期时间</param>
        /// <param name="timingTime">定时日期</param>
        /// <returns></returns>
        public static int PostMobileGlobal(string msgTitle, string msgContent, int operatorId, string operatorName, DateTime expireTime, DateTime? timingTime = null)
        {
            int code = -1;
            var model = new MobileMsgModel();
            model.fromAccId = 0;
            model.priority = 1;
            model.msgType = 1;
            model.msgClass = 3;
            model.msgTitle = msgTitle;
            model.msgContent = msgContent;
            model.operatorId = operatorId;
            model.operatorName = operatorName;
            model.timingTime = timingTime;
            model.expireTime = expireTime;
            model.contentType = "text";
            model.openType = 2;

            var oResult = PostMobileGlobal(model);
            if (oResult.Status)
            {
                code = 1;
            }

            return code;
        }
        #endregion

        #region GetMobileMessage 店铺移动端消息列表
        /// <summary>
        /// 店铺移动端消息列表
        /// </summary>
        /// <param name="accId">店铺Id</param>
        /// <param name="boardCastId">消息Id</param>
        /// <param name="pageSize"></param>
        /// <param name="nowPage"></param>
        /// <returns></returns>
        public static MessageModel GetMobileMessage(int accId, string boardCastId, int pageSize, int nowPage)
        {
            var model = new MessageModel();

            var authDict = GetAuth();

            var client = new RestClient(MsgHost);
            var request = new RestRequest("api/mobileMessage", Method.GET);
            request.AddHeader("appkey", authDict["appkey"]);
            request.AddHeader("timestamp", authDict["timestamp"]);
            request.AddHeader("nonce", authDict["nonce"]);
            request.AddHeader("signature", authDict["signature"]);

            if (accId > 0)
            {
                request.AddParameter("accid", accId.ToString().Trim());
            }
            if (boardCastId != "")
            {
                request.AddParameter("boardcastid", boardCastId.ToString().Trim());
            }
            request.AddParameter("pagesize", pageSize.ToString().Trim());
            request.AddParameter("nowpage", nowPage.ToString().Trim());

            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Content != "error")
                {
                    model = CommonLib.Helper.JsonDeserializeObject<MessageModel>(response.Content);
                }
            }

            return model;
        }
        #endregion

        #region GetMobileMessageEx 用于消息更新的店铺移动端消息列表
        /// <summary>
        /// 店铺移动端消息列表
        /// </summary>
        /// <param name="accId">店铺Id</param>
        /// <param name="boardCastId">消息Id</param>
        /// <param name="pageSize"></param>
        /// <param name="nowPage"></param>
        /// <returns></returns>
        public static MessageModel GetMobileMessageEx(int accId, string boardCastId, DateTime bgDate,DateTime edDate)
        {
            var model = new MessageModel();

            var authDict = GetAuth();

            var client = new RestClient(MsgHost);
            var request = new RestRequest("api/mobileMessage", Method.GET);
            request.AddHeader("appkey", authDict["appkey"]);
            request.AddHeader("timestamp", authDict["timestamp"]);
            request.AddHeader("nonce", authDict["nonce"]);
            request.AddHeader("signature", authDict["signature"]);

            if (accId > 0)
            {
                request.AddParameter("accid", accId.ToString().Trim());
            }
            if (boardCastId != "")
            {
                request.AddParameter("boardcastid", boardCastId.ToString().Trim());
            }
            request.AddParameter("bgdate", bgDate);
            request.AddParameter("eddate", edDate);

            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Content != "error")
                {
                    model = CommonLib.Helper.JsonDeserializeObject<MessageModel>(response.Content);
                }
            }

            return model;
        }
        #endregion

        #region GetMobileMessageNotice 手机端系统消息列表
        /// <summary>
        /// 手机端系统消息列表
        /// </summary>
        /// <param name="pageSize">分页</param>
        /// <param name="nowPage">当前页</param>
        /// <param name="msgShowType">消息类型 1-普通消息 2-公告消息 3-API消息</param>
        /// <returns></returns>
        public static MessageNoticeModel GetMobileMessageNotice(int pageSize, int nowPage, int msgShowType)
        {
            var model = new MessageNoticeModel();

            var authDict = GetAuth();

            var client = new RestClient(MsgHost);
            var request = new RestRequest("api/mobileMessageNotice", Method.GET);
            request.AddHeader("appkey", authDict["appkey"]);
            request.AddHeader("timestamp", authDict["timestamp"]);
            request.AddHeader("nonce", authDict["nonce"]);
            request.AddHeader("signature", authDict["signature"]);

            request.AddParameter("pagesize", pageSize.ToString().Trim());
            request.AddParameter("nowpage", nowPage.ToString().Trim());
            request.AddParameter("msgShowType", msgShowType.ToString().Trim());

            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Content != "error")
                {
                    model = CommonLib.Helper.JsonDeserializeObject<MessageNoticeModel>(response.Content);
                }
            }

            return model;
        }
        #endregion

        #region GetMobileMessageAnalysis 获得手机端简单统计信息
        /// <summary>
        /// 获得手机端简单统计信息
        /// </summary>
        /// <param name="pageSize">分页</param>
        /// <param name="nowPage">当前页</param>
        /// <param name="msgShowType">消息类型 1-普通消息 2-公告消息 3-API消息</param>
        /// <returns></returns>
        public static MessageAnalysis GetMobileMessageAnalysis(int pageSize, int nowPage, int msgShowType)
        {
            var model = new MessageAnalysis();

            var authDict = GetAuth();

            var client = new RestClient(MsgHost);
            var request = new RestRequest("api/mobileMessageAnalysis", Method.GET);
            request.AddHeader("appkey", authDict["appkey"]);
            request.AddHeader("timestamp", authDict["timestamp"]);
            request.AddHeader("nonce", authDict["nonce"]);
            request.AddHeader("signature", authDict["signature"]);

            request.AddParameter("pagesize", pageSize.ToString().Trim());
            request.AddParameter("nowpage", nowPage.ToString().Trim());
            request.AddParameter("msgShowType", msgShowType.ToString().Trim());

            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Content != "error")
                {
                    model = CommonLib.Helper.JsonDeserializeObject<MessageAnalysis>(response.Content);
                }
            }

            return model;
        }
        #endregion


        #region GetAuth 获得Auth验证头
        /// <summary>
        /// 获得Auth验证头
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, string> GetAuth()
        {
            var authDict = new Dictionary<string, string>();

            var timestamp = GetTimestamp();
            var nonce = GetRandomNum();
            var strSign = new StringBuilder();
            strSign.Append(AppKey);
            strSign.Append(timestamp);
            strSign.Append(nonce);
            strSign.Append(Secret);
            var signature = GetMd5(strSign.ToString());

            authDict.Add("appkey", AppKey);
            authDict.Add("timestamp", timestamp);
            authDict.Add("nonce", nonce);
            authDict.Add("signature", signature);

            return authDict;
        }
        #endregion

        #region GetTimestamp 获得当前时间戳
        /// <summary>
        /// 获得当前时间戳
        /// </summary>
        /// <returns></returns>
        private static string GetTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        #endregion

        #region GetRandomNum 获得随机数
        /// <summary>
        /// 获得随机数
        /// </summary>
        /// <returns></returns>
        private static string GetRandomNum()
        {
            Random r = new Random(DateTime.Now.Millisecond);
            return r.Next(100000, 999999).ToString();
        }
        #endregion

        #region GetMd5 获取字符串MD5哈希
        /// <summary>
        /// 获取字符串MD5哈希
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        private static string GetMd5(string strInput)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(strInput));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        #endregion
    }

    #region MessageModel
    public class MsgItem
    {
        public bool Status { get; set; }
        public string _id { get; set; }
        public int toAccId { get; set; }
        public int priority { get; set; }
        public int msgType { get; set; }
        /// <summary>
        /// 消息类别
        /// <para>
        /// {2：客服消息，3：公告信息，11：新注册模板消息，12：新年红包，13：初一到初三}
        /// </para>
        /// </summary>
        public int msgClass { get; set; }
        public string msgTitle { get; set; }
        public string msgContent { get; set; }
        public string msgUrl { get; set; }
        public string msgFlag { get; set; }
        public string msgPic { get; set; }
        public int global { get; set; }
    }

    public class MsgModel : MsgItem
    {
        public string accIdList { get; set; }
        public int fromAccId { get; set; }
        public DateTime createTime { get; set; }
        public DateTime? timingTime { get; set; }
        public DateTime pushTime { get; set; }
        public int isPush { get; set; }
        public int isRead { get; set; }
        public DateTime readTime { get; set; }
        public string boardCastId { get; set; }
        public int operatorId { get; set; }
        public string operatorName { get; set; }
        public DateTime expireTime { get; set; }
        /// <summary>
        /// 移动端推送类型
        /// </summary>
        public string contentType { get; set; }
        /// <summary>
        /// 移动端推送url
        /// </summary>
        public string contentUrl { get; set; }
    }

    public class MessageModel
    {
        public int sumCount { get; set; }
        public int readCount { get; set; }
        public List<MsgModel> msgList { get; set; }
    }


    public class MessageNotice : MsgModel
    {
        public object toAccId { get; set; }
    }

    public class MessageNoticeModel
    {
        public int sumCount { get; set; }
        public int readCount { get; set; }
        public List<MessageNotice> msgList { get; set; }
    }

    public class OnlineModel
    {
        /// <summary>
        /// 在线店铺数量
        /// </summary>
        public int StoreNum { get; set; }

        /// <summary>
        /// 在线用户数量
        /// </summary>
        public int UserNum { get; set; }
    }

    public class MobileMsgModel : MsgModel
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public string contentType { get; set; }

        /// <summary>
        /// 打开方式 1-打开APP 2-查看消息 3-打开Url
        /// </summary>
        public int openType { get; set; }
    }

    #region MessageAnalysis Model
    public class MessageAnalysis
    {
        public int sumCount { get; set; }
        public int readCount { get; set; }
        public List<AnalysisItem> msgList { get; set; }
    }

    public class AnalysisItem
    {
        public AnalysisMsg _id { get; set; }
        public int SumPush { get; set; }
        public int SumRead { get; set; }
        public int msgCount { get; set; }
        public DateTime msgDate { get; set; }
    }

    public class AnalysisMsg
    {
        public string msgTitle { get; set; }
        public int msgClass { get; set; }
        public int msgType { get; set; }
    }
    #endregion

    #endregion
}
