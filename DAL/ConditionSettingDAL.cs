using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Utility;

namespace DAL
{
    public class ConditionSettingDAL
    {
        /// <summary>
        /// 初始化条件设置记录
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="accIdList"></param>
        /// <param name="verif"></param>
        /// <returns></returns>
        public int InitialConditionRecord(int uid,string accIdList,int accIdCount, string verif)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(
                "insert into I200_Task.dbo.ConditionSetting (Verification,AccIdSet,AccIdCount,ActiveStatus,Operator) " +
                "Values (@verif,@accIdList,@accIdCount,0,@uid);" +
                "select @@IDENTITY;");

            try
            {
                return DapperHelper.Execute(strSql.ToString(), new
                {
                    verif = verif,
                    accIdList = accIdList,
                    accIdCount=accIdCount,
                    uid = uid
                });
            }
            catch (Exception ex)
            {
                Logger.Error("发送消息初始化条件组失败！", ex);
                return 0;
            }
        }

        /// <summary>
        /// 页面获取条件组
        /// </summary>
        /// <param name="verif"></param>
        /// <returns></returns>
        public List<ConditionSettingModel.ConditionRecoveryModel> GetRuleCondition(string verif)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(
                "select rl.Remark,rl.RuleId,rl.MaxValue,rl.MinValue,rl.RangeData,rd.ColDesc,rl.Id,rd.ColName,rd.ConditionGroup,rd.ConditionType " +
                "from Sys_RuleList rl left join Sys_RuleDetail rd " +
                "on rl.RuleId=rd.Id " +
                "where VerifId=@verif;");

            try
            {
                return DapperHelper.Query<ConditionSettingModel.ConditionRecoveryModel>(strSql.ToString(), new
                {
                    verif = verif
                }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("在消息发送页面获取筛选器条件出错！", ex);
                return null;
            }
            
        }


        /// <summary>
        /// 添加后台消息发送任务设置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddContitionSetting(ConditionSettingModel.SettingModel model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(
                "insert into I200_Task.dbo.ConditionSetting (Verification,SendingType,SqlSet,AccIdSet,AccIdCount,SourceType," +
                "MobileTitle,PubTitle,EmailTitle,SmsContent,MobileContent,PubContent,EmailContent,Remark,ActiveStatus," +
                "SendingDate,RecurringSendingCron,TargetMark,TargetMarkId,Operator) " +
                "Values(@Verification,@SendingType,@SqlSet,@AccIdSet,@AccIdCount,@SourceType," +
                "@MobileTitle,@PubTitle,@EmailTitle,@SmsContent,@MobileContent,@PubContent,@EmailContent,@Remark,@ActiveStatus," +
                "@SendingDate,@RecurringSendingCron,@TargetMark,@TargetMarkId,@Operator);" +
                "select @@IDENTITY;");

            try
            {
                return DapperHelper.ExecuteScalar<int>(strSql.ToString(), new
                {
                    Verification = model.Verification,
                    SendingType = model.SendingType,
                    SqlSet = model.SqlSet,
                    AccIdSet = model.AccIdSet,
                    AccIdCount = model.AccIdCount,
                    SourceType = model.SourceType,
                    MobileTitle = model.MobileTitle,
                    PubTitle = model.PubTitle,
                    EmailTitle = model.EmailTitle,
                    SmsContent = model.SmsContent,
                    MobileContent = model.MobileContent,
                    PubContent = model.PubContent,
                    EmailContent = model.EmailContent,
                    Remark = model.Remark,
                    ActiveStatus = model.ActiveStatus,
                    SendingDate = model.SendingDate,
                    RecurringSendingCron = model.RecurringSendingCron,
                    TargetMark = model.TargetMark,
                    TargetMarkId = model.TargetMarkId,
                    Operator = model.Operator
                });
            }
            catch (Exception ex)
            {
                Logger.Error("添加新的后台消息发送任务设置出错！", ex);
                return 0;
            }
        }

        /// <summary>
        /// 更新后台消息发送任务设置的激活状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int SetTaskActive(int id)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update I200_Task.dbo.ConditionSetting set ActiveStatus=1 where Id=@id;");

            try
            {
                return DapperHelper.Execute(strSql.ToString(),new
                {
                    id=id
                });
            }
            catch (Exception ex)
            {
                Logger.Error("更新后台消息发送任务设置的状态时出错！", ex);
                return 0;
            }
        }

        /// <summary>
        /// 更新后台任务最近一次发送的批次ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public int SetBatchId(int id,string batchId)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update I200_Task.dbo.ConditionSetting set LatestBatchId=@batchId where Id=@id;");

            try
            {
                return DapperHelper.Execute(strSql.ToString(), new
                {
                    batchId=batchId,
                    id = id
                });
            }
            catch (Exception ex)
            {
                Logger.Error("更新" + id + "记录的最新发送批次Id出错！", ex);
                return 0;
            }
        }

        /// <summary>
        /// 从设置表里抽取并还原发送设置的内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ConditionSettingModel.SettingModel GetSendingTaskItem(int id)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select * from I200_Task.dbo.ConditionSetting where Id=@id;");

            try
            {
                return DapperHelper.GetModel<ConditionSettingModel.SettingModel>(strSql.ToString(), new
                {
                    id = id
                });
            }
            catch (Exception ex)
            {
                Logger.Error("从设置表获取Id为" + id + "的信息出错！", ex);
                return null;
            }
        }

        /// <summary>
        /// 通过设置Id获取循环任务的Sql
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetSqlJsonById(int id)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select SqlSet from i200_task.dbo.conditionsetting where Id=@id;");

            try
            {
                return DapperHelper.ExecuteScalar<string>(strSql.ToString(), new
                {
                    id = id
                });
            }
            catch (Exception ex)
            {
                Logger.Error("获取循环任务Sql出错！", ex);
                return "";
            }
        }
    }
}
