using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;
namespace BLL
{
    /// <summary>
    /// 店铺收集信息
    /// </summary>
   public static class Sys_AccountBLL
    {
        /// <summary>
        /// 根据列名更新信息
        /// </summary>
        /// <param name="accid">店铺ID</param>
        /// <param name="Column">列名</param>
        /// <param name="value">值</param>
        /// <returns></returns>
       public static bool UpdateColumn(int accid, string Column, object value)
       {
           Sys_AccountDAL dal = new Sys_AccountDAL();
           return dal.UpdateColumn(accid, Column, value);
       }
       /// <summary>
       /// 得到一个店铺收集信息
       /// </summary>
       /// <param name="accid"></param>
       /// <returns></returns>
       public static SysAccountInfo GetAccountInfo(int accid)
       {
           Sys_AccountDAL dal = new Sys_AccountDAL();
           return dal.GetAccountInfo(accid);
       }
    }
}
