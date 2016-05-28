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
    /// 店铺权限
    /// </summary>
   public static class t_App_AuBLL
    {

       /// <summary>
       /// 根据店铺ID得到一个店铺的开通的所有权限
       /// </summary>
       /// <param name="accid"></param>
       /// <returns></returns>
       public static List<AppInfoModel> GetListByAccId(int accid)
       {
           t_App_AuDAL dal = new t_App_AuDAL();
           return dal.GetListByAccId(accid);
       }
        /// <summary>
        /// 新增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       public static int Add(t_App_Au model)
       {
           t_App_AuDAL dal = new t_App_AuDAL();
           return dal.Add(model);
       }
        /// <summary>
        /// 移除授权
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
       public static bool RemoveApp(int key, int accid)
       {
           t_App_AuDAL dal = new t_App_AuDAL();
           return dal.RemoveApp(key, accid);
       }
    }
}
