﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    /// <summary>
    /// 主站信息
    /// </summary>
    public class HelperForFrontend 
    {
        public static string sqlconnection = System.Configuration.ConfigurationManager.AppSettings["DataBase"].ToString();

        /// <summary>
        /// 得到一组信息
        /// </summary>
        /// <typeparam name="T">用于初始化或者转化的对像</typeparam>
        /// <param name="SQLString">查询SQL语句</param>
        /// <param name="cmdParms">包含条件或参数值的对像</param>
        /// <returns>返回 T 的集合</returns>
        public static IEnumerable<T> Query<T>(string SQLString, object cmdParms = null)
        {
            using (IDbConnection conn = new SqlConnection(sqlconnection))
            {
                conn.Open();
                return conn.Query<T>(SQLString, cmdParms);
            }
        }
        /// <summary>
        /// 得到一组信息
        /// </summary>
        /// <typeparam name="T">用于初始化或者转化的对像</typeparam>
        /// <param name="SQLString">查询SQL语句</param>
        /// <param name="cmdParms">包含条件或参数值的对像</param>
        /// <returns>返回 T 的集合</returns>
        public static IEnumerable<dynamic> Query(string SQLString, object cmdParms = null)
        {
            using (IDbConnection conn = new SqlConnection(sqlconnection))
            {
                conn.Open();
                IEnumerable<dynamic> list = conn.Query(SQLString, cmdParms);
                return list;
            }
        }
        /// <summary>
        /// 得到一个对像
        /// <para>此方法只能返回一行数据时使用。</para>
        /// </summary>
        /// <typeparam name="T">用于初始化或者转化的对像</typeparam>
        /// <param name="SQLString">查询SQL语句</param>
        /// <param name="cmdParms">包含条件或参数值的对像</param>
        /// <returns>返回一个对像</returns>
        public static T GetModel<T>(string SQLString, object cmdParms = null)
        {
            using (IDbConnection conn = new SqlConnection(sqlconnection))
            {
                conn.Open();
                return conn.Query<T>(SQLString, cmdParms).SingleOrDefault<T>();
            }
        }

        /// <summary>
        /// 执行SQL 并返回影响的行数
        /// </summary>
        /// <param name="SQLString">用于执行的SQL</param>
        /// <param name="cmdParms">包含条件或参数值的对像</param>
        /// <returns>影响的行数</returns>
        public static int Execute(string SQLString, object cmdParms = null)
        {
            using (IDbConnection conn = new SqlConnection(sqlconnection))
            {
                conn.Open();
                int row = conn.Execute(SQLString, cmdParms);
                return row;
            }
        }
        /// <summary>
        /// 执行SQL 并返回第一行第一列的值
        /// </summary>
        /// <param name="SQLString">用于执行的SQL</param>
        /// <param name="cmdParms">包含条件或参数值的对像</param>
        /// <returns>第一行第一列的值</returns>
        public static object ExecuteScalar(string SQLString, object cmdParms = null)
        {

            using (IDbConnection conn = new SqlConnection(sqlconnection))
            {
                conn.Open();
                object firstSelected = conn.ExecuteScalar(SQLString, cmdParms);
                return firstSelected;
            }
        }
        /// <summary>
        /// 执行SQL 并返回一个对像
        /// </summary>
        /// <typeparam name="T">用于初始化或者转化的对像</typeparam>
        /// <param name="SQLString">用于执行的SQL</param>
        /// <param name="cmdParms">包含条件或参数值的对像</param>
        /// <returns>一个对像</returns>
        public static T ExecuteScalar<T>(string SQLString, object cmdParms = null)
        {

            using (IDbConnection conn = new SqlConnection(sqlconnection))
            {
                conn.Open();
                T firstSelected = conn.ExecuteScalar<T>(SQLString, cmdParms);
                return firstSelected;
            }
        }
        /// <summary>
        /// 运行事务，执行SQL 并返回影响的行数
        /// </summary>
        /// <param name="SQLString">用于执行的SQL</param>
        /// <param name="cmdParms">包含条件或参数值的对像</param>
        /// <returns>影响的行数</returns>
        public static int ExecuteTransaction(string SQLString, object cmdParms = null)
        {
            using (IDbConnection conn = new SqlConnection(sqlconnection))
            {
                conn.Open();
                IDbTransaction transaction = conn.BeginTransaction();
                int row = conn.Execute(SQLString, cmdParms, transaction, null, null);
                transaction.Commit();
                return row;
            }
        }
        /// <summary>
        /// 运行事务，执行多条SQL 并返回每条影响的行数
        /// </summary>
        /// <param name="execteParameter">多条运行的变量</param>
        /// <returns>按入口参数顺序返回每条SQL的影响行数</returns>
        public static int[] ExecuteTransaction(List<ExecuteParameter> execteParameter)
        {
            using (IDbConnection conn = new SqlConnection(sqlconnection))
            {
                conn.Open();
                IDbTransaction transaction = conn.BeginTransaction();
                List<int> rows = new List<int>();

                foreach (ExecuteParameter item in execteParameter)
                {
                    rows.Add(conn.Execute(item.SQLString, item.cmdParms, transaction, null, null));
                }
                transaction.Commit();
                return rows.ToArray();
            }
        }

        /// <summary>
        /// 查询多数据集，返回一个针对数据集的Reader
        /// </summary>
        /// <param name="SQLString"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static SqlMapper.GridReader QueryMultiple(string SQLString, object cmdParms = null)
        {
            using (IDbConnection conn = new SqlConnection(sqlconnection))
            {
                conn.Open();
                SqlMapper.GridReader gridReader = conn.QueryMultiple(SQLString, cmdParms);
                return gridReader;
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int ExecuteProc(string procName, DynamicParameters param)
        {
            using (IDbConnection conn = new SqlConnection(sqlconnection))
            {
                conn.Open();
                return SqlMapper.Execute(conn, procName, param, null, null, CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// sql执行的变量
        /// </summary>
        public class ExecuteParameter
        {
            public ExecuteParameter()
            {
                cmdParms = null;
            }
            /// <summary>
            /// SQL语句
            /// </summary>
            public string SQLString { get; set; }
            /// <summary>
            /// 包含条件或参数值的对像
            /// </summary>
            public object cmdParms { get; set; }
        }
    }

    /// <summary>
    /// 数据访问抽象基础类
    /// Copyright (C) Maticsoft
    /// </summary>
    public abstract class DapperHelper
    {

        public static string sqlconnection = System.Configuration.ConfigurationManager.AppSettings["ManageDataBase"].ToString();


       /// <summary>
       /// 得到一组信息
       /// </summary>
       /// <typeparam name="T">用于初始化或者转化的对像</typeparam>
       /// <param name="SQLString">查询SQL语句</param>
       /// <param name="cmdParms">包含条件或参数值的对像</param>
       /// <returns>返回 T 的集合</returns>
       public static IEnumerable<T> Query<T>(string SQLString, object cmdParms = null)
       {
           using (IDbConnection conn = new SqlConnection(sqlconnection))
           {
               conn.Open();
               return conn.Query<T>(SQLString, cmdParms);
           }
       }
       /// <summary>
       /// 得到一组信息
       /// </summary>
       /// <typeparam name="T">用于初始化或者转化的对像</typeparam>
       /// <param name="SQLString">查询SQL语句</param>
       /// <param name="cmdParms">包含条件或参数值的对像</param>
       /// <returns>返回 T 的集合</returns>
       public static IEnumerable<dynamic> Query(string SQLString, object cmdParms = null)
       {
           using (IDbConnection conn = new SqlConnection(sqlconnection))
           {
               conn.Open();
               IEnumerable<dynamic> list= conn.Query(SQLString, cmdParms);
               return list;
           }
       }

       /// <summary>
       /// 得到一组信息(通过存储过程)
       /// </summary>
       /// <typeparam name="T">用于初始化或者转化的对像</typeparam>
       /// <param name="SQLString">查询SQL语句</param>
       /// <param name="cmdParms">包含条件或参数值的对像</param>
       /// <returns>返回 T 的集合</returns>
       public static IEnumerable<dynamic> QueryEx(string SQLString, object cmdParms = null)
       {
           using (IDbConnection conn = new SqlConnection(sqlconnection))
           {
               conn.Open();
               IEnumerable<dynamic> list = conn.Query(SQLString, cmdParms, commandType: CommandType.StoredProcedure);
               return list;
           }
       }

       /// <summary>
       /// 得到一个对像
       /// <para>此方法只能返回一行数据时使用。</para>
       /// </summary>
       /// <typeparam name="T">用于初始化或者转化的对像</typeparam>
       /// <param name="SQLString">查询SQL语句</param>
       /// <param name="cmdParms">包含条件或参数值的对像</param>
       /// <returns>返回一个对像</returns>
       public static T GetModel<T>(string SQLString, object cmdParms = null)
       {
           using (IDbConnection conn = new SqlConnection(sqlconnection))
           {
               conn.Open();
               return conn.Query<T>(SQLString, cmdParms).SingleOrDefault<T>();
           }
       }

       /// <summary>
       /// 执行SQL 并返回影响的行数
       /// </summary>
       /// <param name="SQLString">用于执行的SQL</param>
       /// <param name="cmdParms">包含条件或参数值的对像</param>
       /// <returns>影响的行数</returns>
       public static int Execute(string SQLString, object cmdParms = null)
       {
           using (IDbConnection conn = new SqlConnection(sqlconnection))
           {
               conn.Open();
               int row = conn.Execute(SQLString, cmdParms);
               return row;
           }
       }
       /// <summary>
       /// 执行SQL 并返回第一行第一列的值
       /// </summary>
       /// <param name="SQLString">用于执行的SQL</param>
       /// <param name="cmdParms">包含条件或参数值的对像</param>
       /// <returns>第一行第一列的值</returns>
       public static object ExecuteScalar(string SQLString, object cmdParms = null)
       {

           using (IDbConnection conn = new SqlConnection(sqlconnection))
           {
               conn.Open();
               object firstSelected = conn.ExecuteScalar(SQLString, cmdParms);
               return firstSelected;
           }
       }
       /// <summary>
       /// 执行SQL 并返回一个对像
       /// </summary>
       /// <typeparam name="T">用于初始化或者转化的对像</typeparam>
       /// <param name="SQLString">用于执行的SQL</param>
       /// <param name="cmdParms">包含条件或参数值的对像</param>
       /// <returns>一个对像</returns>
       public static T ExecuteScalar<T>(string SQLString, object cmdParms = null)
       {

           using (IDbConnection conn = new SqlConnection(sqlconnection))
           {
               conn.Open();
               T firstSelected = conn.ExecuteScalar<T>(SQLString, cmdParms);
               return firstSelected;
           }
       }
       /// <summary>
       /// 运行事务，执行SQL 并返回影响的行数
       /// </summary>
       /// <param name="SQLString">用于执行的SQL</param>
       /// <param name="cmdParms">包含条件或参数值的对像</param>
       /// <returns>影响的行数</returns>
       public static int ExecuteTransaction(string SQLString, object cmdParms = null)
       {
           using (IDbConnection conn = new SqlConnection(sqlconnection))
           {
               conn.Open();
               IDbTransaction transaction = conn.BeginTransaction();
               int row = conn.Execute(SQLString, cmdParms, transaction, null, null);
               transaction.Commit();
               return row;
           }
       }
       /// <summary>
       /// 运行事务，执行多条SQL 并返回每条影响的行数
       /// </summary>
       /// <param name="execteParameter">多条运行的变量</param>
       /// <returns>按入口参数顺序返回每条SQL的影响行数</returns>
       public static int[] ExecuteTransaction(List<ExecuteParameter> execteParameter)
       {
           using (IDbConnection conn = new SqlConnection(sqlconnection))
           {
               conn.Open();
               IDbTransaction transaction = conn.BeginTransaction();
               List<int> rows = new List<int>();

               foreach (ExecuteParameter item in execteParameter)
               {
                   rows.Add(conn.Execute(item.SQLString, item.cmdParms, transaction, null, null));
               }
               transaction.Commit();
               return rows.ToArray();
           }
       }

        /// <summary>
       /// 查询多数据集，返回一个针对 IEnumerable.dynamic  的 List
        /// </summary>
        /// <param name="SQLString"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
       public static List<IEnumerable<dynamic>> QueryMultiple(string SQLString, object cmdParms = null)
        {
            using (IDbConnection conn = new SqlConnection(sqlconnection))
            {
                conn.Open();
                SqlMapper.GridReader gridReader = conn.QueryMultiple(SQLString, cmdParms);


                //var l = gridReader.Read<dynamic>();
                //var t = gridReader.Read<dynamic>();

                List<IEnumerable<dynamic>> objList = new List<IEnumerable<dynamic>>();

                while (!gridReader.IsConsumed)
                {
                    objList.Add(gridReader.Read<dynamic>());
                }


                //var a = gridReader.Read().First();
                //int b = a.cnt;

                //var c = gridReader.Read<dynamic>().FirstOrDefault();
                //int status = gridReader.Read<int>().FirstOrDefault(); 
                return objList;
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int ExecuteProc(string procName, DynamicParameters param)
        {
            using (IDbConnection conn = new SqlConnection(sqlconnection))
            {
                conn.Open();
                return SqlMapper.Execute(conn, procName, param, null, null, CommandType.StoredProcedure);
            }
        }

        /// <summary>
       /// sql执行的变量
       /// </summary>
       public  class ExecuteParameter
       {
           public ExecuteParameter()
           {
               cmdParms = null;
           }
           /// <summary>
           /// SQL语句
           /// </summary>
           public string SQLString { get; set; }
           /// <summary>
           /// 包含条件或参数值的对像
           /// </summary>
           public object cmdParms { get; set; }
       }
    }
}
