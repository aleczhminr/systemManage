using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Utility;

namespace DAL
{
    public class OperationReportDAL
    {
        /// <summary>
        /// 获取拉新数据
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public List<NewAccountItem> GetNewAccountModel(DateTime stDate, DateTime edDate)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select * into #sourceDepartLast " +
                          "from i200.dbo.T_Account " +
                          "where State=1 and RegTime between @stDate and @edDate;" +
                          "" +
                          "update #sourceDepartLast set Remark=fromName " +
                          "where (Remark='11' or remark='8') and fromName is not null and fromName<>'';" +
                          "update #sourceDepartLast set Remark='other_android' where Remark='11' and (fromName is null or fromName='');" +
                          "update #sourceDepartLast set Remark='other_pc' where Remark='8' and  (fromName is null or fromName='');" +
                          "" +
                          "select case isnull(Remark,'') " +
                          "when '0' then '主站注册' " +
                          "when '10' then 'iPhone' " +
                          "when '13' then 'iPad' " +
                          "when '12' then '手机web' " +
                          "when 'market_zhuzhan' then '主站安卓' " +
                          "when 'market_360' then '360' " +
                          "when 'market_tengxun_cpd' then '应用宝CPD' " +                          
                          "when 'market_huawei' then '华为' " +
                          "when 'market_xiaomi' then '小米' " +
                          "when 'market_meizu' then '魅族' " +
                          "when 'market_oppo' then 'oppo' " +
                          "when 'market_wandoujia' then '豌豆荚' " +
                          "when 'market_baidu' then '百度' " +
                          "when 'market_tengxun' then '应用宝' " +
                          "when 'market_android' then '安智' " +
                          "when 'market_androids' then '安智' " +
                          "when 'other_android' then '其他' " +
                          "when 'download_2345' then '下载站' " +
                          "when 'download_huajun' then '下载站' " +
                          "when 'download_hao123' then '下载站' " +
                          "when 'download_360' then '下载站' " +
                          "when 'market_91zs' then '百度' " +
                          "when 'pc_client' then '主站客户端' " +
                          "when 'pc_main' then '主站客户端' " +
                          "when 'sougou' then '其他' " +
                          "when 'other_android' then '其他' " +
                          "when 'other_pc' then '其他PC客户端' " +
                          "when null then '来源未知' " +
                          "when '' then '来源未知' " +
                          "else Remark end Source " +
                          "into #groupTable from #sourceDepartLast;" +
                          "" +
                          "select Source SourceName,count(*) ThisWeekVal from #groupTable group by Source;" +
                          "" +
                          "drop table #groupTable;" +
                          "drop table #sourceDepartLast;");

            try
            {
                return
                    DapperHelper.Query<NewAccountItem>(strSql.ToString(), new { stDate = stDate, edDate = edDate })
                        .ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("获取运营分析报表1出错！", ex);
                return null;
            }

        }

        public List<ConversionSource> GetConversionList(DateTime stDate, DateTime edDate)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(
                "select * into #sourceDepart from i200.dbo.T_Account where State=1 and RegTime between @stDate and @edDate; " +
                "update #sourceDepart set Remark=fromName where (Remark='11') and fromName is not null and fromName<>''; " +
                "update #sourceDepart set Remark='other_android' where Remark='11' and fromName is null; " +
                "select si.*,case ta.remark  " +
                "when '0' then '网页'  " +
                "when '8' then 'PC' " +
                "when '10' then 'iPhone' " +
                "when '13' then 'iPad' " +
                "when '12' then '网页' " +
                "when 'market_zhuzhan' then '主站' " +
                "when 'market_360' then '360' " +
                "when 'market_huawei' then '华为' " +
                "when 'market_xiaomi' then '小米' " +
                "when 'market_meizu' then '魅族' " +
                "when 'market_oppo' then 'oppo' " +
                "when 'market_wandoujia' then '豌豆荚' " +
                "when 'market_baidu' then '百度' " +
                "when 'market_tengxun' then '应用宝' " +
                "when 'market_android' then '安智' " +
                "when 'market_androids' then '安智' " +
                "when 'other_android' then '' " +
                "when 'download_2345' then 'PC下载站' " +
                "when 'download_huajun' then 'PC下载站' " +
                "when 'download_hao123' then 'PC下载站' " +
                "when 'download_360' then 'PC下载站' " +
                "when 'market_91zs' then '百度' " +
                "when 'pc_client' then 'PC主站' " +
                "when 'pc_main' then 'PC主站' " +
                "when 'sougou' then '' " +
                "when 'other_android' then '其他' " +
                "else ta.remark " +
                "end " +
                "Source into #List from [Sys_I200].[dbo].[SysRpt_ShopInfo] si left join #sourceDepart ta " +
                "on si.accountid=ta.ID " +
                "where ta.State=1 and ta.RegTime between @stDate and @edDate; " +
                "select Source,COUNT(*) Reg into #reg from #List group by Source; " +
                "select Source,COUNT(*) Login into #log from #List where allLoginNum>0 group by Source; " +
                "select Source,COUNT(*) DataInput into #data from #List where (userNum+smsNum+goodsNum+moodNum+saleNum+orderNum+outlayNum)>0 group by Source; " +
                "select Source,COUNT(distinct l.accountid) SecRetention into #sec from #List l left join [i200].[dbo].T_LOG on l.accountid=T_LOG.Accountid where DATEDIFF(day,regTime,OperDate)=1 group by l.Source; " +
                "select Source,COUNT(*) Paid into #paid from #List where orderMoney>0 group by Source; " +
                "select Source,COUNT(*) Active into #active from #List where active in (5,7) group by Source; " +
                "select #reg.Source,isnull(#reg.Reg,0) Reg,isnull(#log.Login,0) Login,isnull(#data.DataInput,0) DataInput,isnull(#sec.SecRetention,0) SecRetention,isnull(#paid.Paid,0) Paid,isnull(#active.Active,0) Active from #reg left join #log on #reg.Source=#log.Source left join #data on #reg.Source=#data.Source " +
                "left join #sec on #reg.Source=#sec.Source left join #paid on #reg.Source=#paid.Source left join #active on #reg.Source=#active.Source where #reg.Source is not null and #reg.Source<>''; " +
                "drop table #reg;drop table #log;drop table #data;drop table #sec;drop table #paid;drop table #active; " +
                "drop table #List; " +
                "drop table #sourceDepart; ");

            try
            {
                return
                    DapperHelper.Query<ConversionSource>(strSql.ToString(), new { stDate = stDate, edDate = edDate })
                        .ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("获取运营分析报表2出错！", ex);
                return null;
            }
        }

        /// <summary>
        /// 获取多维用户登录数
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public List<NewAccountItem> GetDimensionLogin(DateTime stDate, DateTime edDate)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(
                "declare @stDate datetime,@edDate datetime;" +
                "set @stDate=@date1;" +
                "set @edDate=@date2;" +

                //"select CAST(OperDate as date) dt,count(distinct Accountid) lCnt into #logTemp from [i200].[dbo].T_LOG  " +
                //"where OperDate between @stDate and @edDate  " +
                //"group by CAST(OperDate as date) " +
                //"order by CAST(OperDate as date); " +

                "select case left(LogMode,1) when 0 then 'Web & PC' when 1 then 'Web & PC' when 3 then 'Web & PC' end Source,count(distinct Accountid) modeCnt,cast(OperDate as date) dt into #logTempMob from [i200].[dbo].T_LOG " +
                "where OperDate between @stDate and @edDate  " +
                "group by cast(OperDate as date),left(LogMode,1) " +
                "order by dt; " +

                "select case AppKey when 'AndroidYnHWyROQosO' then 'Android' when 'iPadMaO8VUvVH0eBss' then 'iPad' when 'iPhoneHT5I0O4HDN65' then 'iPhone' end Source,count(distinct accId) modeCnt,cast(createTime as date) dt into #mobileTemp from i200.dbo.T_Token_Api  " +
                "where createTime between @stDate and @edDate  " +
                "group by cast(createTime as date),AppKey " +
                "order by dt; " +

                "select case b.aotjb when 1 then '免费版' when 3 then '高级版' end Source,count(distinct l.Accountid) aCnt,cast(l.OperDate as date) dt into #logEdition from i200.dbo.T_LOG l  " +
                "left join i200.dbo.T_Business b " +
                "on l.Accountid=b.accountid " +
                "where l.OperDate between @stDate and @edDate  " +
                "group by cast(l.OperDate as date),b.aotjb " +
                "order by dt;  " +

                "select accID,sum(RealMoney) mCnt into #saleMoney from i200.dbo.T_SaleInfo " +
                "where insertTime between @stDate and @edDate  " +
                "group by accID; " +

                "select count(distinct Accountid) aCnt,cast(OperDate as date) dt into #sale1W from i200.dbo.T_LOG  " +
                "where Accountid in (select accID from #saleMoney where mCnt<10000) " +
                "and OperDate between @stDate and @edDate " +
                "group by cast(OperDate as date) " +
                "order by dt; " +

                "select count(distinct Accountid) aCnt,cast(OperDate as date) dt into #sale5W from i200.dbo.T_LOG  " +
                "where Accountid in (select accID from #saleMoney where mCnt>=10000 and mCnt<50000) " +
                "and OperDate between @stDate and @edDate " +
                "group by cast(OperDate as date) " +
                "order by dt; " +

                "select count(distinct Accountid) aCnt,cast(OperDate as date) dt into #sale10W from i200.dbo.T_LOG  " +
                "where Accountid in (select accID from #saleMoney where mCnt>=50000 and mCnt<100000) " +
                "and OperDate between @stDate and @edDate " +
                "group by cast(OperDate as date) " +
                "order by dt; " +

                "select count(distinct Accountid) aCnt,cast(OperDate as date) dt into #sale30W from i200.dbo.T_LOG  " +
                "where Accountid in (select accID from #saleMoney where mCnt>=100000 and mCnt<300000) " +
                "and OperDate between @stDate and @edDate " +
                "group by cast(OperDate as date) " +
                "order by dt; " +

                "select count(distinct Accountid) aCnt,cast(OperDate as date) dt into #saleNW from i200.dbo.T_LOG  " +
                "where Accountid in (select accID from #saleMoney where mCnt>=300000) " +
                "and OperDate between @stDate and @edDate " +
                "group by cast(OperDate as date) " +
                "order by dt; " +

                "select sa.active,count(distinct l.Accountid) aCnt,cast(l.OperDate as date) dt into #logActive from i200.dbo.T_LOG l " +
                "left join Sys_I200.dbo.SysRpt_ShopActive sa  " +
                "on l.Accountid=sa.accid " +
                "where l.OperDate between @stDate and @edDate  " +
                "and startTime<=l.OperDate and updatetime>=l.OperDate " +
                "group by cast(l.OperDate as date),sa.active " +
                "order by dt;  " +

                "select case active when -3 then '流失' when -1 then '休眠' when 1 then '新注册' when 3 then '需关怀' when 4 then '需关怀' when 5 then '活跃' when 7 then '忠诚' end SourceName," +
                "avg(aCnt) ThisWeekVal into #tempStatus from #logActive group by active; " +

                //新增新老用户维度
                "select distinct Accountid,DATEDIFF(SECOND,@stDate,ta.RegTime) interval into #logUserType from i200.dbo.T_LOG l " +
                "left join i200.dbo.T_Account ta " +
                "on l.Accountid=ta.ID " +
                "where OperDate between @stDate and @edDate;                 " +

                "update #logUserType set interval=1 where interval>=0; " +
                "update #logUserType set interval=0 where interval<0;                 " +

                "select case ut.interval when 1 then '新用户' when 0 then '老用户' end Source,count(distinct l.Accountid) aCnt,cast(l.OperDate as date) dt into #logNew from i200.dbo.T_LOG l   " +
                "left join #logUserType ut " +
                "on l.Accountid=ut.Accountid  " +
                "where l.OperDate between @stDate and @edDate   " +
                "group by cast(l.OperDate as date),ut.interval " +
                "order by dt;   " +

                //"select '日均登录' SourceName,AVG(lCnt) ThisWeekVal from #logTemp " +
                //"union all " +
                "select Source SourceName,avg(modeCnt) ThisWeekVal from #logTempMob  where Source is not null " +
                "group by Source " +
                "union all " +
                "select Source SourceName,avg(modeCnt) ThisWeekVal from #mobileTemp " +
                "group by Source " +
                "union all " +
                "select Source SourceName,avg(aCnt) ThisWeekVal from #logEdition " +
                "group by Source " +
                "union all " +
                "select SourceName," +
                "sum(ThisWeekVal) ThisWeekVal from #tempStatus group by SourceName " +
                "union all " +
                "select '<1万' SourceName,avg(aCnt) ThisWeekVal from #sale1W " +
                "union all " +
                "select '1-5万' SourceName,avg(aCnt) ThisWeekVal from #sale5W " +
                "union all " +
                "select '5-10万' SourceName,avg(aCnt) ThisWeekVal from #sale10W " +
                "union all " +
                "select '10-30万' SourceName,avg(aCnt) ThisWeekVal from #sale30W " +
                "union all " +
                "select '>30万' SourceName,avg(aCnt) ThisWeekVal from #saleNW " +
                "union all  " +
                "select Source SourceName,avg(aCnt) ThisWeekVal from #logNew  " +
                "group by Source;   " +

                "drop table #tempStatus;" +
                "drop table #logActive;" +
                "drop table #saleNW; " +
                "drop table #sale30W " +
                "drop table #sale10W; " +
                "drop table #sale5W; " +
                "drop table #sale1W; " +
                "drop table #saleMoney; " +
                //"drop table #logTemp; " +
                "drop table #mobileTemp; " +
                "drop table #logTempMob; " +
                "drop table #logUserType; " +
                "drop table #logNew; " +
                "drop table #logEdition; ");

            try
            {
                return
                    DapperHelper.Query<NewAccountItem>(strSql.ToString(), new { date1 = stDate, date2 = edDate })
                        .ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("获取多维度用户登录出错！", ex);
                return null;
            }
        }


        /// <summary>
        /// 获取登录数据外其他留存相关分析数据
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public List<NewAccountItem> GetRetentionData(DateTime stDate, DateTime edDate)
        {
            StringBuilder strSql = new StringBuilder();

            //获取时间段80%时间的天数
            double dateDiff = (edDate - stDate).TotalDays * 0.8;

            strSql.Append(
                //声明日期部分
                "declare @stDate datetime,@edDate datetime,@loginNum decimal,@regNum decimal," +
                "@stActive int,@edActive int,@allLog int,@mobileLog int,@pcLog int;" +
                "set @stDate=@date1;" +
                "set @edDate=@date2;" +

                //获取数据临时表
                "select CAST(OperDate as date) dt,count(distinct Accountid) lCnt into #logTemp from [i200].[dbo].T_LOG  " +
                "where OperDate between @stDate and @edDate  " +
                "group by CAST(OperDate as date) " +
                "order by CAST(OperDate as date); " +

                "select @loginNum=count(distinct Accountid) from i200.dbo.T_LOG  " +
                "where OperDate between @stDate and @edDate; " +
                "select @regNum=COUNT(Id) from i200.dbo.T_Account where RegTime<@edDate and State=1; " +

                "select Accountid,cast(OperDate as date) dt into #logCon from i200.dbo.T_LOG " +
                "where OperDate between @stDate and @edDate " +
                "group by Accountid,cast(OperDate as date) " +
                "order by dt; " +

                "select Accountid,count(Accountid) aCnt into #tempLogDay from #logCon " +
                "group by Accountid " +
                "having count(Accountid)>=@dateDiff " +

                //"select @stActive=count(distinct accid) from Sys_I200.dbo.SysRpt_ShopActive  " +
                //"where startTime<=@stDate and updatetime>=@stDate and active in (5,7); " +
                //"select @edActive=count(distinct accid) from Sys_I200.dbo.SysRpt_ShopActive  " +
                //"where startTime<=@edDate and updatetime>=@edDate and active in (5,7); " +

                "select Accountid,LogMode into #logMode from i200.dbo.T_LOG  " +
                "where OperDate between @stDate and @edDate " +
                "group by Accountid,LogMode; " +

                "select @mobileLog=count(distinct Accountid) from #logMode " +
                "where LogMode=4 " +
                "and Accountid not in ( " +
                "select Accountid from #logMode where left(LogMode,1) in ('0','1','3') " +
                "); " +

                "select @pcLog=count(distinct Accountid) from #logMode " +
                "where left(LogMode,1) in ('0','1','3')  " +
                "and Accountid not in ( " +
                "select Accountid from #logMode where LogMode=4 " +
                "); " +

                "select @allLog=count(distinct Accountid) from i200.dbo.T_LOG " +
                "where OperDate between @stDate and @edDate; " +


                //获取输出结果部分
                "select '日均登录' SourceName,AVG(lCnt) ThisWeekVal from #logTemp " +
                "union all " +
                "select '登录率' SourceName,case  when @regNum=0 then 0 else cast(@loginNum/@regNum*100 as decimal(9,3)) end ThisWeekVal " +
                "union all " +
                "select '连续登录' SourceName,count(Accountid) ThisWeekVal from #tempLogDay " +
                "union all " +
                "select '新增活忠' SourceName,count(distinct accid) ThisWeekVal from Sys_I200.dbo.SysRpt_ShopActive where startTime >= @stDate and updatetime <= @edDate and active in (5, 7) " +
                "and accid not in (select accid from Sys_I200.dbo.SysRpt_ShopActive where active in (5,7) and startTime<@stDate group by accid) " +
                //"select '新增活忠' SourceName,(@edActive-@stActive) ThisWeekVal " +
                "union all " +
                "select '流失活忠' SourceName,count(accid) ThisWeekVal from Sys_I200.dbo.SysRpt_ShopActive " +
                "where startTime<=@stDate and updatetime>=@stDate and active=5 " +
                "and accid not in ( " +
                "select l.Accountid from i200.dbo.T_LOG l where OperDate between @stDate and @edDate group by l.Accountid) " +
                "union all " +
                "select '流失回流' SourceName,count(accid) ThisWeekVal from Sys_I200.dbo.SysRpt_ShopActive " +
                "where startTime<=@stDate and updatetime>=@stDate and active=-3 " +
                "and accid in ( " +
                "select l.Accountid from i200.dbo.T_LOG l where OperDate between @stDate and @edDate group by l.Accountid) " +
                "union all " +
                "select '仅移动端登录' SourceName,@mobileLog ThisWeekVal " +
                "union all " +
                "select '仅电脑端登录' SourceName,@pcLog ThisWeekVal " +
                "union all " +
                "select '全端登录' SourceName,(@allLog-@mobileLog-@pcLog) ThisWeekVal; " +

                //删除临时表部分
                "drop table #logMode;" +
                "drop table #tempLogDay; " +
                "drop table #logCon; " +
                "drop table #logTemp; "


                );

            try
            {
                return
                    DapperHelper.Query<NewAccountItem>(strSql.ToString(), new { date1 = stDate, date2 = edDate, dateDiff = dateDiff })
                        .ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("获取留存报表出错！", ex);
                return null;
            }
        }

        /// <summary>
        /// 获取销售报表数据
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public List<NewAccountItem> GetSalePart(DateTime stDate, DateTime edDate)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(
                //声明日期部分
                "declare @stDate datetime,@edDate datetime;" +
                "set @stDate=@date1;" +
                "set @edDate=@date2;" +

                //获取数据临时表
                "select count(saleID) aCnt,cast(insertTime as date) dt into #allSale from i200.dbo.T_SaleInfo " +
                "where insertTime between @stDate and @edDate " +
                "group by cast(insertTime as date) " +
                "order by dt; " +

                "select case saleFlag when 0 then 'Web & PC' when 1 then 'iPhone' when 2 then 'Android' when 3 then 'iPad' end saleFlag,count(saleID) aCnt,cast(insertTime as date) dt into #deviceSale from i200.dbo.T_SaleInfo " +
                "where insertTime between @stDate and @edDate " +
                "group by cast(insertTime as date),saleFlag " +
                "order by dt; " +

                "select sa.active,count(si.saleID) aCnt,cast(insertTime as date) dt into #statusSale from i200.dbo.T_SaleInfo si " +
                "left join sys_i200.dbo.SysRpt_ShopActive sa " +
                "on si.accID=sa.accid " +
                "where insertTime between @stDate and @edDate and  " +
                "sa.startTime<=si.insertTime and sa.updatetime>=si.insertTime " +
                "group by cast(insertTime as date),sa.active " +
                "order by dt; " +
                //关于状态的例外处理
                "select case active when -3 then '流失' when -1 then '休眠' when 1 then '新注册' when 3 then '需关怀' when 4 then '需关怀' when 5 then '活跃' when 7 then '忠诚' end SourceName," +
                "avg(aCnt) ThisWeekVal into #tempStatus from #statusSale group by active; " +


                "select case b.aotjb when 1 then '免费版' when 3 then '高级版' end Source,count(si.saleID) aCnt,cast(insertTime as date) dt into #editionSale from i200.dbo.T_SaleInfo si " +
                "left join i200.dbo.T_Business b " +
                "on si.accID=b.accountid " +
                "where insertTime between @stDate and @edDate  " +
                "group by cast(insertTime as date),aotjb " +
                "order by dt; " +

                "select accID,sum(RealMoney) mCnt into #saleMoney from i200.dbo.T_SaleInfo " +
                "where insertTime between @stDate and @edDate  " +
                "group by accID; " +

                "select cast(insertTime as date) dt,count(saleID) aCnt into #money1 from i200.dbo.T_SaleInfo " +
                "where insertTime between @stDate and @edDate " +
                "and accID in (select accID from #saleMoney where mCnt<10000) " +
                "group by cast(insertTime as date); " +

                "select cast(insertTime as date) dt,count(saleID) aCnt into #money2 from i200.dbo.T_SaleInfo " +
                "where insertTime between @stDate and @edDate " +
                "and accID in (select accID from #saleMoney where mCnt<50000 and mCnt>=10000) " +
                "group by cast(insertTime as date); " +

                "select cast(insertTime as date) dt,count(saleID) aCnt into #money3 from i200.dbo.T_SaleInfo " +
                "where insertTime between @stDate and @edDate " +
                "and accID in (select accID from #saleMoney where mCnt<100000 and mCnt>=50000) " +
                "group by cast(insertTime as date); " +

                "select cast(insertTime as date) dt,count(saleID) aCnt into #money4 from i200.dbo.T_SaleInfo " +
                "where insertTime between @stDate and @edDate " +
                "and accID in (select accID from #saleMoney where mCnt<300000 and mCnt>=100000) " +
                "group by cast(insertTime as date); " +

                "select cast(insertTime as date) dt,count(saleID) aCnt into #money5 from i200.dbo.T_SaleInfo " +
                "where insertTime between @stDate and @edDate " +
                "and accID in (select accID from #saleMoney where mCnt>=300000) " +
                "group by cast(insertTime as date); " +


                //新增新老用户维度
                "select distinct accID,DATEDIFF(SECOND,@stDate,ta.RegTime) interval into #logUserType from i200.dbo.T_SaleInfo l " +
                "left join i200.dbo.T_Account ta " +
                "on l.accID=ta.ID " +
                "where insertTime between @stDate and @edDate;                 " +

                "update #logUserType set interval=1 where interval>=0; " +
                "update #logUserType set interval=0 where interval<0;                 " +

                "select case ut.interval when 1 then '新用户' when 0 then '老用户' end Source,count(saleID) aCnt,cast(l.insertTime as date) dt into #logNew from i200.dbo.T_SaleInfo l   " +
                "left join #logUserType ut " +
                "on l.accID=ut.accID  " +
                "where l.insertTime between @stDate and @edDate   " +
                "group by cast(l.insertTime as date),ut.interval " +
                "order by dt;   " +


                //获取输出结果部分
                "select '登录日均销售' SourceName,avg(aCnt) ThisWeekVal from #allSale " +
                "union all " +
                "select saleFlag SourceName,avg(aCnt) ThisWeekVal from #deviceSale " +
                "group by saleFlag " +
                "union all " +
                "select SourceName," +
                "sum(ThisWeekVal) ThisWeekVal from #tempStatus group by SourceName " +
                "union all " +
                "select Source SourceName,avg(aCnt) ThisWeekVal from #editionSale group by Source " +
                "union all " +
                "select '<1万' SourceName,avg(aCnt) ThisWeekVal from #money1 " +
                "union all " +
                "select '1-5万' SourceName,avg(aCnt) ThisWeekVal from #money2 " +
                "union all " +
                "select '5-10万' SourceName,avg(aCnt) ThisWeekVal from #money3 " +
                "union all " +
                "select '10-30万' SourceName,avg(aCnt) ThisWeekVal from #money4 " +
                "union all " +
                "select '>30万' SourceName,avg(aCnt) ThisWeekVal from #money5 " +
                "union all " +
                "select Source SourceName,avg(aCnt) ThisWeekVal from #logNew  " +
                "group by Source   " +
                "union all " +
                "select '销售独立店铺' SourceName,COUNT(distinct accID) ThisWeekVal from i200.dbo.T_SaleInfo where insertTime between @stDate and @edDate " +
                "union all " +
                "select '补录笔数' SourceName,count(saleID) ThisWeekVal from i200.dbo.T_SaleInfo  " +
                "where insertTime between @stDate and @edDate " +
                "and DATEDIFF(MINUTE,saleTime,insertTime)>1 " +
                "union all " +
                "select '补录店铺数' SourceName,count(distinct accID) ThisWeekVal from i200.dbo.T_SaleInfo  " +
                "where insertTime between @stDate and @edDate " +
                "and DATEDIFF(MINUTE,saleTime,insertTime)>1; " +
                //"union all " +




                //删除临时表部分
                "drop table #money1; " +
                "drop table #money2; " +
                "drop table #money3; " +
                "drop table #money4; " +
                "drop table #money5; " +

                "drop table #saleMoney; " +
                "drop table #editionSale; " +
                "drop table #tempStatus; " +
                "drop table #statusSale; " +
                "drop table #deviceSale; " +
                "drop table #logUserType; " +
                "drop table #logNew; " +
                "drop table #allSale; "


                );

            //itemGroupSaleAfter.Add(new ItemGroup("挂单笔数", 1, 1, new NewAccountItem("挂单笔数", 0, 0)));

            try
            {
                return
                    DapperHelper.Query<NewAccountItem>(strSql.ToString(), new { date1 = stDate, date2 = edDate })
                        .ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("获取促活报表销售数据出错！", ex);
                return null;
            }
        }

        /// <summary>
        /// 获取商品报表数据
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public List<NewAccountItem> GetGoodsPart(DateTime stDate, DateTime edDate)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(
                //声明日期部分
                "declare @stDate datetime,@edDate datetime;" +
                "set @stDate=@date1;" +
                "set @edDate=@date2;" +

                //获取数据临时表
                "select count(gid) gCnt,cast(gInsertDate as date) dt into #allUser from i200.dbo.T_GoodsInfo " +
                "where gInsertDate between @stDate and @edDate " +
                "group by cast(gInsertDate as date) " +
                "order by dt; " +

                "select case gFlag when 0 then 'Web & PC' when 1 then 'iPhone' when 2 then 'Android' when 3 then 'iPad' end gFlag,count(gid) uCnt,cast(gInsertDate as date) dt into #deviceUser from i200.dbo.T_GoodsInfo " +
                "where gInsertDate between @stDate and @edDate " +
                "group by cast(gInsertDate as date),gFlag " +
                "order by dt;" +

                "select sa.active,count(gid) uCnt,cast(gInsertDate as date) dt into #statusUser from i200.dbo.T_GoodsInfo ui " +
                "left join sys_i200.dbo.SysRpt_ShopActive sa " +
                "on ui.accID=sa.accid " +
                "where gInsertDate between @stDate and @edDate " +
                "and sa.startTime<=gInsertDate and sa.updatetime>=gInsertDate " +
                "group by cast(gInsertDate as date),sa.active " +
                "order by dt; " +
                //关于状态的例外处理
                "select case active when -3 then '流失' when -1 then '休眠' when 1 then '新注册' when 3 then '需关怀' when 4 then '需关怀' when 5 then '活跃' when 7 then '忠诚' end SourceName," +
                "avg(uCnt) ThisWeekVal into #tempStatus from #statusUser group by active; " +

                "select case b.aotjb when 1 then '免费版' when 3 then '高级版' end Source,count(gid) uCnt,cast(gInsertDate as date) dt into #editionUser from i200.dbo.T_GoodsInfo ui " +
                "left join i200.dbo.T_Business b " +
                "on ui.accID=b.accountid " +
                "where gInsertDate between @stDate and @edDate " +
                "group by cast(gInsertDate as date),b.aotjb " +
                "order by dt; " +

                "select accID,sum(RealMoney) mCnt into #saleMoney from i200.dbo.T_SaleInfo " +
                "where insertTime between @stDate and @edDate  " +
                "group by accID; " +

                "select count(gid) uCnt,cast(gInsertDate as date) dt into #user1 from i200.dbo.T_GoodsInfo ui " +
                "where gInsertDate between @stDate and @edDate " +
                "and accID in (select accID from #saleMoney where mCnt<10000) " +
                "group by CAST(gInsertDate as date); " +

                "select count(gid) uCnt,cast(gInsertDate as date) dt into #user2 from i200.dbo.T_GoodsInfo ui " +
                "where gInsertDate between @stDate and @edDate " +
                "and accID in (select accID from #saleMoney where mCnt<50000 and mCnt>=10000) " +
                "group by CAST(gInsertDate as date); " +

                "select count(gid) uCnt,cast(gInsertDate as date) dt into #user3 from i200.dbo.T_GoodsInfo ui " +
                "where gInsertDate between @stDate and @edDate " +
                "and accID in (select accID from #saleMoney where mCnt<100000 and mCnt>=50000) " +
                "group by CAST(gInsertDate as date); " +

                "select count(gid) uCnt,cast(gInsertDate as date) dt into #user4 from i200.dbo.T_GoodsInfo ui " +
                "where gInsertDate between @stDate and @edDate " +
                "and accID in (select accID from #saleMoney where mCnt<300000 and mCnt>=100000) " +
                "group by CAST(gInsertDate as date); " +

                "select count(gid) uCnt,cast(gInsertDate as date) dt into #user5 from i200.dbo.T_GoodsInfo ui " +
                "where gInsertDate between @stDate and @edDate " +
                "and accID in (select accID from #saleMoney where mCnt>=300000) " +
                "group by CAST(gInsertDate as date); " +

                //新增新老用户维度
                "select distinct accID,DATEDIFF(SECOND,@stDate,ta.RegTime) interval into #logUserType from i200.dbo.T_GoodsInfo l " +
                "left join i200.dbo.T_Account ta " +
                "on l.accID=ta.ID " +
                "where gInsertDate between @stDate and @edDate;                 " +

                "update #logUserType set interval=1 where interval>=0; " +
                "update #logUserType set interval=0 where interval<0;                 " +

                "select case ut.interval when 1 then '新用户' when 0 then '老用户' end Source,count(gid) aCnt,cast(l.gInsertDate as date) dt into #logNew from i200.dbo.T_GoodsInfo l   " +
                "left join #logUserType ut " +
                "on l.accID=ut.accID  " +
                "where l.gInsertDate between @stDate and @edDate   " +
                "group by cast(l.gInsertDate as date),ut.interval " +
                "order by dt;   " +


                //获取输出结果部分
                "select '登录日均商品' SourceName,avg(gCnt) ThisWeekVal from #allUser " +
                "union all " +
                "select gFlag SourceName,avg(uCnt) ThisWeekVal from #deviceUser " +
                "group by gFlag " +
                "union all " +
                "select SourceName," +
                "sum(ThisWeekVal) ThisWeekVal from #tempStatus group by SourceName " +
                "union all " +
                "select Source SourceName,avg(uCnt) ThisWeekVal from #editionUser group by Source " +
                "union all " +
                "select '<1万' SourceName,avg(uCnt) ThisWeekVal from #user1 " +
                "union all " +
                "select '1-5万' SourceName,avg(uCnt) ThisWeekVal from #user2 " +
                "union all " +
                "select '5-10万' SourceName,avg(uCnt) ThisWeekVal from #user3 " +
                "union all " +
                "select '10-30万' SourceName,avg(uCnt) ThisWeekVal from #user4 " +
                "union all " +
                "select '>30万' SourceName,avg(uCnt) ThisWeekVal from #user5 " +
                "union all " +
                "select Source SourceName,avg(aCnt) ThisWeekVal from #logNew  " +
                "group by Source   " +
                "union all " +
                "select '商品录入独立店铺' SourceName,count(distinct accID) ThisWeekVal from i200.dbo.T_GoodsInfo  " +
                "where gInsertDate between @stDate and @edDate; " +

                //删除临时表部分
                "drop table #allUser; " +
                "drop table #deviceUser; " +
                "drop table #statusUser; " +
                "drop table #tempStatus; " +
                "drop table #editionUser; " +

                "drop table #user1; " +
                "drop table #user2; " +
                "drop table #user3; " +
                "drop table #user4; " +
                "drop table #user5; " +
                "drop table #logUserType; " +
                "drop table #logNew; " +                
                "drop table #saleMoney; "

                );

            try
            {
                return
                    DapperHelper.Query<NewAccountItem>(strSql.ToString(), new { date1 = stDate, date2 = edDate })
                        .ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("获取商品报表销售数据出错！", ex);
                return null;
            }
        }

        /// <summary>
        /// 获取促活报表会员部分
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public List<NewAccountItem> GetUserPart(DateTime stDate, DateTime edDate)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(
                //声明日期部分
                "declare @stDate datetime,@edDate datetime;" +
                "set @stDate=@date1;" +
                "set @edDate=@date2;" +

                //获取数据临时表
                "select count(uid) uCnt,cast(uInsertTime as date) dt into #allUser from i200.dbo.T_UserInfo " +
                "where uInsertTime between @stDate and @edDate " +
                "group by cast(uInsertTime as date) " +
                "order by dt; " +

                "select case uFlag when 0 then 'Web & PC' when 1 then 'iPhone' when 2 then 'Android' when 3 then 'iPad' end uFlag,count(uid) uCnt,cast(uInsertTime as date) dt into #deviceUser from i200.dbo.T_UserInfo " +
                "where uInsertTime between @stDate and @edDate " +
                "group by cast(uInsertTime as date),uFlag " +
                "order by dt; " +

                "select sa.active,count(uid) uCnt,cast(uInsertTime as date) dt into #statusUser from i200.dbo.T_UserInfo ui " +
                "left join sys_i200.dbo.SysRpt_ShopActive sa " +
                "on ui.accID=sa.accid " +
                "where uInsertTime between @stDate and @edDate " +
                "and sa.startTime<=uInsertTime and sa.updatetime>=uInsertTime " +
                "group by cast(uInsertTime as date),sa.active " +
                "order by dt; " +
                //关于状态的例外处理
                "select case active when -3 then '流失' when -1 then '休眠' when 1 then '新注册' when 3 then '需关怀' when 4 then '需关怀' when 5 then '活跃' when 7 then '忠诚' end SourceName," +
                "avg(uCnt) ThisWeekVal into #tempStatus from #statusUser group by active; " +

                "select case b.aotjb when 1 then '免费版' when 3 then '高级版' end Source,count(uid) uCnt,cast(uInsertTime as date) dt into #editionUser from i200.dbo.T_UserInfo ui " +
                "left join i200.dbo.T_Business b " +
                "on ui.accID=b.accountid " +
                "where uInsertTime between @stDate and @edDate " +
                "group by cast(uInsertTime as date),b.aotjb " +
                "order by dt; " +

                "select accID,sum(RealMoney) mCnt into #saleMoney from i200.dbo.T_SaleInfo " +
                "where insertTime between @stDate and @edDate  " +
                "group by accID; " +

                "select count(uid) uCnt,cast(uInsertTime as date) dt into #user1 from i200.dbo.T_UserInfo ui " +
                "where uInsertTime between @stDate and @edDate " +
                "and accID in (select accID from #saleMoney where mCnt<10000) " +
                "group by CAST(uInsertTime as date); " +

                "select count(uid) uCnt,cast(uInsertTime as date) dt into #user2 from i200.dbo.T_UserInfo ui " +
                "where uInsertTime between @stDate and @edDate " +
                "and accID in (select accID from #saleMoney where mCnt<50000 and mCnt>=10000) " +
                "group by CAST(uInsertTime as date); " +

                "select count(uid) uCnt,cast(uInsertTime as date) dt into #user3 from i200.dbo.T_UserInfo ui " +
                "where uInsertTime between @stDate and @edDate " +
                "and accID in (select accID from #saleMoney where mCnt<100000 and mCnt>=50000) " +
                "group by CAST(uInsertTime as date); " +

                "select count(uid) uCnt,cast(uInsertTime as date) dt into #user4 from i200.dbo.T_UserInfo ui " +
                "where uInsertTime between @stDate and @edDate " +
                "and accID in (select accID from #saleMoney where mCnt<300000 and mCnt>=100000) " +
                "group by CAST(uInsertTime as date); " +

                "select count(uid) uCnt,cast(uInsertTime as date) dt into #user5 from i200.dbo.T_UserInfo ui " +
                "where uInsertTime between @stDate and @edDate " +
                "and accID in (select accID from #saleMoney where mCnt>=300000) " +
                "group by CAST(uInsertTime as date); " +

                //新增新老用户维度
                "select distinct accID,DATEDIFF(SECOND,@stDate,ta.RegTime) interval into #logUserType from i200.dbo.T_UserInfo l " +
                "left join i200.dbo.T_Account ta " +
                "on l.accID=ta.ID " +
                "where uInsertTime between @stDate and @edDate;                 " +

                "update #logUserType set interval=1 where interval>=0; " +
                "update #logUserType set interval=0 where interval<0;                 " +

                "select case ut.interval when 1 then '新用户' when 0 then '老用户' end Source,count(uid) aCnt,cast(l.uInsertTime as date) dt into #logNew from i200.dbo.T_UserInfo l   " +
                "left join #logUserType ut " +
                "on l.accID=ut.accID  " +
                "where l.uInsertTime between @stDate and @edDate   " +
                "group by cast(l.uInsertTime as date),ut.interval " +
                "order by dt;   " +

                //获取输出结果部分
                "select '登录日均会员' SourceName,avg(uCnt) ThisWeekVal from #allUser " +
                "union all " +
                "select uFlag SourceName,avg(uCnt) ThisWeekVal from #deviceUser " +
                "group by uFlag " +
                "union all " +
                "select  SourceName,sum(ThisWeekVal) ThisWeekVal from #tempStatus group by SourceName " +
                "union all " +
                "select Source SourceName,avg(uCnt) ThisWeekVal from #editionUser group by Source " +
                "union all " +
                "select '<1万' SourceName,avg(uCnt) ThisWeekVal from #user1 " +
                "union all " +
                "select '1-5万' SourceName,avg(uCnt) ThisWeekVal from #user2 " +
                "union all " +
                "select '5-10万' SourceName,avg(uCnt) ThisWeekVal from #user3 " +
                "union all " +
                "select '10-30万' SourceName,avg(uCnt) ThisWeekVal from #user4 " +
                "union all " +
                "select '>30万' SourceName,avg(uCnt) ThisWeekVal from #user5 " +
                "union all " +
                "select Source SourceName,avg(aCnt) ThisWeekVal from #logNew  " +
                "group by Source   " +
                "union all " +
                "select '会员录入独立店铺' SourceName,count(distinct accID) ThisWeekVal from i200.dbo.T_UserInfo  " +
                "where uInsertTime between @stDate and @edDate; " +

                //删除临时表部分
                "drop table #allUser; " +
                "drop table #deviceUser; " +
                "drop table #statusUser; " +
                "drop table #tempStatus; " +
                "drop table #editionUser; " +
                "drop table #logUserType; " +
                "drop table #logNew; " +

                "drop table #user1; " +
                "drop table #user2; " +
                "drop table #user3; " +
                "drop table #user4; " +
                "drop table #user5; " +

                "drop table #saleMoney; "

                );

            try
            {
                return
                    DapperHelper.Query<NewAccountItem>(strSql.ToString(), new { date1 = stDate, date2 = edDate })
                        .ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("获取会员报表信息出错！", ex);
                return null;
            }
        }

        /// <summary>
        /// 获取促活报表支出部分
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public List<NewAccountItem> GetPayPart(DateTime stDate, DateTime edDate)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(
                //声明日期部分
                "declare @stDate datetime,@edDate datetime;" +
                "set @stDate=@date1;" +
                "set @edDate=@date2;" +

                //获取数据临时表
                "select count(ID) pCnt,CAST(PayDate as date) dt into #allPay from i200.dbo.t_PayRecord " +
                "where PayDate between @stDate and @edDate " +
                "group by CAST(PayDate as date) " +
                "order by dt; " +

                "select case pFlag when 0 then 'Web & PC' when 1 then 'iPhone' when 2 then 'Android' when 3 then 'iPad' end pFlag,count(ID) pCnt,CAST(PayDate as date) dt into #devicePay from i200.dbo.t_PayRecord " +
                "where PayDate between @stDate and @edDate " +
                "group by CAST(PayDate as date),pFlag " +
                "order by dt; " +

                "select sa.active,count(pr.ID) pCnt,CAST(PayDate as date) dt into #statusPay from i200.dbo.t_PayRecord pr " +
                "left join Sys_I200.dbo.SysRpt_ShopActive sa " +
                "on pr.ShopperId=sa.accid " +
                "where PayDate between @stDate and @edDate " +
                "and sa.startTime<=pr.PayDate and sa.updatetime>=pr.PayDate " +
                "group by CAST(PayDate as date),active; " +
                //关于状态的例外处理
                "select case active when -3 then '流失' when -1 then '休眠' when 1 then '新注册' when 3 then '需关怀' when 4 then '需关怀' when 5 then '活跃' when 7 then '忠诚' end SourceName," +
                "avg(pCnt) ThisWeekVal into #tempStatus from #statusPay group by active; " +

                "select case b.aotjb when 1 then '免费版' when 3 then '高级版' end Source,count(pr.ID) pCnt,CAST(PayDate as date) dt into #editionPay from i200.dbo.t_PayRecord pr " +
                "left join i200.dbo.T_Business b " +
                "on pr.ShopperId=b.accountid " +
                "where PayDate between @stDate and @edDate " +
                "group by aotjb,CAST(PayDate as date); " +

                "select accID,sum(RealMoney) mCnt into #saleMoney from i200.dbo.T_SaleInfo " +
                "where insertTime between @stDate and @edDate  " +
                "group by accID; " +

                "select count(pr.ID) pCnt,cast(PayDate as date) dt into #pay1 from i200.dbo.t_PayRecord pr " +
                "where PayDate between @stDate and @edDate " +
                "and pr.ID in (select accID from #saleMoney where mCnt<10000) " +
                "group by CAST(PayDate as date); " +

                "select count(pr.ID) pCnt,cast(PayDate as date) dt into #pay2 from i200.dbo.t_PayRecord pr " +
                "where PayDate between @stDate and @edDate " +
                "and pr.ID in (select accID from #saleMoney where mCnt<50000 and mCnt>=10000) " +
                "group by CAST(PayDate as date); " +

                "select count(pr.ID) pCnt,cast(PayDate as date) dt into #pay3 from i200.dbo.t_PayRecord pr " +
                "where PayDate between @stDate and @edDate " +
                "and pr.ID in (select accID from #saleMoney where mCnt<100000 and mCnt>=50000) " +
                "group by CAST(PayDate as date); " +

                "select count(pr.ID) pCnt,cast(PayDate as date) dt into #pay4 from i200.dbo.t_PayRecord pr " +
                "where PayDate between @stDate and @edDate " +
                "and pr.ID in (select accID from #saleMoney where mCnt<300000 and mCnt>=100000) " +
                "group by CAST(PayDate as date); " +

                "select count(pr.ID) pCnt,cast(PayDate as date) dt into #pay5 from i200.dbo.t_PayRecord pr " +
                "where PayDate between @stDate and @edDate " +
                "and pr.ID in (select accID from #saleMoney where mCnt>=300000) " +
                "group by CAST(PayDate as date); " +

                //新增新老用户维度
                "select distinct ShopperId,DATEDIFF(SECOND,@stDate,ta.RegTime) interval into #logUserType from i200.dbo.t_PayRecord l " +
                "left join i200.dbo.T_Account ta " +
                "on l.ShopperId=ta.ID " +
                "where PayDate between @stDate and @edDate;                 " +

                "update #logUserType set interval=1 where interval>=0; " +
                "update #logUserType set interval=0 where interval<0;                 " +

                "select case ut.interval when 1 then '新用户' when 0 then '老用户' end Source,count(l.ID) aCnt,cast(l.PayDate as date) dt into #logNew from i200.dbo.t_PayRecord l   " +
                "left join #logUserType ut " +
                "on l.ShopperId=ut.ShopperId  " +
                "where l.PayDate between @stDate and @edDate   " +
                "group by cast(l.PayDate as date),ut.interval " +
                "order by dt;   " +

                //获取输出结果部分
                "select '登录日均支出' SourceName,avg(pCnt) ThisWeekVal from #allPay " +
                "union all " +
                "select pFlag SourceName,avg(pCnt) ThisWeekVal from #devicePay " +
                "group by pFlag " +
                "union all " +
                "select SourceName,sum(ThisWeekVal) ThisWeekVal from #tempStatus group by SourceName " +
                "union all " +
                "select Source SourceName,avg(pCnt) ThisWeekVal from #editionPay group by Source " +
                "union all " +
                "select '<1万' SourceName,avg(pCnt) ThisWeekVal from #pay1 " +
                "union all " +
                "select '1-5万' SourceName,avg(pCnt) ThisWeekVal from #pay2 " +
                "union all " +
                "select '5-10万' SourceName,avg(pCnt) ThisWeekVal from #pay3 " +
                "union all " +
                "select '10-30万' SourceName,avg(pCnt) ThisWeekVal from #pay4 " +
                "union all " +
                "select '>30万' SourceName,avg(pCnt) ThisWeekVal from #pay5 " +
                "union all " +
                "select Source SourceName,avg(aCnt) ThisWeekVal from #logNew  " +
                "group by Source   " +
                "union all " +
                "select '支出录入独立店铺' SourceName,count(distinct ShopperId) ThisWeekVal from i200.dbo.t_PayRecord  " +
                "where PayDate between @stDate and @edDate; " +


                //删除临时表部分
                "drop table #allPay; " +
                "drop table #devicePay; " +
                "drop table #statusPay; " +
                "drop table #tempStatus; " +
                "drop table #editionPay; " +
                "drop table #logUserType; " +
                "drop table #logNew; " +

                "drop table #pay1; " +
                "drop table #pay2; " +
                "drop table #pay3; " +
                "drop table #pay4; " +
                "drop table #pay5; " +
                "drop table #saleMoney; "

                );

            try
            {
                return
                    DapperHelper.Query<NewAccountItem>(strSql.ToString(), new { date1 = stDate, date2 = edDate })
                        .ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("获取支出报表信息出错！", ex);
                return null;
            }
        }

        /// <summary>
        ///  获取促活报表短信部分
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public List<NewAccountItem> GetSmsPart(DateTime stDate, DateTime edDate)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(
                //声明日期部分
                "declare @stDate datetime,@edDate datetime;" +
                "set @stDate=@date1;" +
                "set @edDate=@date2;" +

                //获取数据临时表
                "select SUM(realSmsCnt) sCnt,CAST(submitTime as date) dt into #smsAll from i200.dbo.T_Sms_Notify  " +
                "where submitTime between @stDate and @edDate " +
                "group by CAST(submitTime as date) " +
                "order by dt; " +

                "select sa.active,sum(sn.realSmsCnt) sCnt,CAST(submitTime as date) dt into #statusSms from i200.dbo.T_Sms_Notify sn  " +
                "left join Sys_I200.dbo.SysRpt_ShopActive sa " +
                "on sn.accID=sa.accid  " +
                "where submitTime between @stDate and @edDate " +
                "and sa.startTime<=sn.submitTime and sa.updatetime>=sn.submitTime " +
                "group by active,CAST(submitTime as date); " +
                //关于状态的例外处理
                "select case active when -3 then '流失' when -1 then '休眠' when 1 then '新注册' when 3 then '需关怀' when 4 then '需关怀' when 5 then '活跃' when 7 then '忠诚' end SourceName," +
                "avg(sCnt) ThisWeekVal into #tempStatus from #statusSms group by active; " +

                "select case b.aotjb when 1 then '免费版' when 3 then '高级版' end Source,sum(sn.realSmsCnt) sCnt,CAST(submitTime as date) dt into #editionSms from i200.dbo.T_Sms_Notify sn " +
                "left join i200.dbo.T_Business b " +
                "on sn.accID=b.accountid " +
                "where submitTime between @stDate and @edDate " +
                "group by aotjb,CAST(submitTime as date); " +


                "select accID,sum(RealMoney) mCnt into #saleMoney from i200.dbo.T_SaleInfo " +
                "where insertTime between @stDate and @edDate  " +
                "group by accID; " +

                "select sum(sn.realSmsCnt) sCnt,cast(submitTime as date) dt into #sms1 from i200.dbo.T_Sms_Notify sn " +
                "where submitTime between @stDate and @edDate " +
                "and sn.accID in (select accID from #saleMoney where mCnt<10000) " +
                "group by CAST(submitTime as date); " +

                "select sum(sn.realSmsCnt) sCnt,cast(submitTime as date) dt into #sms2 from i200.dbo.T_Sms_Notify sn " +
                "where submitTime between @stDate and @edDate " +
                "and sn.accID in (select accID from #saleMoney where mCnt<50000 and mCnt>=10000) " +
                "group by CAST(submitTime as date); " +

                "select sum(sn.realSmsCnt) sCnt,cast(submitTime as date) dt into #sms3 from i200.dbo.T_Sms_Notify sn " +
                "where submitTime between @stDate and @edDate " +
                "and sn.accID in (select accID from #saleMoney where mCnt<100000 and mCnt>=50000) " +
                "group by CAST(submitTime as date); " +

                "select sum(sn.realSmsCnt) sCnt,cast(submitTime as date) dt into #sms4 from i200.dbo.T_Sms_Notify sn " +
                "where submitTime between @stDate and @edDate " +
                "and sn.accID in (select accID from #saleMoney where mCnt<300000 and mCnt>=100000) " +
                "group by CAST(submitTime as date); " +

                "select sum(sn.realSmsCnt) sCnt,cast(submitTime as date) dt into #sms5 from i200.dbo.T_Sms_Notify sn " +
                "where submitTime between @stDate and @edDate " +
                "and sn.accID in (select accID from #saleMoney where mCnt>=300000) " +
                "group by CAST(submitTime as date); " +

                "select sum(realSmsCnt) sCnt,CAST(submitTime as date) dt into #dailySms from i200.dbo.T_Sms_Notify " +
                "where submitTime between @stDate and @edDate and isFree=0 " +
                "group by CAST(submitTime as date); " +

                "select sum(realSmsCnt) sCnt,CAST(submitTime as date) dt into #dailySmsW from i200.dbo.T_Sms_Notify " +
                "where submitTime between @stDate and @edDate and isFree=1 " +
                "group by CAST(submitTime as date); " +

                //新增新老用户维度
                "select distinct accID,DATEDIFF(SECOND,@stDate,ta.RegTime) interval into #logUserType from i200.dbo.T_Sms_Notify l " +
                "left join i200.dbo.T_Account ta " +
                "on l.accID=ta.ID " +
                "where submitTime between @stDate and @edDate;                 " +

                "update #logUserType set interval=1 where interval>=0; " +
                "update #logUserType set interval=0 where interval<0;                 " +

                "select case ut.interval when 1 then '新用户' when 0 then '老用户' end Source,sum(realSmsCnt) aCnt,cast(l.submitTime as date) dt into #logNew from i200.dbo.T_Sms_Notify l   " +
                "left join #logUserType ut " +
                "on l.accID=ut.accID  " +
                "where l.submitTime between @stDate and @edDate   " +
                "group by cast(l.submitTime as date),ut.interval " +
                "order by dt;   " +

                //获取输出结果部分
                "select '登录日均短信数' SourceName,avg(sCnt) ThisWeekVal from #smsAll " +
                "union all " +
                "select SourceName,sum(ThisWeekVal) ThisWeekVal from #tempStatus group by SourceName " +
                "union all " +
                "select Source SourceName,avg(sCnt) ThisWeekVal from #editionSms group by Source " +
                "union all " +
                "select '<1万' SourceName,avg(sCnt) ThisWeekVal from #sms1 " +
                "union all " +
                "select '1-5万' SourceName,avg(sCnt) ThisWeekVal from #sms2 " +
                "union all " +
                "select '5-10万' SourceName,avg(sCnt) ThisWeekVal from #sms3 " +
                "union all " +
                "select '10-30万' SourceName,avg(sCnt) ThisWeekVal from #sms4 " +
                "union all " +
                "select '>30万' SourceName,avg(sCnt) ThisWeekVal from #sms5 " +
                "union all " +
                "select Source SourceName,avg(aCnt) ThisWeekVal from #logNew  " +
                "group by Source   " +
                "union all " +
                "select '发送短信独立店铺' SourceName,count(distinct accID) ThisWeekVal from i200.dbo.T_Sms_Notify  " +
                "where submitTime between @stDate and @edDate " +
                "union all " +
                "select '营销短信' SourceName,avg(sCnt) ThisWeekVal from #dailySms " +
                "union all " +
                "select '维客短信' SourceName,avg(sCnt) ThisWeekVal from #dailySmsW; " +


                //删除临时表部分
                "drop table #smsAll; " +
                "drop table #statusSms; " +
                "drop table #tempStatus; " +
                "drop table #editionSms; " +
                "drop table #dailySms; " +
                "drop table #dailySmsW; " +
                "drop table #logUserType; " +
                "drop table #logNew; " +

                "drop table #sms1; " +
                "drop table #sms2; " +
                "drop table #sms3; " +
                "drop table #sms4; " +
                "drop table #sms5; " +
                "drop table #saleMoney; "

                );

            try
            {
                return
                    DapperHelper.Query<NewAccountItem>(strSql.ToString(), new { date1 = stDate, date2 = edDate })
                        .ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("获取短信报表信息出错！", ex);
                return null;
            }
        }

        public List<NewAccountItem> GetShowPart(DateTime stDate, DateTime edDate)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(
                //声明日期部分
                "declare @stDate datetime,@edDate datetime;" +
                "set @stDate=@date1;" +
                "set @edDate=@date2;" +

                "select CAST(l_time as date) opDate,count(id) pv into #pv from [I200_Log].[dbo].[HTML_Log] " +
                "where l_time between @stDate and @edDate " +
                "group by CAST(l_time as date); " +

                "select CAST(l_time as date) opDate,count(distinct l_ip) uv into #uv from [I200_Log].[dbo].[HTML_Log] " +
                "where l_time between @stDate and @edDate " +
                "group by CAST(l_time as date); " +

                "select CAST(updateTime as date) operDate,count(*) cnt into #dailyGood from [i200].[dbo].[t_GoodsExtend] " +
                "where ge_stat=1 and  updateTime between @stDate and @edDate " +
                "group by CAST(updateTime as date); " +

                "select 'pv' SourceName,avg(pv) ThisWeekVal from #pv " +
                "union all " +
                "select 'uv' SourceName,avg(uv) ThisWeekVal from #uv " +
                "union all " +
                "select '日均新增商品' SourceName,avg(cnt) ThisWeekVal from #dailyGood " +
                "union all " +
                "select '手机橱窗订单' SourceName,COUNT(*) ThisWeekVal from [i200].[dbo].[T_Goods_Booking] " +
                "where bInsertTime between @stDate and @edDate " +
                "union all " +
                "select '周期内交易金额' SourceName,sum(bRealMoney) ThisWeekVal from [i200].[dbo].[T_Goods_Booking] " +
                "where bState in (1, 2, 3, 4) and payType = 1 and bInsertTime between @stDate and @edDate " +
                "union all " +
                "select '手机橱窗支付' SourceName,COUNT(*) ThisWeekVal from [i200].[dbo].[T_Goods_Booking] " +
                "where bInsertTime between @stDate and @edDate and bState in (1,2,3,4) and payType=1; " +

                "drop table #dailyGood; " +
                "drop table #uv; " +
                "drop table #pv; "

                );

            try
            {
                return
                    DapperHelper.Query<NewAccountItem>(strSql.ToString(), new { date1 = stDate, date2 = edDate })
                        .ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("获取手机橱窗报表信息出错！", ex);
                return null;
            }
        }

        /// <summary>
        /// 获取付费信息报表
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public List<NewAccountItem> GetIncomePart(DateTime stDate, DateTime edDate)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(
                //声明日期部分
                "declare @stDate datetime,@edDate datetime,@sendCoupon int,@useCoupon int,@endUser int,@repayUser decimal;" +
                "set @stDate=@date1;" +
                "set @edDate=@date2;" +

                "SELECT @sendCoupon=count(distinct toAccId) " +
                "FROM [I200].[dbo].[T_Order_CouponList] cl " +
                "left join [I200].[dbo].[T_Order_CouponInfo] ci " +
                "on cl.groupId=ci.id " +
                "where cl.receiveDate between @stDate and @edDate and ci.couponType=1 " +
                "and toAccId not in (select acc_id from Sys_I200.dbo.Sys_TagNexus where tag_id=446); " +

                "SELECT @useCoupon=count(distinct toAccId) " +
                "FROM [I200].[dbo].[T_Order_CouponList] cl " +
                "left join [I200].[dbo].[T_Order_CouponInfo] ci " +
                "on cl.groupId=ci.id " +
                "where cl.receiveDate between @stDate and @edDate and cl.usedDate between @stDate and @edDate and ci.couponType=1 " +
                "and toAccId not in (select acc_id from Sys_I200.dbo.Sys_TagNexus where tag_id=446); " +

                "select accId,RealPayMoney,DATEADD(MONTH,ol.itemQuantity,oi.transactionDate) dt into #ordList from i200.dbo.T_OrderInfo oi " +
                "left join i200.dbo.T_Order_List ol " +
                "on oi.oid=ol.oid " +
                "where DATEADD(MONTH,ol.itemQuantity,oi.transactionDate) between @stDate and @edDate " +
                "and ol.itemId<>1 and oi.orderStatus=2 " +
                "and ol.itemName not like '%充值%'; " +
                "select @endUser=count(distinct accid) from #ordList; " +

                "select o.accId,o.RealPayMoney into #repayList from i200.dbo.T_OrderInfo o " +
                "left join #ordList  " +
                "on o.accId=#ordList.accId " +
                "left join i200.dbo.T_Order_List ol " +
                "on o.oid=ol.oid " +
                "where transactionDate between @stDate and @edDate " +
                "and transactionDate>=dt and orderStatus=2 " +
                "and ol.itemId<>1 and ol.itemName not like '%充值%'; " +
                "select @repayUser=count(distinct accid) from #repayList; " +


                "SELECT '发出现金券' SourceName,@sendCoupon ThisWeekVal " +
                "union all " +
                "SELECT '使用现金券' SourceName,@useCoupon ThisWeekVal " +
                "union all " +
                "SELECT '现金券使用率' SourceName,case when @sendCoupon = 0 then 0 else cast(@useCoupon / @sendCoupon * 100 as decimal(9, 3)) end ThisWeekVal " +
                "union all " +
                "select '付费用户数' SourceName,COUNT(distinct accId) ThisWeekVal from i200.dbo.T_OrderInfo " +
                "where createDate between @stDate and @edDate and ((orderStatus=1 and OrderTypeId=2) or orderStatus=2) " +
                "union all " +
                "SELECT '使用现金券金额' SourceName,sum(ci.couponValue) ThisWeekVal " +
                "FROM [I200].[dbo].[T_Order_CouponList] cl " +
                "left join [I200].[dbo].[T_Order_CouponInfo] ci " +
                "on cl.groupId=ci.id " +
                "where cl.receiveDate between @stDate and @edDate and cl.usedDate between @stDate and @edDate and ci.couponType=1 " +
                "and toAccId not in (select acc_id from Sys_I200.dbo.Sys_TagNexus where tag_id=446) " +
                "union all " +
                "select '抵现金额' SourceName,sum(commuteIntegralMoney) ThisWeekVal from i200.dbo.T_OrderInfo " +
                "where createDate between @stDate and @edDate and ((orderStatus=1 and OrderTypeId=2) or orderStatus=2) " +
                "union all " +
                "SELECT '付费用户续约率' SourceName,case when @endUser = 0 then 0 else cast(@repayUser* 100 / @endUser  as decimal(18, 1)) end ThisWeekVal " +
                "union all " +
                "select '高付费用户续约率' SourceName,case when @endUser = 0 then 0 else cast(cast(count(distinct accId) as decimal) * 100/ @endUser  as decimal(18, 1)) end ThisWeekVal from #repayList where RealPayMoney>=99 and  " +
                "accid in (select accId from #ordList where RealPayMoney>=99) " +
                "union all " +
                "select '活忠用户平均收入' SourceName,CAST(SUM(RealPayMoney) as decimal(9,3))/count(distinct o.accId) ThisWeekVal from i200.dbo.T_OrderInfo o " +
                "left join Sys_I200.dbo.SysRpt_ShopActive sa " +
                "on o.accId=sa.accid " +
                "where transactionDate>=sa.startTime and transactionDate<=sa.updatetime and active in (5,7) " +
                "and ((orderStatus=1 and OrderTypeId=2) or orderStatus=2) and transactionDate between @stDate and @edDate; " +

                "drop table #ordList; " +
                "drop table #repayList; "


                );

            try
            {
                return
                    DapperHelper.Query<NewAccountItem>(strSql.ToString(), new { date1 = stDate, date2 = edDate })
                        .ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("获取付费信息报表出错！", ex);
                return null;
            }
        }



        /// <summary>
        /// 获取周期内日均独立登录数
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public int GetIndependLog(DateTime stDate, DateTime edDate)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(
                "select COUNT(distinct Accountid) cnt,cast(OperDate as date) dt into #logTemp from i200.dbo.T_Log where OperDate between @stDate and @edDate group by cast(OperDate as date);");
            strSql.Append(
                "select avg(cnt) from #logTemp;");
            strSql.Append("drop table #logTemp;");

            return DapperHelper.ExecuteScalar<int>(strSql.ToString(), new { stDate = stDate, edDate = edDate });
        }
    }
}
