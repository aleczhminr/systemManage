using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using Model;

namespace Controls.HangFire
{
    /// <summary>
    /// 处理发送条件的公用类
    /// </summary>
    public static class ConditionHandler
    {
        /// <summary>
        /// 初始化条件设定表记录
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="accIdList"></param>
        /// <param name="verif"></param>
        /// <returns></returns>
        public static int InitialConditionRecord(int uid, string accIdList, int accIdCount, string verif)
        {
            return ConditionSettingBLL.InitialConditionSetting(uid, accIdList,accIdCount, verif);
        }

        #region 针对有筛选器初始条件的消息处理
        /// <summary>
        /// 根据筛选器还原条件到页面
        /// </summary>
        /// <returns></returns>
        public static string RecoverDisplayCondition(string verif)
        {
            ConditionSettingModel.ShowModelList modelList = new ConditionSettingModel.ShowModelList();

            List<ConditionSettingModel.ConditionRecoveryModel> list = ConditionSettingBLL.GetRuleCondition(verif);

            for (int i = 1; i < 7; i++)
            {
                List<ConditionSettingModel.ConditionRecoveryModel> tmpList =
                    list.FindAll(x => x.ConditionGroup == i);

                if (tmpList != null && tmpList.Count > 0)
                {
                    ConditionSettingModel.ShowModel tmpModel = new ConditionSettingModel.ShowModel();
                    tmpModel.ConditionGroup = i;

                    switch (i)
                    {
                        case 1:
                            tmpModel.GroupDesc = "属性信息";
                            break;
                        case 2:
                            tmpModel.GroupDesc = "订单信息";
                            break;
                        case 3:
                            tmpModel.GroupDesc = "业务信息";
                            break;
                        case 4:
                            tmpModel.GroupDesc = "登录信息";
                            break;
                        case 5:
                            tmpModel.GroupDesc = "基础数据信息";
                            break;
                        case 6:
                            tmpModel.GroupDesc = "用户标签";
                            break;
                        default:
                            tmpModel.GroupDesc = "其他信息";
                            break;
                    }

                    tmpModel.GroupList = tmpList;

                    modelList.DataList.Add(tmpModel);
                }
            }

            return CommonLib.Helper.JsonSerializeObject(modelList);
        }

        /// <summary>
        /// 添加后台消息发送任务设置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddTaskSetting(ConditionSettingModel.SettingModel model)
        {
            return ConditionSettingBLL.AddContitionSetting(model);
        }

        /// <summary>
        /// 更新后台消息发送任务设置的激活状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int SetTaskActive(int id)
        {
            return ConditionSettingBLL.SetTaskActive(id);
        }

        /// <summary>
        /// 通过条件还原设定循环任务需要用到的SQL
        /// 在发送页面二次处理条件
        /// </summary>
        /// <returns></returns>
        public static string GenerateSqlByCondition(string postJson)
        {

            return "";
        }
        
        #endregion                

        /// <summary>
        /// 设定追踪动作
        /// </summary>
        /// <returns></returns>
        public static string SetTraceTargetCondition()
        {
            return "";
        }


    }
}
