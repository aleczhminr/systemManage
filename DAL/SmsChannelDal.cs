using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib;
using Model;

namespace DAL
{
    public class SmsChannelDal
    {
        /// <summary>
        /// 获得短信通道设置信息
        /// </summary>
        /// <returns></returns>
        public SmsChannelInfo GetChannelInfo()
        {
            SmsConfigInfoTmp tmpResult = new SmsConfigInfoTmp();
            SmsChannelInfo smsChannelInfo = new SmsChannelInfo();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("declare @SendMode int;");
            strSql.Append(" select @SendMode=Value from T_SysConfig where Item='sms_SendMode';");
            strSql.Append(" if(@SendMode=1)");
            strSql.Append(" begin");
            strSql.Append("   SELECT @SendMode as sendmode,max(case when Item='sms_channel_sys' then Value end) as sms_channel_sys,max(case when Item='sms_channel_f' then Value end) as sms_channel_f,max(case when Item='sms_channel_s' then Value end) as sms_channel_s FROM T_SysConfig where Item='sms_channel_f' or Item='sms_channel_s' or Item='sms_channel_sys';");
            strSql.Append(" end");
            strSql.Append(" else");
            strSql.Append(" begin");
            strSql.Append("  SELECT @SendMode as sendmode,Value from T_SysConfig where Item='sms_option';");
            strSql.Append(" end");


            try
            {
                tmpResult = HelperForFrontend.Query<SmsConfigInfoTmp>(strSql.ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                tmpResult = null;
            }

            if (tmpResult != null)
            {
                if (tmpResult.sendmode != null)
                {
                    smsChannelInfo.SendMode = tmpResult.sendmode;

                    if (smsChannelInfo.SendMode == 2)
                    {
                        //运营商绑定模式
                        if (tmpResult.Value != null)
                        {
                            string objVal = tmpResult.Value.ToString();
                            SmsChannel smsChannel = new SmsChannel();
                            smsChannel = Helper.JsonDeserializeObject<SmsChannel>(objVal.Trim());
                            smsChannelInfo.SmsOperator = smsChannel;
                        }
                    }
                    else
                    {
                        //通道绑定模式

                        SmsPriorityItem smsPriorityItem = new SmsPriorityItem();
                        SmsChannelUnit smsChannelUnitSys = new SmsChannelUnit();
                        SmsChannelUnit smsChannelUnitFast = new SmsChannelUnit();
                        SmsChannelUnit smsChannelUnitGroup = new SmsChannelUnit();
                        smsPriorityItem.SysPriority = smsChannelUnitSys;
                        smsPriorityItem.FastPriority = smsChannelUnitFast;
                        smsPriorityItem.GroupPriority = smsChannelUnitGroup;

                        smsPriorityItem.SysPriority.ChannelId = tmpResult.sms_channel_sys;
                        smsPriorityItem.FastPriority.ChannelId = tmpResult.sms_channel_f;
                        smsPriorityItem.GroupPriority.ChannelId = tmpResult.sms_channel_s;

                        smsChannelInfo.SmsPriority = smsPriorityItem;
                    }
                }
            }

            return smsChannelInfo;
        }
        /// <summary>
        /// 更新运营商绑定通道设置
        /// </summary>
        /// <param name="smsChannel"></param>
        /// <returns></returns>
        public bool SetSmsChannelOption(SmsChannel smsChannel)
        {
            bool bResult = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" UPDATE T_SysConfig SET Value =@Option, SetTime =GETDATE() where Item='sms_option';");

            try
            {
                int result = HelperForFrontend.Execute(strSql.ToString(),
                    new {Option = Helper.JsonSerializeObject(smsChannel).ToString()});
                if (result>0)
                {
                    bResult = true;                    
                }
            }
            catch (Exception ex)
            {
                bResult = false;
            }

            return bResult;
        }


        /// <summary>
        /// 获得短信通道设置信息
        /// </summary>
        /// <returns></returns>
        public SmsChannelInfo GetSysChannelInfo()
        {
            SmsConfigInfoTmp tmpResult = new SmsConfigInfoTmp();
            SmsChannelInfo smsChannelInfo = new SmsChannelInfo();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("declare @SendMode int;");
            strSql.Append(" select @SendMode=Value from Sms_SysConfig where Item='sms_SendMode';");
            strSql.Append(" if(@SendMode=1)");
            strSql.Append(" begin");
            strSql.Append("   SELECT @SendMode as sendmode,max(case when Item='sms_channel_sys' then Value end) as sms_channel_sys,max(case when Item='sms_channel_f' then Value end) as sms_channel_f,max(case when Item='sms_channel_s' then Value end) as sms_channel_s FROM Sms_SysConfig where Item='sms_channel_f' or Item='sms_channel_s' or Item='sms_channel_sys';");
            strSql.Append(" end");
            strSql.Append(" else");
            strSql.Append(" begin");
            strSql.Append("  SELECT @SendMode as sendmode,Value from Sms_SysConfig where Item='sms_option';");
            strSql.Append(" end");

            try
            {
                tmpResult = DapperHelper.Query<SmsConfigInfoTmp>(strSql.ToString()).ToList()[0];
            }
            catch (Exception)
            {
                tmpResult = null;
            }

            if (tmpResult != null)
            {
                if (tmpResult.sendmode != null)
                {
                    smsChannelInfo.SendMode = tmpResult.sendmode;

                    if (smsChannelInfo.SendMode == 2)
                    {
                        //运营商绑定模式
                        if (tmpResult.Value != null)
                        {
                            string objVal = tmpResult.Value.ToString();
                            SmsChannel smsChannel = new SmsChannel();
                            smsChannel = Helper.JsonDeserializeObject<SmsChannel>(objVal.Trim());
                            smsChannelInfo.SmsOperator = smsChannel;
                        }
                    }
                    else
                    {
                        //通道绑定模式

                        SmsPriorityItem smsPriorityItem = new SmsPriorityItem();
                        SmsChannelUnit smsChannelUnitSys = new SmsChannelUnit();
                        SmsChannelUnit smsChannelUnitFast = new SmsChannelUnit();
                        SmsChannelUnit smsChannelUnitGroup = new SmsChannelUnit();
                        smsPriorityItem.SysPriority = smsChannelUnitSys;
                        smsPriorityItem.FastPriority = smsChannelUnitFast;
                        smsPriorityItem.GroupPriority = smsChannelUnitGroup;

                        smsPriorityItem.SysPriority.ChannelId = tmpResult.sms_channel_sys;
                        smsPriorityItem.FastPriority.ChannelId = tmpResult.sms_channel_f;
                        smsPriorityItem.GroupPriority.ChannelId = tmpResult.sms_channel_s;

                        smsChannelInfo.SmsPriority = smsPriorityItem;
                    }
                }
            }

            return smsChannelInfo;
        }



        /// <summary>
        /// 更新运营商绑定通道设置
        /// </summary>
        /// <param name="smsChannel"></param>
        /// <returns></returns>
        public bool SetSysSmsChannelOption(SmsChannel smsChannel)
        {
            bool bResult = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" UPDATE Sms_SysConfig SET Value =@Option, SetTime =GETDATE() where Item='sms_option';");

            try
            {
                int result = DapperHelper.Execute(strSql.ToString(),
                    new { Option = Helper.JsonSerializeObject(smsChannel).ToString() });
                if (result > 0)
                {
                    bResult = true;
                }
            }
            catch (Exception ex)
            {
                bResult = false;
            }

            return bResult;
        }

        /// <summary>
        /// 短信通道测试
        /// </summary>
        /// <param name="strContent">短信内容</param>
        /// <param name="phoneNum">手机号码</param>
        /// <param name="userCnt">号码个数</param>
        /// <param name="channelId">通道ID</param>
        /// <returns></returns>
        public string GetChannelTest(string strContent, string phoneNum, int userCnt, int channelId, int smsFlag)
        {
            string sResult = "0";
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "   INSERT INTO T_Sms_Notify(accID, userList, userCnt, smsContent, smsType, useBalance, priority, succeedCnt, failCnt, skipCnt, smsStatus, submitTime, [RegularTime],smsChannel, PhoneList, isFree)");
            strSql.Append(
                " VALUES (0,'',@userCnt,@content,99,0,0,0,0,0,0,GETDATE(),'1900-1-1',@channelID,@phoneNum,1)");

            if (smsFlag == 1)
            {
                //添加时间和通道Id
                strContent = DateTime.Now.ToString("HH:mm") + "-" + channelId.ToString() + strContent;
            }

            try
            {
                int result = HelperForFrontend.Execute(strSql.ToString(), new
                {
                    content = strContent,
                    channelID = channelId,
                    phoneNum = phoneNum.TrimEnd(','),
                    userCnt = userCnt
                });

                if (result > 0)
                {
                    sResult = "ok";
                }
            }
            catch (Exception)
            {
                sResult = "0";
            }
  
            return sResult;
        }

        /// <summary>
        /// 获得短信通道发送条数
        /// </summary>
        /// <param name="channelId">通道Id</param>
        /// <returns></returns>
        public int GetChannelSum(int channelId)
        {
            int iResult = -1;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(realCnt) as smsCnt from T_Sms_List where smsStatus=1 and smsChannel=@smsChannel;");
            var sResult = HelperForFrontend.ExecuteScalar(strSql.ToString(), new { smsChannel = channelId });
            if (sResult != null && sResult != DBNull.Value)
            {
                iResult = Convert.ToInt32(sResult);
            }

            return iResult;
        }
    }
}
