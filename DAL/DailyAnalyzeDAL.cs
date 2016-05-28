using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib;
using Model;

namespace DAL
{
    public class DailyAnalyzeDAL
    {
        public List<IEnumerable<dynamic>> GetDailyAnalyzeList(int page, int source, string column, string whereStr, DateTime dateTime, string orderWhere)
        {
            orderWhere = " order by " + (orderWhere.Length > 0 ? orderWhere : "id desc");
            page = page < 1 ? 1 : page;
            int pageSize = 15;

            StringBuilder strSql = new StringBuilder();

            //用户来源标签显示
            var flagColumn = "";
            var flagSql = new StringBuilder();
            if (source == 0)
            {
                flagColumn = " ,Sys_TagNexus.t_Name";
                flagSql.Append(" outer apply ( select Sys_TagInfo.t_Name from Sys_TagNexus left outer join Sys_TagInfo on Sys_TagInfo.id=Sys_TagNexus.tag_id");
                flagSql.Append(" where Sys_TagNexus.tag_id in (21,22,23,24,30) and Sys_TagNexus.acc_id=SysRpt_ShopDayInfo.accountid) Sys_TagNexus");
            }
            else if (source != 0)
            {
                flagColumn = " ,Sys_TagNexus.t_Name";
                flagSql.Append(" cross apply ( select Sys_TagInfo.t_Name from Sys_TagNexus left outer join Sys_TagInfo on Sys_TagInfo.id=Sys_TagNexus.tag_id");
                flagSql.Append(" where Sys_TagNexus.tag_id=@tagId and Sys_TagNexus.acc_id=SysRpt_ShopDayInfo.accountid) Sys_TagNexus");
            }
            else
            {
                flagSql.Append("");
            }

            strSql.Append(" SELECT  ");
            strSql.Append("  " + column + " ");
            strSql.Append(flagColumn);
            strSql.Append(" ,ROW_NUMBER() OVER (" + orderWhere + ") AS 'RowNumber' into #OrderedOrders ");
            strSql.Append(" FROM SysRpt_ShopDayInfo  ");
            strSql.Append(flagSql);
            if (whereStr.Length > 0)
            {
                strSql.Append("     where " + whereStr);
            }
            strSql.Append("; SELECT #OrderedOrders.* ");
            strSql.Append(" FROM #OrderedOrders  ");
            strSql.Append(" WHERE RowNumber BETWEEN ((@pageIndex-1) * @pageSize)+1 AND (@pageIndex * @pageSize) ");
            strSql.Append(" select count(*) num from #OrderedOrders; drop table #OrderedOrders");

            return DapperHelper.QueryMultiple(strSql.ToString(), new
            {
                tagId = source,
                pageIndex = page,
                pageSize = pageSize,
                nowDay = dateTime
            });
        }
    }
}
