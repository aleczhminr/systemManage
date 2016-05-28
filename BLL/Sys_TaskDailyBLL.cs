using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    /// <summary>
    /// 日常回访任务
    /// </summary>
   public static class Sys_TaskDailyBLL
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="pageSize">每页面显示数</param>
        /// <param name="dapperWheres">条件</param>
        /// <param name="filedOrder">排序</param>
        /// <returns></returns>
       public static List<Sys_TaskDailyInfo> GetList(int pageIndex, int pageSize, List<DapperWhere> dapperWheres, string filedOrder)
       {
           Sys_TaskDailyDAL dal = new Sys_TaskDailyDAL();
           return dal.GetList(pageIndex, pageSize, dapperWheres, filedOrder);
       }

        public static List<Sys_TaskDailyInfo> GetUserForumFeedBack(int accId)
        {
            Sys_TaskDailyDAL dal = new Sys_TaskDailyDAL();
            return dal.GetUserForumFeedBack(accId);
        }

        /// <summary>
       /// 更新为处理成功
       /// </summary>
       /// <param name="id">任务ID</param>
       /// <param name="treatName">处理人</param>
       /// <param name="uid">回访记录ID</param>
       /// <returns></returns>
       public static bool UpdateWorkOut(int id, string treatName, int visitId)
       {
           Sys_TaskDailyDAL dal = new Sys_TaskDailyDAL();
           return dal.UpdateWorkOut(id, treatName, visitId);
       }

        /// <summary>
        /// 根据ID 得到一个 精简 的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       public static Sys_TaskDailySimplify GetSimplifyModel(int id)
       {
           Sys_TaskDailyDAL dal = new Sys_TaskDailyDAL();
           return dal.GetSimplifyModel(id);
       }

       public static Dictionary<string, string> GetFeedbackList(int pageIndex, int accId, DateTime start, DateTime end, int visitStatus, int feedType, string content)
        {
            Sys_TaskDailyDAL dal = new Sys_TaskDailyDAL();
            return dal.GetFeedbackList(pageIndex, accId, start, end, visitStatus, feedType, content);
        }

        public static Dictionary<string, string> GetFeedbackSummary(DateTime stDate,DateTime edDate)
        {
            Sys_TaskDailyDAL dal = new Sys_TaskDailyDAL();
            return dal.GetFeedbackSummary(stDate,edDate);
        }

        public static List<SimpleVisitTask> GetShopSimpleVisitTasks(int accId)
        {
            Sys_TaskDailyDAL dal = new Sys_TaskDailyDAL();
            return dal.GetVisitSimpleInfo(accId);
        }

        public static int AddModel(Sys_TaskDailyInfo model)
        {
            Sys_TaskDailyDAL dal = new Sys_TaskDailyDAL();
            return dal.AddModel(model);
        }

        public static bool CheckForumUrl(string url)
        {
            Sys_TaskDailyDAL dal = new Sys_TaskDailyDAL();
            return dal.CheckForumUrl(url);
        }

        public static bool CheckTaskDailyExist(string content)
        {
            Sys_TaskDailyDAL dal = new Sys_TaskDailyDAL();
            return dal.CheckTaskDailyExist(content);
        }

        /// <summary>
       /// 更新反馈表关联的需求ID
       /// </summary>
       /// <param name="reqId"></param>
       /// <param name="taskId"></param>
       /// <returns></returns>
        public static int UpdateReqId(int reqId, int taskId)
        {
            Sys_TaskDailyDAL dal = new Sys_TaskDailyDAL();
            return dal.UpdateReqId(reqId, taskId);
        }
    }
}
