using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public static class ConditionSettingBLL
    {

        public static int InitialConditionSetting(int uid, string accIdList, int accIdCount, string verif)
        {
            ConditionSettingDAL dal = new ConditionSettingDAL();
            return dal.InitialConditionRecord(uid, accIdList,accIdCount, verif);
        }

        public static List<ConditionSettingModel.ConditionRecoveryModel> GetRuleCondition(string verif)
        {
            ConditionSettingDAL dal = new ConditionSettingDAL();
            return dal.GetRuleCondition(verif);
        }

        /// <summary>
        /// 添加后台消息发送任务设置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddContitionSetting(ConditionSettingModel.SettingModel model)
        {
            ConditionSettingDAL dal = new ConditionSettingDAL();
            return dal.AddContitionSetting(model);
        }

        /// <summary>
        /// 更新后台任务最近一次发送的批次Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public static int SetBatchId(int id, string batchId)
        {
            ConditionSettingDAL dal = new ConditionSettingDAL();
            return dal.SetBatchId(id, batchId);
        }

        /// <summary>
        /// 更新后台消息发送任务设置的激活状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int SetTaskActive(int id)
        {
            ConditionSettingDAL dal = new ConditionSettingDAL();
            return dal.SetTaskActive(id);
        }

        /// <summary>
        /// 从设置列表获取定时任务
        /// </summary>
        /// <param name="settingId"></param>
        /// <returns></returns>
        public static ConditionSettingModel.SettingModel GetSendingTaskItem(int settingId)
        {
            ConditionSettingDAL dal = new ConditionSettingDAL();
            return dal.GetSendingTaskItem(settingId);
        }

        /// <summary>
        /// 通过设置Id获取循环任务的Sql语句
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetSqlJsonById(int id)
        {
            ConditionSettingDAL dal = new ConditionSettingDAL();
            return dal.GetSqlJsonById(id);
        }
    }
}
