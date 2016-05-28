using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;
namespace BLL
{
    /// <summary>
    /// 回访分类
    /// </summary>
   public static class Sys_VisitReasonBLL
    {

        /// <summary>
        /// 得到列表
        /// </summary>
        /// <param name="vrId"></param>
        /// <returns></returns>
       public static List<Sys_VisitReason> GetList(int vrMaxId)
       {
           Sys_VisitReasonDAL dal = new Sys_VisitReasonDAL();
           return dal.GetList(vrMaxId);
       }
        /// <summary>
        /// 根据ID得到列表
        /// </summary>
        /// <param name="vrid"></param>
        /// <returns></returns>
       public static List<Sys_VisitReason> GetList(int[] vrid)
       {
           Sys_VisitReasonDAL dal = new Sys_VisitReasonDAL();
           return dal.GetList(vrid);
       }
    }
}
