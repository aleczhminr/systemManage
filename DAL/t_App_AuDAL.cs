using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace DAL
{
    /// <summary>
    /// 店铺权限
    /// </summary>
    public class t_App_AuDAL:Base.t_App_AuBaseDAL
    {
        /// <summary>
        /// 根据店铺ID得到店铺权限列表
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public List<AppInfoModel> GetListByAccId(int accid)
        {
            List<DapperWhere> where = new List<DapperWhere>();
            where.Add(new DapperWhere("accid", accid));
            string column = "id, accid, appkey, appName, stattime StartDate, endtime EndDate, aa_time OperatorDate, aa_remark Remark, aa_ShortUrl ShortUrl, aa_Status Status";

            return GetNewList<AppInfoModel>(0, column, where, " appkey asc, id desc");
        }
        /// <summary>
        /// 从主站直接获取最新信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="top"></param>
        /// <param name="columnName"></param>
        /// <param name="dapperWheres"></param>
        /// <param name="filedOrder"></param>
        /// <returns></returns>
        public List<T> GetNewList<T>(int top, string columnName, List<DapperWhere> dapperWheres, string filedOrder)
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

            strSql.Append("select ");

            if (top > 0)
            {
                strSql.Append(" top " + top.ToString() + " ");
            }
            strSql.Append(" " + columnName + " ");
            strSql.Append(" FROM I200.dbo.t_App_Au ");
            if (where.Length > 0)
            {
                strSql.Append(" where " + where);
            }

            if (filedOrder.Length > 0)
            {
                strSql.Append(" order by " + filedOrder);
            }
            strSql.Append(";");
            return HelperForFrontend.Query<T>(strSql.ToString(), parm).ToList();
        }

        /// <summary>
        /// 新增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(t_App_Au model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" declare @oid int; ");
            strSql.Append(" select @oid=id from t_App_Au where accid=@accid and appkey=@appkey; ");
            strSql.Append(" if(isnull(@oid,0)>0) ");
            strSql.Append(" begin ");
            strSql.Append(" 	update t_App_Au set aa_Status = @aa_Status, appkey = @appkey , ");
            strSql.Append(" 	appName = @appName ,stattime = @stattime ,endtime = @endtime ,aa_time = GETDATE() , ");
            strSql.Append(" 	aa_remark = @aa_remark  where accid=@accid and appkey=@appkey; ");
            strSql.Append(" 	select @oid; ");
            strSql.Append(" end ");
            strSql.Append(" else ");
            strSql.Append(" begin ");
            strSql.Append(" 	insert into t_App_Au(aa_Status,accid,appkey,appName,stattime,endtime,aa_time,aa_remark)  ");
            strSql.Append(" 	values (@aa_Status,@accid,@appkey,@appName,@stattime,@endtime,@aa_time,@aa_remark) ; ");
            strSql.Append(" 	select @@IDENTITY; ");
            strSql.Append(" end ");

            object id = HelperForFrontend.ExecuteScalar(strSql.ToString(), model);
            if (id != null)
            {
                return Convert.ToInt32(id);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 移除授权
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public bool RemoveApp(int key, int accid)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("delete t_App_Au where appkey=@appkey and accid=@accid;");

            int rows = HelperForFrontend.Execute(sqlStr.ToString(), new { appkey = key, accid = accid });
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
