﻿using DAL.O2O;
using Model;
using Model.O2O;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.O2O
{
    //goodsPic
    public static class goodsPicBaseBLL
    {



        #region  Method
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static goodsPic GetModel(int id)
        {
            goodsPicBaseDAL dal = new goodsPicBaseDAL();
            return dal.GetModel(id);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="dapperWheres">查询条件列表</param>
        public static List<goodsPic> GetList(List<DapperWhere> dapperWheres)
        {
            goodsPicBaseDAL dal = new goodsPicBaseDAL();
            return dal.GetList(dapperWheres);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="top">前几行</param>
        /// <param name="dapperWheres">查询条件列表</param>
        /// <param name="filedOrder">排序</param>
        public static List<goodsPic> GetList(int top, List<DapperWhere> dapperWheres, string filedOrder)
        {
            goodsPicBaseDAL dal = new goodsPicBaseDAL();
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
            goodsPicBaseDAL dal = new goodsPicBaseDAL();
            return dal.GetList<T>(top, columnName, dapperWheres, filedOrder);
        }
        /// <summary>
        /// 得到总数
        /// </summary>
        /// <param name="dapperWheres">条件列表</param>
        /// <returns></returns>
        public static int GetCount(List<DapperWhere> dapperWheres)
        {
            goodsPicBaseDAL dal = new goodsPicBaseDAL();
            return dal.GetCount(dapperWheres);
        }
        /// <summary>
        /// 分页得到列表
        /// </summary>
        /// <param name="pageIndex">显示页号</param>
        /// <param name="pageSize">每页显示数</param>
        /// <param name="dapperWheres">条件列表</param>
        /// <param name="filedOrder">排序</param>
        /// <returns>返回列表</returns>
        public static List<goodsPic> GetList(int pageIndex, int pageSize, List<DapperWhere> dapperWheres, string filedOrder)
        {
            goodsPicBaseDAL dal = new goodsPicBaseDAL();
            return dal.GetList(pageIndex, pageSize, dapperWheres, filedOrder);
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
        public static List<T> GetList<T>(int pageIndex, int pageSize, string columnName, List<DapperWhere> dapperWheres, string filedOrder)
        {
            goodsPicBaseDAL dal = new goodsPicBaseDAL();
            return dal.GetList<T>(pageIndex, pageSize, columnName, dapperWheres, filedOrder);
        }



        #endregion

    }
}