using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Model;
using System.Linq;
using System.Text.RegularExpressions;

namespace DAL.Base
{
    //T_Account
    /// <summary>
    /// I200.dbo.T_Account
    /// </summary>
    public class T_AccountBaseDAL
    {

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public T_Account GetModel(long ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, RegTime, LoginTimeWeb, LoginTimeLast, State, SFZNumber, dxstatus, proportion, nexttotal, reguserid, shotName, UserRealName, subjection, max_shop, logintimebreak, loginbrslast, BBSusername, emailChk, phoneChk, Parent_AccountId, Reg_Code, Type, userpasswd, Imgurl, manager_name, AgentId, Remark, ShopType, py_full, py_code, Guider_Step, BBS_Uid, weixin_openid, PhoneNumber, RecommendId, Flag, useremail, RandomNumber, CompanyName, CompanyAddress, ServiceManager  ");
            strSql.Append("  from I200.dbo.T_Account ");
            strSql.Append(" where ID=@ID");
            return DapperHelper.GetModel<T_Account>(strSql.ToString(), new { ID = ID });
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="dapperWheres">查询条件列表</param>
        public List<T_Account> GetList(List<DapperWhere> dapperWheres)
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
            strSql.Append(" FROM I200.dbo.T_Account ");
            if (where.Length > 0)
            {
                strSql.Append(" where " + where);
            }

            return DapperHelper.Query<T_Account>(strSql.ToString(), parm).ToList();
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="top">前几行</param>
        /// <param name="dapperWheres">查询条件列表</param>
        /// <param name="filedOrder">排序</param>
        public List<T_Account> GetList(int top, List<DapperWhere> dapperWheres, string filedOrder)
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
            strSql.Append(" FROM I200.dbo.T_Account ");
            if (where.Length > 0)
            {
                strSql.Append(" where " + where);
            }

            if (filedOrder.Length > 0)
            {
                strSql.Append(" order by " + filedOrder);
            }
            strSql.Append(";");
            return DapperHelper.Query<T_Account>(strSql.ToString(), parm).ToList();
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
            strSql.Append(" FROM I200.dbo.T_Account ");
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
        /// <param name="dapperWheres"></param>
        /// <param name="searchStr"></param>
        /// <returns></returns>
        public int GetCount(List<DapperWhere> dapperWheres, string searchStr = "")
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select count(*) from I200.dbo.T_Account ");

            if (string.IsNullOrEmpty(searchStr))
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

                if (where.Length > 0)
                {
                    strSql.Append(" where " + where);
                }
                object obj = HelperForFrontend.ExecuteScalar(strSql.ToString(), parm);
                if (obj != null)
                {
                    return Convert.ToInt32(obj);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                //Regex reg = new Regex("@/^0?1[3|4|5|8|7][0-9]/d{8}$/");//验证手机号正则
                //if (reg.IsMatch(searchStr))
                //{
                strSql.Append(" where state=1 and (phoneNumber like '%" + searchStr + "%' or ");
                strSql.Append(" CompanyName like '%" + searchStr + "%' or ");
                strSql.Append(" UserRealName like '%" + searchStr + "%' or ");
                strSql.Append(" UserEmail like '%" + searchStr + "%' or ");
                strSql.Append(" BBS_Uid like '%" + searchStr + "%' ");
                if (Regex.IsMatch(searchStr, "^((\\+|-)\\d)?\\d*$"))
                {
                    strSql.Append(" or ID=" + searchStr);
                }

                strSql.Append(" )");
                //}
                object obj = HelperForFrontend.ExecuteScalar(strSql.ToString());
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

        /// <summary>
        /// 分页得到列表
        /// </summary>
        /// <param name="pageIndex">显示页号</param>
        /// <param name="pageSize">每页显示数</param>
        /// <param name="dapperWheres">条件列表</param>
        /// <param name="filedOrder">排序</param>
        /// <returns>返回列表</returns>
        public List<T_Account> GetList(int pageIndex, int pageSize, List<DapperWhere> dapperWheres, string filedOrder)
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
            strSql.Append(" SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER ( ");
            if (filedOrder.Length > 0)
            {
                strSql.Append(" order by " + filedOrder + " ");
            }
            else
            {
                strSql.Append(" order by T.ID  desc");
            }
            strSql.Append(" )AS Row, * from I200.dbo.T_Account T  ");

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

            int bgNumber = ((pageIndex - 1) * pageSize) + 1;
            int edNumber = (pageIndex) * pageSize;
            parm["startIndex"] = bgNumber;
            parm["endIndex"] = edNumber;

            return DapperHelper.Query<T_Account>(strSql.ToString(), parm).ToList();
        }


        /// <summary>
        /// 分页得到列表
        /// </summary>
        /// <param name="pageIndex">显示页号</param>
        /// <param name="pageSize">每页显示数</param>
        /// <param name="columnName">需要获取的列名<para>为了方便不在处理，列与列用逗号分开，参照SQL写法</para></param>
        /// <param name="dapperWheres">条件列表</param>
        /// <param name="filedOrder">排序</param>
        /// <returns>返回列表</returns>
        public List<T> GetList<T>(int pageIndex, int pageSize, string columnName, List<DapperWhere> dapperWheres, string filedOrder)
        {
            if (columnName.Length < 1)
            {
                columnName = "*";
            }

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
            strSql.Append(" SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER ( ");
            if (filedOrder.Length > 0)
            {
                strSql.Append(" order by " + filedOrder + " ");
            }
            else
            {
                strSql.Append(" order by T.ID  desc");
            }
            strSql.Append(" )AS overRow, " + columnName + " from I200.dbo.T_Account T  ");

            if (where.Length > 0)
            {
                strSql.Append(" where " + where + " ");
            }
            strSql.Append(" ) TT ");
            strSql.Append(" WHERE TT.overRow between @startIndex and @endIndex; ");

            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            int bgNumber = ((pageIndex - 1) * pageSize) + 1;
            int edNumber = (pageIndex) * pageSize;
            parm["startIndex"] = bgNumber;
            parm["endIndex"] = edNumber;

            return DapperHelper.Query<T>(strSql.ToString(), parm).ToList();
        }

    }
}
