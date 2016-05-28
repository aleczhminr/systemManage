using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// 积分商城兑换日志
    /// </summary>
    public class T_ExchangeLogDAL
    {
        /// <summary>
        /// 更新物流信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Logistics"></param>
        /// <param name="LogisticsNumber"></param>
        /// <param name="SysName"></param>
        /// <returns></returns>
        public bool UpdateLogistics(int id, string Logistics, string LogisticsNumber, string SysName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_ExchangeLog set eLogistics=@Logistics,eLogisticsNumber=@LogisticsNumber,eSysName=@SysName,eSysTime=GETDATE(),eState=1 where id=@id  and eState=0;");

            int e = HelperForFrontend.Execute(strSql.ToString(), new
            {
                id = id,
                Logistics = Logistics,
                LogisticsNumber = LogisticsNumber,
                SysName = SysName
            });
            if (e > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}
