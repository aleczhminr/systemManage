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
    /// 店铺其他信息
    /// </summary>
   public static class tb_user_inforBLL
    {
        /// <summary>
        /// 初始化一个店铺信息
        /// </summary>
        /// <param name="accid"></param>
       public static void InitializeUserInfor(int accid)
       {
           tb_user_inforDAL dal = new tb_user_inforDAL();
           dal.InitializeUserInfor(accid);
       }

       /// <summary>
       /// 更新模板
       /// </summary>
       /// <param name="accid"></param>
       /// <param name="val"></param>
       /// <returns></returns>
       public static bool UpdateHtmlTemes(int accid, int val)
       {
           tb_user_inforDAL dal = new tb_user_inforDAL();
           return dal.UpdateHtmlTemes(accid, val);
       }
    }
}
