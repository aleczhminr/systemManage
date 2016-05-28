using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Model;

namespace DAL
{
    public class Sys_ManageLogDAL
    {
        /// <summary>
        /// 记录登录日志
        /// </summary>
        /// <param name="UserID">ID</param>
        /// <param name="sIP"></param>
        /// <param name="sBrowser"></param>
        public void LoginLog(int UserID, string sIP, string sBrowser)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Sys_Manage_User set LastLoginTime=getdate(),loginCounter=isnull(loginCounter,0)+1  where id=@UserID;");
            strSql.Append("insert into Sys_ManageLog (logtype,operdate,managerid,loginfo,logmode,typeid,Loginbrslast,ip) values ");
            strSql.Append("(1,getdate(),@UserID,'后台用户登录',0,@UserID,@sBrowser,@sIP);");

            int iResult = DapperHelper.Execute(strSql.ToString(), new { UserID = UserID, sIP = sIP, sBrowser = sBrowser });
        }

        /// <summary>
        /// 返回分页的登录日志
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<dynamic> GetLog(int pageIndex, int pageSize, string where)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append(" select count(*) as cnt from Sys_ManageLog ");
            //if (where.Length > 0)
            //{
            //    strSql.Append("where " + where + "");
            //}
            //strSql.Append("; ");

            strSql.Append(" select top (@pageSize) * from Sys_ManageLog   ");
            strSql.Append(" left join Sys_Manage_User on  Sys_ManageLog.managerid=Sys_Manage_User.id where   ");
            if (where.Length > 0)
            {
                strSql.Append("  " + where + " and ");
            }
            strSql.Append(" Sys_ManageLog.id not in (  ");
            strSql.Append("  select top  (@pageSize * @pageIndex)  id from Sys_ManageLog   ");
            if (where.Length > 0)
            {
                strSql.Append("  where " + where + "   ");
            }
            strSql.Append("  order by operdate desc) order by operdate desc  ");

            return DapperHelper.Query<dynamic>(strSql.ToString(), new {pageIndex = pageIndex, pageSize = pageSize}).ToList();

        }

        public int GetLogListCount(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select count(*) as cnt from Sys_ManageLog ");
            if (where.Length > 0)
            {
                strSql.Append("where " + where + "");
            }
            strSql.Append("; ");

            return DapperHelper.ExecuteScalar<int>(strSql.ToString());
        }

    }
}
