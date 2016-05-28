using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// 推广链接
    /// </summary>
    public class T_OutLinkDAL : Base.T_OutLinkBaseDAL
    {

        /// <summary>
        /// 分页得到列表
        /// </summary>
        /// <param name="pageIndex">显示页号</param>
        /// <param name="pageSize">每页显示数</param>
        /// <param name="dapperWheres">条件列表</param>
        /// <param name="filedOrder">排序</param>
        /// <returns>返回列表</returns>
        public new Dictionary<string,string> GetList(int pageIndex, int pageSize, List<DapperWhere> dapperWheres, string filedOrder)
        {
            Dictionary<string, string> dataDic = new Dictionary<string, string>()
            {
                {"dataList",""},
                {"maxPage",""},
                {"rowCount",""}
            };

            StringBuilder strSqlCount = new StringBuilder();
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

            strSqlCount.Append(" select count(T_OutLink.id) from I200.dbo.T_OutLink  ");
            strSqlCount.Append(" left join Sys_Manage_User on   T_OutLink.managerid=Sys_Manage_User.id  ");

            if (where.Length > 0)
            {
                strSqlCount.Append(" where " + where + " ; ");
            }

            int rowCount = DapperHelper.ExecuteScalar<int>(strSqlCount.ToString(), parm);
            dataDic["rowCount"] = rowCount.ToString();

            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            parm["pageSize"] = pageSize;
            parm["pageIndex"] = pageIndex - 1;         

            if (rowCount % 15 != 0)
            {
                dataDic["maxPage"] = (rowCount / 15 + 1).ToString();
            }
            else
            {
                dataDic["maxPage"] = (rowCount / 15).ToString();
            }

            strSql.Append(" select top (@pageSize) T_OutLink.id,linkurl,remark,ClickCount,CreateTime,t_outlink.state,linkname,ShortUrl,name manageName,ot_name1 maxClassName,ot_name2 minClassName from I200.dbo.T_OutLink  ");
            strSql.Append(" left join Sys_Manage_User on T_OutLink.managerid=Sys_Manage_User.id  ");
            strSql.Append(" left outer join ( ");
            strSql.Append(" select b.id id1,b.ot_name ot_name1,a.id id2,a.ot_name ot_name2  ");
            strSql.Append(" from I200.dbo.T_outlinktype a  ");
            strSql.Append(" inner join ");
            strSql.Append(" (select id,ot_name from I200.dbo.T_OutLinkType where ot_id=0) b  ");
            strSql.Append(" on b.id=a.ot_id) c on c.id2=T_OutLink.linktype where  ");
            if (where.Length > 0)
            {
                strSql.Append(" " + where + " and  ");
            }
            strSql.Append(" T_OutLink.id not in ( ");
            strSql.Append(" select top (@pageSize * @pageIndex) id  ");
            strSql.Append(" from I200.dbo.T_OutLink  ");
            if (where.Length > 0)
            {
                strSql.Append(" where " + where + "  ");
            }
            strSql.Append(" order by createtime desc) order by createtime desc  ");

            dataDic["dataList"] =
                CommonLib.Helper.JsonSerializeObject(DapperHelper.Query<T_OutLinkInfo>(strSql.ToString(), parm).ToList(),"yyyy-MM-dd HH:mm:ss");

            return dataDic;
        }

        /// <summary>
        /// 新增加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(T_OutLink model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_OutLink(");
            strSql.Append("linkClass,PV,EndTime,ShortUrl,linktype,linkurl,remark,ClickCount,CreateTime,managerid,state,linkname");
            strSql.Append(") values (");
            strSql.Append("@linkClass,@PV,@EndTime,@ShortUrl,@linktype,@linkurl,@remark,@ClickCount,@CreateTime,@managerid,@state,@linkname");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            object obj = HelperForFrontend.ExecuteScalar(strSql.ToString(), model);
            if (obj != null)
            {
                return Convert.ToInt32(obj);
            }
            else
            {
                return 0;
            }
        }
    }
}
