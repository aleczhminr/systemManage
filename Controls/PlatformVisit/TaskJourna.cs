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
    /// 前台分享记录
    /// </summary>
   public  class TaskJourna
    {
         /// <summary>
        /// 得到列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="type"></param>
        /// <param name="statTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetList(int pageIndex, int pageSize, DateTime? statTime = null, DateTime? endTime = null,int state=-1,int accid=0, string type = "")
        {
            List<DapperWhere> dapperWhere = new List<DapperWhere>();

            if (type != "")
            {
                dapperWhere.Add(new DapperWhere("t_explan", type, " t_explan like '%'+ @t_explan +'%' "));
            }
            if (statTime != null)
            {
                dapperWhere.Add(new DapperWhere("statTime", statTime, "CAST(t_time as date) >= @statTime"));
            }

            if (endTime != null)
            {
                dapperWhere.Add(new DapperWhere("endTime", endTime, "CAST(t_time as date)<=@endTime"));
            }

            if (state >= 0)
            {
                dapperWhere.Add(new DapperWhere("t_status", state));
            }
            if (accid > 0)
            {
                dapperWhere.Add(new DapperWhere("acc_id", accid));
            }

            Dictionary<string, object> list = new Dictionary<string, object>();

            if (pageSize < 1)
            {
                pageSize = 20;
            }

            int rowCount = 0;
            if (pageIndex == 1)
            {
                rowCount = BLL.Base.T_Task_JournalBaseBLL.GetCount(dapperWhere);
            }
            int maxPage = 0;
            if (rowCount > 0)
            {
                maxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(rowCount) / pageSize));
            }




            List<T_Task_JournalInfo> listModel = BLL.Base.T_Task_JournalBaseBLL.GetList<T_Task_JournalInfo>(pageIndex, pageSize," * ", dapperWhere, " id desc");

            List<int> accidList = new List<int>();
            foreach (T_Task_JournalInfo model in listModel)
            {
                accidList.Add(model.acc_id);
                model.t_StatusName = Enum.GetName(typeof(Model.Enum.Task_JournalEnum.status), model.t_status);
            }
            if (accidList.Count > 0)
            {
                Dictionary<int, string> accountName = BLL.T_AccountBLL.GetCompanyName(accidList.ToArray());

                foreach (T_Task_JournalInfo model in listModel)
                {
                    model.CompanyName = accountName[model.acc_id];
                }
            }

            list["rowCount"] = rowCount;
            list["maxPage"] = maxPage;
            list["pageIndex"] = pageIndex;
            list["listData"] = listModel; 
            return list;
        }
       /// <summary>
       /// 得到列表
       /// </summary>
       /// <param name="pageIndex"></param>
       /// <param name="pageSize"></param>
       /// <param name="type"></param>
       /// <param name="statTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       public static List<T_Task_Journal> GetList(int pageIndex, int pageSize, string type="", DateTime? statTime = null, DateTime? endTime = null)
       {
           List<DapperWhere> dapperWhere = new List<DapperWhere>();

           if (type != "")
           {
               dapperWhere.Add(new DapperWhere("t_explan", type));
           }
           if (statTime != null)
           {
               dapperWhere.Add(new DapperWhere("statTime", statTime, "CAST(t_time as date) >= @statTime"));
           }

           if (endTime != null)
           {
               dapperWhere.Add(new DapperWhere("endTime", endTime, "CAST(t_time as date)<=@endTime"));
           }

           return BLL.Base.T_Task_JournalBaseBLL.GetList(pageIndex, pageSize, dapperWhere, " id desc");
       }

       /// <summary>
       /// 处理任务
       /// <para>｛-1：出现异常，0：信息不存在，1：已经处理完成，2、处理完成｝</para>
       /// </summary>
       /// <param name="id"></param>
       /// <returns>｛-1：出现异常，0：信息不存在，1：已经处理完成，2、处理完成｝</returns>
       public static int HandleTask(int id)
       {
           return T_Task_JournalBLL.HandleTask(id);
       }
       /// <summary>
       /// 删除一条数据
       /// </summary>
       public static bool Delete(int id)
       {
           return T_Task_JournalBLL.Delete(id);
       }
        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="auditCon"></param>
        /// <returns></returns>
        public static int TaskAuditOk(int id, string auditCon)
        {
            if (id < 1)
            {
                return -1;
            }

            if (T_Task_JournalBLL.TaskAuditOk(id, auditCon))
            {
                T_Task_JournalBLL.ApiTaskProvideIntegral(id);
                return 1;
            }
            else
            {
                return 0;
            }
        }

    }
}
