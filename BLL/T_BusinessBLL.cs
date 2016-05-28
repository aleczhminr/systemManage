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
    /// 店铺基本配置信息
    /// </summary>
    public static class T_BusinessBLL
    {

        /// <summary>
        /// 得到短信配置
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public static T_AccountConfig.SmsBillingConfig GetSmsBillingConfig(int accid)
        {
            T_BusinessDAL dal = new T_BusinessDAL();
            return dal.GetSmsBillingConfig(accid);
        }

    }
}
