using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace DAL
{
    /// <summary>
    /// 店铺回访信息
    /// </summary>
    public class Sys_VisitInfoDAL : Base.Sys_VisitInfoBaseDAL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public new  int Add(Sys_VisitInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Sys_VisitInfo(accid,rt_Maxid,vm_id,initiative,vi_Content,handleStat,insertName,insertTime,Contact,remark,vr_Maxid,vr_Minid,vr_Threeid) ");
            strSql.Append(" values(@accid,@rt_Maxid,@vm_id,@initiative,@vi_Content,@handleStat,@insertName,@insertTime,@Contact,@remark,@vr_Maxid,@vr_Minid,@vr_Threeid) ;");
            strSql.Append("select @@IDENTITY;");
            object ro = DapperHelper.ExecuteScalar(strSql.ToString(), model);
            if (ro != null)
            {
                return Convert.ToInt32(ro);
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 得到列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="dapperWheres"></param>
        /// <param name="filedOrder"></param>
        /// <returns></returns>
        public new List<Sys_VisitInfoBase> GetList(int pageIndex, int pageSize, List<DapperWhere> dapperWheres, string filedOrder)
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
            strSql.Append(" SELECT id,accid,rt_Maxid,vm_id,vi_Content,handleStat,insertName,insertTime into #list FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER ( ");
            if (filedOrder.Length > 0)
            {
                strSql.Append(" order by " + filedOrder + " ");
            }
            else
            {
                strSql.Append(" order by T.id  desc");
            }
            strSql.Append(" )AS Row, * from Sys_VisitInfo T ");

            if (where.Length > 0)
            {
                strSql.Append(" where " + where + " ");
            }
            strSql.Append(" ) TT ");
            strSql.Append(" WHERE TT.Row between @startIndex and @endIndex; ");

            strSql.Append(" select #list.*,CompanyName,a.UserRealName,a.LoginLast,a.Edit,da.t_mk from #list left outer join ");
            strSql.Append(" (select ta.ID,ta.CompanyName,ta.UserRealName,datediff(dd,LoginTimeLast,getdate()) as LoginLast,tb.aotjb as Edit from i200.dbo.T_Account ta left join i200.dbo.T_Business tb on ta.id=tb.accountid where ta.ID in(select accid from #list)) a on a.ID=#list.accid " +
                          "left join [Sys_I200].[dbo].[Sys_TaskDaily] da on #list.id=da.vi_id;");
            strSql.Append(" drop table #list; ");

            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            int bgNumber = ((pageIndex - 1) * pageSize) + 1;
            int edNumber = (pageIndex) * pageSize;
            parm["startIndex"] = bgNumber;
            parm["endIndex"] = edNumber;

            return DapperHelper.Query<Sys_VisitInfoBase>(strSql.ToString(), parm).ToList();
        }

        /// <summary>
        /// 得到一个详情的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysVisitParticularModel GetParticularModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,accid,rt_Maxid,vm_id,vi_Content,handleStat,insertName,insertTime,Contact,remark,vr_Maxid,vr_Minid,vr_Threeid from Sys_VisitInfo where id=@id;");
            SysVisitParticularModel model= DapperHelper.GetModel<SysVisitParticularModel>(strSql.ToString(), new { id = id });
            return model;

        }

        /// <summary>
        /// 得到单个店铺需要处理回访
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public List<SysNeedVisitModel> GetNeedVisitList(int accid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,accid,vi_Content,handleStat,insertTime,insertName,vr_UpDateTime into #list from Sys_VisitInfo where accid=@accid and handleStat!=1 order by insertTime desc,id desc;");
            strSql.Append(" select #list.*,CompanyName from #list left outer join  (select ID,CompanyName from i200.dbo.T_Account where ID in(select accid from #list)) a on a.ID=#list.accid; ");
            strSql.Append(" drop table #list; ");

            return DapperHelper.Query<SysNeedVisitModel>(strSql.ToString(), new { accid = accid }).ToList();
        }

        /// <summary>
        /// 更新回访状态
        /// </summary>
        /// <param name="vid"></param>
        /// <param name="handleStat"></param>
        /// <returns></returns>
        public bool UpdateVisitStat(int vid, int handleStat)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Sys_VisitInfo set handleStat=@stat,vr_UpDateTime=getdate() where id=@id;");
            int rNum = DapperHelper.Execute(strSql.ToString(), new { stat = handleStat, id = vid });
            if (rNum > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到事件列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="dapperWhere"></param>
        /// <param name="filedOrder"></param>
        /// <returns></returns>
        public List<SysCaseModel> GetCaseList(int pageIndex, int pageSize, List<DapperWhere> dapperWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            string where = "";
            Dictionary<string, object> parm = new Dictionary<string, object>();
            foreach (DapperWhere item in dapperWhere)
            {
                if (where.Length > 0)
                {
                    where += " and ";
                }
                where += item.Where;
                parm[item.ColumnName] = item.Value;
            }
            strSql.Append(" SELECT *  into #list FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER ( ");
            if (filedOrder.Length > 0)
            {
                strSql.Append(" order by " + filedOrder + " ");
            }
            else
            {
                strSql.Append(" order by T.id  desc");
            }
            strSql.Append(" )AS Row, id,accid,vr_Threeid,insertName,insertTime,handleStat,vr_UpDateTime from Sys_VisitInfo T  ");

            if (where.Length > 0)
            {
                strSql.Append(" where " + where + " ");
            }
            strSql.Append(" ) TT ");
            strSql.Append(" WHERE TT.Row between @startIndex and @endIndex; ");

            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            strSql.Append(" select #list.id,accid,vr_Threeid,insertName,insertTime,handleStat,vr_UpDateTime,CompanyName,visitReasonThree from #list left outer join ( ");
            strSql.Append(" select ID,CompanyName from i200.dbo.T_Account where ID in(select accid from #list)) a on a.ID=#list.accid ");
            strSql.Append(" left outer join ( ");
            strSql.Append(" select id,vr_name visitReasonThree from Sys_VisitReason where id in(select vr_Threeid from #list)) b on b.id=#list.vr_Threeid order by #list.Row asc; ");
            strSql.Append(" drop table #list; ");



            int bgNumber = ((pageIndex - 1) * pageSize) + 1;
            int edNumber = (pageIndex) * pageSize;
            parm["startIndex"] = bgNumber;
            parm["endIndex"] = edNumber;

            return DapperHelper.Query<SysCaseModel>(strSql.ToString(), parm).ToList();
        }

        public dynamic GetCaseClassAnalyze(int rank,DateTime startTime,DateTime endTime)
        {
            return null;
        }

        /// <summary>
        /// 获取某个店铺最后一次的回访
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public dynamic GetLastVisit(int accid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select top 1 accid,vm_id,insertTime,DATEDIFF(DAY,insertTime,GETDATE()) as diff from Sys_VisitInfo where accid=@accid and insertName<>'系统' order by " +
                "insertTime desc;");

            List<dynamic> data = DapperHelper.Query<dynamic>(strSql.ToString(), new { accid = accid }).ToList();
            if (data != null && data.Count>0)
            {
                return data[0];
            }
            else
            {
                return null;
            }
        }
    }
}
