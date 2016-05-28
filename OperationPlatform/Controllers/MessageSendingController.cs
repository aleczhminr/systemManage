using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Controls;
using Controls.HangFire;
using Controls.MessageTemplate;
using Model;
using Utility;

namespace OperationPlatform.Controllers
{
    public class MessageSendingController : Controller
    {
        // GET: MessageSending
        public ActionResult Index(string verif = "", int count = 0)
        {
            if (verif != "" && count != 0)
            {
                ViewBag.Verification = verif;

                string verification = verif;
                ManageUserModel uM = (ManageUserModel)Session["logUser"];
                int uid = uM.UserID;

                string UidList = Controls.Filtrate.Filtrate.GetAccountList(uid, verification);

                string activeList = UidList.Substring(UidList.IndexOf(',') + 1);//去除首个0
                if (!string.IsNullOrEmpty(activeList))
                {
                    ViewBag.List = activeList;

                    #region 初始化条件组

                    int activeCount = activeList.Split(',').Length;
                    //ViewBag.ConditionId = ConditionHandler.InitialConditionRecord(uid, activeList, activeCount, verif);

                    #endregion
                }
                else
                {
                    ViewBag.List = "";
                }

                #region Abandon
                //string TopList = UidList.Substring(0, index);

                //int starting = 0;
                //int finish = 0;
                //int size = 0;
                //string nowStr = "";
                //string itemList = "";

                //while (starting > -1)
                //{
                //    starting = UidList.IndexOf(',', finish);
                //    if (starting > -1)
                //    {
                //        finish = UidList.IndexOf(',', starting + 1);
                //        if (finish > 0)
                //        {
                //            nowStr = UidList.Substring(starting + 1, finish - starting - 1);
                //        }
                //        else
                //        {
                //            nowStr = UidList.Substring(starting + 1);
                //            finish = starting + nowStr.Length + 1;
                //        }
                //        if (nowStr != null && nowStr != "")
                //        {
                //            if (size > 0)
                //            {
                //                itemList += ",";
                //            }
                //            itemList += "" + nowStr;
                //            size++;
                //        }
                //    }
                //    else
                //    {
                //        break;
                //    }
                //}
                //List<dynamic> ds = Controls.Filtrate.Filtrate.GetSummarizingData(itemList);    
                #endregion

            }
            else
            {
                ViewBag.List = "";
            }

            return View();
        }

        public ActionResult MessageRuleList()
        {
            return View();
        }

        public string GetShopSummary(string originList)
        {
            string detailList = "";
            string sumStr = "";

            List<string> list = new List<string>(originList.Split(','));

            int strCount = 0;
            int count = list.Count;

            foreach (string str in list)
            {
                try
                {
                    detailList += T_AccountBLL.GetCompanyName(Convert.ToInt32(str)) + "[" + Convert.ToInt32(str) + "]<br/>";


                    if (strCount < 5)
                    {
                        sumStr += T_AccountBLL.GetCompanyName(Convert.ToInt32(str)) + ",";
                    }
                    strCount++;
                }
                catch (Exception ex)
                {
                    return "没有找到有关【" + str + "】这个Id相关的信息！";
                }

            }

            Dictionary<string, string> dic = new Dictionary<string, string>()
            {
                {"list",""},
                {"count",""},
                {"strsum",""},
                {"detail",""}
            };

            dic["list"] = CommonLib.Helper.JsonSerializeObject(list);
            dic["count"] = count.ToString();
            dic["detail"] = detailList;
            dic["strsum"] = sumStr.Substring(0, sumStr.LastIndexOf(','));


            return CommonLib.Helper.JsonSerializeObject(dic);
        }

        /// <summary>
        /// 提交消息推送请求
        /// </summary>
        /// <param name="accidSet"></param>
        /// <param name="SmsTitle"></param>
        /// <param name="MobileTitle"></param>
        /// <param name="PubTitle"></param>
        /// <param name="EmailTitle"></param>
        /// <param name="SmsContent"></param>
        /// <param name="MobileContent"></param>
        /// <param name="PubContent"></param>
        /// <param name="EmailContent"></param>
        /// <param name="remark"></param>
        /// <param name="verif"></param>
        /// <returns></returns>
        public string SubmitPushingRequest(string accidSet, int accIdCount, string MobileTitle = "", string PubTitle = "", string EmailTitle = "",
            string SmsContent = "", string MobileContent = "", string PubContent = "", string EmailContent = "", string remark = "", string verif = "", string contentType = "", string contentUrl = "")
        {

            #region 初始化条件组（来源不是筛选器的时候）

            //if (string.IsNullOrEmpty(verif))
            //{
            //    ManageUserModel uM = (ManageUserModel)Session["logUser"];
            //    int uid = uM.UserID;

            //    //ConditionHandler.InitialConditionRecord(uid,accidSet,accIdCount, "");
            //}
            #endregion

            if (!string.IsNullOrEmpty(PubContent))
            {
                PubContent = Server.UrlDecode(PubContent);
            }
            if (!string.IsNullOrEmpty(EmailContent))
            {
                EmailContent = Server.UrlDecode(EmailContent);
            }
            if (!string.IsNullOrEmpty(MobileContent))
            {
                MobileContent = Server.UrlDecode(MobileContent);
            }

            int sourceType = 1;
            if (!string.IsNullOrEmpty(verif))
            {
                sourceType = 2;
            }
            return MessageSysControl.PreparePushingMsg(accidSet, accIdCount, sourceType, MobileTitle, PubTitle, EmailTitle, SmsContent, MobileContent, PubContent, EmailContent, remark, verif, "", contentType, contentUrl);
        }

        public string GetBatchList(int pageIndex, string stDate, string edDate, string batchId,
            string remark, string content, int sourceType, int channel)
        {
            return
                CommonLib.Helper.JsonSerializeObject(MessageSysControl.GetBatchList(pageIndex, stDate, edDate, batchId,
                    remark, content, sourceType, channel), "yyyy-MM-dd HH:mm:ss");
        }

        public ActionResult BatchList(string batchId)
        {
            if (!string.IsNullOrEmpty(batchId))
            {
                ViewBag.batch = batchId;
            }

            ManageUserModel uM = (ManageUserModel)Session["logUser"];
            int uid = uM.UserID;

            if (uid == 74 || uid == 83 || uid == 38 || uid == 91 || uid == 68 || uid == 52)
            {
                ViewBag.auth = 1;
            }
            else
            {
                ViewBag.auth = 0;
            }

            return View();
        }

        public ActionResult Detail(string batch, int channel)
        {
            ViewBag.BatchId = batch;
            ViewBag.ChannelId = channel;

            return View();
        }

        public string GetBatchDetail(int pageIndex, string batchId, int channelId, int arrived = 0)
        {
            return
                CommonLib.Helper.JsonSerializeObject(MessageSysControl.GetBatchDetailList(pageIndex, batchId, channelId, arrived), "yyyy-MM-dd HH:mm:ss");
        }

        public string SendMessage(string batchId, int channelId)
        {
            ManageUserModel uM = (ManageUserModel)Session["logUser"];
            int uid = uM.UserID;
            string uName = uM.Name;

            MessageSysControl.UpdateOperator(uid, batchId, channelId);

            switch (channelId)
            {
                case 1:
                    return MessageSysControl.SendingSms(batchId);
                case 2:
                    return MessageSysControl.SendingWebNotify(batchId, uid, uName);
                    break;
                case 3:
                    return MessageSysControl.SendingAppNotify(batchId, uid, uName);
                    break;
                case 4:
                    //Logger.Debug("发送");
                    return MessageSysControl.SendingEmail(batchId);

                default:
                    return "1";
            }

            return "1";
        }

        /// <summary>
        /// 更新发送状态
        /// </summary>
        /// <param name="batchId"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public string RefreshStatus(string batchId, int channelId, string sendId)
        {
            return MessageSysControl.UpadateSendingStatus(batchId, channelId, sendId).ToString();
        }

        public ActionResult BatchSummary()
        {
            return View();
        }

        public string GetBatchSummaryInfo(int pageIndex, string stDate, string edDate, string batchId,
            string remark, string content, int sourceType, int channel)
        {

            var model = MessageSysControl.GetBatchSummaryInfo(pageIndex, stDate, edDate, batchId,
                remark, sourceType, channel);
            return
                CommonLib.Helper.JsonSerializeObject(
                    model, "yyyy-MM-dd HH:mm:ss");
        }

        #region 筛选器和设定发送任务的相关处理
        /// <summary>
        /// 获取消息发送的初始条件
        /// </summary>
        /// <param name="verif"></param>
        /// <returns></returns>
        public string GetShowCondition(string verif)
        {
            return ConditionHandler.RecoverDisplayCondition(verif);
        }

        /// <summary>
        /// 提交定时任务请求
        /// </summary>
        /// <param name="accidSet"></param>
        /// <param name="accIdCount"></param>
        /// <param name="MobileTitle"></param>
        /// <param name="PubTitle"></param>
        /// <param name="EmailTitle"></param>
        /// <param name="SmsContent"></param>
        /// <param name="MobileContent"></param>
        /// <param name="PubContent"></param>
        /// <param name="EmailContent"></param>
        /// <param name="remark"></param>
        /// <param name="verif"></param>
        /// <returns></returns>
        public string AddSpecTimeSendingTask(string accidSet, int accIdCount, DateTime date, string MobileTitle = "", string PubTitle = "", string EmailTitle = "",
            string SmsContent = "", string MobileContent = "", string PubContent = "", string EmailContent = "", string remark = "", string verif = "")
        {
            #region 设定定时的时间处理

            DateTime dt = DateTime.Now;

            if (dt > date)
            {
                return "0";
            }
            TimeSpan ts = date - dt;

            string minuteSpan = ts.Minutes.ToString();

            #endregion

            ConditionSettingModel.SettingModel model = new ConditionSettingModel.SettingModel();

            if (!string.IsNullOrEmpty(PubContent))
            {
                PubContent = Server.UrlDecode(PubContent);
            }
            if (!string.IsNullOrEmpty(EmailContent))
            {
                EmailContent = Server.UrlDecode(EmailContent);
            }
            if (!string.IsNullOrEmpty(MobileContent))
            {
                MobileContent = Server.UrlDecode(MobileContent);
            }

            ManageUserModel uM = (ManageUserModel)Session["logUser"];
            int uid = uM.UserID;
            string uName = uM.Name;

            model.Verification = verif;
            model.Operator = uid;
            model.Remark = remark;
            model.SendingDate = date;
            model.AccIdSet = accidSet;
            model.AccIdCount = accIdCount;

            model.EmailTitle = model.EmailTitle;
            model.EmailContent = EmailContent;
            model.SmsContent = SmsContent;
            model.MobileTitle = MobileTitle;
            model.MobileContent = MobileContent;
            model.PubTitle = PubTitle;
            model.PubContent = PubContent;

            model.SendingType = 1;
            model.SourceType = 3;

            int setId = ConditionHandler.AddTaskSetting(model);

            if (setId == 0)
            {
                return "-1";
            }
            else
            {
                //添加任务成功后更新状态
                ConditionHandler.SetTaskActive(setId);
            }

            MessageSysControl.SetTask(setId, 1, minuteSpan);

            return "1";

        }

        /// <summary>
        /// 提交循环任务请求
        /// </summary>
        /// <param name="modelStr"></param>
        /// <param name="accidSet"></param>
        /// <param name="accIdCount"></param>
        /// <param name="MobileTitle"></param>
        /// <param name="PubTitle"></param>
        /// <param name="EmailTitle"></param>
        /// <param name="SmsContent"></param>
        /// <param name="MobileContent"></param>
        /// <param name="PubContent"></param>
        /// <param name="EmailContent"></param>
        /// <param name="remark"></param>
        /// <param name="verif"></param>
        /// <param name="cronExp"></param>
        /// <returns></returns>
        public string AddRecurringTask(string modelStr, string accidSet, int accIdCount, string MobileTitle = "", string PubTitle = "", string EmailTitle = "",
            string SmsContent = "", string MobileContent = "", string PubContent = "", string EmailContent = "", string remark = "", string verif = "", string cronExp = "", string sendMark = "")
        {
            ConditionSettingModel.SettingModel model = new ConditionSettingModel.SettingModel();

            #region 处理循环筛选条件

            string sqlSet = MessageSysControl.RegenerateSql(modelStr, verif);

            if (string.IsNullOrEmpty(sqlSet))
            {
                return "-1";
            }

            #endregion

            if (!string.IsNullOrEmpty(PubContent))
            {
                PubContent = Server.UrlDecode(PubContent);
            }
            if (!string.IsNullOrEmpty(EmailContent))
            {
                EmailContent = Server.UrlDecode(EmailContent);
            }
            if (!string.IsNullOrEmpty(MobileContent))
            {
                MobileContent = Server.UrlDecode(MobileContent);
            }

            //如果选定本次需要发送则执行提交批次的逻辑
            if (!string.IsNullOrEmpty(sendMark))
            {
                MessageSysControl.PreparePushingMsg(accidSet, accIdCount, 2, MobileTitle, PubTitle, EmailTitle, SmsContent, MobileContent,
                    PubContent, EmailContent, remark, verif, "");
            }


            #region 设定循环的Cron表达式处理
            #endregion

            ManageUserModel uM = (ManageUserModel)Session["logUser"];
            int uid = uM.UserID;
            string uName = uM.Name;

            model.Verification = verif;
            model.Operator = uid;
            model.Remark = remark;
            //model.SendingDate = date;
            model.AccIdSet = accidSet;
            model.AccIdCount = accIdCount;

            model.EmailTitle = model.EmailTitle;
            model.EmailContent = EmailContent;
            model.SmsContent = SmsContent;
            model.MobileTitle = MobileTitle;
            model.MobileContent = MobileContent;
            model.PubTitle = PubTitle;
            model.PubContent = PubContent;

            model.SqlSet = sqlSet;
            model.RecurringSendingCron = cronExp;

            model.SendingType = 2;
            model.SourceType = 4;
            model.SendingDate = DateTime.Now;

            int setId = ConditionHandler.AddTaskSetting(model);

            if (setId == 0)
            {
                return "-1";
            }
            else
            {
                //添加任务成功后更新状态
                ConditionHandler.SetTaskActive(setId);
            }

            //MessageSysControl.ExcuteRecurringTask(setId);
            MessageSysControl.SetTask(setId, 2, cronExp);

            return "1";

        }

        #endregion

        #region 触发类消息相关
        //生成发送模板
        public ActionResult TriggerTemplate()
        {
            return View();
        }

        public string AddTemplate(int eventId, string MobileTitle = "", string PubTitle = "", string EmailTitle = "",
            string SmsContent = "", string MobileContent = "", string PubContent = "", string EmailContent = "", string contentType = "", string contentUrl = "")
        {
            //Model初始化
            TriggerTemplateModel model = new TriggerTemplateModel();
            if (!string.IsNullOrEmpty(SmsContent))
            {
                model.SmsMark = 1;
                model.SmsContent = SmsContent;
            }
            else
            {
                model.SmsMark = 0;
                model.SmsContent = "";
            }

            if (!string.IsNullOrEmpty(MobileContent))
            {
                model.MobileMark = 1;
                model.MobileTitle = MobileTitle;
                model.MobileContent = Server.UrlDecode(MobileContent);
                model.MobileContentType = contentType;
                model.MobileContentUrl = contentUrl;
            }
            else
            {
                model.MobileMark = 0;
                model.MobileTitle = "";
                model.MobileContent = "";
                model.MobileContentType = "";
                model.MobileContentUrl = "";
            }

            if (!string.IsNullOrEmpty(PubContent))
            {
                model.WebMark = 1;
                model.WebTitle = PubTitle;
                model.WebContent = Server.UrlDecode(PubContent);
            }
            else
            {
                model.WebMark = 0;
                model.WebTitle = "";
                model.WebContent = "";
            }

            if (!string.IsNullOrEmpty(EmailContent))
            {
                model.EmailMark = 1;
                model.EmailTitle = EmailTitle;
                model.EmailContent = Server.UrlDecode(EmailContent);
            }
            else
            {
                model.EmailMark = 0;
                model.EmailTitle = "";
                model.EmailContent = "";
            }

            model.CreateTime = DateTime.Now;
            model.EnableStatus = 1;

            //<option value="1">注册后</option>
            //        <option value="2">首次登录后</option>
            //        <option value="3">订单付费后</option>
            //        <option value="4">优惠券到账后</option>
            //        <option value="5">实物订单发货后</option>
            //        <option value="6">反馈转为需求后</option>
            //        <option value="7">需求开发完成后</option>
            //        <option value="8">销售商品后</option>
            switch (eventId)
            {
                case 1://注册后
                    model.MissionTarget = "告知";
                    model.MissionName = "注册通知";
                    model.UserDesc = "";
                    break;
                case 2://首次登录后
                    model.MissionTarget = "告知";
                    model.MissionName = "注册通知";
                    model.UserDesc = "";
                    break;
                case 3://订单付费后
                    model.MissionTarget = "告知";
                    model.MissionName = "付费通知";
                    model.UserDesc = "";
                    break;
                case 4://优惠券到账后
                    model.MissionTarget = "提醒";
                    model.MissionName = "优惠券到账提醒";
                    model.UserDesc = "";
                    break;
                case 5://实物订单发货后
                    model.MissionTarget = "提醒";
                    model.MissionName = "实物发货提醒";
                    model.UserDesc = "";
                    break;
                case 6://反馈转为需求后
                    model.MissionTarget = "提醒";
                    model.MissionName = "用户反馈提醒";
                    model.UserDesc = "";
                    break;
                case 7://需求开发完成后
                    model.MissionTarget = "提醒";
                    model.MissionName = "用户反馈提醒";
                    model.UserDesc = "";
                    break;
                case 8://销售商品后
                    model.MissionTarget = "提醒";
                    model.MissionName = "店员销售提醒";
                    model.UserDesc = "";
                    break;
                case 101://订单购买后
                case 102:
                case 103:
                case 104:
                case 105:
                case 106:
                case 107:
                case 108:
                    model.MissionTarget = "告知";
                    model.MissionName = "付费通知";
                    model.UserDesc = "";
                    break;
            }


            model.EventId = eventId;

            ManageUserModel uM = (ManageUserModel)Session["logUser"];
            model.Operator = uM.UserID;

            return MessageTemplateControl.AddTemplate(model);
        }

        //从模板提取并生成批次发送
        //以接口方式调用
        /// <summary>
        /// 使用模板Id和特定Model填充发送数据
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public int SendNotifyMsg(string requestModel)
        {
            var oModel = CommonLib.Helper.JsonDeserializeObject<EventMessage>(requestModel);
            var model = new MessageRequestModel();
            //var model = CommonLib.Helper.JsonDeserializeObject<MessageRequestModel>(requestModel);

            switch (oModel.EventId)
            {
                case 1:
                    break;
                case 2://销售后                    
                    break;
                case 4://绑定优惠券
                    var cmodel = CommonLib.Helper.JsonDeserializeObject<AfterBindCoupon>(oModel.SpecModel);

                    model.Id = cmodel.EventId;
                    model.EventName = "【优惠券绑定提醒】用户" + cmodel.AccId + "绑定优惠券" + cmodel.CouponDesc;
                    model.SpecModel = oModel.SpecModel;
                    model.AccId = cmodel.AccId;

                    break;
                case 5://实物订单发货时
                    var gmodel = CommonLib.Helper.JsonDeserializeObject<AfterExpress>(oModel.SpecModel);

                    model.Id = gmodel.EventId;
                    model.EventName = "【实物发货提醒】用户" + gmodel.AccId + "购买的" + gmodel.GoodsName + "发货";
                    model.SpecModel = oModel.SpecModel;
                    model.AccId = gmodel.AccId;
                    break;
                case 6://反馈转为需求时
                    var imodel = CommonLib.Helper.JsonDeserializeObject<AfterImportReq>(oModel.SpecModel);

                    model.Id = imodel.EventId;
                    model.EventName = "【反馈转需求提醒】需求" + imodel.RequirementDesc + " -来自用户" + imodel.AccId;
                    model.SpecModel = oModel.SpecModel;
                    model.AccId = imodel.AccId;
                    break;
                case 7://需求开发完成后
                    var iamodel = CommonLib.Helper.JsonDeserializeObject<AfterImportReq>(oModel.SpecModel);

                    model.Id = iamodel.EventId;
                    model.EventName = "【需求完成提醒】需求【" + iamodel.RequirementDesc + "】已完成 -来自用户" + iamodel.AccId;
                    model.SpecModel = oModel.SpecModel;
                    model.AccId = iamodel.AccId;
                    break;
                case 9: //订单支付成功后                    
                    var ordModel = CommonLib.Helper.JsonDeserializeObject<AfterOrderSuccess>(oModel.SpecModel);

                    switch (ordModel.OrderType)
                    {
                        case 1://虚拟商品
                            //对商品类型做例外判断

                            //高级版
                            if (ordModel.BusId == 82)
                            {
                                model.Id = 101;
                            }
                            //手机橱窗
                            else if (ordModel.BusId == 6)
                            {
                                model.Id = 102;
                            }
                            //短信包
                            else if (ordModel.BusId == 36 || ordModel.BusId == 44 || ordModel.BusId == 45 || ordModel.BusId == 81)
                            {
                                model.Id = 103;
                            }
                            //行业版
                            else if (ordModel.BusId>=67 && ordModel.BusId <= 80)
                            {
                                model.Id = 107;                                
                            }
                            //连锁版
                            //else if (ordModel.BusId == 1)
                            //{
                            //    model.Id = 108;
                            //}
                            break;
                        case 2://实物商品
                            model.Id = 104;
                            break;
                        case 3://京东订单
                            model.Id = 105;
                            break;
                        case 4://话费充值
                            model.Id = 106;
                            break;
                    }


                    model.EventName = "【订单购买成功提醒】用户" + ordModel.AccId + "购买【" + ordModel.OrderName + "】成功";
                    model.SpecModel = oModel.SpecModel;
                    model.AccId = ordModel.AccId;

                    break;
            }

            return MessageTemplateControl.SendNotifyMsg(model.Id, model.EventName, model.SpecModel, model.AccId, 0, model.EventName + "触发");
        }

        /// <summary>
        /// 获取全部Kafka事件
        /// （当事件数比较多的时候从数据库获取）
        /// （暂在页面处理）
        /// </summary>
        /// <returns></returns>
        public string GetAllKafkaEvent()
        {
            return CommonLib.Helper.JsonSerializeObject(MessageTemplateBLL.GetAllKafkaEvent());
        }

        //自动发送操作
        //针对设定的目标对结果进行筛选

        #endregion

        #region 获取在运行的消息规则列表

        public string GetMsgRuleList(int pageIndex, int ruleType, string sendMethod, string name, string target)
        {
            ListView viewModel = new ListView();

            if (ruleType == 1 || ruleType == -99)//触发类消息处理
            {
                if (!string.IsNullOrEmpty(sendMethod))
                {
                    string[] methodList = sendMethod.Split(',');
                    viewModel = MessageTemplateBLL.GetTemplateList(pageIndex, methodList, target, name);
                }
                else
                {
                    viewModel = MessageTemplateBLL.GetTemplateList(pageIndex, null, target, name);
                }

                if (viewModel != null)
                {
                    return CommonLib.Helper.JsonSerializeObject(viewModel);
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        #endregion

        /// <summary>
        /// 设置后台循环更新消息列表的临时任务
        /// </summary>
        /// <returns></returns>
        public string SetUpdateTask()
        {
            return MessageSysControl.SetUpdateMethod().ToString();
        }

        /// <summary>
        /// 接口调用的每日定时刷新消息状态方法
        /// </summary>
        public string MsgDailyUpdate()
        {
            MessageSysControl.UpdateMsgStatus();
            return "";
        }


        /// <summary>
        /// 调用推送方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string SendingMsgByService(int id)
        {
            MessageSysControl.MsgManageByService(id);
            return "";
        }

        /// <summary>
        /// 用户短信回访
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="smsContent"></param>
        /// <returns></returns>
        public string SendingVisitSms(int accid, string smsContent)
        {
            ManageUserModel uM = (ManageUserModel)Session["logUser"];

            return MessageSysControl.SendVisitSms(accid, smsContent, uM.UserID, uM.Name);
        }

        /// <summary>
        /// 接口测试方法
        /// </summary>
        /// <param name="testStr"></param>
        /// <returns></returns>
        public string SendingMsgTest(string testStr)
        {
            return testStr + "从后台返回成功！";
        }
    }


}
