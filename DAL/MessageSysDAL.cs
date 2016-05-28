using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Utility;

namespace DAL
{
    public class MessageSysDAL
    {
        /// <summary>
        /// 插入批次信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddMessageBatch(MessageBatch model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "insert into MessageBatch (BatchId,AccIdSet,AccIdCount,ConditionDesc,SourceType," +
                "FilterLogVerify,CreateTime,ReviewState,SendingStatus,FeedBackArrive,FeedBackOpen," +
                "ChannelId,Title,Content,Remark,Operator,ContentType,ContentUrl) " +
                "Values(@BatchId,@AccIdSet,@AccIdCount,@ConditionDesc,@SourceType,@FilterLogVerify," +
                "@CreateTime,@ReviewState,@SendingStatus,@FeedBackArrive,@FeedBackOpen,@ChannelId," +
                "@Title,@Content,@Remark,@Operator,@ContentType,@ContentUrl);");

            try
            {
                int reVal = DapperHelper.Execute(strSql.ToString(), new
                {
                    BatchId = model.BatchId,
                    AccIdSet = model.AccIdSet,
                    AccIdCount = model.AccIdCount,
                    ConditionDesc = "",
                    SourceType = model.SourceType,
                    FilterLogVerify = model.FilterLogVerify,
                    CreateTime = DateTime.Now,
                    ReviewState = 0,
                    SendingStatus = 0,
                    FeedBackArrive = 0,
                    FeedBackOpen = 0,
                    ChannelId = model.ChannelId,
                    Title = model.Title,
                    Content = model.Content,
                    Remark = model.Remark,
                    Operator = model.Operator,
                    ContentType=model.ContentType,
                    ContentUrl=model.ContentUrl
                });

                return reVal;
            }
            catch (Exception ex)
            {
                Logger.Error("插入消息批次" + model.BatchId + "出错", ex);
                return 0;
            }
        }

        /// <summary>
        /// 获取批次列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <param name="batchId"></param>
        /// <param name="remark"></param>
        /// <param name="content"></param>
        /// <param name="sourceType"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetBatchList(int pageIndex, string stDate, string edDate, string batchId, string remark, string content, int sourceType, int channel)
        {
            Dictionary<string, object> dicData = new Dictionary<string, object>()
            {
                {"data",null},
                {"count",null}
            };

            StringBuilder strSql = new StringBuilder();
            string strWhere = "";

            //页数计算
            int bgNumber = ((pageIndex - 1) * 15) + 1;
            int edNumber = (pageIndex) * 15;

            strSql.Append("select * from (");

            strSql.Append("select *,ROW_NUMBER() over (order by id desc) rowNumber from MessageBatch where 1=1 ");

            if (Convert.ToDateTime(edDate) > Convert.ToDateTime(stDate))
            {
                if (stDate != "")
                {
                    DateTime stTime = Convert.ToDateTime(stDate);
                    strWhere += " and CreateTime >='" + stTime.ToString("yyyy-MM-dd") + "' ";
                }
                if (edDate != "")
                {
                    DateTime edTime = Convert.ToDateTime(edDate);
                    strWhere += " and CreateTime <'" + edTime.AddDays(1).Date.ToString("yyyy-MM-dd") + "' ";
                }
            }
            else if ((Convert.ToDateTime(edDate) == Convert.ToDateTime(stDate)) && Convert.ToDateTime(stDate).ToShortDateString() != DateTime.Now.ToShortDateString())
            {
                DateTime time = Convert.ToDateTime(stDate);
                strWhere += " and datediff(day,CreateTime,'" + time.Date.ToString("yyyy-MM-dd") + "')=0";
            }

            if (!string.IsNullOrEmpty(batchId))
            {
                strWhere += " and batchId like '%" + batchId + "%' ";
            }
            if (!string.IsNullOrEmpty(remark))
            {
                strWhere += " and Remark like '%" + remark + "%' ";
            }
            if (!string.IsNullOrEmpty(content))
            {
                strWhere += " and Content like '%" + content + "%' ";
            }
            if (sourceType > 0)
            {
                strWhere += " and SourceType=" + sourceType;
            }
            if (channel > 0)
            {
                strWhere += " and ChannelId=" + channel;
            }
            strSql.Append(strWhere);

            strSql.Append(" ) t ");
            strSql.Append(" where t.rowNumber between @bgNumber and @edNumber; ");



            try
            {
                List<MessageBatch> list = DapperHelper.Query<MessageBatch>(strSql.ToString(), new
                {
                    bgNumber = bgNumber,
                    edNumber = edNumber
                }).ToList();

                dicData["data"] = list;
                dicData["count"] = GetPageCount(strWhere);

                return dicData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 发送批次汇总
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <param name="batchId"></param>
        /// <param name="remark"></param>
        /// <param name="content"></param>
        /// <param name="sourceType"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetBatchSummaryInfo(int pageIndex, string stDate, string edDate, string batchId,
            string remark, int sourceType, int channel)
        {
            Dictionary<string, object> dicData = new Dictionary<string, object>()
            {
                {"data",null},
                {"count",null}
            };

            StringBuilder strSql = new StringBuilder();
            string strWhere = "";

            List<BatchSummary> sumList = new List<BatchSummary>();
            List<BatchSummary> reList = new List<BatchSummary>();

            //页数计算
            int bgNumber = ((pageIndex - 1) * 15) + 1;
            int edNumber = (pageIndex) * 15;

            strSql.Append("select * into #List from (");

            strSql.Append("select *,ROW_NUMBER() over (order by id desc) rowNumber from MessageSending where 1=1 ");

            if (Convert.ToDateTime(edDate) > Convert.ToDateTime(stDate))
            {
                if (stDate != "")
                {
                    DateTime stTime = Convert.ToDateTime(stDate);
                    strWhere += " and CreateTime >='" + stTime.ToString("yyyy-MM-dd") + "' ";
                }
                if (edDate != "")
                {
                    DateTime edTime = Convert.ToDateTime(edDate);
                    strWhere += " and CreateTime <'" + edTime.AddDays(1).Date.ToString("yyyy-MM-dd") + "' ";
                }
            }
            else if ((Convert.ToDateTime(edDate) == Convert.ToDateTime(stDate)) && Convert.ToDateTime(stDate).ToShortDateString() != DateTime.Now.ToShortDateString())
            {
                DateTime time = Convert.ToDateTime(stDate);
                strWhere += " and datediff(day,CreateTime,'" + time.Date.ToString("yyyy-MM-dd") + "')=0";
            }

            if (!string.IsNullOrEmpty(batchId))
            {
                strWhere += " and batchId like '%" + batchId + "%' ";
            }
            if (!string.IsNullOrEmpty(remark))
            {
                strWhere += " and Remark like '%" + remark + "%' ";
            }
            //if (!string.IsNullOrEmpty(content))
            //{
            //    strWhere += " and Content like '%" + content + "%' ";
            //}
            //if (sourceType > 0)
            //{
            //    strWhere += " and SourceType=" + sourceType;
            //}
            if (channel > 0)
            {
                strWhere += " and ChannelSet like '%" + channel + "%' ";
            }
            strSql.Append(strWhere);

            strSql.Append(" ) t ");
            strSql.Append(" where t.rowNumber between @bgNumber and @edNumber; ");

            #region 处理合并Batch逻辑
            StringBuilder strCondition = new StringBuilder();
            strCondition.Append(strSql.ToString());
            strCondition.Append("select BatchId from #List;");

            List<string> batchList = DapperHelper.Query<string>(strCondition.ToString(), new
            {
                bgNumber = bgNumber,
                edNumber = edNumber
            }).ToList();
            if (batchList != null && batchList.Count > 0)
            {
                foreach (var str in batchList)
                {
                    sumList.Add(new BatchSummary(str));
                }
            }
            else
            {
                return null;
            }

            strCondition.Clear();
            strCondition.Append(strSql.ToString());
            strCondition.Append(
                "select bat.BatchId,ChannelId,AllowNumber,FeedBackArrive,FeedBackOpen,bat.AccIdCount,bat.CreateTime,bat.Remark from #List left join MessageBatch bat on #List.BatchId=bat.BatchId;");

            List<dynamic> detailList = DapperHelper.Query<dynamic>(strCondition.ToString(), new
            {
                bgNumber = bgNumber,
                edNumber = edNumber
            }).ToList();

            foreach (var item in detailList)
            {
                BatchSummary summary = sumList.Find(x => x.BatchId == item.BatchId.ToString());
                summary.AccIdCount = Convert.ToInt32(item.AccIdCount);
                summary.CreateTime = Convert.ToDateTime(item.CreateTime);
                summary.SendRemark = item.Remark.ToString();

                string channelId = item.ChannelId.ToString();

                switch (channelId)
                {
                    case "1":
                        summary.SmsSend = Convert.ToInt32(item.AllowNumber);
                        summary.SmsArrive = Convert.ToInt32(item.FeedBackArrive);
                        summary.SmsOpen = Convert.ToInt32(item.FeedBackOpen);

                        if (summary.SmsSend!=0)
                        {
                            summary.SmsPartition = (Convert.ToDouble(summary.SmsOpen) / summary.SmsSend * 100).ToString("F2") + "%";    
                        }                        
                        break;
                    case "2":
                        summary.WebSend = Convert.ToInt32(item.AllowNumber);
                        summary.WebArrive = Convert.ToInt32(item.FeedBackArrive);
                        summary.WebOpen = Convert.ToInt32(item.FeedBackOpen);

                        if (summary.WebSend != 0)
                        {
                            summary.WebPartition = (Convert.ToDouble(summary.WebOpen) / summary.WebSend * 100).ToString("F2") + "%";
                        }
                        
                        break;
                    case "3":
                        summary.MobSend = Convert.ToInt32(item.AllowNumber);
                        summary.MobArrive = Convert.ToInt32(item.FeedBackArrive);
                        summary.MobOpen = Convert.ToInt32(item.FeedBackOpen);

                        if (summary.MobSend!=0)
                        {
                            summary.MobPartition = (Convert.ToDouble(summary.MobOpen) / summary.MobSend * 100).ToString("F2") + "%";
                        }
                        
                        break;
                    case "4":
                        summary.EmailSend = Convert.ToInt32(item.AllowNumber);
                        summary.EmailArrive = Convert.ToInt32(item.FeedBackArrive);
                        summary.EmailOpen = Convert.ToInt32(item.FeedBackOpen);

                        if (summary.EmailSend!=0)
                        {
                            summary.EmailPartition = (Convert.ToDouble(summary.EmailOpen) / summary.EmailSend * 100).ToString("F2") + "%";    
                        }
                        
                        break;
                }
                reList.Add(summary);

            }
            #endregion

            dicData["data"] = reList.Distinct();
            dicData["count"] = GetSendingCount(strWhere);

            return dicData;

        }

        /// <summary>
        /// 添加一条信息详情
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddMessageDetail(MessageDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "insert into MessageDetail (BatchId,AccId,Title,Content,ArriveMark,OpenMark,ChannelId,Remark,CreateTime,AccIdNumber,ContentType,ContentUrl) " +
                "Values(@BatchId,@AccId,@Title,@Content,@ArriveMark,@OpenMark,@ChannelId,@Remark,@CreateTime,@AccIdNumber,@ContentType,@ContentUrl)");

            try
            {
                int reVal = DapperHelper.Execute(strSql.ToString(), new
                {
                    BatchId = model.BatchId,
                    AccId = model.AccId,
                    Title = model.Title,
                    Content = model.Content,
                    ArriveMark = 0,
                    OpenMark = 0,
                    ChannelId = model.ChannelId,
                    Remark = model.Remark,
                    CreateTime = DateTime.Now,
                    AccIdNumber = model.AccIdNumber,
                    ContentType=model.ContentType,
                    ContentUrl=model.ContentUrl
                });

                return reVal;
            }
            catch (Exception ex)
            {
                Logger.Error("插入批次" + model.BatchId + "消息详情出错", ex);
                return 0;
            }
        }

        /// <summary>
        /// SMSTest
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //public int AddMessageSmsDetail(MessageDetail model)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append(
        //        "insert into MessageDetail (BatchId,AccId,Title,Content,ArriveMark,OpenMark,ChannelId,Remark,CreateTime,AccIdNumber) " +
        //        "Values(@BatchId,@AccId,@Title,@Content,@ArriveMark,@OpenMark,@ChannelId,@Remark,@CreateTime,@AccIdNumber)");

        //    try
        //    {
        //        int reVal = DapperHelper.Execute(strSql.ToString(), new
        //        {
        //            BatchId = model.BatchId,
        //            AccId = model.AccId,
        //            Title = model.Title,
        //            Content = model.Content,
        //            ArriveMark = 0,
        //            OpenMark = 0,
        //            ChannelId = model.ChannelId,
        //            Remark = model.Remark,
        //            CreateTime = DateTime.Now,
        //            AccIdNumber = model.AccIdNumber
        //        });

        //        return reVal;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error("插入批次" + model.BatchId + "消息详情出错", ex);
        //        return 0;
        //    }
        //}


        /// <summary>
        /// 获取批次计数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int GetPageCount(string strWhere)
        {
            int count = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from MessageBatch where 1=1 ");

            strSql.Append(strWhere);

            try
            {
                count = DapperHelper.Query<int>(strSql.ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                count = 0;
            }

            return count;
        }

        public int GetSendingCount(string strWhere)
        {
            int count = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from MessageSending where 1=1 ");

            strSql.Append(strWhere);

            try
            {
                count = DapperHelper.Query<int>(strSql.ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                count = 0;
            }

            return count;
        }

        /// <summary>
        /// 获取分页详情列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="batchId"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetBatchDetail(int pageIndex, string batchId, int channelId, int arrived = 0)
        {
            Dictionary<string, object> dicData = new Dictionary<string, object>()
            {
                {"data",null},
                {"count",null}
            };

            StringBuilder strSql = new StringBuilder();
            string strWhere = " where BatchId='" + batchId + "' and channelId=" + channelId;

            if (arrived != 0)
            {
                strWhere += " and OpenMark=1";
            }

            //页数计算
            int bgNumber = ((pageIndex - 1) * 15) + 1;
            int edNumber = (pageIndex) * 15;

            strSql.Append("select * into #List from MessageDetail ");
            strSql.Append(strWhere);            
                       
            strSql.Append(" select a.ID,a.CompanyName ,a.UserRealName ,f.PhoneNumber,f.UserEmail,a.RegTime,  ");
            strSql.Append(" case when b.aotjb=3 then '高级' when b.aotjb=2 then '标准' else '免费' end aotjb  ");
            strSql.Append(" ,b.endtime aotjbEndtime,c.userNum,c.goodsNum,c.saleNum,c.smsNum,b.dxunity,c.outlayNum,e.insertName returnInsertTime ,  ");
            strSql.Append(" d.allCount ,d.userCount,a.LoginTimeWeb,LoginTimeLast,  ");
            strSql.Append(" case when c.active=1 then '新注册' when c.active=3 then '需关怀' when c.active=5 then '活跃'   ");
            strSql.Append(" when c.active=7 then '忠诚' when c.active=-1 then '休眠' when c.active=-3 then '流失' else '新注册' end active,  ");
            strSql.Append(" c.orderMoney ,g.AgentName into #ListAll from i200.dbo.T_Account a left outer join i200.dbo.T_Business b on a.ID=b.accountid   ");
            strSql.Append(" left outer join SysRpt_ShopInfo c on a.ID=c.accountid left outer join (  ");
            strSql.Append(" select toAccId,COUNT(id) allCount,sum(case when useAccId IS null then 0 else 1 end) userCount  from i200.dbo.T_Order_CouponList   ");
            strSql.Append(" where toAccId in(select AccId from #list) group by toAccId) d on a.ID=d.toAccId  ");
            strSql.Append(" left outer join(  ");
            strSql.Append(" select a.accid,a.insertName from Sys_VisitInfo a inner join(  ");
            strSql.Append(" select accid,MAX(insertTime) it from Sys_VisitInfo   where    ");
            strSql.Append(" accid in(select AccId from #list)  group by accid ) b on a.insertTime=b.it and a.accid=b.accid) e on a.ID=e.accid  ");
            strSql.Append(" left outer join (  ");
            strSql.Append(" select accountid,PhoneNumber,UserEmail from i200.dbo.T_Account_User where grade='管理员') f on a.ID=f.accountid  left outer join Sys_agent_mess g on a.AgentId=g.ID ");
            strSql.Append(" where a.ID in( ");
            strSql.Append(" select accid from #list ");
            strSql.Append(" );  ");


            strSql.Append("select * from (");



            strSql.Append("select #List.ArriveMark,#List.OpenMark,#List.Remark,#List.CreateTime,#ListAll.*,ROW_NUMBER() over (order by #List.AccId desc) rowNumber from #List left join #ListAll on #List.AccId=#ListAll.ID ");
            strSql.Append(" ) t ");
            strSql.Append(" where t.rowNumber between @bgNumber and @edNumber; ");

            strSql.Append(" drop table #List  ");
            strSql.Append(" drop table #ListAll  ");

            try
            {
                List<dynamic> list = DapperHelper.Query<dynamic>(strSql.ToString(), new
                {
                    bgNumber = bgNumber,
                    edNumber = edNumber
                }).ToList();

                T_AccountDAL aDal = new T_AccountDAL();
                //foreach (var item in list)
                //{
                //    item.CompanyName = aDal.GetCompanyName(item.AccId);
                //}

                dicData["data"] = list;
                dicData["count"] = GetDetailCount(strWhere);

                return dicData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取详情计数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int GetDetailCount(string strWhere)
        {
            int count = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from MessageDetail ");

            strSql.Append(strWhere);

            try
            {
                count = DapperHelper.Query<int>(strSql.ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                count = 0;
            }

            return count;
        }

        /// <summary>
        /// 添加批次概览信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddMsgSendingInfo(MessageSending model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "insert into MessageSending (BatchId,AccIdSet,AccIdCount,ChannelSet,ActionType,ActionMark,Remark,CreateTime) " +
                "Values(@BatchId,@AccIdSet,@AccIdCount,@ChannelSet,@ActionType,@ActionMark,@Remark,@CreateTime)");

            try
            {
                int reVal = DapperHelper.Execute(strSql.ToString(), new
                {
                    BatchId = model.BatchId,
                    AccIdSet = model.AccIdSet,
                    AccIdCount = model.AccIdCount,
                    ChannelSet = model.ChannelSet,
                    ActionType = 0,
                    ActionMark = 0,
                    Remark = model.Remark,
                    CreateTime = DateTime.Now
                });

                return reVal;
            }
            catch (Exception ex)
            {
                Logger.Error("插入批次" + model.BatchId + "概览详情出错", ex);
                return 0;
            }
        }

        /// <summary>
        /// 获取发送批次信息
        /// </summary>
        /// <param name="batchId"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public List<MessageDetail> GetBatchForSend(string batchId, int channelId)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select * from MessageDetail where BatchId='" + batchId + "' and ChannelId=" + channelId);

            try
            {
                return DapperHelper.Query<MessageDetail>(strSql.ToString()).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("获取发送批次" + batchId + "详情错误", ex);
                return null;
            }
        }

        /// <summary>
        /// 更新批次表的发送数以及关联发送ID
        /// </summary>
        /// <param name="batchId"></param>
        /// <param name="channelId"></param>
        /// <param name="count"></param>
        /// <param name="sendId"></param>
        /// <returns></returns>
        public int UpdateAllowCount(string batchId, int channelId, int count, string sendId)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update MessageBatch set SendId='" + sendId + "',AllowNumber=" + count + " where BatchId='" +
                          batchId + "' and channelId=" + channelId);

            return DapperHelper.Execute(strSql.ToString());
        }

        public int UpdateDetailUnableMark(int id)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update MessageDetail set ArriveMark=-1 where Id=" + id);

            return DapperHelper.Execute(strSql.ToString());
        }

        /// <summary>
        /// 获取批次发送用户数
        /// </summary>
        /// <param name="batchId"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public int GetBatchCount(string batchId, int channelId)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select AccIdCount from MessageBatch where BatchId='" +
                          batchId + "' and channelId=" + channelId);

            return DapperHelper.ExecuteScalar<int>(strSql.ToString());
        }

        /// <summary>
        /// 获取单条批次信息
        /// </summary>
        /// <param name="batchId"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public MessageBatch GetSingleBatchInfo(string batchId, int channelId)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select top 1 * from MessageBatch where BatchId='" +
                          batchId + "' and channelId=" + channelId);

            return DapperHelper.GetModel<MessageBatch>(strSql.ToString());
        }

        /// <summary>
        /// 根据接口返回状态更新详情列表状态
        /// </summary>
        /// <param name="batchId"></param>
        /// <param name="channelId"></param>
        /// <param name="statusModel"></param>
        /// <returns></returns>
        public int UpdateDetailSendingStatus(string batchId, int channelId, SendStatus statusModel)
        {
            StringBuilder strSql = new StringBuilder();

            if (statusModel != null)
            {
                if (statusModel.DataList != null)
                {
                    if (statusModel.DataList.Count > 0)
                    {
                        foreach (var item in statusModel.DataList)
                        {
                            if (channelId == 1 || channelId == 4)
                            {
                                strSql.Append("update MessageDetail set ArriveMark=@arriveMark,OpenMark=@openMark " +
                                              " where AccIdNumber='" + item.TargetId + "' and BatchId='" + batchId +
                                              "' and channelId=" + channelId);
                            }
                            else
                            {
                                strSql.Append("update MessageDetail set ArriveMark=@arriveMark,OpenMark=@openMark " +
                                              " where AccId=" + item.TargetId + " and BatchId='" + batchId +
                                              "' and channelId=" + channelId);
                            }


                            try
                            {
                                DapperHelper.Execute(strSql.ToString(), new
                                {
                                    arriveMark = (item.SendStatus == 1 ? 1 : -99),
                                    openMark = (item.IsRead)
                                });

                                strSql.Clear();
                            }
                            catch (Exception ex)
                            {
                                Logger.Error("更新发送详情列表出错", ex);
                                strSql.Clear();
                                continue;
                            }
                        }
                    }

                    //空列表返回正常状态
                    return 0;
                }
                else
                {
                    //返回发送状态的详情列表为空
                    return -1;
                }
            }
            else
            {
                //返回发送状态为空
                return -2;
            }


        }

        /// <summary>
        /// 更新批次发送状态
        /// </summary>
        /// <param name="batchId"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public int UpdateBatchSendingStatus(string batchId, int channelId)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update MessageBatch set FeedBackArrive=(select count(*) from MessageDetail where BatchId='" +
                          batchId + "' and ChannelId=" + channelId + " and ArriveMark=1)," +
                          "FeedBackOpen=(select count(*) from MessageDetail where BatchId='" + batchId +
                          "' and ChannelId=" + channelId + " and OpenMark=1) where BatchId='" + batchId +
                          "' and ChannelId=" + channelId);

            return DapperHelper.Execute(strSql.ToString());
        }

        public int UpdateOperator(int operId, string batchId, int channelId)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update MessageBatch set Operator=" + operId + " where BatchId='" + batchId +
                          "' and ChannelId=" + channelId);

            return DapperHelper.Execute(strSql.ToString());
        }

        /// <summary>
        /// 根据BatchId获取批次所有的发送渠道
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public List<int> GetBatchChannel(string batchId)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select ChannelId from MessageBatch where BatchId=@batchId;");

            return DapperHelper.Query<int>(strSql.ToString(), new {batchId = batchId}).ToList();
        }

        /// <summary>
        /// 获取每日待更新状态消息列表
        /// </summary>
        /// <returns></returns>
        public List<RecurringUpdateModel> GetUpdateModel()
        {
            DateTime dtNow = DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            DateTime dtBefore = dtNow.AddDays(-1);
            StringBuilder strSql = new StringBuilder();

            strSql.Append(
                "select BatchId,ChannelId,SendId from [Sys_I200].[dbo].[MessageBatch] where CreateTime between @stDate and @edDate and SendId is not null and SendId<>'err'; ");

            try
            {
                return
                    DapperHelper.Query<RecurringUpdateModel>(strSql.ToString(), new {stDate = dtBefore, edDate = dtNow})
                        .ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("获取每日需要更新的消息列表信息出错！", ex);
                return null;                
            }
        }

        /// <summary>
        /// 获取符合条件的推送用户列表
        /// 符合条件用户的公告型消息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<string> GetPushUserList(int id)
        {
            StringBuilder strSql = new StringBuilder();            

            switch (id)
            {
                case 1:
                    //strSql.Append("select ID from i200.dbo.T_Account where LoginTimeLast between CAST((GETDATE()-3) as date) and (CAST(GETDATE() as date)) " +
                    //              "and ID in (select distinct accid from [I200].[dbo].[T_Token_Api] where createTime between CAST((GETDATE()-3) as date) and (CAST(GETDATE() as date)));");
                    //移动端三天内登录过用户
                    strSql.Append(
                        "select distinct Accountid from i200.dbo.T_LOG where DATEDIFF(DAY,OperDate,GETDATE())<=3 and DATEDIFF(DAY,OperDate,GETDATE())>=0 and LogMode=4 ");
                    strSql.Append(" union ");
                    strSql.Append(
                        " select ID from i200.dbo.T_Account where State=1 and DATEDIFF(DAY,RegTime,GETDATE())<=3 and DATEDIFF(DAY,RegTime,GETDATE())>=0;");
                    break;                
                default:
                    return null;                    
            }

            return DapperHelper.Query<string>(strSql.ToString()).ToList();
        }

        /// <summary>
        /// 获取单个用户发送的用户模型列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<SingleShopInfoForMsg> GetSingleUserMsg(int id)
        {
            StringBuilder strSql = new StringBuilder();

            switch (id)
            {
                case 2://付费功能到期(三天和一天)
                    //strSql.Append("")
                    strSql.Append("");
                    break;
                case 3://优惠券到期(三天或一天)
                    strSql.Append(
                        "select ci.couponDesc CouponName,ta.CompanyName,ta.ID Accid,DATEDIFF(DAY,getdate(),cl.endDate) Interval from [I200].[dbo].[T_Order_CouponList] cl " +
                        "left join i200.dbo.T_Account ta on cl.toAccId=ta.ID " +
                        "left join i200.dbo.T_Order_CouponInfo ci on cl.groupId=ci.id " +
                        "where cl.couponStatus=2 and (DATEDIFF(DAY,getdate(),cl.endDate)=3 or DATEDIFF(DAY,getdate(),cl.endDate)=1);");
                    break;
                case 4://获取周年用户
                    strSql.Append(
                        "select DATEDIFF(YEAR,regtime,getdate()) Interval,ID Accid,CompanyName from [i200].[dbo].[T_Account] " +
                        "where DATEPART(MONTH,RegTime)=DATEPART(MONTH,getdate()) and DATEPART(day,RegTime)=DATEPART(day,getdate()) and DATEDIFF(MONTH,RegTime,GETDATE())>0 and state=1");
                    break;
            }

            try
            {
                return DapperHelper.Query<SingleShopInfoForMsg>(strSql.ToString()).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("获取消息发送对象列表出错！", ex);
                return new List<SingleShopInfoForMsg>();//异常则返回空列表
            }
        }
    }
}
