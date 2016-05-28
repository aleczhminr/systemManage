using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Sys_PresetKPIDAL
    {
        /// <summary>
        /// 获取KPI数据列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="condition">true为返回当月预设值</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<MonthlyKPI> GetKPIList(int page,bool condition,DateTime dt)
        {
            List<MonthlyKPI> list = new List<MonthlyKPI>();

            int bgNumber = ((page - 1) * 20) + 1;
            int edNumber = (page) * 20;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from (select row_number() over (order by MDate desc) as rowNumber," +
                          "* from Sys_PresetKPI where DelStatus=0 ");
            if (condition)
            {
                strSql.Append(" and DATEDIFF(year,@year,MDate)=0 and DATEDIFF(month,@month,MDate)=0");
            }
            strSql.Append(") as t where t.rowNumber between @bgNumber and @edNumber;");

            if (condition)
            {
                list = DapperHelper.Query<MonthlyKPI>(strSql.ToString(), new { year = dt, month = dt, bgNumber = bgNumber, edNumber = edNumber }).ToList();
            }
            else
            {
                list = DapperHelper.Query<MonthlyKPI>(strSql.ToString(), new { bgNumber = bgNumber, edNumber = edNumber }).ToList();
            }

            return list;
        }

        /// <summary>
        /// 新增KPI数据
        /// </summary>
        /// <param name="kpiModel"></param>
        /// <returns></returns>
        public int SetNewKPI(MonthlyKPI kpiModel)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" declare @count int; ");
            strSql.Append(" select @count=COUNT(*) from Sys_PresetKPI where MDate=@MDate and DelStatus=0; ");
            strSql.Append(" if(isnull(@count,0)>0) ");
            strSql.Append(" begin ");
            strSql.Append(" 	update Sys_PresetKPI set RegNum = @RegNum, SellCount = @SellCount , ");
            strSql.Append(" 	UsrAdd = @UsrAdd ,Sku = @Sku ,Sms = @Sms ,OrderCount = @OrderCount where MDate=@MDate; ");
            strSql.Append(" 	select 2; ");
            strSql.Append(" end ");
            strSql.Append(" else ");
            strSql.Append(" begin ");
            strSql.Append(" 	insert into Sys_PresetKPI(MDate,RegNum,SellCount,UsrAdd,Sku,Sms,OrderCount)  ");
            strSql.Append(" 	values (@MDate,@RegNum,@SellCount,@UsrAdd,@Sku,@Sms,@OrderCount) ; ");
            strSql.Append(" 	select 1; ");
            strSql.Append(" end ");

            object id = DapperHelper.ExecuteScalar(strSql.ToString(), kpiModel);
            if (id != null)
            {
                return Convert.ToInt32(id);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 删除一条KPI数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteKPI(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update Sys_PresetKPI set DelStatus=1 where id=@id");

            int status = DapperHelper.Execute(strSql.ToString(), new {id = id});

            return status;
        }
    }
}
