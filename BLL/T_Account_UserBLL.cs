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
    /// 登录账号信息
    /// </summary>
   public static class T_Account_UserBLL
    {
       /// <summary>
       /// 得到一个店铺的登录账号信息
       /// </summary>
       /// <param name="accid"></param>
       /// <returns></returns>
       public static List<T_Account_UserBasic> GetAccountUserBasic( int accid)
       {
           T_Account_UserDAL dal = new T_Account_UserDAL();
           return dal.GetAccountUserBasic(accid);
       } 
    }
}
