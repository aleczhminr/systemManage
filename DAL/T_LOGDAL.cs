using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// 登录信息
    /// </summary>
    public class T_LOGDAL : Base.T_LOGBaseDAL
    {


        /// <summary>
        /// 根据店铺ID 得到浏览器比率（店铺为0 则是所有）
        /// </summary>
        /// <param name="accountid">店铺ID</param>
        /// <param name="stat">开始时间</param>
        /// <param name="end">结束时间</param>
        public List<dynamic> GetBrslastGroup(int accountid, DateTime statTime, DateTime endTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" Loginbrslast,COUNT(*) num ");
            strSql.Append(" FROM i200.dbo.T_LOG ");
            strSql.Append(" where OperDate>= CAST(@stat as date) and OperDate <=  CAST( @end as date)");
            if (accountid > 0)
            {
                strSql.Append(" and Accountid=@account");
            }
            strSql.Append(" group by Loginbrslast order by newid() ");

            return DapperHelper.Query(strSql.ToString(), new
            {
                account = accountid,
                stat = statTime,
                end = endTime
            }).ToList();

        }

        /// <summary>
        /// 登录分析
        /// </summary>
        /// <param name="statTime"></param>
        /// <param name="endTime"></param>
        /// <param name="sourceList"></param>
        /// <returns></returns>
        public List<SourceAnalyzeModel> LogSourceAnalyze(DateTime startTime, DateTime endTime, int[] sourceList)
        {

            string sourceStr = "";
            if (sourceList.Length > 0)
            {
                foreach (int id in sourceList)
                {
                    sourceStr += "," + id.ToString();
                }
            }
            if (sourceStr.Length > 0)
            {
                sourceStr = sourceStr.Trim(',');
            }
            else
            {
                sourceStr = "21,22,23,24";
            }

            StringBuilder strSql = new StringBuilder();

            strSql.Append(" create table #table (id int,OperDate datetime,tag_id int) ");
            strSql.Append(" insert into #table(id,OperDate) ");
            strSql.Append(" select Accountid,CAST(OperDate as date) oper from i200.dbo.T_LOG  where  ");
            strSql.Append(" OperDate>@statTime and OperDate<@endTime group by Accountid,CAST(OperDate as date)  ");
            strSql.Append(" update #table set tag_id=a.tag_id from (select id,acc_id,tag_id from Sys_TagNexus where  ");
            strSql.Append(" tag_id in(" + sourceStr + ") and acc_id in(select ID from #table)) a where a.acc_id=#table.id; ");
            strSql.Append(" update #table set tag_id=24 where tag_id is null; ");
            strSql.Append(" select OperDate,tag_id,COUNT(tag_id) countNum from #table group by OperDate,tag_id; ");
            strSql.Append(" drop table #table; ");

            List<dynamic> dynamicList = DapperHelper.Query(strSql.ToString(), new { statTime = startTime, endTime = endTime }).ToList();



            Dictionary<string, SourceAnalyzeModel> sourceModleList = new Dictionary<string, SourceAnalyzeModel>();
            foreach (dynamic item in sourceModleList)
            {
                string timeString = Convert.ToDateTime(item.OperDate).ToString("yyyy-MM-dd");
                if (!sourceModleList.ContainsKey(timeString))
                {
                    sourceModleList[timeString] = new SourceAnalyzeModel()
                    {
                        DateTime = Convert.ToDateTime(item.OperDate)
                    };
                }
                int sourceid = Convert.ToInt32(item.tag_id);

                SourceAnalyzeItemList sourceItemList = new SourceAnalyzeItemList();
                sourceItemList.SourceId = sourceid;
                sourceItemList.CountValue = Convert.ToDecimal(item.countNum);

                sourceModleList[timeString].ItemList.Add(sourceid.ToString(), sourceItemList);

                sourceModleList[timeString].CountValue += sourceItemList.CountValue;
                sourceModleList[timeString].count++;
            }
            List<SourceAnalyzeModel> modelList = new List<SourceAnalyzeModel>();

            foreach (KeyValuePair<string, SourceAnalyzeModel> keyValue in sourceModleList)
            {
                SourceAnalyzeModel sm = keyValue.Value;
                foreach (KeyValuePair<string, SourceAnalyzeItemList> il in sm.ItemList)
                {
                    il.Value.ValueScore = (il.Value.CountValue / sm.CountValue);
                    il.Value.weekend = ((int)sm.DateTime.DayOfWeek).ToString();
                }


                modelList.Add(sm);
            }

            return modelList;
        }

        /// <summary>
        /// 得到一个店铺的最后登录来源
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public T_LogAccountLast GetAccountLastLogSource(int accid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 OperDate,LogInfo IpAddressInfo,LogMode,TypeID Ip from i200.dbo.T_LOG where Accountid=@accid order by ID desc");

            T_LogAccountLast model = DapperHelper.GetModel<T_LogAccountLast>(strSql.ToString(), new { accid = accid });

            if (model != null)
            {
                string logMode = model.LogMode;
                if (logMode.Length > 0)
                {
                    logMode = logMode.Substring(0, 1);
                }
                if (logMode == "4")
                {
                    model.LogSource = "移动端";
                }
                else if (logMode=="3")
                {
                    model.LogSource = "客户端";
                }
                else
                {
                    model.LogSource = "网站";
                }
            }

            return model;
        }


        public List<HourAnalysisItemList> GetLogRegTimeReportSourceDAL(int timeType)
        {
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            startTime = startTime.AddDays(0 - Math.Abs(Convert.ToInt32(timeType)));
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select cast((DATENAME(HOUR,OperDate))as int) [hour],COUNT(DISTINCT Accountid) [num] ");
            strSql.Append(" from  i200.dbo.T_LOG ");
            strSql.Append(" where OperDate>@statTime and OperDate<@endTime ");
            strSql.Append(" group by DATENAME(HOUR,OperDate)  ");
            strSql.Append(" order by hour asc  ");
            List<HourAnalysisItemList> dynamicList = DapperHelper.Query<HourAnalysisItemList>(strSql.ToString(), new { statTime = startTime, endTime = endTime }).ToList();
            return dynamicList;

            //foreach (dynamic itemList in dynamicList)
            //{
            //    HourAnalysisItemList item = new HourAnalysisItemList();
            //    item.hour = itemList.hour;
            //    item.num = itemList.num;
            //}
        }

        public List<HourAnalysisItemList> GetSaleRegTimeReportSourceDAL(int timeType)
        {
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            startTime = startTime.AddDays(0 - Math.Abs(Convert.ToInt32(timeType)));
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select cast((DATENAME(HOUR,insertTime))as int) [hour],COUNT(saleID) [num] ");
            strSql.Append(" from  i200.dbo.T_SaleInfo ");
            strSql.Append(" where insertTime>@statTime and insertTime<@endTime ");
            strSql.Append(" group by DATENAME(HOUR,insertTime)  ");
            strSql.Append(" order by hour asc  ");
            List<HourAnalysisItemList> dynamicList = DapperHelper.Query<HourAnalysisItemList>(strSql.ToString(), new { statTime = startTime, endTime = endTime }).ToList();
            return dynamicList;
        }

        public List<HourAnalysisItemList> GetRegRegTimeReportSourceDAL(int timeType)
        {
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            startTime = startTime.AddDays(0 - Math.Abs(Convert.ToInt32(timeType)));
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select cast((DATENAME(HOUR,RegTime))as int) [hour],COUNT(ID) [num] ");
            strSql.Append(" from  i200.dbo.T_Account ");
            strSql.Append(" where RegTime>@statTime and RegTime<@endTime ");
            strSql.Append(" group by DATENAME(HOUR,RegTime)  ");
            strSql.Append(" order by hour asc  ");
            List<HourAnalysisItemList> dynamicList = DapperHelper.Query<HourAnalysisItemList>(strSql.ToString(), new { statTime = startTime, endTime = endTime }).ToList();
            return dynamicList;
        }

        public List<HourAnalysisItemList> GetClientRegTimeReportSourceDAL(int timeType)
        {
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            startTime = startTime.AddDays(0 - Math.Abs(Convert.ToInt32(timeType)));
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select cast((DATENAME(HOUR,OperDate))as int) [hour],COUNT(DISTINCT Accountid) [num] ");
            strSql.Append(" from  i200.dbo.T_LOG ");
            strSql.Append(" where OperDate>@statTime and OperDate<@endTime and LogMode=3 ");
            strSql.Append(" group by DATENAME(HOUR,OperDate)  ");
            strSql.Append(" order by hour asc  ");
            List<HourAnalysisItemList> dynamicList = DapperHelper.Query<HourAnalysisItemList>(strSql.ToString(), new { statTime = startTime, endTime = endTime }).ToList();
            return dynamicList;
        }
    }
}
