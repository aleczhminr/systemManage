using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class T_LogInfoDAL
    {
        public List<dynamic> GetIntegrals(DateTime stDate, DateTime edDate)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(
                "SELECT cast(CreatTime as date) dayDate," +
                "sum( case when (cast(FinialVal as int)>cast(OriginalVal as int)) then cast(EditVal as int) else 0 end ) IntegralPaid," +
                "sum( case when (cast(FinialVal as int)<cast(OriginalVal as int)) then cast(EditVal as int) else 0 end ) IntegralExchange " +
                "FROM [i200].[dbo].[T_LogInfo] where CreatTime>=@stDate and CreatTime<@edDate and Keys='Integral' group by cast(CreatTime as date);");

            return
                DapperHelper.Query<dynamic>(strSql.ToString(), new {stDate = stDate, edDate = edDate})
                    .ToList();
        }
    }
}
