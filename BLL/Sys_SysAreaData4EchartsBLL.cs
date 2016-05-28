using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
   public static  class Sys_SysAreaData4EchartsBLL
   {

       /// <summary>
       /// 获得区域统计数据
       /// </summary>
       /// <param name="areaKey">统计关键值</param>
       /// <param name="sClass">统计类型</param>
       /// <param name="recDate">开始日期</param>
       /// <param name="endDate">截止日期</param>
       /// <param name="sAreaName">省份名称 none-全部</param>
       /// <returns></returns>
       public static Sys_AreaDate4EchartsMapList GetAreaDataMap(string areaKey, string sClass, DateTime recDate, DateTime endDate, string order)
       {
           Sys_SysAreaData4EchartsDAL dal = new Sys_SysAreaData4EchartsDAL();
           Sys_AreaDate4EchartsMapList areaDateMapList = new Sys_AreaDate4EchartsMapList();
           areaDateMapList = dal.GetAreaDataMap(areaKey, sClass, recDate, endDate, order);
           if (areaDateMapList.areaDataList != null && areaDateMapList.areaDataList.Count > 0)
           {

           }

           return areaDateMapList;
       }

       /// <summary>
       /// 获得归属地店铺信息
       /// </summary>
       /// <param name="areaName">归属地名称</param>
       /// <param name="bgDate">开始日期</param>
       /// <param name="edDate">结束日期</param>
       /// <param name="iPage">页数</param>
       /// <returns></returns>
       public static Dictionary<string, object> GetAreaShopInfoEx(string areaName, DateTime bgDate, DateTime edDate, int iPage, int pageSize)
       {
           Sys_SysAreaData4EchartsDAL dal = new Sys_SysAreaData4EchartsDAL();
           return dal.GetAreaShopInfoEx(areaName, bgDate, edDate, iPage,pageSize);
       }
    }
}
