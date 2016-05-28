using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Model;

namespace DAL
{
    public class UserRetentionDAL
    {
        #region 平均留存测试
        public UserRetentionModel GetUserRetentionTest(string dateType, DateTime bgDate, DateTime edDate, string usrType, string regSource)
        {
            UserRetentionModel retentionModel = new UserRetentionModel();
            List<TimePeriodModel> timePeriod = GetDatePeriod(bgDate, edDate, dateType);
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
                    retentionModel.dataList.Add(GetRetentionRowData(dateType, period, usrType, regSource, ""));
                }
            }
            else
            {
                foreach (TimePeriodModel period in timePeriod)
                {
                    retentionModel.dataList.Add(GetRetentionRowData(dateType, period, usrType, regSource, ""));
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
                    if (item.DataList[i].NowActive != 0)
                    {
                        count += 1;
                        dailySum += item.DataList[i].NowActive;
                    }
                }

                if (count == 0)
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
        #endregion

        #region 留存率旧方法
        public UserRetentionModel GetUserRetention(string dateType, DateTime bgDate, DateTime edDate, string usrType, string regSource, string agent)
        {
            UserRetentionModel retentionModel = new UserRetentionModel();
            List<TimePeriodModel> timePeriod = GetDatePeriod(bgDate, edDate, dateType);
            List<TimePeriodModel> limitList = new List<TimePeriodModel>();

            int limitCount = 0;

            switch (dateType)
            {
                case "day":
                    limitCount = 100;
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
                    retentionModel.dataList.Add(GetRetentionRowData(dateType, period, usrType, regSource, agent));
                }
            }
            else
            {
                foreach (TimePeriodModel period in timePeriod)
                {
                    retentionModel.dataList.Add(GetRetentionRowData(dateType, period, usrType, regSource, agent));
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
                        if (item.DataList[i].NowActive != -1)
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
        public RetentionList GetRetentionRowData(string dateType, TimePeriodModel rowInitTime, string usrType, string regSource, string agent)
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
            rowObj = GetInitialAccidByCondition(rowPeriod[0], usrType, regSource, agent);
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
                    strSql.Append("select count(distinct Accountid) from i200.dbo.T_Log where OperDate between @bgTime and @edTime and datediff(day,getdate(),OperDate)<>0 and Accountid in " + accids + " ;");
                    usrCount = DapperHelper.ExecuteScalar<int>(strSql.ToString(), new { bgTime = period.StDate, edTime = period.EdDate });

                    //strSql.Clear();
                    //strSql.Append("select distinct accountid from i200.dbo.T_LOG where OperDate between @bgTime and @edTime and datediff(day,getdate(),OperDate)<>0 and accountid in " + accids + " ;");
                    //dayAccids = DapperHelper.Query<int>(strSql.ToString(), new { bgTime = period.StDate, edTime = period.EdDate }).ToList();

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

                    //List<int> lostAccids = new List<int>();
                    //foreach (int i in rowObj.InitialAccId)
                    //{
                    //    if (!dayAccids.Contains(i))
                    //    {
                    //        lostAccids.Add(i);
                    //    }
                    //}

                    mdString = usrCount.ToString() + periodCount.ToString() + ratio.ToString();
                    if (dateType == "day")
                    {
                        TimeSpan ts = DateTime.Now - Convert.ToDateTime(rowInitTime.StDate);

                        if (ts.Days - periodCount - 1 < 0)
                        {
                            rowObj.DataList.Add(new DailyRetention(periodCount, -1, ratio, CommonLib.Helper.JsonSerializeObject(dayAccids),
                            CommonLib.Helper.Md5Hash(mdString), ts.Days+"-"+ period.StDate.Date + "-"+ periodCount));//定制列标记符，（规则为记录数+天数标识+比例数值）转MD5  
                        }
                        else
                        {
                            rowObj.DataList.Add(new DailyRetention(periodCount, usrCount, ratio, CommonLib.Helper.JsonSerializeObject(dayAccids),
                            CommonLib.Helper.Md5Hash(mdString), ""));//定制列标记符，（规则为记录数+天数标识+比例数值）转MD5   
                        }
                        //rowObj.DataList.Add(new DailyRetention(periodCount, usrCount, ratio, CommonLib.Helper.JsonSerializeObject(dayAccids),
                        //    CommonLib.Helper.Md5Hash(mdString), ""));//定制列标记符，（规则为记录数+天数标识+比例数值）转MD5    
                    }
                    else
                    {
                        rowObj.DataList.Add(new DailyRetention(periodCount, usrCount, ratio, CommonLib.Helper.JsonSerializeObject(dayAccids),
                            CommonLib.Helper.Md5Hash(mdString), ""));//定制列标记符，（规则为记录数+天数标识+比例数值）转MD5    
                    }
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
        public RetentionList GetInitialAccidByCondition(TimePeriodModel timePeriod, string usrType, string regSource, string agent)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSource = new StringBuilder();

            RetentionList rowFirstObj = new RetentionList();
            int initialCount = 0;
            List<int> initAccidList = new List<int>();

            int firstDayLog = 0;
            List<int> firstDayAccids = new List<int>();

            switch (usrType)
            {
                case "regUser":
                    if (agent == "all")
                    {
                        strSql.Append("select count(*) from i200.dbo.T_Account where RegTime between @stTime and @edTime and state=1 and AgentId<>0 ");
                    }
                    else if (agent == "")
                    {
                        strSql.Append("select count(*) from i200.dbo.T_Account where RegTime between @stTime and @edTime and state=1");
                    }
                    else
                    {
                        strSql.Append("select count(*) from i200.dbo.T_Account where RegTime between @stTime and @edTime and state=1 and AgentId=" + Convert.ToInt32(agent) + " ");
                    }

                    if (regSource != "all")
                    {
                        strSql.Append(" and Remark='" + regSource + "';");
                    }
                    else
                    {
                        strSql.Append(";");
                    }
                    initialCount = DapperHelper.ExecuteScalar<int>(strSql.ToString(),
                        new { stTime = timePeriod.StDate, edTime = timePeriod.EdDate });

                    strSql.Clear();

                    if (agent == "all")
                    {
                        strSql.Append("select Id from i200.dbo.T_Account where RegTime between @stTime and @edTime and state=1 and AgentId<>0 ");
                    }
                    else if (agent == "")
                    {
                        strSql.Append("select Id from i200.dbo.T_Account where RegTime between @stTime and @edTime and state=1");
                    }
                    else
                    {
                        strSql.Append("select Id from i200.dbo.T_Account where RegTime between @stTime and @edTime and state=1 and AgentId=" + Convert.ToInt32(agent) + " ");
                    }

                    if (regSource != "all")
                    {
                        strSql.Append(" and Remark='" + regSource + "';");
                    }
                    else
                    {
                        strSql.Append(";");
                    }
                    initAccidList =
                        DapperHelper.Query<int>(strSql.ToString(),
                            new { stTime = timePeriod.StDate, edTime = timePeriod.EdDate }).ToList();
                    break;
                case "paidUser":
                    string where = "";
                    if (agent == "all")
                    {
                        where += " a.AgentId<>0 ";
                    }
                    else if (agent == "")
                    {
                        where += " 1=1 ";
                    }
                    else
                    {
                        where += " a.AgentId=" + Convert.ToInt32(agent) + " ";
                    }

                    strSql.Append("select count(distinct accId) from i200.dbo.T_OrderInfo o left join i200.dbo.T_Account a on o.accId=a.ID ");

                    if (regSource != "all")
                    {
                        strSql.Append(" where a.Remark='" + regSource + "' and o.transactionDate between @stTime and @edTime and " + where + ";");
                    }
                    else
                    {
                        strSql.Append(" where transactionDate between @stTime and @edTime and " + where + ";");
                    }
                    initialCount = DapperHelper.ExecuteScalar<int>(strSql.ToString(), new { stTime = timePeriod.StDate, edTime = timePeriod.EdDate });

                    strSql.Clear();

                    strSql.Append("select accId from i200.dbo.T_OrderInfo o left join i200.dbo.T_Account a on o.accId=a.ID ");

                    if (regSource != "all")
                    {
                        strSql.Append(" where a.Remark='" + regSource + "' and o.transactionDate between @stTime and @edTime  and " + where + " group by o.accId;");
                    }
                    else
                    {
                        strSql.Append(" where transactionDate between @stTime and @edTime and " + where + " group by accId;");
                    }
                    initAccidList = DapperHelper.Query<int>(strSql.ToString(), new { stTime = timePeriod.StDate, edTime = timePeriod.EdDate }).ToList();
                    break;
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
                        timePeriodList.Add(new TimePeriodModel(stDate.ToShortDateString() + "~" + stDate.AddDays(7).ToShortDateString(), stDate, stDate.AddDays(6).AddHours(23).AddMinutes(59).AddSeconds(59)));
                        stDate = stDate.AddDays(7);
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

        #region 留存率新方法
        public UserRetentionModel GetUserRetentionEx(string dateType, DateTime bgDate, DateTime edDate, string usrType, string regSource)
        {
            UserRetentionModel retentionModel = new UserRetentionModel();

            #region Former Implementation
            List<RetentionList> dataList = new List<RetentionList>();
            List<RetentionList> periodDataList = new List<RetentionList>();

            DateTime start = bgDate;
            DateTime end = edDate;

            //拉伸初始日期范围
            switch (dateType)
            {
                case "day":
                    break;
                case "week":
                    if ((int)start.DayOfWeek != 0)
                    {
                        start = start.AddDays(0 - (int)start.DayOfWeek);
                    }
                    break;
                case "month":
                    if (start.Day != 1)
                    {
                        start = Convert.ToDateTime(start.Year + "-" + start.Month + "-1");
                    }
                    break;
            }


            List<TimePeriodModel> rowPeriod = new List<TimePeriodModel>();
            rowPeriod = GetDatePeriod(start, end, "day");

            StringBuilder strSqlReg = new StringBuilder();
            if (usrType == "regUser")
            {
                strSqlReg.Append("select count(*) UsrCount,cast(RegTime as date) RegDate from i200.dbo.T_Account where RegTime between @stDate and @edDate and state=1 group by cast(RegTime as date) ");
            }
            //else if (usrType == "paidUser")
            //{
            //    strSqlReg.Append("select count(*) UsrCount,cast(RegTime as date) RegDate from i200.dbo.T_Account where RegTime between @stDate and @edDate and state=1 group by cast(RegTime as date) ");    
            //}

            List<dynamic> regList = DapperHelper.Query<dynamic>(strSqlReg.ToString(), new { stDate = start, edDate = end }).ToList();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CAST(regTime as date) R_Date," +
                          "DATEDIFF(dd,regtime,daydate) DateCount," +
                          "COUNT(*) Accounts from  " +
                          "SysRpt_ShopDayInfo where dayDate>@stDate and dayDate<@edDate and regTime>@stDate and regTime<@edDate" +
                          " group by CAST(regTime as date),DATEDIFF(dd,regtime,daydate)");

            //获取聚合后的留存数据
            List<RetentionGroup> retentionGroups =
                DapperHelper.Query<RetentionGroup>(strSql.ToString(), new { stDate = start, edDate = end }).ToList();

            //初始化留存行对象
            foreach (TimePeriodModel timePeriod in rowPeriod)
            {
                RetentionList retentionList = new RetentionList();
                DateTime iterDateSt = timePeriod.StDate;//游标时间 
                DateTime iterDateEd = timePeriod.EdDate;//游标时间

                retentionList.Date = timePeriod.TimePeriod;
                retentionList.RowMark = CommonLib.Helper.Md5Hash(timePeriod.TimePeriod);

                retentionList.DataList = new List<DailyRetention>();
                retentionList.InitialAccId = new List<int>();
                retentionList.InitialCount = regList.Find(x => x.RegDate.ToShortDateString() == iterDateSt.ToShortDateString()).UsrCount;
                //public DailyRetention(int DayNum, int NowActive, int Ratio, string DayAccids, string ColumnMark)

                //if (dateType == "day")
                //{
                //找出所有当日行
                List<RetentionGroup> rowRetention =
                    retentionGroups.FindAll(x => x.R_Date.ToShortDateString() == iterDateSt.ToShortDateString());

                if (rowRetention.Exists(x => x.DateCount == 0))
                {

                    for (int i = 0; i < 12; i++)
                    {
                        if (rowRetention.Exists(y => y.DateCount == i))
                        {
                            int colData = rowRetention.Find(y => y.DateCount == i).Accounts;
                            retentionList.DataList.Add(new DailyRetention(i, colData, colData * 100 / retentionList.InitialCount, "", ""));
                        }
                        else
                        {
                            TimeSpan ts = DateTime.Now - Convert.ToDateTime(retentionList.Date);

                            if (ts.Days - i - 1 <= 0 )
                            {
                                retentionList.DataList.Add(new DailyRetention(i, -1, 0, "", ""));
                            }
                            else
                            {
                                retentionList.DataList.Add(new DailyRetention(i, 0, 0, "", ""));
                            }

                        }
                    }
                }
                else
                {
                    continue;
                }
                //}
                //else
                //{
                //    //找出当前时间段所有行
                //    List<RetentionGroup> rowRetention =
                //        retentionGroups.FindAll(x => ((x.R_Date > iterDateSt) && (x.R_Date <= iterDateEd)));

                //    if (rowRetention.Exists(x => x.DateCount == 0))
                //    {
                //        List<RetentionGroup> initialAccList =
                //            rowRetention.FindAll(x => x.DateCount == 0);

                //        int initialAcc = initialAccList.Sum(x => x.Accounts);

                //        for (int i = 0; i < 12; i++)
                //        {
                //            if (rowRetention.Exists(y => y.DateCount == i))
                //            {
                //                List<RetentionGroup> colAccList =
                //                    rowRetention.FindAll(x => x.DateCount == i);

                //                int colData = colAccList.Sum(y => y.Accounts);
                //                retentionList.DataList.Add(new DailyRetention(i, colData, colData * 100 / initialAcc, "", ""));
                //            }
                //            else
                //            {
                //                retentionList.DataList.Add(new DailyRetention(i, 0, 0, "", ""));
                //            }

                //        }
                //    }
                //    else
                //    {
                //        continue;
                //    }
                //}

                dataList.Add(retentionList);

            }

            if (dateType != "day")
            {
                rowPeriod = GetDatePeriod(start, end, dateType);
                foreach (TimePeriodModel timePeriod in rowPeriod)
                {
                    RetentionList retentionList = new RetentionList();
                    retentionList.Date = timePeriod.TimePeriod;
                    retentionList.RowMark = CommonLib.Helper.Md5Hash(timePeriod.TimePeriod);

                    retentionList.DataList = new List<DailyRetention>();
                    retentionList.InitialAccId = new List<int>();

                    retentionList.InitialCount = dataList.FindAll(x => Convert.ToDateTime(x.Date) < timePeriod.EdDate && Convert.ToDateTime(x.Date) >= timePeriod.StDate).Sum(x => x.InitialCount);
                    int initAcc = 0;

                    for (int i = 0; i < 12; i++)
                    {
                        int colData = dataList.FindAll(x => Convert.ToDateTime(x.Date) < timePeriod.EdDate && Convert.ToDateTime(x.Date) >= timePeriod.StDate).Sum(x => x.DataList.ElementAt(i).NowActive);

                        //筛除多余数据
                        if (i >= (rowPeriod.Count - rowPeriod.IndexOf(timePeriod)))
                        {
                            retentionList.DataList.Add(new DailyRetention(i, 0, 0, "", ""));
                        }
                        else
                        {
                            retentionList.DataList.Add(new DailyRetention(i, colData, colData * 100 / retentionList.InitialCount, "", ""));
                        }

                    }

                    periodDataList.Add(retentionList);
                }

                retentionModel.dataList = periodDataList;
            }
            else
            {
                retentionModel.dataList = dataList;
            }

            #endregion

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
                        if (item.DataList[i].NowActive != -1)
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

        #endregion

        /// <summary>
        /// 返回每日的新增活跃用户和流失活跃用户
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public ActiveChangeModel GetActiveChangeData(DateTime stDate, DateTime edDate)
        {
            StringBuilder strSql = new StringBuilder();
            ActiveChangeModel activeModel = new ActiveChangeModel();

            while (stDate < edDate)
            {
                ActiveChangeUnit singleData = new ActiveChangeUnit();

                singleData.Date = stDate;

                #region 获取已不活跃用户数
                strSql.Clear();
                strSql.Append(" select count(accid) as cnt from [Sys_I200].[dbo].[SysRpt_ShopActive]  " +
                              " where DATEDIFF(DAY,startTime,@now)=0 and active<5 and accid in(");
                strSql.Append(" select accid from [Sys_I200].[dbo].[SysRpt_ShopActive] " +
                              " where DATEDIFF(DAY,updatetime,@before)=0 and active in (5,7))");

                singleData.LostActive = DapperHelper.ExecuteScalar<int>(strSql.ToString(), new { now = stDate, before = stDate.AddDays(-1) });

                strSql.Clear();
                strSql.Append(" select accid from [Sys_I200].[dbo].[SysRpt_ShopActive]  " +
                              " where DATEDIFF(DAY,startTime,@now)=0 and active<5 and accid in(");
                strSql.Append(" select accid from [Sys_I200].[dbo].[SysRpt_ShopActive] " +
                              " where DATEDIFF(DAY,updatetime,@before)=0 and active in (5,7))");

                singleData.LostAccids =
                   CommonLib.Helper.JsonSerializeObject(DapperHelper.Query<int>(strSql.ToString(), new { now = stDate, before = stDate.AddDays(-1) }).ToList());

                #endregion

                #region 获取新增活跃用户数
                strSql.Clear();
                strSql.Append(" select count(accid) as cnt from [Sys_I200].[dbo].[SysRpt_ShopActive]  " +
                              " where DATEDIFF(DAY,startTime,@now)=0 and active=5 and accid in(");
                strSql.Append(" select accid from [Sys_I200].[dbo].[SysRpt_ShopActive] " +
                              " where DATEDIFF(DAY,updatetime,@before)=0 and active<5)");

                singleData.NewActive = DapperHelper.ExecuteScalar<int>(strSql.ToString(), new { now = stDate, before = stDate.AddDays(-1) });

                strSql.Clear();
                strSql.Append(" select accid as cnt from [Sys_I200].[dbo].[SysRpt_ShopActive]  " +
                              " where DATEDIFF(DAY,startTime,@now)=0 and active=5 and accid in(");
                strSql.Append(" select accid from [Sys_I200].[dbo].[SysRpt_ShopActive] " +
                              " where DATEDIFF(DAY,updatetime,@before)=0 and active<5)");

                singleData.NewAccids =
                    CommonLib.Helper.JsonSerializeObject(DapperHelper.Query<int>(strSql.ToString(), new { now = stDate, before = stDate.AddDays(-1) }).ToList());
                #endregion

                singleData.NetValue = singleData.NewActive - singleData.LostActive;
                singleData.Percent = (singleData.LostActive == 0 ? 0 : (singleData.NetValue * 100 / singleData.LostActive));

                singleData.RowMark = CommonLib.Helper.Md5Hash(singleData.Date.ToLongDateString());
                if (stDate.DayOfWeek.ToString() == "Saturday" || stDate.DayOfWeek.ToString() == "Sunday")
                {
                    singleData.Weekend = "1";
                }
                else
                    singleData.Weekend = "0";

                activeModel.dataList.Add(singleData);
                stDate = stDate.AddDays(1);
            }

            activeModel.dataList.Reverse();
            return activeModel;
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
    }
}
