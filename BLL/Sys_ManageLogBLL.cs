using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;


namespace BLL
{
    public class Sys_ManageLogBLL
    {
        /// <summary>
        /// 获取分页的登录日志
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static List<dynamic> GetLog(int pageIndex, int pageSize, string where)
        {
            Sys_ManageLogDAL dal = new Sys_ManageLogDAL();
            return dal.GetLog(pageIndex, pageSize, where);
        }

        /// <summary>
        /// 获取登录日志列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static List<dynamic> GetLoginLogList(int pageIndex, int pageSize, string where)
        {
            Sys_ManageLogDAL manageLog = new Sys_ManageLogDAL();
            return manageLog.GetLog(pageIndex, pageSize, where);
        }

        /// <summary>
        /// 获取登录日志总行数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static int GetLogListCount(string where)
        {
            Sys_ManageLogDAL manageLog = new Sys_ManageLogDAL();
            return manageLog.GetLogListCount(where);
        }
    }
}
