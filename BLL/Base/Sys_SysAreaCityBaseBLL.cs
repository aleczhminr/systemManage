using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using DAL.Base;
using Model;
namespace BLL.Base
{
    //Sys_SysAreaCity
    public static class Sys_SysAreaCityBaseBLL
    {



        #region  Method
        /// <summary>
        /// 新增加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Add(Sys_SysAreaCity model)
        {
            Sys_SysAreaCityBaseDAL dal = new Sys_SysAreaCityBaseDAL();
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(Sys_SysAreaCity model)
        {
            Sys_SysAreaCityBaseDAL dal = new Sys_SysAreaCityBaseDAL();
            return dal.Update(model);
        } 
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="dapperWheres">查询条件列表</param>
        public static List<Sys_SysAreaCity> GetList(List<DapperWhere> dapperWheres)
        {
            Sys_SysAreaCityBaseDAL dal = new Sys_SysAreaCityBaseDAL();
            return dal.GetList(dapperWheres);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="top">前几行</param>
        /// <param name="dapperWheres">查询条件列表</param>
        /// <param name="filedOrder">排序</param>
        public static List<Sys_SysAreaCity> GetList(int top, List<DapperWhere> dapperWheres, string filedOrder)
        {
            Sys_SysAreaCityBaseDAL dal = new Sys_SysAreaCityBaseDAL();
            return dal.GetList(top, dapperWheres, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="top">前几行</param>
        /// <param name="columnName">需要获取的列名<para>为了方便不在处理，列与列用逗号分开，参照SQL写法</para></param>
        /// <param name="dapperWheres">查询条件列表</param>
        /// <param name="filedOrder">排序</param>
        public static List<T> GetList<T>(int top, string columnName, List<DapperWhere> dapperWheres, string filedOrder)
        {
            Sys_SysAreaCityBaseDAL dal = new Sys_SysAreaCityBaseDAL();
            return dal.GetList<T>(top, columnName, dapperWheres, filedOrder);
        }
        /// <summary>
        /// 得到总数
        /// </summary>
        /// <param name="dapperWheres">条件列表</param>
        /// <returns></returns>
        public static int GetCount(List<DapperWhere> dapperWheres)
        {
            Sys_SysAreaCityBaseDAL dal = new Sys_SysAreaCityBaseDAL();
            return dal.GetCount(dapperWheres);
        } 


        #endregion

    }
}