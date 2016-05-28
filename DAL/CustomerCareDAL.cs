using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Utility;


namespace DAL
{
    public class CustomerCareDAL
    {
        /// <summary>
        /// 返回客服数据汇总信息
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public Dictionary<string, List<dynamic>> GetServStatChartData(DateTime startTime, DateTime endTime)
        {
            Dictionary<string, List<dynamic>> dataDic = new Dictionary<string, List<dynamic>>();

                dataDic.Add("UsrPer", null);
                dataDic.Add("ServPer", null);
                dataDic.Add("ChannelPer", null);

                StringBuilder strSql = new StringBuilder();
                strSql.Append("select convert(varchar(10),vf.insertTime,120) as insertTime,accid,SysRpt_ShopInfo.regTime,Sys_VisitManner.vm_name,vf.insertName into #tab ");
                strSql.Append(" from Sys_VisitInfo vf left join SysRpt_ShopInfo on accid=SysRpt_ShopInfo.accountid left join Sys_VisitManner on vf.vm_id=Sys_VisitManner.id ");
                strSql.Append(" where handleStat<>0 and vf.insertTime<@endTime and vf.insertTime>@begTime order by vf.insertTime desc;");
                strSql.Append(" select * from #tab where insertName in (select name from Sys_Manage_User where state=1);");
                strSql.Append(" drop table #tab;");

                dataDic["UsrPer"] =
                    DapperHelper.Query<dynamic>(strSql.ToString(), new { begTime = startTime, endTime = endTime }).ToList();
                strSql.Clear();

                strSql.Append("select convert(varchar(10),vf.insertTime,120) as insertTime,accid,SysRpt_ShopInfo.regTime,Sys_VisitManner.vm_name,vf.insertName into #tab ");
                strSql.Append(" from Sys_VisitInfo vf left join SysRpt_ShopInfo on accid=SysRpt_ShopInfo.accountid left join Sys_VisitManner on vf.vm_id=Sys_VisitManner.id ");
                strSql.Append(" where handleStat<>0 and vf.insertTime<@endTime and vf.insertTime>@begTime order by vf.insertTime desc;");
                strSql.Append(" select insertName,COUNT(insertName) as cnt from #tab where insertName in (select name from Sys_Manage_User where state=1) group by insertName;");
                strSql.Append(" drop table #tab;");

                dataDic["ServPer"] =
                    DapperHelper.Query<dynamic>(strSql.ToString(), new { begTime = startTime, endTime = endTime }).ToList();
                strSql.Clear();

                strSql.Append("select convert(varchar(10),vf.insertTime,120) as insertTime,accid,SysRpt_ShopInfo.regTime,Sys_VisitManner.vm_name,vf.insertName into #tab ");
                strSql.Append(" from Sys_VisitInfo vf left join SysRpt_ShopInfo on accid=SysRpt_ShopInfo.accountid left join Sys_VisitManner on vf.vm_id=Sys_VisitManner.id ");
                strSql.Append(" where handleStat<>0 and vf.insertTime<@endTime and vf.insertTime>@begTime order by vf.insertTime desc;");
                strSql.Append(" select vm_name,COUNT(vm_name) as cnt from #tab where insertName in (select name from Sys_Manage_User where state=1) group by vm_name;");
                strSql.Append(" drop table #tab;");

                dataDic["ChannelPer"] =
                    DapperHelper.Query<dynamic>(strSql.ToString(), new { begTime = startTime, endTime = endTime }).ToList();

            
            
            return dataDic;
        }

        /// <summary>
        /// 返回订单数据
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="dayCnt"></param>
        /// <param name="type"></param>
        /// <param name="person"></param>
        /// <returns></returns>
        public List<dynamic> getOrderInfo(DateTime startTime, DateTime endTime, int dayCnt, int type,
            string person)
        {
            return DapperHelper.QueryEx("i200.dbo.PayAfterService", new
            {
                edDate = endTime,
                bgDate = startTime,
                dayCount = dayCnt,
                type = type
            }).ToList();
            //return DbManageSQL.RunProcedure("i200.dbo.PayAfterService", sqlparm, "data");
        }

        public List<dynamic> getVisitInfo_customer()
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select a_customerSourceType,COUNT(*) as Cnt ");
            strSql.Append(" from Sys_Account ");
            strSql.Append(" where a_customerSourceType is not null group by a_customerSourceType;");

            return DapperHelper.Query<dynamic>(strSql.ToString()).ToList();
        }

        public List<dynamic> getVisitInfo_software()
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select a_OtherSoftwareType,COUNT(*) as Cnt ");
            strSql.Append(" from Sys_Account ");
            strSql.Append(" where a_OtherSoftwareType is not null group by a_OtherSoftwareType;");

            return DapperHelper.Query<dynamic>(strSql.ToString()).ToList();
        }

        public int getVisitInfo_count()
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" select count(*) from Sys_TagNexus where tag_id=20;");

            return DapperHelper.Query<int>(strSql.ToString()).ToList()[0];
        }

        public List<dynamic> getVisitDetail(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select accid,[I200].[dbo].[T_Account].CompanyName,a_OtherSoftware,a_UseCause,a_CustomerSource ");
            strSql.Append(" from Sys_Account left join [I200].[dbo].[T_Account] on Sys_Account.accid=[I200].[dbo].[T_Account].id ");
            strSql.Append(" where " + where);

            return DapperHelper.Query<dynamic>(strSql.ToString()).ToList();

        }


        #region 客服后用户登录留存
        /// <summary>
        /// 获取客服后用户登录留存
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public UserRetentionModel GetCareRetention(DateTime stDate, DateTime edDate,string dateType,string usrName)
        {
            UserRetentionModel retentionModel = new UserRetentionModel();
            List<TimePeriodModel> timePeriod = GetDatePeriod(stDate, edDate, dateType);
            List<TimePeriodModel> limitList = new List<TimePeriodModel>();

            int limitCount = 0;

            switch (dateType)
            {
                case "day":
                    limitCount = 50;
                    break;
                case "week":
                case "month":
                    limitCount = 30;
                    break;
            }

            if (timePeriod.Count > limitCount)
            {
                timePeriod.Reverse();
                limitList = timePeriod.Take(limitCount).ToList();
                limitList.Reverse();

                foreach (TimePeriodModel period in limitList)
                {
                    retentionModel.dataList.Add(GetRetentionRowData(dateType, period, usrName));
                }
            }
            else
            {
                foreach (TimePeriodModel period in timePeriod)
                {
                    retentionModel.dataList.Add(GetRetentionRowData(dateType, period, usrName));
                }
            }

            #region 获取平均留存率
            List<decimal> dailyRatio = new List<decimal>();

            //初始注册值列表
            Dictionary<int, int> initialAccid = new Dictionary<int, int>();

            for (int i = 0; i < retentionModel.dataList.Count; i++)
            {
                initialAccid.Add(i + 1, GetInitialSum(retentionModel.dataList, i + 1));
            }

            int count = 0;
            int dailySum = 0;
            for (int i = 0; i < 12; i++)
            {
                foreach (RetentionList item in retentionModel.dataList)
                {
                    if (item != null)
                    {
                        if (item.DataList[i].NowActive != 0)
                        {
                            count += 1;
                            dailySum += item.DataList[i].NowActive;
                        }
                    }
                }

                if (count == 0 || initialAccid[count] == 0)
                {
                    dailyRatio.Add(0);
                }
                else
                {
                    dailyRatio.Add((decimal)dailySum * 100 / initialAccid[count]);
                }


                count = 0;
                dailySum = 0;

            }

            retentionModel.avgRatio = dailyRatio;
            #endregion

            return retentionModel;
        }

        /// <summary>
        /// 获取留存率每行的对象
        /// </summary>
        /// <param name="dateType"></param>
        /// <param name="rowInitTime"></param>
        /// <param name="usrType"></param>
        /// <param name="regSource"></param>
        /// <returns></returns>
        public RetentionList GetRetentionRowData(string dateType, TimePeriodModel rowInitTime,string usrName)
        {
            RetentionList rowObj = new RetentionList();
            List<int> accIdList = new List<int>();
            StringBuilder strSql = new StringBuilder();

            List<TimePeriodModel> rowPeriod = new List<TimePeriodModel>();

            int usrCount = 0;
            int initialCount = 0;
            List<int> dayAccids = new List<int>();

            DateTime edDate = DateTime.Now;

            //根据日期类型对行数据段进行二次拆分
            switch (dateType)
            {
                case "day":
                    edDate = rowInitTime.StDate.AddDays(12);
                    break;
                case "week":
                    edDate = rowInitTime.StDate.AddDays(7 * 12);
                    break;
                case "month":
                    edDate = rowInitTime.StDate.AddMonths(12);
                    break;
            }

            //拆出每行的时间段对象
            rowPeriod = GetDatePeriod(rowInitTime.StDate, edDate, dateType);
            if (rowPeriod.Count < 1)
            {
                return null;
            }

            //获取每行首个数据
            rowObj = GetInitialAccidByCondition(rowPeriod[0],usrName);
            if (rowObj == null)
            {
                return null;
            }

            initialCount = rowObj.InitialCount;
            //拼接初始化的AccId列表，为零则返回null
            string accids = "(";
            foreach (int accid in rowObj.InitialAccId)
            {
                accids += accid.ToString() + ",";
            }
            if (accids.Length > 1)
            {
                accids = accids.Substring(0, accids.LastIndexOf(',')) + ")";
            }
            else
            {
                return null;
            }

            int periodCount = 0;
            rowPeriod.RemoveAt(0);

            if (rowPeriod.Count > 0)
            {
                foreach (TimePeriodModel period in rowPeriod)
                {
                    strSql.Clear();
                    strSql.Append("select count(distinct accountid) from i200.dbo.T_LOG where OperDate between @bgTime and @edTime and accountid in " + accids + " ;");
                    usrCount = DapperHelper.ExecuteScalar<int>(strSql.ToString(), new { bgTime = period.StDate, edTime = period.EdDate });

                    strSql.Clear();
                    strSql.Append("select distinct accountid from i200.dbo.T_LOG where OperDate between @bgTime and @edTime and accountid in " + accids + " ;");
                    dayAccids = DapperHelper.Query<int>(strSql.ToString(), new { bgTime = period.StDate, edTime = period.EdDate }).ToList();

                    periodCount++;

                    string mdString = "";
                    int ratio = 0;

                    if (initialCount != 0)
                    {
                        ratio = usrCount * 100 / initialCount;
                    }
                    else
                    {
                        ratio = 0;
                    }

                    List<int> lostAccids = new List<int>();
                    foreach (int i in rowObj.InitialAccId)
                    {
                        if (!dayAccids.Contains(i))
                        {
                            lostAccids.Add(i);
                        }
                    }

                    mdString = usrCount.ToString() + periodCount.ToString() + ratio.ToString();
                    rowObj.DataList.Add(new DailyRetention(periodCount, usrCount, ratio, CommonLib.Helper.JsonSerializeObject(dayAccids),
                        CommonLib.Helper.Md5Hash(mdString), CommonLib.Helper.JsonSerializeObject(lostAccids)));//定制列标记符，（规则为记录数+天数标识+比例数值）转MD5

                }
            }

            return rowObj;
        }

        /// <summary>
        /// 获取每行初始Model并获得初始数据
        /// </summary>
        /// <param name="timePeriod"></param>
        /// <param name="usrType"></param>
        /// <param name="regSource"></param>
        /// <returns></returns>
        public RetentionList GetInitialAccidByCondition(TimePeriodModel timePeriod,string usrName)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSource = new StringBuilder();

            RetentionList rowFirstObj = new RetentionList();
            int initialCount = 0;
            List<int> initAccidList = new List<int>();

            int firstDayLog = 0;
            List<int> firstDayAccids = new List<int>();

            if (usrName=="all")
            {
                strSql.Append("select count(distinct accid) from Sys_VisitInfo where insertTime between @stTime and @edTime and insertName<>'系统' ");
                initialCount = DapperHelper.ExecuteScalar<int>(strSql.ToString(),
                            new { stTime = timePeriod.StDate, edTime = timePeriod.EdDate });
                strSql.Clear();
                strSql.Append(
                    "select distinct accid from Sys_VisitInfo where insertTime between @stTime and @edTime and insertName<>'系统'");
                initAccidList =
                            DapperHelper.Query<int>(strSql.ToString(),
                                new { stTime = timePeriod.StDate, edTime = timePeriod.EdDate }).ToList();
            }
            else
            {
                strSql.Append(
                    "select count(distinct accid) from Sys_VisitInfo where insertTime between @stTime and @edTime and insertName='" +
                    usrName + "' ");
                initialCount = DapperHelper.ExecuteScalar<int>(strSql.ToString(),
                            new { stTime = timePeriod.StDate, edTime = timePeriod.EdDate });
                strSql.Clear();
                strSql.Append(
                    "select distinct accid from Sys_VisitInfo where insertTime between @stTime and @edTime and insertName='" +
                    usrName + "' ");
                initAccidList =
                            DapperHelper.Query<int>(strSql.ToString(),
                                new { stTime = timePeriod.StDate, edTime = timePeriod.EdDate }).ToList();
            }
            

            //添加一个新的留存率日期行对象
            rowFirstObj.Date = timePeriod.TimePeriod;
            rowFirstObj.InitialAccId = initAccidList;
            rowFirstObj.RowMark = CommonLib.Helper.Md5Hash(rowFirstObj.Date);
            rowFirstObj.InitialCount = initialCount;
            DailyRetention monthlyRetention = new DailyRetention();

            //首日注册用户登录量
            //拼接初始化的AccId列表，为零则返回null
            string accids = "(";
            foreach (int accid in rowFirstObj.InitialAccId)
            {
                accids += accid.ToString() + ",";
            }
            if (accids.Length > 1)
            {
                accids = accids.Substring(0, accids.LastIndexOf(',')) + ")";
            }
            else
            {
                return null;
            }

            strSql.Clear();
            strSql.Append("select count(distinct accountid) from i200.dbo.T_LOG where OperDate between @bgTime and @edTime and accountid in " + accids + ";");
            firstDayLog = DapperHelper.ExecuteScalar<int>(strSql.ToString(), new { bgTime = timePeriod.StDate, edTime = timePeriod.EdDate });

            strSql.Clear();
            strSql.Append("select distinct accountid from i200.dbo.T_LOG where OperDate between @bgTime and @edTime and accountid in " + accids + ";");
            firstDayAccids =
                DapperHelper.Query<int>(strSql.ToString(), new { bgTime = timePeriod.StDate, edTime = timePeriod.EdDate })
                    .ToList();

            //添加首日初始化数据，日期标号为0
            monthlyRetention.DayNum = 0;
            monthlyRetention.NowActive = firstDayLog;
            monthlyRetention.NoActive = initialCount;

            if (initialCount != 0)
            {
                monthlyRetention.Ratio = firstDayLog * 100 / initialCount;
            }
            else
            {
                monthlyRetention.Ratio = 0;
            }
            monthlyRetention.DayAccids = CommonLib.Helper.JsonSerializeObject(firstDayAccids);

            string mdString = firstDayLog.ToString() + monthlyRetention.DayNum.ToString() +
                                          monthlyRetention.Ratio.ToString();//定制列标记符，（规则为记录数+天数标识+比例数值）转MD5

            monthlyRetention.ColumnMark = CommonLib.Helper.Md5Hash(mdString);

            rowFirstObj.DataList.Add(monthlyRetention);

            return rowFirstObj;
        }

        /// <summary>
        /// 返回不同类型的日期分段
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <param name="dateType"></param>
        /// <returns></returns>
        public List<TimePeriodModel> GetDatePeriod(DateTime stDate, DateTime edDate, string dateType)
        {
            List<TimePeriodModel> timePeriodList = new List<TimePeriodModel>();
            switch (dateType)
            {
                case "day":
                    while (stDate < edDate)
                    {
                        timePeriodList.Add(new TimePeriodModel(stDate.ToShortDateString(), stDate, stDate.AddHours(23).AddMinutes(59).AddSeconds(59)));
                        stDate = stDate.AddDays(1);
                    }
                    break;
                case "week":
                    while (stDate < edDate)
                    {
                        timePeriodList.Add(new TimePeriodModel(stDate.ToShortDateString() + "~" + stDate.AddDays(6).ToShortDateString(), stDate, stDate.AddDays(6)));
                        stDate = stDate.AddDays(6);
                    }
                    break;
                case "month":
                    while (stDate < edDate)
                    {
                        timePeriodList.Add(new TimePeriodModel(stDate.ToShortDateString() + "~" + stDate.AddMonths(1).ToShortDateString(), stDate, stDate.AddMonths(1)));
                        stDate = stDate.AddMonths(1);
                    }
                    break;
            }

            return timePeriodList;
        }

        #endregion

        public CarePercentModel GetCarePartitionPer(DateTime stDate, DateTime edDate, string usrName, int partIndex)
        {
            CarePercentModel careModel = new CarePercentModel();
            StringBuilder strSql = new StringBuilder();

            string filter = "";
            if (usrName == "all")
            {
                filter = " and insertName <> '系统' ";
            }
            else
            {
                filter = " and insertName='" + usrName + "'";
            }

            //服务过用户
            List<int> newRegAccid = new List<int>();
            List<int> activeAccid = new List<int>();
            List<int> sleepAccid = new List<int>();
            List<int> lostAccid = new List<int>();
            //全部用户
            List<int> newRegAccidAll = new List<int>();
            List<int> activeAccidAll = new List<int>();
            List<int> sleepAccidAll = new List<int>();
            List<int> lostAccidAll = new List<int>();

            switch (partIndex)
            {
                case 1:
                    while (stDate < edDate)
                    {
                        //活跃用户
                        strSql.Append(
                        "select accid from SysRpt_ShopActive where DATEDIFF(DAY,@date,updatetime)<lastNum and DATEDIFF(DAY,@date,updatetime)>0 and active in (5,7);");
                        List<int> active = DapperHelper.Query<int>(strSql.ToString(), new { date = stDate }).ToList();
                        foreach (int i in active)
                        {
                            if (!activeAccidAll.Contains(i))
                            {
                                activeAccidAll.Add(i);
                            }
                        }
                        
                        string accids = "(";
                        if (active.Count > 1)
                        {
                            foreach (int accid in active)
                            {
                                accids += accid.ToString() + ",";
                            }
                            accids = accids.Substring(0, accids.LastIndexOf(',')) + ")";

                            strSql.Clear();
                            strSql.Append(
                            "select accid from Sys_VisitInfo where DATEDIFF(DAY,@date,insertTime)=0 and accid in " + accids + " " + filter + " group by accid;");

                            List<int> accList = DapperHelper.Query<int>(strSql.ToString(), new { date = stDate, dateEnd = stDate.AddHours(23).AddMinutes(59).AddSeconds(59) }).ToList();

                            foreach (int i in accList)
                            {
                                if (!activeAccid.Contains(i))
                                {
                                    activeAccid.Add(i);
                                }
                            }
                        }

                        strSql.Clear();

                        stDate = stDate.AddDays(1);
                    }
                    break;
                case 2:
                    while (stDate < edDate)
                    {
                        //新用户
                        strSql.Append(
                        "select accid from SysRpt_ShopActive where DATEDIFF(DAY,@date,updatetime)<lastNum and DATEDIFF(DAY,@date,updatetime)>0 and active=1;");
                        List<int> newReg = DapperHelper.Query<int>(strSql.ToString(), new { date = stDate }).ToList();
                        foreach (int i in newReg)
                        {
                            if (!newRegAccidAll.Contains(i))
                            {
                                newRegAccidAll.Add(i);
                            }
                        }
                        string newRegAccids = "(";
                        if (newReg.Count > 1)
                        {
                            foreach (int accid in newReg)
                            {
                                newRegAccids += accid.ToString() + ",";
                            }
                            newRegAccids = newRegAccids.Substring(0, newRegAccids.LastIndexOf(',')) + ")";

                            strSql.Clear();
                            strSql.Append(
                            "select accid from Sys_VisitInfo where insertTime between @date and @dateEnd and accid in " + newRegAccids + " " + filter + " group by accid;");

                            List<int> regAccId = DapperHelper.Query<int>(strSql.ToString(), new { date = stDate, dateEnd = stDate.AddHours(23).AddMinutes(59).AddSeconds(59) }).ToList();
                            foreach (int i in regAccId)
                            {
                                if (!newRegAccid.Contains(i))
                                {
                                    newRegAccid.Add(i);
                                }
                            }
                        }

                        strSql.Clear();

                        stDate = stDate.AddDays(1);
                    }
                    break;
                case 3:
                    while (stDate < edDate)
                    {
                        //休眠用户
                        strSql.Append(
                        "select accid from SysRpt_ShopActive where DATEDIFF(DAY,@date,updatetime)<lastNum and DATEDIFF(DAY,@date,updatetime)>0 and active=-1;");
                        List<int> sleep = DapperHelper.Query<int>(strSql.ToString(), new { date = stDate }).ToList();
                        foreach (int i in sleep)
                        {
                            if (!sleepAccidAll.Contains(i))
                            {
                                sleepAccidAll.Add(i);
                            }
                        }
                        string sleepAccids = "(";
                        if (sleep.Count > 1)
                        {
                            foreach (int accid in sleep)
                            {
                                sleepAccids += accid.ToString() + ",";
                            }
                            sleepAccids = sleepAccids.Substring(0, sleepAccids.LastIndexOf(',')) + ")";

                            strSql.Clear();
                            strSql.Append(
                            "select accid from Sys_VisitInfo where insertTime between @date and @dateEnd and accid in " + sleepAccids + " " + filter + " group by accid;" );

                           List<int> sleepAccIdList = DapperHelper.Query<int>(strSql.ToString(), new { date = stDate, dateEnd = stDate.AddHours(23).AddMinutes(59).AddSeconds(59) }).ToList();
                            foreach (int i in sleepAccIdList)
                            {
                                if (!sleepAccid.Contains(i))
                                {
                                    sleepAccid.Add(i);
                                }
                                
                            }
                        }

                        strSql.Clear();

                        stDate = stDate.AddDays(1);
                    }
                    break;
                case 4:
                    while (stDate < edDate)
                    {
                        //流失用户
                        strSql.Append(
                        "select accid into #List from SysRpt_ShopActive where DATEDIFF(DAY,@date,updatetime)<lastNum and DATEDIFF(DAY,@date,updatetime)>0 and active=-3;" +
                        "select accid from #List;");
                        //List<int> lost = DapperHelper.Query<int>(strSql.ToString(), new { date = stDate }).ToList();
                        
                        strSql.Append(
                        "select accid from Sys_VisitInfo where insertTime between @date and @dateEnd and accid in (select accid from #List) " + filter + " group by accid;" +
                        "drop table #List;");

                        List<IEnumerable<dynamic>> lostAccidList = DapperHelper.QueryMultiple(strSql.ToString(), new { date = stDate, dateEnd = stDate.AddHours(23).AddMinutes(59).AddSeconds(59) }).ToList();
                        
                        List<dynamic> lost = lostAccidList[0].ToList();
                        foreach (dynamic i in lost)
                        {
                            if (!lostAccidAll.Contains(i.accid))
                            {
                                lostAccidAll.Add(i.accid);
                            }
                        }

                        List<dynamic> lostId = lostAccidList[1].ToList();
                        foreach (dynamic i in lostId)
                        {
                            if (!lostAccid.Contains(i.accid))
                            {
                                lostAccid.Add(i.accid);
                            }
                        }

                        strSql.Clear();

                        stDate = stDate.AddDays(1);
                    }
                    break;
            }

            switch (partIndex)
            {
                case 1:
                    careModel.ListData.Add(new PercentPart("active", activeAccid.Count, activeAccidAll.Count, (activeAccidAll.Count == 0 ? 0 : (Convert.ToDouble(activeAccid.Count) * 100 / activeAccidAll.Count)).ToString()));
                    break;
                case 2:
                    careModel.ListData.Add(new PercentPart("new", newRegAccid.Count, newRegAccidAll.Count, (newRegAccidAll.Count == 0 ? 0 : (Convert.ToDouble(newRegAccid.Count) * 100 / newRegAccidAll.Count)).ToString()));
                    break;
                case 3:
                    careModel.ListData.Add(new PercentPart("sleep", sleepAccid.Count, sleepAccidAll.Count, (sleepAccidAll.Count == 0 ? 0 : (Convert.ToDouble(sleepAccid.Count) * 100 / sleepAccidAll.Count)).ToString()));
                    break;
                case 4:
                    careModel.ListData.Add(new PercentPart("lost", lostAccid.Count, lostAccidAll.Count, (lostAccidAll.Count == 0 ? 0 : (Convert.ToDouble(lostAccid.Count) * 100 / lostAccidAll.Count)).ToString()));
                    break;
            }
           
            return careModel;
        }

        #region 订单续费率
        public List<OrderRenewalModel> GetOrderRenewalModels(DateTime stDate, DateTime edDate, string type)
        {
            DateTime nowDate = DateTime.Now;
            List<OrderRenewalModel> renewList = new List<OrderRenewalModel>();

            switch (type)
            {
                case "1":
                    renewList=GetSingleOrderRenewalModels(nowDate);
                    break;
                case "2":
                    renewList=GetSingleOrderRenewalModels(nowDate.AddMonths(-1));
                    break;
                case "3":
                    renewList = GetMultOrderRenewalModels(nowDate.AddMonths(-3), nowDate);
                    break;
                case "0":
                    renewList = GetMultOrderRenewalModels(stDate, edDate);
                    break;
            }

            return renewList;
        }

        public static List<OrderRenewalModel> GetSingleOrderRenewalModels(DateTime time)
        {
            List<OrderRenewalModel> thisMonthList = new List<OrderRenewalModel>();
            OrderRenewalModel thisMonth = new OrderRenewalModel();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select o.accid,o.transactionDate,r.itemId,r.itemQuantity into #List " +
                "from i200.dbo.T_OrderInfo o " +
                "left join i200.dbo.T_Order_List r " +
                "on o.oid=r.oid " +
                "where r.itemId<>1;");

            strSql.Append(
                "select accid into #accidList from #List where itemId<>11 and DATEDIFF(MONTH,transactionDate,@date)=itemQuantity " +
                "union " +
                "select accid from #List where itemId=11 and DATEDIFF(MONTH,transactionDate,@date)=itemQuantity*12;");

            strSql.Append("select * from #accidList;");
            strSql.Append("select o.accid from i200.dbo.T_OrderInfo o left join i200.dbo.T_Order_List r on o.oid=r.oid where r.itemId not in (1,21,22,23) and DateDiff(month,o.transactionDate,@date)=0 and o.accid in (select accid from #accidList);");

            strSql.Append("drop table #accidList;");
            strSql.Append("drop table #List;");

            thisMonth.Date = time.ToString("yyyy-MM");
            List<IEnumerable<dynamic>> multiList = DapperHelper.QueryMultiple(strSql.ToString(),
                new { date = time }).ToList();

            thisMonth.ExpireUsr = multiList[0].Count();
            if (thisMonth.ExpireUsr!=0)
            {
                foreach (dynamic i in multiList[0])
                {
                    if (!thisMonth.ExAccids.Contains(i.accid))
                    {
                        thisMonth.ExAccids.Add(i.accid);
                    }
                }
            }
            
            thisMonth.RenewalUsr = multiList[1].Count();
            if (thisMonth.RenewalUsr!=0)
            {
                foreach (dynamic i in multiList[1])
                {
                    if (!thisMonth.ReAccids.Contains(i.accid))
                    {
                        thisMonth.ReAccids.Add(i.accid);
                    }
                }
            }

            if (thisMonth.ExpireUsr!=0)
            {
                thisMonth.Ratio = thisMonth.RenewalUsr*100/thisMonth.ExpireUsr;
            }
            thisMonth.ExAccidStr = CommonLib.Helper.JsonSerializeObject(thisMonth.ExAccids);
            thisMonth.ReAccidStr = CommonLib.Helper.JsonSerializeObject(thisMonth.ReAccids);

            thisMonth.NotReUsr = thisMonth.ExpireUsr - thisMonth.RenewalUsr;
            thisMonth.NotReAccidStr = CommonLib.Helper.JsonSerializeObject(thisMonth.ExAccids.Except(thisMonth.ReAccids));

            thisMonthList.Add(thisMonth);

            List<OrderRenewalModel> dailyList = GetDailyRenewalModel(time);
            if (dailyList.Count > 0)
            {
                foreach (OrderRenewalModel mod in dailyList)
                {
                    thisMonthList.Add(mod);
                }
            }

            return thisMonthList;
        }

        public static List<OrderRenewalModel> GetMultOrderRenewalModels(DateTime stDate,DateTime edDate)
        {
            List<OrderRenewalModel> ordList = new List<OrderRenewalModel>();
            List<DateTime> timeList = new List<DateTime>();

            while (stDate.Year != edDate.Year || stDate.Month != edDate.Month)
            {
                timeList.Add(stDate);
                stDate = stDate.AddMonths(1);
            }
            timeList.Add(edDate);

            foreach (DateTime day in timeList)
            {
                List<OrderRenewalModel> monthList = GetSingleOrderRenewalModels(day);
                if (monthList.Count > 0)
                {
                    foreach (OrderRenewalModel model in monthList)
                    {
                        ordList.Add(model);

                        //List<OrderRenewalModel> dailyList = GetDailyRenewalModel(day);
                        //if (dailyList.Count > 0)
                        //{
                        //    foreach (OrderRenewalModel mod in dailyList)
                        //    {
                        //        ordList.Add(mod);
                        //    }
                        //}
                    }
                }
                
            }

            return ordList;

        }

        public static List<OrderRenewalModel> GetDailyRenewalModel(DateTime monthTime)
        {
            List<OrderRenewalModel> modelList = new List<OrderRenewalModel>();

            DateTime edTime = monthTime.AddMonths(1).AddDays(-monthTime.Day); //获取本月最后一天
            if (monthTime.Month==DateTime.Now.Month)
            {
                if (edTime>DateTime.Now)
                {
                    edTime = DateTime.Now;
                }
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select o.accid,o.transactionDate,r.itemId,r.itemQuantity into #List " +
                "from i200.dbo.T_OrderInfo o " +
                "left join i200.dbo.T_Order_List r " +
                "on o.oid=r.oid " +
                "where r.itemId<>1;");

            strSql.Append(
                "select accid,transactionDate,itemId,itemQuantity into #accidList from #List where itemId<>11 and DATEDIFF(MONTH,transactionDate,@date)=itemQuantity " +
                "union " +
                "select accid,transactionDate,itemId,itemQuantity from #List where itemId=11 and DATEDIFF(MONTH,transactionDate,@date)=itemQuantity*12;");

            strSql.Append("select accid,DATEADD(MONTH,itemQuantity,transactionDate) endTime from #accidList where itemId<>11 " +
                          "union " +
                          "select accid,DATEADD(year,itemQuantity,transactionDate) endTime from #accidList where itemId=11");

            List<EndTimeModel> endTime =
                DapperHelper.Query<EndTimeModel>(strSql.ToString(), new {date = monthTime}).ToList();

            List<EndTimeModel> dayList = new List<EndTimeModel>();
            StringBuilder strPartSql = new StringBuilder();

            for (DateTime iter = monthTime.AddDays(1 - monthTime.Day); iter < edTime; iter = iter.AddDays(1))
            {
                dayList = endTime.FindAll(x => x.EndTime.Date == iter.Date);
                if (dayList.Count > 0)
                {
                    OrderRenewalModel dayModel = new OrderRenewalModel();
                    dayModel.Date = iter.ToShortDateString();
                    dayModel.DailyFlag = iter.ToString("yyyy-MM");
                    dayModel.ExpireUsr = dayList.Count;

                    string exAccid = "";

                    foreach (EndTimeModel model in dayList)
                    {
                        //dayModel.ExpireUsr += 1;
                        dayModel.ExAccids.Add(model.AccId);
                        exAccid += model.AccId.ToString() + ",";
                    }

                    dayModel.ExAccidStr = CommonLib.Helper.JsonSerializeObject(dayModel.ExAccids);

                    strPartSql.Clear();
                    strPartSql.Append(
                        "select o.accid from i200.dbo.T_OrderInfo o left join i200.dbo.T_Order_List r on o.oid=r.oid where r.itemId<>1 and DateDiff(day,o.transactionDate,@date)=0 and o.accid in (" +
                        exAccid.Trim(',') + "); ");

                    List<int> reAccid = DapperHelper.Query<int>(strPartSql.ToString(), new {date = iter}).ToList();

                    if (reAccid.Count > 0)
                    {
                        dayModel.RenewalUsr = reAccid.Count;
                        dayModel.ReAccids = reAccid;
                        dayModel.ReAccidStr = CommonLib.Helper.JsonSerializeObject(dayModel.ReAccids);
                    }

                    if (dayModel.ExpireUsr!=0)
                    {
                        dayModel.Ratio = dayModel.RenewalUsr*100/dayModel.ExpireUsr;
                    }
                    else
                    {
                        dayModel.Ratio = 0;
                    }

                    dayModel.NotReUsr = dayModel.ExpireUsr - dayModel.RenewalUsr;
                    dayModel.NotReAccidStr = CommonLib.Helper.JsonSerializeObject(dayModel.ExAccids.Except(dayModel.ReAccids));

                    modelList.Add(dayModel);

                }
            }

            return modelList;
        }
        /// <summary>
        /// 提现相关
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int GetPageCount(string strWhere)
        {
            int count = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from T_WithdrawalRecord twr left outer join T_WithdrawalInfo twi on twr.withdrawalInfoId=twi.withdrawalInfoId ");

            if (strWhere.Length > 0)
            {
                strSql.Append(" where ");
            }
            strSql.Append(strWhere);
            try
            {
                count = HelperForFrontend.Query<int>(strSql.ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                count = 0;
            }
            return count;
        }
        #endregion

        /// <summary>
        /// 获取提现记录某几行
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="Column"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<WithdrawalRecordModel> GetPage(int pageIndex, int pageSize, string Column, string strWhere)
        {
            List<WithdrawalRecordModel> listitem = new List<WithdrawalRecordModel>();
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageSize = pageSize < 1 ? 20 : pageSize;

            //页数计算
            int bgNumber = ((pageIndex - 1) * pageSize) + 1;
            int edNumber = (pageIndex) * pageSize;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from (");
            strSql.Append("select ROW_NUMBER() over (order by id desc) as rowNumber ");
            if (Column.Length == 0)
            {
                Column = "id,twr.accId,dbo.GetAccountName(twr.accId)accountName,twi.paymentType,twi.accountInfo,twi.paymentName,twr.withdrawalInfoId,twr.withdrawalAmount,twr.balanceAmount,twr.totalAmount,twr.createTime,twr.certTime,twr.transferTime,twr.completeTime,twr.status";
            }
            strSql.Append(" ," + Column + " ");
            strSql.Append(" from T_WithdrawalRecord twr left outer join T_WithdrawalInfo twi on twr.withdrawalInfoId=twi.withdrawalInfoId  ");
            if (strWhere.Length > 0)
            {
                strSql.Append(" where ");
            }
            strSql.Append(strWhere);
            strSql.Append(" ) t ");
            strSql.Append(" where t.rowNumber between @bgNumber and @edNumber; ");
            try
            {
                listitem = HelperForFrontend.Query<WithdrawalRecordModel>(strSql.ToString(), new
                {
                    bgNumber = bgNumber,
                    edNumber = edNumber
                }).ToList();
            }
            catch (Exception ex)
            {
                listitem = null;
            }
            return listitem;
        }
        /// <summary>
        /// 更新提现记录表中状态
        /// </summary>
        /// <param name="withdrawalRecordId"></param>
        /// <param name="status"></param>
        /// <param name="operatorIP"></param>
        /// <param name="operatorUserId"></param>
        /// <returns></returns>
        public bool UpdateWithdrawalStatus(int withdrawalRecordId, int status, string operatorIP, int operatorUserId) 
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update T_WithdrawalRecord ");
            strSql.Append(" set status=@status ");
            switch (status)
            {
                case 1:
                    strSql.Append(" ,certTime=@datetime  ");
                    break;
                case 2:
                    strSql.Append(" ,transferTime=@datetime  ");
                    break;
                case 5:
                    strSql.Append(" ,certTime=@datetime  ");
                    break;               
            }
            strSql.Append(" where id=@withdrawalRecordId; ");
            strSql.Append(" insert into T_WithdrawalRecord_Log ");
            strSql.Append(" select accId,@withdrawalRecordId,withdrawalInfoId,withdrawalAmount,balanceAmount,totalAmount,status,@operatorIP,@operatorUserId,@datetime ");
            strSql.Append(" from T_WithdrawalRecord ");
            strSql.Append(" where id=@withdrawalRecordId ");
            DateTime datetime=DateTime.Now;
            int rows = HelperForFrontend.Execute(strSql.ToString(), new 
            { withdrawalRecordId = withdrawalRecordId,
              status = status,
              operatorIP = operatorIP,
              operatorUserId = operatorUserId,
              datetime=datetime
            });
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据id获得提现金钱数
        /// </summary>
        /// <param name="withdrawalRecordId"></param>
        /// <returns></returns>
        public decimal GetWithdrawalMoneyByWithdrawalRecordId(int withdrawalRecordId)
        {
            decimal count = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select withdrawalAmount from T_WithdrawalRecord where id=@withdrawalRecordId ");
            try
            {
                count = HelperForFrontend.ExecuteScalar<decimal>(strSql.ToString(), new
                    {
                        withdrawalRecordId = withdrawalRecordId
                    });
            }
            catch (Exception ex)
            {
                count = 0;
            }
            return count;
        }

        #region 辅助方法
        /// <summary>
        /// 获取初始注册数字典
        /// </summary>
        /// <param name="list"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int GetInitialSum(List<RetentionList> list, int count)
        {
            int sum = 0;
            for (int i = 0; i < count; i++)
            {
                if (list.Count > i)
                {
                    if (list[i] == null)
                    {
                        sum += 0;
                    }
                    else
                    {
                        sum += list[i].InitialCount;
                    }

                }
            }

            return sum;
        }

        //public int GetDailySum()
        //{

        //}

        #endregion

        /// <summary>
        /// 获取一段时间内某个客服的服务量
        /// </summary>
        /// <param name="insertName"></param>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public int GetVisitCount(int index,string insertName, DateTime stDate, DateTime edDate)
        {
            StringBuilder strSql = new StringBuilder();

            if (index==1)
            {
                strSql.Append("select count(distinct accid) from Sys_VisitInfo where insertTime between @stTime and @edTime and insertName=@insertName and vi_Content not like '%申请开通支付宝%' and vi_Content not like '%首次购买20元高级%'; ");    
            }
            else
            {
                strSql.Append("select count(distinct accid) from Sys_VisitInfo where insertTime between @stTime and @edTime and insertName=@insertName; ");
            }
            

            return DapperHelper.ExecuteScalar<int>(strSql.ToString(), new
            {
                insertName = insertName,
                stTime = stDate,
                edTime = edDate
            });
        }
    }
}
