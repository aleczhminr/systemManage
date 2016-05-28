using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DAL
{

    /// <summary>
    /// 日常回访任务
    /// </summary>
    public class Sys_TaskDailyDAL : Base.Sys_TaskDailyBaseDAL
    {

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="pageSize">每页面显示数</param>
        /// <param name="dapperWheres">条件</param>
        /// <param name="filedOrder">排序</param>
        /// <returns></returns>
        public new List<Sys_TaskDailyInfo> GetList(int pageIndex, int pageSize, List<DapperWhere> dapperWheres, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            string where = "";
            Dictionary<string, object> parm = new Dictionary<string, object>();
            foreach (DapperWhere item in dapperWheres)
            {
                if (where.Length > 0)
                {
                    where += " and ";
                }
                where += item.Where;
                parm[item.ColumnName] = item.Value;
            }
            strSql.Append(" SELECT * into #list FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER ( ");
            if (filedOrder.Length > 0)
            {
                strSql.Append(" order by " + filedOrder + " ");
            }
            else
            {
                strSql.Append(" order by T.id  desc");
            }
            strSql.Append(" )AS Row, * from Sys_TaskDaily T  ");

            if (where.Length > 0)
            {
                strSql.Append(" where " + where + " ");
            }
            strSql.Append(" ) TT ");
            strSql.Append(" WHERE TT.Row between @startIndex and @endIndex; ");

            strSql.Append(" select #list.*,CompanyName from #list left outer join (select ID,CompanyName from i200.dbo.T_Account where ID in(select accountid from #list)) a on a.ID=#list.accountid; ");
            strSql.Append(" drop table #list; ");

            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            int bgNumber = ((pageIndex - 1) * pageSize) + 1;
            int edNumber = (pageIndex) * pageSize;
            parm["startIndex"] = bgNumber;
            parm["endIndex"] = edNumber;

            return DapperHelper.Query<Sys_TaskDailyInfo>(strSql.ToString(), parm).ToList();

        }

        /// <summary>
        /// 更新为处理成功
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <param name="treatName">处理人</param>
        /// <param name="uid">回访记录ID</param>
        /// <returns></returns>
        public bool UpdateWorkOut(int id, string treatName, int visitId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update Sys_TaskDaily set vi_id=@visitId,dt_Status=3,dt_Name=@treatName where id=@id;  ");
            strSql.Append(" insert into Sys_TaskRecord(treatTime,treatName,vi_Id,dt_remark,dt_logUid,dt_Source,accountid,t_mk,vm_id,dt_Time,dt_Level,dt_Status,inertTime,insetName)  ");
            strSql.Append(" select GETDATE(),@treatName,@visitId ,dt_remark, dt_logUid,dt_Source,accountid,t_mk,vm_id, dt_Time,dt_Level,dt_Status, inertTime, insetName from   ");
            strSql.Append(" Sys_TaskDaily where id=@id and dt_Status=3;  ");
            int r = DapperHelper.Execute(strSql.ToString(), new { id = id, treatName = treatName, visitId = visitId });
            if (r > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据ID 得到一个 精简 的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Sys_TaskDailySimplify GetSimplifyModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id,t_mk,dt_Level,insetName,dt_Source,dt_Status from dbo.Sys_TaskDaily where id=@id;");
            return DapperHelper.GetModel<Sys_TaskDailySimplify>(strSql.ToString(), new { id = id });
        }

        /// <summary>
        /// 获取反馈列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetFeedbackList(int pageIndex, int accId, DateTime start, DateTime end, int visitStatus, int feedType, string content)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>(
                )
            {
                { "pageIndex", "" },
                {"rowCount",""},
                {"maxPage",""},
                {"listData",""}

            };

            StringBuilder strSql = new StringBuilder();

            int pageSize = 15;

            int bgNumber = ((pageIndex - 1) * pageSize) + 1;
            int edNumber = (pageIndex) * pageSize;

            string strWhere = "";
            if (visitStatus != -99)
            {
                if (visitStatus == 0)
                {
                    strWhere += " and (vi_id is null or vi_id=0) ";
                }
                else
                {
                    strWhere += " and vi_id is not null and vi_id<>0 ";
                }

            }
            if (feedType != -99)
            {
                string source = "";
                switch (feedType)
                {
                    case 0:
                        source = "IOS";
                        break;
                    case 1:
                        source = "前台反馈";
                        break;
                    case 2:
                        source = "安卓";
                        break;
                    case 3:
                        source = "系统";
                        break;
                    case 4:
                        source = "iPad";
                        break;
                    case 5:
                        source = "论坛反馈";
                        break;
                    case 6:
                        source = "客服反馈";
                        break;
                    case 7:
                        source = "推广建议";
                        break;
                    case 8:
                        source = "商城反馈";
                        break;
                }
                strWhere += " and dt_Source = '" + source + "' ";
            }
            else
            {
                strWhere += " and dt_Source <> '系统' ";
            }
            if (content != "")
            {
                strWhere += " and t_mk like '%" + content + "%' ";
            }

            if (start.ToShortDateString() != end.ToShortDateString())
            {
                strWhere += " and inertTime between '" + start.ToShortDateString() + "' and '" + end.ToShortDateString() +
                            "' ";
            }

            if (accId != 0)
            {
                strWhere += " and td.accountid =" + accId + " ";
            }

            if (strWhere != "")
            {
                strWhere = "where 1=1 " + strWhere;
            }


            strSql.Append("select * from " +
                          "(select ROW_NUMBER() over (order by inertTime desc) rowNumber,td.id,td.RequirementId,td.accountid,ta.companyName,t_mk,inertTime,dt_remark,dt_logUid,dt_Source,vi_id,ta.UserRealName,datediff(dd,ta.LoginTimeLast,getdate()) as LoginLast,tb.aotjb Edit,rm.Status,rm.Description " +

                          " from Sys_TaskDaily td left join RequirementManage rm on td.RequirementId=rm.Id left join i200.dbo.T_Account ta on td.accountid=ta.id left join i200.dbo.T_Business tb on td.accountid=tb.accountid " + (strWhere == "" ? "" : strWhere) + ") t where t.rowNumber between @bgNumber and @edNumber;");

            List<FeedbackModel> list = new List<FeedbackModel>();
            try
            {
                list =
                DapperHelper.Query<FeedbackModel>(strSql.ToString(), new { bgNumber = bgNumber, edNumber = edNumber })
                    .ToList();

                foreach (var item in list)
                {
                    if (item.dt_remark.IndexOf('@') == 0)
                    {
                        item.forumUrl = item.dt_remark.Substring(item.dt_remark.IndexOf('(')+1,
                            item.dt_remark.IndexOf(')') - item.dt_remark.IndexOf('(')-1);
                    }

                    item.t_mk = item.t_mk.Replace("\n", "");
                }
            }
            catch (Exception ex)
            {
                return null;
            }


            strSql.Clear();
            strSql.Append("select count(*) from Sys_TaskDaily td " + (strWhere == "" ? "" : strWhere) + ";");
            int rowCount = DapperHelper.ExecuteScalar<int>(strSql.ToString());

            int maxPage = (rowCount % 15 == 0) ? (rowCount / 15) : (rowCount / 15 + 1);

            dic["rowCount"] = rowCount.ToString();
            dic["maxPage"] = maxPage.ToString();
            dic["pageIndex"] = pageIndex.ToString();
            dic["listData"] = CommonLib.Helper.JsonSerializeObject(list, "yyyy-MM-dd");

            return dic;
        }

        public Dictionary<string, string> GetFeedbackSummary(DateTime stDate, DateTime edDate)
        {
            string strWhere = "";
            if (stDate.ToShortDateString() != edDate.ToShortDateString())
            {
                strWhere += " and inertTime between '" + stDate.ToShortDateString() + "' and '" + edDate.ToShortDateString() +
                            "' ";
            }

            StringBuilder strSql = new StringBuilder();
            Dictionary<string, string> dic = new Dictionary<string, string>()
            {
                {"IOS",""},
                {"安卓",""},
                {"系统",""},
                {"前台反馈",""},
                {"IOS比例",""},
                {"安卓比例",""},
                {"系统比例",""},
                {"前台反馈比例",""}
            };

            strSql.Append("declare @iOS int,@Android int,@Sys int,@Web int;");

            if (strWhere != "")
            {
                strSql.Append("select @iOS=count(*) from Sys_TaskDaily where dt_Source='IOS'" + strWhere + ";");
                strSql.Append("select @Android=count(*) from Sys_TaskDaily where dt_Source='安卓'" + strWhere + ";");
                strSql.Append("select @Sys=count(*) from Sys_TaskDaily where dt_Source='系统'" + strWhere + ";");
                strSql.Append("select @Web=count(*) from Sys_TaskDaily where dt_Source='前台反馈'" + strWhere + ";");
            }
            else
            {
                strSql.Append("select @iOS=count(*) from Sys_TaskDaily where dt_Source='IOS';");
                strSql.Append("select @Android=count(*) from Sys_TaskDaily where dt_Source='安卓';");
                strSql.Append("select @Sys=count(*) from Sys_TaskDaily where dt_Source='系统';");
                strSql.Append("select @Web=count(*) from Sys_TaskDaily where dt_Source='前台反馈';");
            }

            strSql.Append("select @iOS ios,@Android android,@Sys syst ,@Web web;");

            dynamic data = DapperHelper.Query<dynamic>(strSql.ToString()).ToList()[0];

            dic["IOS"] = data.ios.ToString();
            dic["安卓"] = data.android.ToString();
            dic["系统"] = data.syst.ToString();
            dic["前台反馈"] = data.web.ToString();

            decimal all = data.ios + data.android + data.syst + data.web;

            if (all != 0)
            {
                dic["IOS比例"] = (Convert.ToDecimal(data.ios) * 100 / all).ToString("f2") + "%";
                dic["安卓比例"] = (Convert.ToDecimal(data.android) * 100 / all).ToString("f2") + "%";
                dic["系统比例"] = (Convert.ToDecimal(data.syst) * 100 / all).ToString("f2") + "%";
                dic["前台反馈比例"] = (Convert.ToDecimal(data.web) * 100 / all).ToString("f2") + "%";
            }

            return dic;
        }

        /// <summary>
        /// 店铺详情页面获取未处理的回访信息
        /// </summary>
        /// <param name="accId"></param>
        /// <returns></returns>
        public List<SimpleVisitTask> GetVisitSimpleInfo(int accId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select accountid,id,t_mk,dt_remark from Sys_TaskDaily where dt_status=0 and accountid=" + accId);

            try
            {
                var model = DapperHelper.Query<SimpleVisitTask>(strSql.ToString()).ToList();
                return model;
            }
            catch (Exception ex)
            {
                Logger.Error("获取店铺" + accId + "待回访信息出错", ex);
                return null;
            }
        }

        public int AddModel(Sys_TaskDailyInfo model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("insert into Sys_TaskDaily(");
            strSql.Append("insetName,dt_remark,dt_logUid,dt_Source,accountid,t_mk,dt_Time,dt_Level,dt_Status,inertTime");
            strSql.Append(") values (");
            strSql.Append("@insetName,@dt_remark,@dt_logUid,@dt_Source,@accountid,@t_mk,@dt_Time,@dt_Level,@dt_Status,@inertTime");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");

            return DapperHelper.ExecuteScalar<int>(strSql.ToString(), new
            {
                insetName = model.insetName,
                dt_remark = model.dt_remark,
                dt_logUid = model.dt_logUid,
                dt_Source = model.dt_Source,
                accountid = model.accountid,
                t_mk = model.t_mk,
                dt_Time = model.dt_Time,
                dt_Level = model.dt_Level,
                dt_Status = model.dt_Status,
                inertTime = model.inertTime
            });
        }

        public bool CheckForumUrl(string url)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("if exists(select * from Sys_TaskDaily where dt_remark like '%" + "@(" + url + ")" + "%') " +
                          "select 1;" +
                          "else " +
                          "select 0;");

            return DapperHelper.ExecuteScalar<int>(strSql.ToString()) == 1 ? true : false;
        }

        public bool CheckTaskDailyExist(string content)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("if exists(select * from Sys_TaskDaily where t_mk='" + content + "') " +
                          "select 1;" +
                          "else " +
                          "select 0;");

            return DapperHelper.ExecuteScalar<int>(strSql.ToString()) == 1 ? true : false;

        }

        /// <summary>
        /// 获取单个用户的论坛反馈信息
        /// </summary>
        /// <param name="accId"></param>
        /// <returns></returns>
        public List<Sys_TaskDailyInfo> GetUserForumFeedBack(int accId)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select * from Sys_TaskDaily where dt_Source='论坛反馈' and accountid=" + accId + " order by inertTime desc;");

            return DapperHelper.Query<Sys_TaskDailyInfo>(strSql.ToString()).ToList();
        }

        /// <summary>
        /// 更新反馈表关联的需求ID
        /// </summary>
        /// <param name="reqId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public int UpdateReqId(int reqId,int taskId)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update Sys_TaskDaily set RequirementId=@reqId where id=@taskId;");

            return DapperHelper.Execute(strSql.ToString(), new
            {
                reqId = reqId,
                taskId = taskId
            });
        }
    }
}
