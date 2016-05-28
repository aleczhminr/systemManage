using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;
namespace BLL
{
    /// <summary>
    /// 前台分享任务列表
    /// </summary>
    public static class T_Task_JournalBLL
    {
        /// <summary>
        /// 处理任务
        /// <para>｛-1：出现异常，0：信息不存在，1：已经处理完成，2、处理完成｝</para>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>｛-1：出现异常，0：信息不存在，1：已经处理完成，2、处理完成｝</returns>
        public static int HandleTask(int id)
        {
            T_Task_JournalDAL dal = new T_Task_JournalDAL();
            return dal.HandleTask(id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool Delete(int id)
        {
            T_Task_JournalDAL dal = new T_Task_JournalDAL();
            return dal.Delete(id);
        }
        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="auditCon"></param>
        /// <returns></returns>
        public static bool TaskAuditOk(int id, string auditCon)
        {
            T_Task_JournalDAL dal = new T_Task_JournalDAL();
            return dal.TaskAuditOk(id, auditCon);
        }
        /// <summary>
        /// api 发送积分
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int ApiTaskProvideIntegral(int id)
        {
            T_Task_JournalDAL dal = new T_Task_JournalDAL();
            return dal.ApiTaskProvideIntegral(id);
        }
    }
}
