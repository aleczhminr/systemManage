﻿using Model;
using Model.O2O;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.O2O
{
    //NewGoodsInfo
    /// <summary>
    /// NewGoodsInfo
    /// </summary>
    public class NewGoodsInfoBaseDAL
    {



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public NewGoodsInfo GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id, saleNum, saleMoney, accountNum, userNum, retailNum, isPic, cMaxId, cMaxName, cMinId, cMinName, gName, gQuantity, averagePrice, averageCosPrice  ");
            strSql.Append("  from OTwoO.dbo.NewGoodsInfo ");
            strSql.Append(" where id=@id");
            return DapperHelper.GetModel<NewGoodsInfo>(strSql.ToString(), new { id = id });
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="dapperWheres">查询条件列表</param>
        public List<NewGoodsInfo> GetList(List<DapperWhere> dapperWheres)
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
            strSql.Append(" FROM OTwoO.dbo.NewGoodsInfo ");
            if (where.Length > 0)
            {
                strSql.Append(" where " + where);
            }

            return DapperHelper.Query<NewGoodsInfo>(strSql.ToString(), parm).ToList();
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="top">前几行</param>
        /// <param name="dapperWheres">查询条件列表</param>
        /// <param name="filedOrder">排序</param>
        public List<NewGoodsInfo> GetList(int top, List<DapperWhere> dapperWheres, string filedOrder)
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
            strSql.Append(" FROM OTwoO.dbo.NewGoodsInfo ");
            if (where.Length > 0)
            {
                strSql.Append(" where " + where);
            }

            if (filedOrder.Length > 0)
            {
                strSql.Append(" order by " + filedOrder);
            }
            strSql.Append(";");
            return DapperHelper.Query<NewGoodsInfo>(strSql.ToString(), parm).ToList();
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
            strSql.Append(" FROM OTwoO.dbo.NewGoodsInfo ");
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
            strSql.Append(" select count(*) from OTwoO.dbo.NewGoodsInfo ");

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

        /// <summary>
        /// 分页得到列表
        /// </summary>
        /// <param name="pageIndex">显示页号</param>
        /// <param name="pageSize">每页显示数</param>
        /// <param name="dapperWheres">条件列表</param>
        /// <param name="filedOrder">排序</param>
        /// <returns>返回列表</returns>
        public List<NewGoodsInfo> GetList(int pageIndex, int pageSize, List<DapperWhere> dapperWheres, string filedOrder)
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
                strSql.Append(" order by T.id  desc");
            }
            strSql.Append(" )AS Row, * from OTwoO.dbo.NewGoodsInfo T  ");

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

            return DapperHelper.Query<NewGoodsInfo>(strSql.ToString(), parm).ToList();
        }


        /// <summary>
        /// 分页得到列表<para>如果没有对像的可以传入 object 或者  dynamic </para>
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
                strSql.Append(" order by T.id  desc");
            }
            strSql.Append(" )AS overRow, " + columnName + " from OTwoO.dbo.NewGoodsInfo T  ");

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