using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using Model;
using Utility;
using System.Configuration;
using System.Net;
using System.Runtime.CompilerServices;
using CommonLib;
using Hangfire;
using Newtonsoft.Json;

namespace Controls
{
    public static class MessageSysControl
    {
        public static string AuthCode = "SYSxh75z3erz1sfg7r";
        public static string SecretKey = "UA8HElSgGrx5llgMZeIE0z07ZRQFlXZZ";

        public static string smsUrl = string.Format("http://{0}/api/sms",
            ConfigurationManager.AppSettings["GeneralMessage"].ToString());

        public static string webUrl = string.Format("http://{0}/api/WebMessage",
            ConfigurationManager.AppSettings["GeneralMessage"].ToString());

        public static string mobUrl = string.Format("http://{0}/api/mobmessage",
            ConfigurationManager.AppSettings["GeneralMessage"].ToString());

        public static string emialUrl = string.Format("http://{0}/api/email",
            ConfigurationManager.AppSettings["GeneralMessage"].ToString());

        public static string PreparePushingMsg(string accidSet, int accIdCount, int sourceType, string MobileTitle = "", string PubTitle = "", string EmailTitle = "",
            string SmsContent = "", string MobileContent = "", string PubContent = "", string EmailContent = "", string remark = "", string verif = "", string taskMark = "", string contentType = "", string contentUrl = "")
        {
            MessageSending msgSendingModel = new MessageSending();
            //生成批次Id
            string batchId = System.Guid.NewGuid().ToString();
            string reMsg = "";

            msgSendingModel.BatchId = batchId;
            msgSendingModel.AccIdSet = accidSet;
            msgSendingModel.AccIdCount = accIdCount;
            msgSendingModel.Remark = remark;

            string channelSet = "";

            if (!string.IsNullOrEmpty(SmsContent))
            {
                channelSet += "1,";
                string smsMsg = GenerateBatchByChannel(1, accidSet, accIdCount, sourceType, verif, "", SmsContent,
                    remark, batchId);

                if (smsMsg.IndexOf(batchId) >= 0)
                {
                    reMsg += smsMsg;
                }
            }
            if (!(string.IsNullOrEmpty(MobileTitle) || string.IsNullOrEmpty(MobileContent)))
            {
                channelSet += "3,";
                string mMsg = GenerateBatchByChannel(3, accidSet, accIdCount, sourceType, verif, MobileTitle, MobileContent,
                    remark, batchId, contentType, contentUrl);

                if (mMsg.IndexOf(batchId) >= 0)
                {
                    reMsg += mMsg;
                }
            }
            if (!(string.IsNullOrEmpty(PubTitle) || string.IsNullOrEmpty(PubContent)))
            {
                channelSet += "2,";
                //PubContent = Server.UrlDecode(PubContent);
                string pMsg = GenerateBatchByChannel(2, accidSet, accIdCount, sourceType, verif, PubTitle, PubContent,
                    remark, batchId);

                if (pMsg.IndexOf(batchId) >= 0)
                {
                    reMsg += pMsg;
                }
            }
            if (!(string.IsNullOrEmpty(EmailTitle) || string.IsNullOrEmpty(EmailContent)))
            {
                channelSet += "4,";
                //EmailContent = Server.UrlDecode(EmailContent);
                string eMsg = GenerateBatchByChannel(4, accidSet, accIdCount, sourceType, verif, EmailTitle, EmailContent,
                    remark, batchId);

                if (eMsg.IndexOf(batchId) >= 0)
                {
                    reMsg += eMsg;
                }
            }
            msgSendingModel.ChannelSet = channelSet;
            if (string.IsNullOrEmpty(channelSet))
            {
                reMsg = "没有获取到发送渠道！请联系技术~";
            }
            else
            {
                if (MessageSysBLL.AddMsgSendingInfo(msgSendingModel) < 1)
                {
                    reMsg += "添加消息概览信息失败！";
                }
            }


            if (string.IsNullOrEmpty(reMsg))
            {
                if (string.IsNullOrEmpty(taskMark))
                {
                    return "消息提交成功！";
                }
                else
                {
                    return batchId;
                }
                //return "消息提交成功！";
            }
            else
            {
                return reMsg;
            }

        }

        public static string GenerateBatchByChannel(int channelId, string accIdSet, int accIdCount, int sourceType, string filterVerify, string title, string content, string remark, string batchId, string contentType = "", string contentUrl = "")
        {
            MessageBatch batchModel = new MessageBatch();

            batchModel.AccIdCount = accIdCount;
            batchModel.AccIdSet = accIdSet;
            batchModel.BatchId = batchId;
            batchModel.ChannelId = channelId;
            batchModel.Content = content;
            batchModel.CreateTime = DateTime.Now;
            batchModel.Remark = remark;
            batchModel.Title = title;
            batchModel.ReviewState = 0;
            batchModel.SendingStatus = 0;
            batchModel.SourceType = sourceType;
            batchModel.FilterLogVerify = filterVerify;
            batchModel.ContentType = contentType;
            batchModel.ContentUrl = contentUrl;

            return MessageSysBLL.AddMessageBatch(batchModel);
        }

        public static Dictionary<string, object> GetBatchList(int pageIndex, string stDate, string edDate, string batchId,
            string remark, string content, int sourceType, int channel)
        {
            Dictionary<string, object> dicData = MessageSysBLL.GetBatchList(pageIndex, stDate, edDate, batchId, remark, content, sourceType, channel); ;

            if (dicData != null)
            {
                int rowCount = Convert.ToInt32(dicData["count"]);
                int maxPage = rowCount % 15 == 0 ? rowCount / 15 : (rowCount / 15 + 1);

                dicData["maxPage"] = maxPage;

                List<MessageBatch> list = (List<MessageBatch>)dicData["data"];

                foreach (var item in list)
                {
                    if (item.FeedBackArrive != 0)
                    {
                        item.OpenRatio = (Convert.ToDouble(item.FeedBackOpen) / item.FeedBackArrive * 100).ToString("F2") +
                                         "%";
                    }
                    else
                    {
                        item.OpenRatio = "无数据";
                    }

                    item.OperatorName = Sys_Manage_UserBLL.GetManageUserNameById(item.Operator);
                }
            }

            return dicData;
        }

        public static Dictionary<string, object> GetBatchDetailList(int pageIndex, string batchId, int channelId, int arrived = 0)
        {
            Dictionary<string, object> dicData = MessageSysBLL.GetBatchDetail(pageIndex, batchId, channelId, arrived); ;

            if (dicData != null)
            {
                int rowCount = Convert.ToInt32(dicData["count"]);
                int maxPage = rowCount % 15 == 0 ? rowCount / 15 : (rowCount / 15 + 1);

                dicData["maxPage"] = maxPage;
            }

            return dicData;
        }

        #region 发送接口处理
        /// <summary>
        /// 获取短信发送Model
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public static SmsRequest GetSmsBatch(string batchId)
        {
            List<MessageDetail> smsBatch = MessageSysBLL.GetBatchForSend(batchId, 1);
            SmsRequest smsModel = new SmsRequest();

            List<string> tmpList = new List<string>();

            if (smsBatch != null && smsBatch.Count > 0)
            {
                smsModel.smsContent = smsBatch[0].Content;
                smsModel.smsType = 1;
            }
            else
            {
                return null;
            }

            foreach (var item in smsBatch)
            {
                if (!string.IsNullOrEmpty(item.AccIdNumber))
                {
                    tmpList.Add(item.AccIdNumber);
                }
                else
                {
                    UpdateDetailUnableMark(item.Id);
                }
            }
            if (tmpList.Count <= 0)
            {
                UpdateAllowCount(batchId, 1, 0, "err");

                tmpList.Add("000");

            }

            smsModel.phoneList = tmpList.ToArray();
            return smsModel;
        }

        /// <summary>
        ///  获取站内信推送Model
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public static WebMessageRequest GetWebBatch(string batchId)
        {
            List<MessageDetail> webBatch = MessageSysBLL.GetBatchForSend(batchId, 2);
            WebMessageRequest webModel = new WebMessageRequest();

            string accidList = "";

            if (webBatch != null && webBatch.Count > 0)
            {
                webModel.msgContent = webBatch[0].Content;
                webModel.msgTitle = webBatch[0].Title;
            }
            else
            {
                return null;
            }

            foreach (var item in webBatch)
            {
                accidList += item.AccId.ToString() + ",";
            }
            accidList = accidList.Substring(0, accidList.LastIndexOf(','));
            webModel.accIdList = accidList;

            return webModel;
        }

        /// <summary>
        /// 获取移动端推送Model
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public static MobMessageRequest GetAppBatch(string batchId)
        {
            List<MessageDetail> appBatch = MessageSysBLL.GetBatchForSend(batchId, 3);
            MobMessageRequest appModel = new MobMessageRequest();

            string accidList = "";

            if (appBatch != null && appBatch.Count > 0)
            {
                appModel.msgContent = appBatch[0].Content;
                appModel.msgTitle = appBatch[0].Title;
                appModel.ContentType = appBatch[0].ContentType;
                appModel.ContentUrl = appBatch[0].ContentUrl;
            }
            else
            {
                return null;
            }

            foreach (var item in appBatch)
            {
                accidList += item.AccId.ToString() + ",";
            }
            accidList = accidList.Substring(0, accidList.LastIndexOf(','));
            appModel.accIdList = accidList;

            return appModel;
        }

        /// <summary>
        /// 获取邮件发送Model
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public static EmailRequest GetEmailBatch(string batchId)
        {
            List<MessageDetail> emailBatch = MessageSysBLL.GetBatchForSend(batchId, 4);
            EmailRequest emailModel = new EmailRequest();

            List<string> tmpList = new List<string>();

            if (emailBatch != null && emailBatch.Count > 0)
            {
                emailModel.EmailChannel = 2;
                emailModel.EmailContent = emailBatch[0].Content;
                emailModel.TemplateIndex = 4;
                emailModel.SendType = 8;
                emailModel.Remark = emailBatch[0].Remark;
                emailModel.EmailTitle = emailBatch[0].Title;
            }
            else
            {
                return null;
            }

            foreach (var item in emailBatch)
            {
                if (!string.IsNullOrEmpty(item.AccIdNumber))
                {
                    tmpList.Add(item.AccIdNumber);
                }
                else
                {
                    UpdateDetailUnableMark(item.Id);
                }
            }

            if (tmpList.Count <= 0)
            {
                UpdateAllowCount(batchId, 4, 0, "err");

                tmpList.Add("000");

            }

            emailModel.AddressList = tmpList.ToArray();

            return emailModel;
        }

        /// <summary>
        /// 发送短信方法
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public static string SendingSms(string batchId)
        {
            Dictionary<string, string> formData = new Dictionary<string, string>();

            SmsRequest request = GetSmsBatch(batchId);
            //string requestJson = CommonLib.Helper.JsonSerializeObject(GetSmsBatch(batchId));          
            int count = request.phoneList.Length;
            string phoneList = "";
            if (request.phoneList.Length > 0 && request.phoneList[0] != "000")
            {
                foreach (var str in request.phoneList)
                {
                    phoneList += str + ",";
                }

                phoneList = phoneList.Substring(0, phoneList.LastIndexOf(','));
            }
            else
            {
                return "1";
            }

            Dictionary<string, string> parameters = CreateHeader();

            //formData["requestJson"] = requestJson;
            formData["smsContent"] = request.smsContent;
            formData["phoneList"] = phoneList;
            formData["smsType"] = request.smsType.ToString();

            string postStr = CommonLib.Helper.SendHttpPost(smsUrl, formData, parameters);

            try
            {
                ResponseModel resModel = CommonLib.Helper.JsonDeserializeObject<ResponseModel>(postStr);
                if (resModel.Status == 0)
                {
                    UpdateAllowCount(batchId, 1, count, resModel.Data.ToString());
                }
                return resModel.Status.ToString();
            }
            catch (Exception ex)
            {
                Logger.Error("转换接口返回数据出错~", ex);
                return "1";
            }

            //return postStr;
        }

        /// <summary>
        /// 发送站内信方法
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public static string SendingWebNotify(string batchId, int operId, string operName)
        {
            //GetWebBatch
            Dictionary<string, string> formData = new Dictionary<string, string>();
            WebMessageRequest request = GetWebBatch(batchId);

            request.operatorId = operId;
            request.operatorName = operName;
            //string requestJson = CommonLib.Helper.JsonSerializeObject(GetEmailBatch(batchId));
            int count = GetBatchCount(batchId, 2);

            Dictionary<string, string> parameters = CreateHeader();

            //formData["requestJson"] = requestJson;
            formData["accIdList"] = request.accIdList;
            formData["msgTitle"] = request.msgTitle;
            formData["msgContent"] = request.msgContent;
            formData["operatorId"] = request.operatorId.ToString();
            formData["operatorName"] = request.operatorName;

            string postStr = CommonLib.Helper.SendHttpPost(webUrl, formData, parameters);
            try
            {
                ResponseModel resModel = CommonLib.Helper.JsonDeserializeObject<ResponseModel>(postStr);
                if (resModel.Status == 0)
                {
                    UpdateAllowCount(batchId, 2, count, resModel.Data.ToString());
                }
                return resModel.Status.ToString();
            }
            catch (Exception ex)
            {
                Logger.Error("转换接口返回数据出错~", ex);
                return "1";
            }
        }

        /// <summary>
        /// 发送移动端消息方法
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public static string SendingAppNotify(string batchId, int operId, string operName)
        {
            Dictionary<string, string> formData = new Dictionary<string, string>();
            MobMessageRequest request = GetAppBatch(batchId);

            request.operatorId = operId;
            request.operatorName = operName;
            //string requestJson = CommonLib.Helper.JsonSerializeObject(GetEmailBatch(batchId));
            int count = GetBatchCount(batchId, 3);

            Dictionary<string, string> parameters = CreateHeader();

            //formData["requestJson"] = requestJson;
            formData["accIdList"] = request.accIdList;
            formData["msgTitle"] = request.msgTitle;
            formData["msgContent"] = request.msgContent;
            formData["operatorId"] = request.operatorId.ToString();
            formData["operatorName"] = request.operatorName;

            formData["contentType"] = request.ContentType;
            formData["contentUrl"] = request.ContentUrl;

            string postStr = CommonLib.Helper.SendHttpPost(mobUrl, formData, parameters);
            Logger.Info(postStr);
            try
            {
                ResponseModel resModel = CommonLib.Helper.JsonDeserializeObject<ResponseModel>(postStr);
                if (resModel.Status == 0)
                {
                    UpdateAllowCount(batchId, 3, count, resModel.Data.ToString());
                }
                return resModel.Status.ToString();
            }
            catch (Exception ex)
            {
                Logger.Error("转换接口返回数据出错~", ex);
                return "1";
            }
        }

        /// <summary>
        /// 发送邮件方法
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public static string SendingEmail(string batchId)
        {
            Dictionary<string, string> formData = new Dictionary<string, string>();
            //Logger.Debug("开始获取");
            EmailRequest request = GetEmailBatch(batchId);
            //Logger.Debug("获取成功处理");
            //string requestJson = CommonLib.Helper.JsonSerializeObject(GetEmailBatch(batchId));
            int count = request.AddressList.Length;
            string emailList = "";
            try
            {

                if (request.AddressList.Length > 0 && request.AddressList[0] != "000")
                {
                    foreach (var str in request.AddressList)
                    {
                        emailList += str + ",";
                    }

                    emailList = emailList.Substring(0, emailList.LastIndexOf(','));
                }
                else
                {
                    return "1";
                }
            }
            catch (Exception ex)
            {
                Logger.Error("处理字符串出错", ex);

            }

            Dictionary<string, string> parameters = CreateHeader();

            //Logger.Debug("得到邮件列表");
            //formData["requestJson"] = requestJson;
            formData["EmailTitle"] = request.EmailTitle;
            formData["EmailContent"] = request.EmailContent;
            formData["Remark"] = request.Remark;
            formData["EmailChannel"] = request.EmailChannel.ToString();
            formData["TemplateIndex"] = request.TemplateIndex.ToString();
            formData["SendType"] = request.SendType.ToString();
            formData["AddressList"] = emailList;
            //Logger.Debug("向接口请求" + string.Format("http://{0}/api/email", ConfigurationManager.AppSettings["GeneralMessage"].ToString()));
            string postStr = CommonLib.Helper.SendHttpPost(emialUrl, formData, parameters);
            try
            {

                ResponseModel resModel = CommonLib.Helper.JsonDeserializeObject<ResponseModel>(postStr);
                //Logger.Debug("得到返回值");
                if (resModel.Status == 0)
                {
                    UpdateAllowCount(batchId, 4, count, resModel.Data.ToString());
                }
                return resModel.Status.ToString();
            }
            catch (Exception ex)
            {
                Logger.Error("转换接口返回数据出错~", ex);
                return "1";
            }
        }

        /// <summary>
        /// 更新批次表的发送数和发送表关联ID
        /// </summary>
        /// <param name="batchId"></param>
        /// <param name="channelId"></param>
        /// <param name="count"></param>
        /// <param name="sendId"></param>
        /// <returns></returns>
        public static int UpdateAllowCount(string batchId, int channelId, int count, string sendId)
        {
            return MessageSysBLL.UpdateAllowCount(batchId, channelId, count, sendId);
        }

        /// <summary>
        /// 更新没有联系方式用户的标记
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateDetailUnableMark(int id)
        {
            return MessageSysBLL.UpdateDetailUnableMark(id);
        }

        public static int GetBatchCount(string batchId, int channelId)
        {
            return MessageSysBLL.GetBatchCount(batchId, channelId);
        }

        #endregion

        /// <summary>
        /// 更新发送后状态
        /// </summary>
        /// <param name="batchId"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public static int UpadateSendingStatus(string batchId, int channelId, string sendId)
        {
            string postStr = "";
            ////获取批次信息
            //MessageBatch batchModel = MessageSysBLL.GetSingleBatchInfo(batchId, channelId);

            ////获取批次详情
            //List<MessageDetail> detailList=MessageSysBLL.GetBatchForSend(batchId, channelId);

            //从接口获取发送状态信息
            string urlAddress = ConfigurationManager.AppSettings["GeneralMessage"].ToString();

            Dictionary<string, string> parameters = CreateHeader();

            try
            {
                switch (channelId)
                {
                    case 1:
                        postStr = CommonLib.Helper.SendHttpGet(smsUrl + "/" + sendId, null, parameters);
                        break;
                    case 2:
                        postStr = CommonLib.Helper.SendHttpGet(webUrl + "/" + sendId, null, parameters);
                        break;
                    case 3:
                        postStr = CommonLib.Helper.SendHttpGet(mobUrl + "/" + sendId, null, parameters);
                        break;
                    case 4:
                        postStr = CommonLib.Helper.SendHttpGet(webUrl + "/" + sendId, null, parameters);
                        break;
                }


                SendStatus resModel = CommonLib.Helper.JsonDeserializeObject<SendStatus>(postStr);
                //Logger.Debug("得到返回值");

                int reVal = MessageSysBLL.UpdateDetailSendingStatus(batchId, channelId, resModel);
                if (reVal == 0)
                {
                    return MessageSysBLL.UpdateBatchSendingStatus(batchId, channelId);
                }
                else
                {
                    return reVal;
                }
                //return 
            }
            catch (Exception ex)
            {
                Logger.Error("转换接口返回数据出错~", ex);
                return 0;
            }


            //更新批次详情
            //更新批次信息

        }

        public static int UpdateOperator(int operId, string batchId, int channelId)
        {
            return MessageSysBLL.UpdateOperator(operId, batchId, channelId);
        }

        /// <summary>
        /// 生成验证信息
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> CreateHeader()
        {
            var requestMd = new HeaderModel { AppKey = AuthCode, Timestamp = Helper.GetTimeStamp(), Nonce = Helper.GetRandomNum() };

            var strSign = new StringBuilder();

            strSign.Append(AuthCode);
            strSign.Append(requestMd.Timestamp);
            strSign.Append(requestMd.Nonce);
            strSign.Append(SecretKey);
            requestMd.Signature = Helper.Md5Hash(strSign.ToString());

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Signature", requestMd.Signature);
            parameters.Add("Timestamp", requestMd.Timestamp);
            parameters.Add("Nonce", requestMd.Nonce);
            parameters.Add("AppKey", requestMd.AppKey);

            return parameters;
        }

        public static Dictionary<string, object> GetBatchSummaryInfo(int pageIndex, string stDate, string edDate, string batchId,
            string remark, int sourceType, int channel)
        {
            Dictionary<string, object> dicData = MessageSysBLL.GetBatchSummaryInfo(pageIndex, stDate, edDate, batchId, remark, sourceType, channel);

            if (dicData != null)
            {
                int rowCount = Convert.ToInt32(dicData["count"]);
                int maxPage = rowCount % 15 == 0 ? rowCount / 15 : (rowCount / 15 + 1);

                dicData["maxPage"] = maxPage;
            }

            return dicData;
        }

        #region 独立发送方法

        /// <summary>
        /// 发送消息通用方法
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        //public static ResponseModel SendMessageGeneric(List<GenericMessageModel> modelList)
        //{
        //    Dictionary<string, string> dicData = new Dictionary<string, string>();
        //    ResponseModel resModel = new ResponseModel();

        //    foreach (var model in modelList)
        //    {
        //        switch (model.ChannelId)
        //        {
        //            case 1:

        //                break;
        //            case 2:
        //                break;
        //            case 3:
        //                break;
        //            case 4:
        //                break;
        //        }
        //    }



        //}

        #endregion

        #region 自动生成任务批次并执行发送操作 此方法为循环任务所调用的方法
        /// <summary>
        /// 调用HangFire后台处理
        /// </summary>
        /// <param name="settingId"></param>
        /// <param name="sendingType"></param>
        /// <param name="timeStr"></param>
        public static void SetTask(int settingId, int sendingType, string timeStr)
        {
            switch (sendingType)
            {
                case 1:
                    BackgroundJob.Schedule(
                        () => ExcuteTimerTask(settingId),
                            TimeSpan.FromMinutes(Convert.ToInt32(timeStr)));
                    break;
                case 2:
                    RecurringJob.AddOrUpdate(
                        () => ExcuteRecurringTask(settingId),
                            timeStr, TimeZoneInfo.Local);
                    break;
            }
        }

        #region 设置更新消息阅读状态的循环方法
        public static int SetUpdateMethod()
        {
            try
            {
                RecurringJob.AddOrUpdate(
                        () => UpdateMsgStatus(),
                            "50 23 1/1 * *", TimeZoneInfo.Local);

                return 1;
            }
            catch (Exception ex)
            {
                Logger.Error("设置定时刷新任务失败！", ex);
                return 0;
            }

        }

        public static void UpdateMsgStatus()
        {
            Logger.Debug("开始每日定时刷新消息列表任务");
            //获取当日发送的消息批次、渠道和SendId
            List<RecurringUpdateModel> list = MessageSysBLL.GetUpdateModel();
            if (list == null)
            {
                Logger.Debug("获取待更新状态的消息列表为Null，结束刷新任务");
            }
            else
            {
                foreach (var item in list)
                {
                    try
                    {
                        //遍历当日发送的数据并更新状态
                        UpadateSendingStatus(item.BatchId, item.ChannelId, item.SendId);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("更新消息" + item.BatchId + "," + item.ChannelId + "," + "," + item.SendId + "时出错！", ex);
                        continue;
                    }

                }
            }


        }

        #endregion

        /// <summary>
        /// 后台HangFire调用定时任务
        /// </summary>
        /// <param name="settingId"></param>        
        public static void ExcuteTimerTask(int settingId)
        {
            //从后台任务列表获取到需要发送的记录
            ConditionSettingModel.SettingModel model = ConditionSettingBLL.GetSendingTaskItem(settingId);

            string batchId = "";
            if (string.IsNullOrEmpty(model.LatestBatchId))
            {
                //从后台设置的任务生成消息批次
                batchId = PreparePushingMsg(model.AccIdSet, model.AccIdCount, 3, model.MobileTitle, model.PubTitle,
                    model.EmailTitle,
                    model.SmsContent, model.MobileContent, model.PubContent, model.EmailContent,
                    model.Remark + "(" + model.SendingDate + "定时发送)", model.Verification, "1");

                //更新任务的批次Id防止重复提交
                ConditionSettingBLL.SetBatchId(settingId, batchId);
            }
            else
            {
                batchId = model.LatestBatchId;
            }

            string uName = Sys_Manage_UserBLL.GetManageUserNameById(model.Operator);

            SendMsgAuto(batchId, model.Operator, uName);
        }

        /// <summary>
        /// 后台HangFire调用循环任务
        /// </summary>
        /// <param name="settingId"></param>
        public static void ExcuteRecurringTask(int settingId)
        {
            //从后台任务列表获取到需要发送的记录
            ConditionSettingModel.SettingModel model = ConditionSettingBLL.GetSendingTaskItem(settingId);

            //获取循环任务的Sql
            List<string> sqlSet =
                CommonLib.Helper.JsonDeserializeObject<List<string>>(ConditionSettingBLL.GetSqlJsonById(settingId));

            //拼装Sql组的字典
            Dictionary<int, List<int>> dicData = new Dictionary<int, List<int>>();

            for (int i = 0; i < sqlSet.Count; i++)
            {
                dicData[i] = RuleListBLL.GetAccIdByStr(sqlSet[i]);
            }

            #region 处理产生的集合并合并

            if (dicData.Count > 0)
            {
                List<int> temp = dicData.First().Value;
                foreach (var item in dicData)
                {
                    temp = temp.Intersect(item.Value).ToList();
                }

                if (temp.Count > 0)
                {
                    string accIdSet = CommonLib.Helper.JsonSerializeObject(temp);
                    int accIdCount = temp.Count;

                    //从后台设置的任务生成消息批次
                    string batchId = PreparePushingMsg(accIdSet, accIdCount, 4, model.MobileTitle, model.PubTitle,
                        model.EmailTitle,
                        model.SmsContent, model.MobileContent, model.PubContent, model.EmailContent,
                        model.Remark + "(" + model.SendingDate + "循环定时发送)", model.Verification, "1");

                    //更新任务的批次Id防止重复提交
                    ConditionSettingBLL.SetBatchId(settingId, batchId);

                    string uName = Sys_Manage_UserBLL.GetManageUserNameById(model.Operator);

                    SendMsgAuto(batchId, model.Operator, uName);
                }
            }

            #endregion



        }

        /// <summary>
        /// 返回页面有修改时间的条件组重新生成的Sql
        /// 在SqlSet字段直接插入拼装的Sql字段Json
        /// </summary>
        /// <param name="modelStr"></param>
        /// <returns></returns>
        public static string RegenerateSql(string modelStr, string verifId)
        {
            string sqlSet = "";
            List<string> sqlList = new List<string>();

            //如果页面带有时间条件的条件组不为空，新增该组的条件
            if (!string.IsNullOrEmpty(modelStr))
            {
                List<FilterCondition> list = CommonLib.Helper.JsonDeserializeObject<List<FilterCondition>>(modelStr);

                foreach (var item in list)
                {
                    item.TableName = RuleListBLL.GetTableNameById(item.Id);
                }

                #region 根据新的时间条件生成Sql组方法
                //穷举涉及的表名
                List<string> tabNameList = new List<string>()
                {
                    "i200.dbo.T_Account",
                    "i200.dbo.T_OrderInfo",
                    "i200.dbo.T_Business",
                    "i200.dbo.T_LOG",
                    "sys_i200.dbo.Sys_TagNexus",
                    "sys_i200.dbo.SysRpt_ShopInfo"
                };

                foreach (var str in tabNameList)
                {
                    List<FilterCondition> tempConditionList = list.FindAll(x => x.TableName == str);

                    if (tempConditionList != null && tempConditionList.Count > 0)
                    {
                        //accIdDic[str] = new List<int>();
                        StringBuilder strTemp = new StringBuilder();
                        switch (str)
                        {
                            case "i200.dbo.T_Account":
                                strTemp.Append("select ID from " + str + " where state=1 and ");
                                break;
                            case "i200.dbo.T_OrderInfo":
                                strTemp.Append("select accId from " + str + " where orderstatus=2 and ");
                                break;
                            case "i200.dbo.T_Business":
                            case "sys_i200.dbo.SysRpt_ShopInfo":
                                strTemp.Append("select accountid from " + str + " where ");
                                break;
                            case "sys_i200.dbo.Sys_TagNexus":
                                strTemp.Append("select acc_id from " + str + " where ");
                                break;
                            case "i200.dbo.T_LOG":
                                strTemp.Append("select accountid from i200.dbo.T_Log lt left join i200.dbo.T_Token_Api ta on lt.accountid=ta.accId where ");
                                break;
                            default:
                                break;
                        }

                        //strTemp.Append(" and ");
                        //针对单表拼装SQL
                        #region 拼接SQL语句

                        foreach (var conditionItem in tempConditionList)
                        {
                            switch (conditionItem.ConditionType)
                            {
                                case "StrPair":
                                    if (!string.IsNullOrEmpty(conditionItem.DataRange.Min.ToString()) && conditionItem.DataRange.Min.ToString() != "null")
                                    {
                                        strTemp.Append(" (" + conditionItem.ColName + " >= '" +
                                                   conditionItem.DataRange.Min + "') and ");
                                    }
                                    if (!string.IsNullOrEmpty(conditionItem.DataRange.Max.ToString()) && conditionItem.DataRange.Max.ToString() != "null")
                                    {
                                        if (conditionItem.ConditionType == "TimePair")
                                        {
                                            DateTime dt = Convert.ToDateTime(conditionItem.DataRange.Max);

                                            strTemp.Append(" (" + conditionItem.ColName + " <= '" + dt.AddDays(1) +
                                                       "') and ");
                                        }
                                        else
                                        {
                                            strTemp.Append(" (" + conditionItem.ColName + " <= '" + conditionItem.DataRange.Max +
                                                       "') and ");
                                        }

                                    }
                                    break;
                                case "TimePair":
                                    strTemp.Append(" (DATEDIFF(day," + conditionItem.ColName + ",getdate())=" +
                                                   conditionItem.DiffVal + ") and");
                                    break;
                                case "IntPair":
                                    if (conditionItem.DataRange.Min.ToString() != "null")
                                    {
                                        strTemp.Append(" (" + conditionItem.ColName + " >= " +
                                                   Convert.ToInt32(conditionItem.DataRange.Min) +
                                                   ") and ");

                                    }
                                    if (conditionItem.DataRange.Max.ToString() != "null")
                                    {
                                        strTemp.Append(" (" + conditionItem.ColName + " <= " +
                                                   Convert.ToInt32(conditionItem.DataRange.Max) +
                                                   ") and ");

                                    }
                                    //strTemp.Append(" (" + conditionItem.ColName + " between " +
                                    //               Convert.ToInt32(conditionItem.DataRange.Max) + " and " +
                                    //               Convert.ToInt32(conditionItem.DataRange.Min) +
                                    //               ") and ");
                                    break;
                                case "IntRange":
                                    string intList = "";

                                    //处理移动端分端登录的相关信息(移动端分端需要重构)
                                    if (conditionItem.ColName == "LogMode")
                                    {
                                        string mobileStr = "";

                                        foreach (var intItem in conditionItem.DataRange.Range)
                                        {
                                            int label = Convert.ToInt32(intItem);
                                            switch (label)
                                            {
                                                case 1:
                                                case 0:
                                                case 3:
                                                    intList += intItem + ",";
                                                    break;
                                                //移动端登录的特殊处理
                                                case 4:
                                                    mobileStr += " ta.AppKey='iPhoneHT5I0O4HDN65' or ";
                                                    intList += "4,";
                                                    break;
                                                case 5:
                                                    mobileStr += " ta.AppKey='iPadMaO8VUvVH0eBss' or ";
                                                    intList += "4,";
                                                    break;
                                                case 6:
                                                    mobileStr += " ta.AppKey='AndroidYnHWyROQosO' or ";
                                                    intList += "4,";
                                                    break;
                                                default:
                                                    break;
                                            }

                                        }

                                        if (!string.IsNullOrEmpty(intList))
                                        {
                                            intList = intList.Substring(0, intList.LastIndexOf(','));

                                            strTemp.Append(" (lt.LogMode in (" + intList + ")) and ");
                                        }
                                        if (!string.IsNullOrEmpty(mobileStr))
                                        {
                                            mobileStr = mobileStr.Substring(0, mobileStr.LastIndexOf('o'));

                                            strTemp.Append(" (" + mobileStr + ") and ");
                                        }

                                    }
                                    else
                                    {
                                        foreach (var intItem in conditionItem.DataRange.Range)
                                        {
                                            intList += intItem + ",";
                                        }
                                        intList = intList.Substring(0, intList.LastIndexOf(','));

                                        strTemp.Append(" (" + conditionItem.ColName + " in (" + intList + ")) and ");
                                    }

                                    break;
                                case "StrRange":
                                    string strList = "";
                                    foreach (var intItem in conditionItem.DataRange.Range)
                                    {
                                        strList += "'" + intItem + "',";
                                    }
                                    intList = strList.Substring(0, strList.LastIndexOf(','));

                                    strTemp.Append(" (" + conditionItem.ColName + " in (" + intList + ")) and ");
                                    break;
                                default:
                                    break;
                            }

                        }
                        #endregion

                        string sql = "";

                        if (str == "i200.dbo.T_LOG")
                        {
                            sql = strTemp.ToString().Substring(0, strTemp.ToString().LastIndexOf('a')) +
                                  " group by accountid";
                        }
                        else
                        {
                            sql = strTemp.ToString().Substring(0, strTemp.ToString().LastIndexOf('a'));
                        }

                        sqlList.Add(sql);

                    }

                }
                #endregion
            }

            if (RuleListBLL.GetOriginSql(verifId) != null)
            {
                sqlList.AddRange(RuleListBLL.GetOriginSql(verifId));
                sqlSet = CommonLib.Helper.JsonSerializeObject(sqlList);

                return sqlSet;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 按渠道自动发送消息
        /// </summary>
        /// <param name="batchId"></param>
        /// <param name="uid"></param>
        /// <param name="uName"></param>
        public static string SendMsgAuto(string batchId, int uid, string uName)
        {
            List<int> channelList = MessageSysBLL.GetBatchChannel(batchId);

            foreach (var item in channelList)
            {
                UpdateOperator(uid, batchId, item);

                switch (item)
                {
                    case 1:
                        return SendingSms(batchId);
                    //break;
                    case 2:
                        SendingWebNotify(batchId, uid, uName);
                        break;
                    case 3:
                        SendingAppNotify(batchId, uid, uName);
                        break;
                    case 4:
                        //Logger.Debug("发送");
                        SendingEmail(batchId);
                        break;
                    default:
                        break;
                }
            }

            return "1";
            //Logger.Error();
        }
        #endregion

        /// <summary>
        /// 根据服务Id推送内容
        /// </summary>
        /// <param name="id"></param>
        public static void MsgManageByService(int id)
        {
            //批次Id列表
            List<string> batchIdList = new List<string>();
            //用于生成同样内容的公告分批字典
            Dictionary<string, List<string>> accParts = new Dictionary<string, List<string>>();
            //用于自定义内容的单店消息对象列表
            List<SingleShopInfoForMsg> singleList = new List<SingleShopInfoForMsg>();

            //推送备注
            string remark = "";

            switch (id)
            {
                case 1://经营分析报表推送（批量用户）
                    remark = DateTime.Now.Date.ToShortDateString() + "-" + "经营分析日报";
                    #region 群发共享型消息测试逻辑
                    //test
                    //List<string> test = new List<string>();
                    //test.Add(397.ToString());
                    //test.Add(79672.ToString());
                    //foreach (var part in accParts)
                    //{
                    //    batchIdList.Add(PreparePushingMsg(JsonConvert.SerializeObject(part.Value), part.Value.Count, 4, "您" + DateTime.Now.Date.ToShortDateString() + "日的经营日报已经出炉，点击查看详情！", "", "",
                    //                                    "", "您" + DateTime.Now.Date.ToShortDateString() + "日的经营日报已经出炉，点击下方按钮看看自己昨天的经营效率吧！", "", "",
                    //                                    part.Key, "", "service", "view", "dailyReport"));
                    //}
                    #endregion
                    accParts = GetConditionUser(remark, id);

                    foreach (var part in accParts)
                    {
                        batchIdList.Add(PreparePushingMsg(CommonLib.Helper.JsonSerializeObject(part.Value), 1, 4, "您昨天的经营日报已经出炉，点击查看详情！", "", "",
                                                        "", "您" + DateTime.Now.Date.AddDays(-1).ToShortDateString() + "日的经营日报已经出炉，点击下方按钮看看自己昨天的经营效率吧！", "", "",
                                                        part.Key, "", "service", "view", "dailyReport"));
                    }

                    break;
                case 2://付费到期提醒推送（单个用户）
                    //不做Remark特殊处理
                    singleList = MessageSysBLL.GetSingleUserMsgBatch(id);
                    
                    break;
                case 3://优惠券到期提醒（单个用户）
                    singleList = MessageSysBLL.GetSingleUserMsgBatch(id);

                    //不做Remark特殊处理
                    break;
                case 4://周年答谢（批量用户）
                    remark = DateTime.Now.Date.ToShortDateString() + "-" + "周年答谢";

                    singleList = MessageSysBLL.GetSingleUserMsgBatch(id);

                    break;
                default:
                    break;
            }                       

            foreach (var str in batchIdList)
            {
                MessageSysControl.SendMsgAuto(str, 0, "系统");
            }

        }

        /// <summary>
        /// 获取对应条件的用户
        /// 群发消息用以切分消息批次
        /// 限定500条为一批
        /// </summary>
        /// <param name="remark"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Dictionary<string, List<string>> GetConditionUser(string remark, int id)
        {
            Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();

            List<string> userList = MessageSysBLL.GetPushUserList(id);

            //在结果集中添加两个测试账号
            if (!userList.Contains("79672"))
            {
                userList.Add("79672");//田震
            }
            if (!userList.Contains("397"))
            {
                userList.Add("397");//田震
            }

            int j = 500;
            for (int i = 0; i < userList.Count; i += 500)
            {
                List<string> cList = new List<string>();
                cList = userList.Take(j).Skip(i).ToList();
                j += 500;
                dic.Add(remark + "_" + (i / 500 + 1).ToString(), cList);
            }

            return dic;
        }


        /// <summary>
        /// 回访短信
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="smsContent"></param>
        /// <param name="uid"></param>
        /// <param name="uName"></param>
        /// <returns></returns>
        public static string SendVisitSms(int accid, string smsContent, int uid, string uName)
        {
            List<string> list = new List<string>();
            list.Add(accid.ToString());

            try
            {
                if (!smsContent.Contains("【生意专家】"))
                {
                    smsContent = smsContent + "【生意专家】";
                }

                string batchId = PreparePushingMsg(CommonLib.Helper.JsonSerializeObject(list), 1, 1,
                "", "", "",
                smsContent, "", "", "",
                "用户回访短信", "", "visit", "", "");

                if (SendMsgAuto(batchId, uid, uName) != "1")
                    return "1";
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                Logger.Error("添加用户回访短信出错！", ex);
                return "0";
            }

        }

    }
}
