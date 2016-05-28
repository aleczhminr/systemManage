using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// 用户状态变更记录
    /// </summary>
    public static class SysRpt_ShopActiveBLL
    {
        
       /// <summary>
       /// 得到某些天中 每天新增的活跃用户数
       /// </summary>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
        public static List<dynamic> GetNewActiveUser(DateTime startTime, DateTime endTime)
        {
            SysRpt_ShopActiveDAL dal = new SysRpt_ShopActiveDAL();
            return dal.GetNewActiveUser(startTime, endTime);
        }
       /// <summary>
       /// 根据条件查看活跃信息数据（返回一些汇总信息）
       /// </summary>
       /// <param name="pageIndex"></param>
       /// <param name="pageSize"></param>
       /// <param name="sqlWhere"></param>
       /// <param name="orderBy"></param>
        public static SysShopActiveList GetListContainSummary(int pageIndex, int pageSize, List<DapperWhere> sqlWhere )
        {
            SysRpt_ShopActiveDAL dal = new SysRpt_ShopActiveDAL();
            return dal.GetListContainSummary(pageIndex, pageSize, sqlWhere );
        }
       /// <summary>
       /// 分组取交集
       /// </summary>
       /// <param name="mainSqlWhere"></param>
       /// <param name="followSqlWhere"></param>
       /// <returns></returns>
        public static SysShopActiveList GetGroupListContainSummary(int pageIndex, int pageSize, List<DapperWhere> mainSqlWhere, List<DapperWhere> followSqlWhere)
        {
            SysRpt_ShopActiveDAL dal = new SysRpt_ShopActiveDAL();
            return dal.GetGroupListContainSummary(pageIndex, pageSize, mainSqlWhere, followSqlWhere);
        }

        /// <summary>
        /// 返回固定日期店铺状态值
        /// </summary>
        /// <param name="dayDate"></param>
        /// <returns></returns>
        public static ActiveStatus GetdailyStatus(DateTime dayDate)
        {
            SysRpt_ShopActiveDAL dal = new SysRpt_ShopActiveDAL();
            return dal.GetdailyStatus(dayDate);
        }

        public static dynamic GetActiveStatusList(DateTime dt,string col)
        {
            SysRpt_ShopActiveDAL dal = new SysRpt_ShopActiveDAL();
            return dal.GetActiveStatusList( dt, col);
        }
        
    }
}
