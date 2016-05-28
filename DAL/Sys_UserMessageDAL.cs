using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Utility;

namespace DAL
{
    public class Sys_UserMessageDAL
    {
        public int AddUserMessage(Sys_UserMessageModel model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(
                "insert into Sys_UserMessage (MsgKey,Accid,IsPush,IsRead,ChannelId,PushTime,Title,PushContent,Operator,OperatorName) " +
                "Values (@MsgKey,@Accid,@IsPush,@IsRead,@ChannelId,@PushTime,@Title,@PushContent,@Operator,@OperatorName);");
            if (CheckMsgExist(model.Accid, model.MsgKey) == 1)
            {
                return 1;
            }
            else
            {
                try
                {
                    return DapperHelper.Execute(strSql.ToString(), new
                    {
                        MsgKey = model.MsgKey,
                        Accid = model.Accid,
                        IsPush = model.IsPush,
                        IsRead = model.IsRead,
                        ChannelId = model.ChannelId,
                        PushTime = model.PushTime,
                        Title = model.Title,
                        PushContent = model.PushContent,
                        Operator = model.Operator,
                        OperatorName = model.OperatorName
                    });
                }
                catch (Exception ex)
                {
                    Logger.Error("汇总插入用户推送消息出错！", ex);
                    return 0;
                }
            }

        }

        /// <summary>
        /// 判断是否重复消息
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="msgKey"></param>
        /// <returns></returns>
        public int CheckMsgExist(int accid, string msgKey)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("if exists(select * from Sys_I200.dbo.Sys_UserMessage where accid=@accid and msgkey=@msgkey) " +
                          " " +
                          " select 1;" +
                          "  " +
                          " else " +
                          " select 0; ");

            return DapperHelper.ExecuteScalar<int>(strSql.ToString(), new { accid = accid, msgkey = msgKey });
        }

        public List<Sys_UserMessageModel> GetUserMessageList(int pageIndex, int accid, string str)
        {
            StringBuilder strSql = new StringBuilder();

            int bgNumber = ((pageIndex - 1) * 15) + 1;
            int edNumber = (pageIndex) * 15;

            strSql.Append("select * from " +
                          "(" +
                          "select ROW_NUMBER() over (order by PushTime desc) rowNumber,* from Sys_UserMessage where accid=@accid ");

            if (!string.IsNullOrEmpty(str))
            {
                strSql.Append(" and " + str);
            }
            strSql.Append(") T where T.rowNumber between @bgNumber and @edNumber;");

            try
            {
                return DapperHelper.Query<Sys_UserMessageModel>(strSql.ToString(), new
                {
                    accid = accid,
                    bgNumber = bgNumber,
                    edNumber = edNumber
                }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("获取用户推送消息列表出错！", ex);
                return null;
            }
        }

        /// <summary>
        /// 获取总条目
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public int GetUserMessageCount(int accid, string str)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select count(*) from Sys_UserMessage where Accid=@accid ");

            if (!string.IsNullOrEmpty(str))
            {
                strSql.Append(" and " + str);
            }

            return DapperHelper.ExecuteScalar<int>(strSql.ToString(), new { accid = accid });
        }

        /// <summary>
        /// 获取最近一次某渠道更新时间
        /// </summary>
        /// <param name="channelId">
        /// 1-短信
        /// 2-邮件
        /// 3-移动端
        /// 4-PC站内
        /// </param>
        /// <returns></returns>
        public DateTime GetLastUpdateTime(int channelId,int accid)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select top 1 PushTime from Sys_UserMessage " +
                          "where ChannelId=@channelId and Accid=@accid " +
                          "order by PushTime desc;");

            try
            {
                string dtStr = DapperHelper.ExecuteScalar<string>(strSql.ToString(), new { channelId = channelId, accid= accid });
                if (!string.IsNullOrEmpty(dtStr))
                {
                    return Convert.ToDateTime(dtStr);
                }
                else
                {
                    return Convert.ToDateTime("2010-1-1 00:00:00");
                }

            }
            catch (Exception ex)
            {
                Logger.Error("获取用户推送信息汇总表最近一次更新时间出错！", ex);
                return DateTime.MinValue;
            }
        }


        /// <summary>
        /// 更新获取
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public int UpdateUserMessage(int accid, int channelId, DateTime specDate)
        {
            StringBuilder strSql = new StringBuilder();

            switch (channelId)
            {
                case 1:     //从短信表获取更新
                    strSql.Append("insert into Sys_UserMessage (MsgKey,Accid,IsPush,IsRead,ChannelId,PushTime,Title,PushContent,Operator,OperatorName) ");
                    strSql.Append("(select '',ta.ID,1,1,1,sendtime,'',smsContent,0,'' " +
                                  "from i200.dbo.T_Sms_List sl " +
                                  "left join i200.dbo.T_Account ta " +
                                  "on sl.phoneNum=ta.PhoneNumber " +
                                  "where sl.accID=0 and DATEADD(SECOND,-1,sl.sendtime)>@specDate and ta.ID=@accid " +
                                  ")");
                    DapperHelper.Execute(strSql.ToString(), new
                    {
                        specDate = specDate,
                        accid = accid
                    });
                    break;
                case 2:     //从邮件列表获取更新
                    strSql.Append("insert into Sys_UserMessage (MsgKey,Accid,IsPush,IsRead,ChannelId,PushTime,Title,PushContent,Operator,OperatorName) ");
                    strSql.Append("(select '',accid,1,1,2,sendtime,emailTitle,emailContent,0,'' " +
                                  "from Sys_I200.dbo.Sys_VisitEmailSnap " +
                                  "where DATEADD(SECOND,-1,sendtime)>@specDate and accid=@accid)");
                    DapperHelper.Execute(strSql.ToString(), new
                    {
                        specDate = specDate,
                        accid = accid
                    });
                    break;
                case 3:     //从移动端消息获取更新
                    //获取增量更新的消息列表
                    MessageModel mobileModel = MessageCenter.GetMobileMessageEx(accid, "", specDate, DateTime.Now);
                    //非空则循环插入消息
                    if (mobileModel != null && mobileModel.msgList != null && mobileModel.msgList.Count > 0)
                    {
                        foreach (var item in mobileModel.msgList)
                        {
                            Sys_UserMessageModel tmpModel = new Sys_UserMessageModel();
                            tmpModel.Accid = accid;
                            tmpModel.ChannelId = 3;
                            tmpModel.IsPush = item.isPush;
                            tmpModel.IsRead = item.isRead;
                            tmpModel.MsgKey = item._id;
                            tmpModel.Operator = item.operatorId;
                            tmpModel.PushContent = item.msgContent;
                            tmpModel.PushTime = item.createTime;
                            tmpModel.Title = item.msgTitle;
                            tmpModel.OperatorName = item.operatorName;

                            AddUserMessage(tmpModel);
                        }

                    }
                    break;
                case 4:     //从PC站内消息获取更新
                    //获取增量更新的消息列表
                    MessageModel pcModel = MessageCenter.GetMessageEx(accid, "", specDate, DateTime.Now);
                    //非空则循环插入消息
                    if (pcModel != null && pcModel.msgList != null && pcModel.msgList.Count > 0)
                    {
                        foreach (var item in pcModel.msgList)
                        {
                            Sys_UserMessageModel tmpModel = new Sys_UserMessageModel();
                            tmpModel.Accid = accid;
                            tmpModel.ChannelId = 4;
                            tmpModel.IsPush = item.isPush;
                            tmpModel.IsRead = item.isRead;
                            tmpModel.MsgKey = item._id;
                            tmpModel.Operator = item.operatorId;
                            tmpModel.PushContent = item.msgContent;
                            tmpModel.PushTime = item.createTime;
                            tmpModel.Title = item.msgTitle;
                            tmpModel.OperatorName = item.operatorName;

                            AddUserMessage(tmpModel);
                        }

                    }

                    break;
            }

            return 1;
        }
    }
}
