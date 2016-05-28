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
    /// 兑换日志
    /// </summary>
    public static class T_ExchangeLogBLL
    {

       /// <summary>
       /// 更新物流信息
       /// </summary>
       /// <param name="id"></param>
       /// <param name="Logistics">物流公司</param>
       /// <param name="LogisticsNumber">物流单号</param>
       /// <param name="SysName">录入人</param>
       /// <returns></returns>
        public static bool UpdateLogistics(int id, string Logistics, string LogisticsNumber, string SysName)
        {
            T_ExchangeLogDAL DAL = new T_ExchangeLogDAL();
            return DAL.UpdateLogistics(id, Logistics, LogisticsNumber, SysName);
        }

    }
}
