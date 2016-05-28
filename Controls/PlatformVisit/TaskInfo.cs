using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using BLL;
namespace Controls.PlatformVisit
{
    /// <summary>
    /// 任务信息
    /// </summary>
    public static class TaskInfo
    {
        /// <summary>
        /// 得到列表信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="status">状态</param>
        /// <param name="startlevel">开始重要性</param>
        /// <param name="endlevel">结束重要性</param>
        /// <param name="source">来源</param>
        /// <returns></returns>
        public static Dictionary<string, object> GetList(int pageIndex, int pageSize, int status, int startlevel = 0, int endlevel = 0, string source = "")
        {
            Dictionary<string, object> list = new Dictionary<string, object>();
            List<DapperWhere> dapperWheres = new List<DapperWhere>();

            dapperWheres.Add(new DapperWhere("dt_Status", status));
            if (startlevel > 0)
            {
                dapperWheres.Add(new DapperWhere("startLevel", startlevel, " dt_Level<@startLevel "));
            }
            if (endlevel > 0)
            {
                dapperWheres.Add(new DapperWhere("endLevel", endlevel, " dt_Level>=@endLevel "));
            }
            if (source.Length > 0)
            {
                dapperWheres.Add(new DapperWhere("dt_Source", source));
            }

            if (pageSize < 1)
            {
                pageSize = 20;
            }

            int rowCount = 0;
            if (pageIndex == 1)
            {
                rowCount = BLL.Base.Sys_TaskDailyBaseBLL.GetCount(dapperWheres);
            }
            int maxPage = 0;
            if (rowCount > 0)
            {
                maxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(rowCount) / pageSize));
            }

            List<Sys_TaskDailyInfo> data = Sys_TaskDailyBLL.GetList(pageIndex, pageSize, dapperWheres,
                " dt_Level asc,id desc");

            foreach (var item in data)
            {
                if (item.dt_remark.IndexOf('@') == 0)
                {
                    item.forumUrl = item.dt_remark.Substring(item.dt_remark.IndexOf('(') + 1,
                        item.dt_remark.IndexOf(')') - item.dt_remark.IndexOf('(') - 1);
                }
            }

            list["rowCount"] = rowCount;
            list["maxPage"] = maxPage;
            list["pageIndex"] = pageIndex;
            list["listData"] = data;

            return list;
        }
        /// <summary>
        /// 根据ID 得到精版信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Sys_TaskDailySimplify GetSimplifyModel(int id)
        {
            return BLL.Sys_TaskDailyBLL.GetSimplifyModel(id);
        }

        /// <summary>
        /// 获取首页显示的五条回访数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="status"></param>
        /// <param name="startlevel">小于 30 为非常紧急</param>
        /// <returns></returns>
        public static List<Sys_TaskDailyInfo> IndexDailyTaskInfo(int pageIndex = 1, int pageSize = 5, int status = 0, int startlevel = 30)
        {
            List<DapperWhere> dapperWheres = new List<DapperWhere>();

            dapperWheres.Add(new DapperWhere("dt_Status", status));
            if (startlevel > 0)
            {
                dapperWheres.Add(new DapperWhere("startLevel", startlevel, " dt_Level<@startLevel "));
            }

            List<Sys_TaskDailyInfo> list = Sys_TaskDailyBLL.GetList(pageIndex, pageSize, dapperWheres, " inertTime desc");

            foreach (var item in list)
            {
                if (item.dt_remark.IndexOf('@') == 0)
                {
                    item.forumUrl = item.dt_remark.Substring(item.dt_remark.IndexOf('(') + 1,
                        item.dt_remark.IndexOf(')') - item.dt_remark.IndexOf('(') - 1);
                }
            }

            return list;
        }

        public static Dictionary<string, string> GetFeedbackList(int pageIndex, int accId, DateTime start, DateTime end, int visitStatus, int feedType, string content)
        {
            return Sys_TaskDailyBLL.GetFeedbackList(pageIndex, accId, start, end, visitStatus, feedType, content);
        }

        public static Dictionary<string, string> GetFeedbackSummary(DateTime stDate, DateTime edDate)
        {
            return Sys_TaskDailyBLL.GetFeedbackSummary(stDate, edDate);
        }

        public static string GetShopSimpleVisitTasks(int accId)
        {
            var model = Sys_TaskDailyBLL.GetShopSimpleVisitTasks(accId);
            if (model != null)
            {
                return CommonLib.Helper.JsonSerializeObject(model);
            }
            else
            {
                return "";
            }
        }

        public static string GetForumFeedBack(int accId)
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();

            List<Sys_TaskDailyInfo> data = Sys_TaskDailyBLL.GetUserForumFeedBack(accId);

            foreach (var item in data)
            {
                Dictionary<string, string> dicData = new Dictionary<string, string>()
                {
                    {"content",""},
                    {"time",""},
                    {"url",""}
                };

                dicData["content"] = item.t_mk;
                dicData["time"] = item.inertTime.ToString();
                dicData["url"] = item.dt_remark.Substring(item.dt_remark.IndexOf('(') + 1,
                    item.dt_remark.IndexOf(')') - item.dt_remark.IndexOf('(') - 1);

                list.Add(dicData);
            }

            return CommonLib.Helper.JsonSerializeObject(list, "yyyy-MM-dd HH:mm:ss");
        }
    }
}
