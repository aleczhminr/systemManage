using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Model;
using System.Linq;
namespace DAL.Base
{
    //Sys_SysAreaCity
    /// <summary>
    /// Sys_SysAreaCity
    /// </summary>
    public class Sys_SysAreaCityBaseDAL
    {
        /// <summary>
        /// 新增加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(Sys_SysAreaCity model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Sys_SysAreaCity(");
            strSql.Append("ParentID,Sort,ParentName,CityID,CityName,PYM,EnName,PostCode,AreaCode,CName");
            strSql.Append(") values (");
            strSql.Append("@ParentID,@Sort,@ParentName,@CityID,@CityName,@PYM,@EnName,@PostCode,@AreaCode,@CName");
            strSql.Append(") ");

            object obj = DapperHelper.ExecuteScalar(strSql.ToString(), model);
            if (obj != null)
            {
                return Convert.ToInt32(obj);
            }
            else
            {
                return 0;
            }
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Sys_SysAreaCity model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Sys_SysAreaCity set ");

            strSql.Append(" ParentID = @ParentID , ");
            strSql.Append(" Sort = @Sort , ");
            strSql.Append(" ParentName = @ParentName , ");
            strSql.Append(" CityID = @CityID , ");
            strSql.Append(" CityName = @CityName , ");
            strSql.Append(" PYM = @PYM , ");
            strSql.Append(" EnName = @EnName , ");
            strSql.Append(" PostCode = @PostCode , ");
            strSql.Append(" AreaCode = @AreaCode , ");
            strSql.Append(" CName = @CName  ");
            strSql.Append(" where ParentID=@ParentID and ParentName=@ParentName and CityID=@CityID and CityName=@CityName and PYM=@PYM and EnName=@EnName and PostCode=@PostCode and AreaCode=@AreaCode and CName=@CName and Sort=@Sort  ");

            int row = DapperHelper.Execute(strSql.ToString(), model);
            if (row > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
         
         

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="dapperWheres">查询条件列表</param>
        public List<Sys_SysAreaCity> GetList(List<DapperWhere> dapperWheres)
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

            strSql.Append("select * ");
            strSql.Append(" FROM Sys_SysAreaCity ");
            if (where.Length > 0)
            {
                strSql.Append(" where " + where);
            }

            return DapperHelper.Query<Sys_SysAreaCity>(strSql.ToString(), parm).ToList();
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="top">前几行</param>
        /// <param name="dapperWheres">查询条件列表</param>
        /// <param name="filedOrder">排序</param>
        public List<Sys_SysAreaCity> GetList(int top, List<DapperWhere> dapperWheres, string filedOrder)
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
            strSql.Append(" * ");
            strSql.Append(" FROM Sys_SysAreaCity ");
            if (where.Length > 0)
            {
                strSql.Append(" where " + where);
            }

            if (filedOrder.Length > 0)
            {
                strSql.Append(" order by " + filedOrder);
            }
            strSql.Append(";");
            return DapperHelper.Query<Sys_SysAreaCity>(strSql.ToString(), parm).ToList();
        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="top">前几行</param>
        /// <param name="columnName">需要获取的列名<para>为了方便不在处理，列与列用逗号分开，参照SQL写法</para></param>
        /// <param name="dapperWheres">查询条件列表</param>
        /// <param name="filedOrder">排序</param>
        public List<T> GetList<T>(int top, string columnName, List<DapperWhere> dapperWheres, string filedOrder)
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
            strSql.Append(" FROM Sys_SysAreaCity ");
            if (where.Length > 0)
            {
                strSql.Append(" where " + where);
            }

            if (filedOrder.Length > 0)
            {
                strSql.Append(" order by " + filedOrder);
            }
            strSql.Append(";");
            return DapperHelper.Query<T>(strSql.ToString(), parm).ToList();
        }

        /// <summary>
        /// 得到总数
        /// </summary>
        /// <param name="dapperWheres">条件列表</param>
        /// <returns></returns>
        public int GetCount(List<DapperWhere> dapperWheres)
        {
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select count(*) from Sys_SysAreaCity ");

            if (where.Length > 0)
            {
                strSql.Append(" where " + where);
            }
            object obj = DapperHelper.ExecuteScalar(strSql.ToString(), parm);
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
