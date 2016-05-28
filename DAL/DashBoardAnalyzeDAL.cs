using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// 首页数据分析DashBoard处理类
    /// </summary>
    public class DashBoardAnalyzeDAL
    {
        private static readonly Dictionary<string, string> pairValue = new Dictionary<string, string>
        {
            {"销售笔数", "saleNum"},
            {"会员数量", "userNum"},
            {"商品数量", "goodsNum"},
            {"短信数量", "smsNum"},
            {"订单笔数", "orderNum"},
            {"付费用户", "paidUsr"}
        };

        /// <summary>
        /// 获取当日登录用户的状态
        /// （活跃/流失/新增）
        /// </summary>
        /// <param name="nowDay"></param>
        /// <returns></returns>
        public DashBoardModel GetLoginType(DateTime nowDay)
        {
            DashBoardModel dashBoardData = new DashBoardModel();
            dashBoardData.Data.Add("ActiveUsr",0);
            dashBoardData.Data.Add("NewUsr", 0);
            dashBoardData.Data.Add("LostUsr", 0);
            dashBoardData.Data.Add("YesterData", 0);

            dashBoardData.Name = "登录";

            var bgDate = Convert.ToDateTime(nowDay.ToShortDateString());
            var edDate = bgDate.AddHours(23).AddMinutes(59).AddSeconds(59);

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select Accountid into #temp from i200.dbo.T_LOG where OperDate between @beginDate and @endDate group by Accountid;");
            strSql.Append(" select count(Accountid) from #temp;");
            strSql.Append(" drop table #temp;");

            //获取当日登录总数（同一个Accountid有可能出现重复）
            dashBoardData.Total = DapperHelper.ExecuteScalar<int>(strSql.ToString(), new { beginDate = bgDate, endDate = edDate });

            strSql.Clear();

            strSql.Append(" select Accountid into #tempYester from i200.dbo.T_LOG where OperDate between @beginDate and (getdate()-1) group by Accountid;");
            strSql.Append(" select count(Accountid) from #tempYester;");
            strSql.Append(" drop table #tempYester;");

            dashBoardData.Data["YesterData"] = DapperHelper.ExecuteScalar<int>(strSql.ToString(), new { beginDate = bgDate.AddDays(-1) });

            strSql.Clear();

            //获取登录用户的活跃度信息
            strSql.Append(" select Accountid into #temp from i200.dbo.T_LOG where OperDate between @beginDate and @endDate group by Accountid;");
            strSql.Append(" select bus.active,count(#temp.Accountid) as cnt");
            strSql.Append(" from #temp");
            strSql.Append(" left outer join i200.dbo.T_Business bus on bus.accountid=#temp.Accountid");
            strSql.Append(" group by bus.active");
            strSql.Append(" drop table #temp;");

            List<dynamic> list = DapperHelper.Query<dynamic>(strSql.ToString(), new { beginDate = bgDate, endDate = edDate }).ToList();

            foreach (dynamic item in list)
            {
                if (item.active<0)
                {
                    dashBoardData.Data["LostUsr"] += item.cnt;
                }
                else if (item.active == 0)
                {
                    dashBoardData.Data["NewUsr"] += item.cnt;
                }
                else if (item.active == 5 || item.active == 7)
                {
                    dashBoardData.Data["ActiveUsr"] += item.cnt;
                }
            }

            return dashBoardData;
        }

        /// <summary>
        /// 获取当日不同活跃状态用户的ID数组
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        /// 分页移动到详细数据获取那层
        public int[] GetLoginAccountId(int pageIndex,string active)
        {
            StringBuilder strSql = new StringBuilder();

            //int bgNumber = ((pageIndex - 1) * 15) + 1;
            //int edNumber = (pageIndex) * 15;

            strSql.Append("select Accountid from ( select ROW_NUMBER() over (order by t.Accountid desc) as rowNumber, " +
                          "t.accountid,b.active from i200.dbo.T_LOG t " +
                          "left join i200.dbo.T_Business b on t.Accountid=b.accountid " +
                          "where DATEDIFF(day,OperDate,GETDATE())=0 and b.active " + active+" group by t.Accountid,b.active) " +
                          "re");

            return DapperHelper.Query<int>(strSql.ToString()).ToArray();
        }

        /// <summary>
        /// 获取当日所有付费用户
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// 分页处理移动到详细信息获取
        public int[] GetPaidAllAccountId(int pageIndex, int index)
        {
            StringBuilder strSql = new StringBuilder();
            DateTime nowDay = DateTime.Now;
            List<int> accId = new List<int>();

            int bgNumber = ((pageIndex - 1) * 15) + 1;
            int edNumber = (pageIndex) * 15;

            strSql.Append(
                "select accid into #List from i200.dbo.T_orderinfo where orderStatus=2 and DateDiff(day,transactionDate,@day)=0 group by accId;");
            strSql.Append(
                "select accid from (select accid,ROW_NUMBER() over (order by accid) as rowNumber from #List) s;");
            strSql.Append("drop table #List");

            accId = DapperHelper.Query<int>(strSql.ToString(), new
            {
                day = nowDay
            }).ToList();

            return accId.ToArray();
        }


        public int[] GetLocationAccId(int pageIndex, string location)
        {
            Dictionary<string, string> list = new Dictionary<string, string>();
            location = location.Trim('市');
            location = location.Trim('省');

            int pageSize = 15;
            
            List<AreaShopInfoEx> areaShopInfoList = new List<AreaShopInfoEx>();
            List<int> accidList = new List<int>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select accid into #list from Sys_Account where accid in(select id from i200.dbo.T_Account where   ");
            strSql.Append(" State=1) and  sysAddress like '%'+ @AreaName +'%';             ");
            strSql.Append(" select  count(distinct accid) as accNum from #list; ");
            strSql.Append(" select * from (select row_number() over(order by accountid desc) as rowNumer,accountid,I200.dbo.T_Account.RegTime,I200.dbo.T_Account.CompanyName, ");
            strSql.Append(" max(allLoginNum) as LoginNum,max(lastLoginTime) as lastLoginTime ");
            strSql.Append(" from SysRpt_ShopDayInfo left outer join I200.dbo.T_Account on I200.dbo.T_Account.ID=SysRpt_ShopDayInfo.accountid ");
            strSql.Append(" where  SysRpt_ShopDayInfo.accountid in(select accid from #list) group by accountid,I200.dbo.T_Account.RegTime,I200.dbo.T_Account.CompanyName ");
            strSql.Append(" ) List ");
            //strSql.Append(" where List.rowNumer between @bgNumber and @edNumber ");
            strSql.Append(" order by LoginNum desc ");
            strSql.Append(" drop table #list  ");

            //页数计算accID
            int bgNumber = ((pageIndex - 1) * pageSize) + 1;
            int edNumber = (pageIndex) * pageSize;

            var dataList = DapperHelper.QueryMultiple(strSql.ToString(), new
            {
                AreaName = location
                //bgNumber = bgNumber,
                //edNumber = edNumber
            });

            if (dataList != null && dataList.Count > 0)
            {
                //var maxCntList = dataList[0].ToList();
                //int rowCount = Convert.ToInt32(maxCntList[0].accNum);

                //int maxPage = 0;
                //if (rowCount > 0)
                //{
                //    maxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(rowCount) / pageSize));
                //}

                foreach (dynamic dr in dataList[1].ToList())
                {
                    accidList.Add(int.Parse(dr.accountid.ToString()));
                }

                //list["rowCount"] = rowCount.ToString();
                //list["maxPage"] = maxPage.ToString();
                //list["accidList"] = CommonLib.Helper.JsonSerializeObject(accidList);
            }

            return accidList.ToArray();
        }

        public int[] GetRetentionUserDetail(int pageIndex, int index, string date, string day)
        {
            StringBuilder strSql = new StringBuilder();
            int pageSize = 15;

            int bgNumber = ((pageIndex - 1) * pageSize) + 1;
            int edNumber = (pageIndex) * pageSize;
            DateTime dt = Convert.ToDateTime(date);

            strSql.Append("select Id from (select Id,ROW_NUMBER() over (order by LoginTimeLast desc) rowNum from i200.dbo.T_Account where cast(regtime as date)=@date and state=1 and Id in (select Accountid from i200.dbo.T_LOG where datediff(day,@date,operdate)=0 group by Accountid)) T where T.rowNum between @bgNumber and @edNumber;");

            return DapperHelper.Query<int>(strSql.ToString(), new
            {
                date = dt,
                day = day,
                bgNumber = bgNumber,
                edNumber = edNumber
            }).ToArray();
        }

        public int[] GetFunnelDetailUser(int pageIndex,string keyword,string activeList)
        {
            StringBuilder strSql = new StringBuilder();

            int pageSize = 15;

            int bgNumber = ((pageIndex - 1) * pageSize) + 1;
            int edNumber = (pageIndex) * pageSize;

            strSql.Append(
                "select * into #List from [Sys_I200].[dbo].[SysRpt_ShopInfo] where accountid in (select ID from [i200].[dbo].[T_Account] where ID in (" +
                activeList + "));");

            strSql.Append("select accountid from (");

            switch (keyword)
            {
                case "reg":
                    strSql.Append("select ROW_NUMBER() over (order by lastLoginTime desc) rowNum,* from #List ");
                    break;
                case "login":
                    strSql.Append("select ROW_NUMBER() over (order by lastLoginTime desc) rowNum,* from #List where allLoginNum>0");
                    break;
                case "paid":
                    strSql.Append("select ROW_NUMBER() over (order by lastLoginTime desc) rowNum,* from #List where orderMoney>0");
                    break;
                case "active":
                    strSql.Append("select ROW_NUMBER() over (order by lastLoginTime desc) rowNum,* from #List where active in (5,7)");
                    break;
                case "datainput":
                    strSql.Append("select ROW_NUMBER() over (order by lastLoginTime desc) rowNum,* from #List where (userNum+smsNum+goodsNum+moodNum+saleNum+orderNum+outlayNum)>0");
                    break;
                case "saleinput":
                    strSql.Append("select ROW_NUMBER() over (order by lastLoginTime desc) rowNum,* from #List where saleNum>0");
                    break;
                case "secretention":
                    strSql.Append("select ROW_NUMBER() over (order by lastLoginTime desc) rowNum,l.* from #List l left join [i200].[dbo].T_LOG on l.accountid=T_LOG.Accountid where DATEDIFF(day,regTime,OperDate)=1");
                    break;
            }
            strSql.Append(") T where T.rowNum between @bgNumber and @edNumber;");
            
            strSql.Append("drop table #List;");

            return DapperHelper.Query<int>(strSql.ToString(), new
            {
                bgNumber = bgNumber,
                edNumber = edNumber
            }).ToArray();
        }

        /// <summary>
        /// 获取当日不同类型的活跃用户ID数组
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// 分页处理移动到详细信息获取
        public int[] GetActiveAccountId(int pageIndex, int index)
        {
            StringBuilder strSql = new StringBuilder();
            DateTime nowDay = DateTime.Now;
            List<int> accId = new List<int>();

            int bgNumber = ((pageIndex - 1) * 15) + 1;
            int edNumber = (pageIndex) * 15;

            switch (index)
            {
                case 1://已不活跃
                    strSql.Append("select accid from ( select accid,ROW_NUMBER() over (order by accid) as rowNumber from [Sys_I200].[dbo].[SysRpt_ShopActive]  " +
                          " where DATEDIFF(DAY,startTime,@now)=0 and active<5 and accid in(");
                    strSql.Append(" select accid from [Sys_I200].[dbo].[SysRpt_ShopActive] " +
                          " where DATEDIFF(DAY,updatetime,@before)=0 and (active=5 or active=7))) re ;");
                    accId = DapperHelper.Query<int>(strSql.ToString(), new
                    {
                        now = nowDay, 
                        before = nowDay.AddDays(-1)
                    }).ToList();
                    break;
                case 3://新增活跃
                    strSql.Append("select accid from ( select accid,ROW_NUMBER() over (order by accid) as rowNumber from [Sys_I200].[dbo].[SysRpt_ShopActive]  " +
                          " where DATEDIFF(DAY,startTime,@now)=0 and active=5 and accid in(");
                    strSql.Append(" select accid from [Sys_I200].[dbo].[SysRpt_ShopActive] " +
                                  " where DATEDIFF(DAY,updatetime,@before)=0 and active<5)) re ;");
                    accId = DapperHelper.Query<int>(strSql.ToString(), new
                    {
                        now = nowDay,
                        before = nowDay.AddDays(-1)
                    }).ToList();
                    break;
                case 2://持续活跃
                    strSql.Append("select accid from (select accid,ROW_NUMBER() over (order by accid) as rowNumber from [Sys_I200].[dbo].[SysRpt_ShopActive] " +
                          "where stateVal=0 and DATEDIFF(DAY,startTime,@date)>0 and active in (5,7)) re;");
                    accId = DapperHelper.Query<int>(strSql.ToString(), new
                    {
                        date = nowDay
                    }).ToList();
                    break;
            }

            return accId.ToArray();

        }

        /// <summary>
        /// 获取当日不同状态用户ID数组的通用方法
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="type"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// 分页信息移动到详细信息处理
        public int[] GetGeneralAccountId(int pageIndex, string type, int index)
        {
            DateTime nowDay = DateTime.Now;
            string active = "";

            int bgNumber = ((pageIndex - 1) * 15) + 1;
            int edNumber = (pageIndex) * 15;

            switch (index)
            {
                case 1://活跃
                    active = ">=5";
                    break;
                case 2://流失
                    active = "<0";
                    break;
                case 3://新增
                    active = "in (0,1)";
                    break;
            }

            return GetNewUsrDetailData(bgNumber, edNumber,nowDay, type, index, active);
            ;
        }

        /// <summary>
        /// 获取当日活跃用户的状态
        /// （已不活跃/新增活跃/持续活跃）
        /// </summary>
        /// <param name="nowDay"></param>
        /// <returns></returns>
        public DashBoardModel GetActiveUsrProp(DateTime nowDay)
        {
            DashBoardModel dashBoardData = new DashBoardModel();
            dashBoardData.Data.Add("NotActive", 0);
            dashBoardData.Data.Add("KeepActive", 0);
            dashBoardData.Data.Add("NewActive", 0);

            dashBoardData.Name = "活跃用户";

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select COUNT(distinct accid) from [Sys_I200].[dbo].SysRpt_ShopActive where stateVal=0 and active in (5,7);");
            //获取活忠用户总数
            dashBoardData.Total = DapperHelper.ExecuteScalar<int>(strSql.ToString());

            #region 获取已不活跃用户数
            strSql.Clear();

            strSql.Append(" select count(distinct accid) as cnt from [Sys_I200].[dbo].[SysRpt_ShopActive]  " +
                          " where DATEDIFF(DAY,startTime,@now)=0 and active<5 and accid in(");
            strSql.Append(" select accid from [Sys_I200].[dbo].[SysRpt_ShopActive] " +
                          " where DATEDIFF(DAY,updatetime,@before)=0 and active in (5,7))");

            dashBoardData.Data["NotActive"] = DapperHelper.ExecuteScalar<int>(strSql.ToString(), new { now = nowDay, before = nowDay.AddDays(-1) });
            #endregion

            #region 获取新增活跃用户数
            strSql.Clear();

            strSql.Append(" select count(distinct accid) as cnt from [Sys_I200].[dbo].[SysRpt_ShopActive]  " +
                          " where DATEDIFF(DAY,startTime,@now)=0 and active=5 and accid in(");
            strSql.Append(" select accid from [Sys_I200].[dbo].[SysRpt_ShopActive] " +
                          " where DATEDIFF(DAY,updatetime,@before)=0 and active<5)");

            dashBoardData.Data["NewActive"] = DapperHelper.ExecuteScalar<int>(strSql.ToString(), new { now = nowDay, before = nowDay.AddDays(-1) });
            #endregion

            #region 获取持续活跃用户数
            strSql.Clear();

            strSql.Append("select count(distinct accid) as cnt from [Sys_I200].[dbo].[SysRpt_ShopActive] " +
                          "where stateVal=0 and DATEDIFF(DAY,startTime,@date)>0 and active in (5,7)");
            dashBoardData.Data["KeepActive"] = DapperHelper.ExecuteScalar<int>(strSql.ToString(), new { date = nowDay });
            #endregion  

            return dashBoardData;
        }

        /// <summary>
        /// 获取新用户的使用数据
        /// （新用户产生的会员、商品、销售等数据）
        /// </summary>
        /// <param name="nowDay"></param>
        /// <returns></returns>
        public DashBoardModel GetNewUsrBehave(DateTime nowDay)
        {
            DashBoardModel dashBoardData = new DashBoardModel();
            dashBoardData.Data.Add("userNum", 0);
            dashBoardData.Data.Add("goodsNum", 0);
            dashBoardData.Data.Add("saleNum", 0);
            dashBoardData.Data.Add("orderNum", 0);
            dashBoardData.Data.Add("smsNum", 0);

            dashBoardData.Name = "新用户";

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select COUNT(id) as accountid from i200.dbo.T_Account where state=1 and DATEDIFF(day,RegTime,getdate())=0;");
            //获取新用户总数）
            dashBoardData.Total = DapperHelper.ExecuteScalar<int>(strSql.ToString());

            //获取新用户产生的数据
            //PanelShowModel.SomedayDataCount data = GetNewUsrData(nowDay, "in (0,1)");

            //dashBoardData.Data["userNum"] = data.userNum;
            //dashBoardData.Data["goodsNum"] = data.goodsNum;
            //dashBoardData.Data["saleNum"] = data.saleNum;
            //dashBoardData.Data["orderNum"] = data.orderNum;
            //dashBoardData.Data["smsNum"] = data.smsNum;

            return dashBoardData;
        }

        /// <summary>
        /// 获取新用户的使用数据
        /// （新用户产生的会员、商品、销售等数据）
        /// </summary>
        /// <param name="nowDay"></param>
        /// <returns></returns>
        public DashBoardModel GetNewUsrBehaveDetail(DateTime nowDay)
        {
            DashBoardModel dashBoardData = new DashBoardModel();
            dashBoardData.Data.Add("userNum", 0);
            dashBoardData.Data.Add("goodsNum", 0);
            dashBoardData.Data.Add("saleNum", 0);
            dashBoardData.Data.Add("orderNum", 0);
            dashBoardData.Data.Add("smsNum", 0);

            dashBoardData.Name = "新用户";

            //获取新用户产生的数据
            PanelShowModel.SomedayDataCount data = GetNewUsrData(nowDay, "in (0,1)");

            dashBoardData.Data["userNum"] = data.userNum;
            dashBoardData.Data["goodsNum"] = data.goodsNum;
            dashBoardData.Data["saleNum"] = data.saleNum;
            dashBoardData.Data["orderNum"] = data.orderNum;
            dashBoardData.Data["smsNum"] = data.smsNum;

            return dashBoardData;
        }

        /// <summary>
        /// 返回当日分渠道信息注册对比
        /// </summary>
        /// <param name="dayDate"></param>
        /// <returns></returns>
        public RegUsrDiff GetUsrRegDiff(DateTime dayDate)
        {
            DateTime yesterday = dayDate.AddDays(-1).Date;
            

            RegUsrDiff model = new RegUsrDiff();
            StringBuilder strSql = new StringBuilder();

            strSql.Append("declare @YesterIOS int," +
                          "        @YesterAndroid int," +
                          "        @YesterPC int," +
                          "        @YesterWeb int," +
                          "        @YesterMobileWeb int," +
                          "        @YesterPad int," +
                          "        @IOS int," +
                          "        @Android int," +
                          "        @PC int," +
                          "        @MobileWeb int," +
                          "        @Pad int," +
                          "        @Web int;");
            strSql.Append(
                "select * into #List from i200.dbo.T_Account where state=1 and (RegTime between @yesterday and getdate());");
            strSql.Append(
                "select @YesterIOS=COUNT(id) from #List where (RegTime between @yesterday and (getdate()-1)) and Remark='10';");
            strSql.Append(
                "select @YesterAndroid=COUNT(id) from #List where (RegTime between @yesterday and (getdate()-1)) and Remark='11';");
            strSql.Append(
                "select @YesterPC=COUNT(id) from #List where (RegTime between @yesterday and (getdate()-1)) and Remark='8';");
            strSql.Append(
                "select @YesterMobileWeb=COUNT(id) from #List where (RegTime between @yesterday and (getdate()-1)) and Remark='12';");
            strSql.Append(
                "select @YesterPad=COUNT(id) from #List where (RegTime between @yesterday and (getdate()-1)) and Remark='13';");
            strSql.Append(
                "select @YesterWeb=COUNT(id) from #List where (RegTime between @yesterday and (getdate()-1)) and (Remark='0' or Remark='' or Remark is null);");
            strSql.Append(
                "select @IOS=COUNT(id) from #List where DATEDIFF(day,RegTime,getdate())=0 and Remark='10';");
            strSql.Append(
                "select @Android=COUNT(id) from #List where DATEDIFF(day,RegTime,getdate())=0 and Remark='11';");
            strSql.Append(
                "select @PC=COUNT(id) from #List where DATEDIFF(day,RegTime,getdate())=0 and Remark='8';");
            strSql.Append(
                "select @MobileWeb=COUNT(id) from #List where DATEDIFF(day,RegTime,getdate())=0 and Remark='12';");
            strSql.Append(
                "select @Pad=COUNT(id) from #List where DATEDIFF(day,RegTime,getdate())=0 and Remark='13';");
            strSql.Append(
                "select @Web=COUNT(id) from #List where DATEDIFF(day,RegTime,getdate())=0 and (Remark='0' or Remark='' or Remark is null);");

            strSql.Append(
                "select @YesterIOS YesterIOS,@YesterAndroid YesterAndroid,@YesterPC YesterPC,@YesterWeb YesterWeb,@YesterPad YesterPad, " +
                "@IOS IOS,@Android Android,@PC PC,@Web Web,@YesterMobileWeb YesterMobileWeb,@MobileWeb MobileWeb,@Pad Pad;");
            strSql.Append(
                "drop table #List;");

            model = DapperHelper.GetModel<RegUsrDiff>(strSql.ToString(), new { yesterday = yesterday });

            return model;
        }

        public LoginTypeDiff GetLoginTypeDiff(DateTime dayDate)
        {
            DateTime yesterday = dayDate.AddDays(-1).Date;
            LoginTypeDiff model=new LoginTypeDiff();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("declare @YesterPCLog int," +
                          "        @YesterWebLog int," +
                          "        @YesterIOSLog int," +
                          "        @YesterAndroidLog int," +
                          "        @YesterPadLog int," +

                          "        @WebPC int," +
                          "        @WebIOS int," +
                          "        @WebAndroid int," +
                          "        @PCAndroid int," +
                          "        @PCIOS int," +
                          "        @IOSAndroid int," +
                          "        @WebPad int," +
                          "        @IOSPad int," +
                          "        @AndroidPad int," +
                          "        @PCPad int," +

                          "        @PCLog int," +
                          "        @WebLog int," +
                          "        @IOSLog int," +
                          "        @PadLog int," +
                          "        @AndroidLog int;");

            strSql.Append("select @YesterPCLog=count(distinct Accountid) from i200.dbo.T_LOG where (cast(LogMode as nvarchar(10)) like '3%') and (OperDate between @yesterday and (getdate()-1));");
            strSql.Append("select @YesterWebLog=count(distinct Accountid) from i200.dbo.T_LOG where (LogMode=1 or LogMode=0) and (OperDate between @yesterday and (getdate()-1));");
            strSql.Append("select @YesterIOSLog=count(distinct AccId) from i200.dbo.T_Token_Api where (AppKey like 'iPh%') and (createTime between @yesterday and (getdate()-1));");
            strSql.Append("select @YesterAndroidLog=count(distinct AccId) from i200.dbo.T_Token_Api where (AppKey like 'Android%') and (createTime between @yesterday and (getdate()-1));");
            strSql.Append("select @YesterPadLog=count(distinct AccId) from i200.dbo.T_Token_Api where (AppKey like 'iPa%') and (createTime between @yesterday and (getdate()-1));");

            strSql.Append("select @WebPC=count(distinct Accountid) from i200.dbo.T_LOG where (cast(LogMode as nvarchar(10)) like '3%') and DATEDIFF(day,OperDate,getdate())=0 and Accountid in (select Accountid from i200.dbo.T_LOG where (LogMode=1 or LogMode=0) and DATEDIFF(day,OperDate,getdate())=0);");
            strSql.Append("select @PCIOS=count(distinct Accountid) from i200.dbo.T_LOG where (cast(LogMode as nvarchar(10)) like '3%') and DATEDIFF(day,OperDate,getdate())=0 and Accountid in (select AccId from i200.dbo.T_Token_Api where (AppKey like 'iPh%') and DATEDIFF(day,createTime,getdate())=0);");
            strSql.Append("select @PCPad=count(distinct Accountid) from i200.dbo.T_LOG where (cast(LogMode as nvarchar(10)) like '3%') and DATEDIFF(day,OperDate,getdate())=0 and Accountid in (select AccId from i200.dbo.T_Token_Api where (AppKey like 'iPa%') and DATEDIFF(day,createTime,getdate())=0);");

            strSql.Append("select @PCAndroid=count(distinct Accountid) from i200.dbo.T_LOG where (cast(LogMode as nvarchar(10)) like '3%') and DATEDIFF(day,OperDate,getdate())=0 and Accountid in (select AccId from i200.dbo.T_Token_Api where (AppKey like 'Android%') and DATEDIFF(day,createTime,getdate())=0);");
            strSql.Append("select @WebAndroid=count(distinct Accountid) from i200.dbo.T_LOG where (LogMode=1 or LogMode=0) and DATEDIFF(day,OperDate,getdate())=0 and Accountid in (select AccId from i200.dbo.T_Token_Api where (AppKey like 'Android%') and DATEDIFF(day,createTime,getdate())=0);");
            strSql.Append("select @WebIOS=count(distinct Accountid) from i200.dbo.T_LOG where (LogMode=1 or LogMode=0) and DATEDIFF(day,OperDate,getdate())=0 and Accountid in (select AccId from i200.dbo.T_Token_Api where (AppKey like 'iPh%') and DATEDIFF(day,createTime,getdate())=0);");
            strSql.Append("select @WebPad=count(distinct Accountid) from i200.dbo.T_LOG where (LogMode=1 or LogMode=0) and DATEDIFF(day,OperDate,getdate())=0 and Accountid in (select AccId from i200.dbo.T_Token_Api where (AppKey like 'iPa%') and DATEDIFF(day,createTime,getdate())=0);");
            strSql.Append("select @IOSAndroid=count(distinct AccId) from i200.dbo.T_Token_Api where (AppKey like 'Android%') and DATEDIFF(day,createTime,getdate())=0 and AccId in (select AccId from i200.dbo.T_Token_Api where (AppKey like 'iPh%') and DATEDIFF(day,createTime,getdate())=0);");
            strSql.Append("select @AndroidPad=count(distinct AccId) from i200.dbo.T_Token_Api where (AppKey like 'Android%') and DATEDIFF(day,createTime,getdate())=0 and AccId in (select AccId from i200.dbo.T_Token_Api where (AppKey like 'iPa%') and DATEDIFF(day,createTime,getdate())=0);");

            strSql.Append("select @IOSPad=count(distinct AccId) from i200.dbo.T_Token_Api where (AppKey like 'iPh%') and DATEDIFF(day,createTime,getdate())=0 and AccId in (select AccId from i200.dbo.T_Token_Api where (AppKey like 'iPa%') and DATEDIFF(day,createTime,getdate())=0);");

            strSql.Append("select @PCLog=count(distinct Accountid) from i200.dbo.T_LOG where (cast(LogMode as nvarchar(10)) like '3%') and DATEDIFF(day,OperDate,getdate())=0;");
            strSql.Append("select @WebLog=count(distinct Accountid) from i200.dbo.T_LOG where (LogMode=1 or LogMode=0) and DATEDIFF(day,OperDate,getdate())=0;");
            strSql.Append("select @IOSLog=count(distinct AccId) from i200.dbo.T_Token_Api where (AppKey like 'iPh%') and DATEDIFF(day,createTime,getdate())=0;");
            strSql.Append("select @PadLog=count(distinct AccId) from i200.dbo.T_Token_Api where (AppKey like 'iPa%') and DATEDIFF(day,createTime,getdate())=0;");
            strSql.Append("select @AndroidLog=count(distinct AccId) from i200.dbo.T_Token_Api where (AppKey like 'Android%') and DATEDIFF(day,createTime,getdate())=0;");

            strSql.Append(
                "select @YesterPCLog YesterPCLog,@YesterWebLog YesterWebLog,@YesterIOSLog YesterIOSLog,@YesterAndroidLog YesterAndroidLog,@YesterPadLog YesterPadLog,@PadLog PadLog," +
                "@PCLog PCLog,@WebLog WebLog,@IOSLog IOSLog,@AndroidLog AndroidLog,@WebPC WebPC,@PCIOS PCIOS,@WebIOS WebIOS,@WebAndroid WebAndroid,@PCAndroid PCAndroid,@IOSAndroid IOSAndroid,@WebPad WebPad,@IOSPad IOSPad,@AndroidPad AndroidPad,@PCPad PCPad;");

            model = DapperHelper.GetModel<LoginTypeDiff>(strSql.ToString(), new { yesterday = yesterday });

            return model;
        }

        #region 特定时间段内转化率（活跃率）数据
        /// <summary>
        /// 转化率数据提取
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public dynamic GetUsrStatusNum(DateTime stDate, DateTime edDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" declare  ");
            strSql.Append(" @regNum int , ");// 注册数
            strSql.Append(" @activeNum int , ");// 活跃数
            strSql.Append(" @payNum int; ");// 付费数

            strSql.Append(" select ID into #List from i200.dbo.T_Account where RegTime<=@edDate and RegTime>=@stDate and state=1;");

            strSql.Append(" select @regNum=count(*) from #List");//注册数
            strSql.Append(
                " select @activeNum=count(*) from T_Business b where b.active>=5 and b.accountid in (select ID from #List);");//活跃数
            strSql.Append(" select @payNum=count(distinct accid) from T_OrderInfo o where o.accId in (select ID from #List) and o.orderStatus=2;");//付费数

            strSql.Append(" select @regNum RegNum,@activeNum ActiveNum,@payNum PayNum;");

            strSql.Append(" Drop table #List;");

            return HelperForFrontend.Query<dynamic>(strSql.ToString(), new {stDate = stDate, edDate = edDate.AddDays(1)}).ToList()[0];
        }

        #endregion

        #region 获取用户状态在不同KPI中的占比
        /// <summary>
        /// 获取用户状态在不同KPI中的占比
        /// （活跃用户/流失用户/新增用户）
        /// </summary>
        /// <param name="nowDay"></param>
        /// <returns></returns>
        public List<DashBoardModel> GetKPIProp(DateTime nowDay)
        {
            List<DashBoardModel> listDashBoradList = new List<DashBoardModel>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("");

            PanelShowModel.SomedayDataCount activeUsr = GetNewUsrData(nowDay, ">=5");
            PanelShowModel.SomedayDataCount newUsr = GetNewUsrData(nowDay, "in (0,1)");
            PanelShowModel.SomedayDataCount lostUsr = GetNewUsrData(nowDay, "<0");

            SysRpt_WebDayInfoDAL crossData = new SysRpt_WebDayInfoDAL();
            PanelShowModel.SomedayDataCount totalData = crossData.GetNowData();

            foreach (string keyName in pairValue.Keys)
            {
                DashBoardModel tmp = new DashBoardModel();
                if (keyName != "付费用户")
                {
                    tmp.Name = keyName;
                    tmp.Total = totalData[pairValue[keyName]];

                    tmp.Data.Add("ActiveUsr", activeUsr[pairValue[keyName]]);
                    tmp.Data.Add("LostUsr", lostUsr[pairValue[keyName]]);
                    tmp.Data.Add("NewUsr", newUsr[pairValue[keyName]]);
                }
                else
                {
                    dynamic paidUsrData = GetPaidUsrData();
                    tmp.Name = keyName;
                    tmp.Total = paidUsrData.paidUsr;

                    tmp.Data.Add("ActiveUsr", paidUsrData.activeUsr);
                    tmp.Data.Add("LostUsr", paidUsrData.lostUsr);
                    tmp.Data.Add("NewUsr", paidUsrData.newUsr);
                } 
               
                listDashBoradList.Add(tmp);
            }

            return listDashBoradList;
        }

        #region 重复方法
        ///// <summary>
        ///// 获取会员数中不同比例用户的贡献数
        ///// （活跃/流失/新增）
        ///// </summary>
        ///// <param name="nowDay"></param>
        ///// <returns></returns>
        //public DashBoardModel GetGoodsProp(DateTime nowDay)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("");
        //}

        ///// <summary>
        ///// 获取会员数中不同比例用户的贡献数
        ///// （活跃/流失/新增）
        ///// </summary>
        ///// <param name="nowDay"></param>
        ///// <returns></returns>
        //public DashBoardModel GetSmsProp(DateTime nowDay)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("");
        //}

        ///// <summary>
        ///// 获取会员数中不同比例用户的贡献数
        ///// （活跃/流失/新增）
        ///// </summary>
        ///// <param name="nowDay"></param>
        ///// <returns></returns>
        //public DashBoardModel GetOrderProp(DateTime nowDay)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("");
        //}

        ///// <summary>
        ///// 获取会员数中不同比例用户的贡献数
        ///// （活跃/流失/新增）
        ///// </summary>
        ///// <param name="nowDay"></param>
        ///// <returns></returns>
        //public DashBoardModel GetUsrAddProp(DateTime nowDay)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("");
        //}

        #endregion

        /// <summary>
        /// 获取付费用户中不同用户状态的占比
        /// （活跃/流失/新增）
        /// </summary>
        /// <param name="nowDay"></param>
        /// <returns></returns>
        //public DashBoardModel GetPaidProp(DateTime nowDay)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("");
        //}
        #endregion

        #region Helper Function
        /// <summary>
        /// 获取不同状态用户当天产生的数据
        /// </summary>
        /// <param name="nowDay"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        public PanelShowModel.SomedayDataCount GetNewUsrData(DateTime nowDay,string active)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" declare  ");
            //strSql.Append(" @regNum int , ");// 注册数
            strSql.Append(" @saleNum int , ");// 销售数
            strSql.Append(" @saleMoney decimal(18,2) , ");// 销售金额
            strSql.Append(" @userNum int , ");// 会员数
            strSql.Append(" @goodsNum int , ");// 商品数
            strSql.Append(" @smsNum int , ");// 短信数
            strSql.Append(" @orderNum int , ");// 订单数
            strSql.Append(" @orderMoney decimal(18,2); ");// 订单金额

            strSql.Append(" select accountid into #List from i200.dbo.T_Business where active " + active + ";");
  
            strSql.Append(" select @saleNum=COUNT(s.saleID),@saleMoney=SUM(s.RealMoney) from  i200.dbo.T_SaleInfo s where DATEDIFF(DAY,s.insertTime,@insertTime)=0 and s.accID in(select accountid from #List) and s.RealMoney<10000 and s.RealMoney>0 ; ");

            strSql.Append(" select @userNum=COUNT(u.uid) from i200.dbo.T_UserInfo u where  DATEDIFF(day,uInsertTime,@insertTime)=0 and u.accID in(select accountid from #List); ");

            strSql.Append(" select @goodsNum=COUNT(g.gid) from i200.dbo.T_GoodsInfo g   where DATEDIFF(day,gAddTime,@insertTime)=0 and g.accID in(select accountid from #List); ");

            strSql.Append(" select @orderNum=COUNT(o.oid),@orderMoney=SUM(RealPayMoney) from i200.dbo.T_OrderInfo o  where orderStatus=2  and  o.accId in(select accountid from #List) and DATEDIFF(day,transactionDate,@insertTime)=0 ;  ");

            strSql.Append(" select @smsNum=COUNT(s.id) from i200.dbo.T_Sms_List s  where smsStatus=1  and  DATEDIFF(day,sendTime,@insertTime)=0 and s.accID in(select accountid from #List); ");

            strSql.Append("  select @insertTime nowTime, @saleNum  saleNum ,isnull(@saleMoney,0)  saleMoney ,");
            strSql.Append("  @userNum  userNum ,@goodsNum  goodsNum , @smsNum  smsNum ,@orderNum  orderNum ,isnull(@orderMoney,0)  orderMoney; ");

            strSql.Append(" Drop table #List;");
            return DapperHelper.GetModel<PanelShowModel.SomedayDataCount>(strSql.ToString(), new { insertTime = nowDay });
        }

        /// <summary>
        /// 返回首页DashBoard详情数据
        /// </summary>
        /// <param name="bgNum"></param>
        /// <param name="edNum"></param>
        /// <param name="nowDay"></param>
        /// <param name="type"></param>
        /// <param name="index"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        public int[] GetNewUsrDetailData(int bgNum,int edNum,DateTime nowDay,string type,int index, string active)
        {
            StringBuilder strSql = new StringBuilder();
            
            strSql.Append(" select accountid into #List from i200.dbo.T_Business where active " + active + ";");

            strSql.Append("create table #tempAccId(");
            strSql.Append("id int identity(1,1) primary key,");
            strSql.Append("accid int);");

            strSql.Append("insert into #tempAccId ");
            switch (type)
            {
                case "Sale":
                    strSql.Append("select distinct s.accid from i200.dbo.T_SaleInfo s " +
                                  "where DATEDIFF(DAY,s.insertTime,@insertTime)=0 and s.accID in(select accountid from #List) and s.RealMoney<10000 ; ");
                    break;
                case "Usr":
                    strSql.Append("select distinct u.accID from i200.dbo.T_UserInfo u " +
                                  "where  DATEDIFF(day,uInsertTime,@insertTime)=0 and u.accID in(select accountid from #List); ");
                    break;
                case "Goods":
                    strSql.Append("select distinct g.accID from i200.dbo.T_GoodsInfo g   " +
                                  "where DATEDIFF(day,gAddTime,@insertTime)=0 and g.accID in(select accountid from #List); ");
                    break;
                case "Sms":
                    strSql.Append("select distinct s.accID from i200.dbo.T_Sms_List s  " +
                                  "where smsStatus=1  and  DATEDIFF(day,sendTime,@insertTime)=0 and s.accID in(select accountid from #List); ");
                    break;
                case "Order":
                    strSql.Append("select distinct o.accId from i200.dbo.T_OrderInfo o  " +
                                  "where orderStatus=2  and  o.accId in(select accountid from #List) and DATEDIFF(day,transactionDate,@insertTime)=0 ;  ");
                    break;
                case "Paid":
                    strSql.Clear();
                    switch (index)
                    {
                        case 1:
                            strSql.Append("select distinct accId from (select accid from i200.dbo.T_orderinfo o " +
                                          "where o.orderStatus=2 and o.accId in(select accountid from i200.dbo.T_Business where active>=5 ) group by accId ) s;");//活跃用户
                            break;
                        case 2:
                            strSql.Append("select distinct accId from (select accid from i200.dbo.T_orderinfo o " +
                                          "where o.orderStatus=2 and o.accId in(select accountid from i200.dbo.T_Business where active<0 ) group by accId ) s;");//流失用户
                            break;
                        case 3:
                            strSql.Append("select distinct accId from (select accid from i200.dbo.T_orderinfo o " +
                                          "where o.orderStatus=2 and o.accId in(select accountid from i200.dbo.T_Business where active in (0,1) ) group by accId ) s ) group by accId ) s;");//新用户
                            break;
                    }
                    break;
            }

            strSql.Append("select accId from #tempAccId;");

            if (type != "Paid")
            {
                strSql.Append(" Drop table #List;");
            }
            strSql.Append(" Drop table #tempAccId;");
            
            return DapperHelper.Query<int>(strSql.ToString(), new { insertTime = nowDay }).ToArray();
        }

        /// <summary>
        /// 获取首页数据用户分页总数
        /// </summary>
        /// <param name="type"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public int GetDashBoardDetailUsrCount(string type, int index)
        {
            StringBuilder strSql = new StringBuilder();
            DateTime nowDay = DateTime.Now;

            string active = "";

            switch (index)
            {
                case 1://活跃
                    active = ">=5";
                    break;
                case 2://流失
                    active = "<0";
                    break;
                case 3://新增
                    active = "in (0,1)";
                    break;
            }


            strSql.Append(" select accountid into #List from i200.dbo.T_Business where active " + active + ";");

            switch (type)
            {
                case "Sale":
                    strSql.Append("select count(distinct s.accid) as cnt from  i200.dbo.T_SaleInfo s " +
                                  "where DATEDIFF(DAY,s.insertTime,@insertTime)=0 and s.accID in(select accountid from #List) and s.RealMoney<10000 ; ");
                    break;
                case "Usr":
                    strSql.Append("select count(distinct u.accID) as cnt from i200.dbo.T_UserInfo u " +
                                  "where  DATEDIFF(day,uInsertTime,@insertTime)=0 and u.accID in(select accountid from #List); ");
                    break;
                case "Goods":
                    strSql.Append("select count(distinct g.accID) as cnt from i200.dbo.T_GoodsInfo g   " +
                                  "where DATEDIFF(day,gAddTime,@insertTime)=0 and g.accID in(select accountid from #List); ");
                    break;
                case "Sms":
                    strSql.Append("select count(distinct s.accID) as cnt from i200.dbo.T_Sms_List s  " +
                                  "where smsStatus=1  and  DATEDIFF(day,sendTime,@insertTime)=0 and s.accID in(select accountid from #List); ");
                    break;
                case "Order":
                    strSql.Append("select count(distinct o.accId) as cnt from i200.dbo.T_OrderInfo o  " +
                                  "where orderStatus=2  and  o.accId in(select accountid from #List) and DATEDIFF(day,transactionDate,@insertTime)=0 ;  ");
                    break;
            }

                strSql.Append(" Drop table #List;");


            return DapperHelper.Query<int>(strSql.ToString(), new { insertTime = nowDay}).ToList()[0];

        }


        /// <summary>
        /// 获取付费用户统计数据
        /// </summary>
        /// <returns></returns>
        public dynamic GetPaidUsrData()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" declare  ");
            strSql.Append(" @paidUsr int , ");// 所有付费用户
            strSql.Append(" @activeUsr int , ");// 活跃付费用户
            strSql.Append(" @lostUsr int , ");// 流失付费用户
            strSql.Append(" @newUsr int ; ");// 新注册付费用户

            strSql.Append("select @paidUsr=COUNT(*) from (select accid from i200.dbo.T_orderinfo where ((orderStatus=2 and orderTypeId=1) or (orderStatus=1 and orderTypeId=2) or (orderStatus=2 and orderTypeId is null)) and RealPayMoney>0 group by accId ) s;");

            strSql.Append("select @lostUsr=COUNT(*) from (select accid from i200.dbo.T_orderinfo o where ((o.orderStatus=2 and o.orderTypeId=1) or (o.orderStatus=1 and o.orderTypeId=2) or (orderStatus=2 and orderTypeId is null)) and o.RealPayMoney>0 and o.accId in(select accountid from i200.dbo.T_Business where active<0 ) group by accId ) s;");//新用户
            strSql.Append("select @activeUsr=COUNT(*) from (select accid from i200.dbo.T_orderinfo o where ((o.orderStatus=2 and o.orderTypeId=1) or (o.orderStatus=1 and o.orderTypeId=2) or (orderStatus=2 and orderTypeId is null)) and o.RealPayMoney>0 and o.accId in(select accountid from i200.dbo.T_Business where active>=5 ) group by accId ) s;");//活跃用户
            strSql.Append("select @newUsr=COUNT(*) from (select accid from i200.dbo.T_orderinfo o where ((o.orderStatus=2 and o.orderTypeId=1) or (o.orderStatus=1 and o.orderTypeId=2) or (orderStatus=2 and orderTypeId is null)) and o.RealPayMoney>0 and DATEDIFF(DAY,transactionDate,GETDATE())=0 and o.accId in(select accountid from i200.dbo.T_Business where active in (0,1) ) group by accId ) s;");//新用户

            strSql.Append("select @paidUsr paidUsr,@activeUsr activeUsr,@lostUsr lostUsr,@newUsr newUsr;");

            return DapperHelper.Query<dynamic>(strSql.ToString()).ToList()[0];
        }

        
        #endregion

        /// <summary>
        /// 获取最近七天每日的活跃用户Accid列表
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public ActiveUsrList GetActiveListForVenn(DateTime stDate,DateTime edDate)
        {
            StringBuilder strSql = new StringBuilder();
            ActiveUsrList model=new ActiveUsrList();
            DailyActiveList dailyModel = new DailyActiveList();

            DateTime iter=stDate;

            while(iter<edDate)
            {
                dailyModel.DayDate = iter;
                strSql.Append("select accid from SysRpt_ShopActive where @dayDate between startTime and updatetime and active in (5,7)");

                dailyModel.AccidList = DapperHelper.Query<int>(strSql.ToString(), new { dayDate = iter }).ToList();

                model.ActiveAccids.Add(new DailyActiveList(dailyModel.DayDate,dailyModel.AccidList));
                strSql.Clear();
                iter = iter.AddDays(1);
            }

            return model;
            
        }

        /// <summary>
        /// 获取Venn数据的通用方法
        /// </summary>
        /// <param name="type"></param>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public ActiveUsrList GetUsrListForVenn(string type, DateTime stDate, DateTime edDate)
        {
            StringBuilder strSql = new StringBuilder();
            ActiveUsrList model = new ActiveUsrList();
            DailyActiveList dailyModel = new DailyActiveList();

            DateTime iter = stDate;

            if (type == "active")
            {
                while (iter < edDate)
                {
                    dailyModel.DayDate = iter;
                    strSql.Append("select accid from SysRpt_ShopActive where @dayDate between startTime and updatetime and active in (5,7)");

                    dailyModel.AccidList = DapperHelper.Query<int>(strSql.ToString(), new { dayDate = iter }).ToList();

                    model.ActiveAccids.Add(new DailyActiveList(dailyModel.DayDate, dailyModel.AccidList));
                    strSql.Clear();
                    iter = iter.AddDays(1);
                }    
            }
            else if (type == "sale")
            {
                while (iter < edDate)
                {
                    dailyModel.DayDate = iter;
                    strSql.Append("select accid from [i200].[dbo].[T_SaleInfo] where DateDiff(day,@dayDate,saleTime)=0 group by accid;");

                    dailyModel.AccidList = DapperHelper.Query<int>(strSql.ToString(), new { dayDate = iter }).ToList();

                    model.ActiveAccids.Add(new DailyActiveList(dailyModel.DayDate, dailyModel.AccidList));
                    strSql.Clear();
                    iter = iter.AddDays(1);
                }
            }
            
            return model;

            
        }

        public BehaviorSet GetSpecFunnel(int type,string mark,DateTime stDate,DateTime edDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "declare @Reg int,@Log int,@DataInput int,@SaleInput int,@SecRetention int,@Paid int,@Active int,@MarketName nvarchar(100);");

            if (type==1) //系统来源
            {
                strSql.Append(
                    "select * into #List from [Sys_I200].[dbo].[SysRpt_ShopInfo] where accountid in (select ID from [i200].[dbo].[T_Account] where remark='" +
                    mark + "' and regtime between @stDate and @edDate);");
            }
            else if(type==2)//百度系
            {
                strSql.Append(
                    "select * into #List from [Sys_I200].[dbo].[SysRpt_ShopInfo] where accountid in (select ID from [i200].[dbo].[T_Account] where (fromName='market_baidu' or fromName='market_91zs') and regtime between @stDate and @edDate);");
            }
            else
            {
                strSql.Append(
                    "select * into #List from [Sys_I200].[dbo].[SysRpt_ShopInfo] where accountid in (select ID from [i200].[dbo].[T_Account] where fromName='" +
                    mark + "' and regtime between @stDate and @edDate)");

            }

            strSql.Append("select @Reg=COUNT(*) from #List;");
            strSql.Append("select @Log=COUNT(*) from #List where allLoginNum>0;");
            strSql.Append("select @DataInput=COUNT(*) from #List where (userNum+smsNum+goodsNum+moodNum+saleNum+orderNum+outlayNum)>0;");
            strSql.Append("select @SaleInput=COUNT(*) from #List where saleNum>0;");
            strSql.Append("select @SecRetention=COUNT(distinct l.accountid) from #List l left join [i200].[dbo].T_LOG on l.accountid=T_LOG.Accountid where DATEDIFF(day,regTime,OperDate)=1;");
            strSql.Append("select @Paid=COUNT(*) from #List where orderMoney>0;");
            strSql.Append("select @Active=COUNT(*) from #List where active in (5,7);");

            strSql.Append("select @Reg Reg,@Log Login,@DataInput DataInput,@SaleInput SaleInput,@SecRetention SecRetention,@Paid Paid,@Active Active;");
            strSql.Append("drop table #List;");

            return DapperHelper.GetModel<BehaviorSet>(strSql.ToString(), new {stDate = stDate, edDate = edDate});

        }

        public BehaviorSet GetSpecFunnelEx(string activeList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "declare @Reg int,@Log int,@DataInput int,@SaleInput int,@SecRetention int,@Paid int,@Active int,@MarketName nvarchar(100);");


                strSql.Append(
                    "select * into #List from [Sys_I200].[dbo].[SysRpt_ShopInfo] where accountid in (select ID from [i200].[dbo].[T_Account] where ID in (" +
                    activeList + "))");
            
            

            strSql.Append("select @Reg=COUNT(*) from #List;");
            strSql.Append("select @Log=COUNT(*) from #List where allLoginNum>0;");
            strSql.Append("select @DataInput=COUNT(*) from #List where (userNum+smsNum+goodsNum+moodNum+saleNum+orderNum+outlayNum)>0;");
            strSql.Append("select @SaleInput=COUNT(*) from #List where saleNum>0;");
            strSql.Append("select @SecRetention=COUNT(distinct l.accountid) from #List l left join [i200].[dbo].T_LOG on l.accountid=T_LOG.Accountid where DATEDIFF(day,regTime,OperDate)=1;");
            strSql.Append("select @Paid=COUNT(*) from #List where orderMoney>0;");
            strSql.Append("select @Active=COUNT(*) from #List where active in (5,7);");

            strSql.Append("select @Reg Reg,@Log Login,@DataInput DataInput,@SaleInput SaleInput,@SecRetention SecRetention,@Paid Paid,@Active Active;");
            strSql.Append("drop table #List;");

            return DapperHelper.GetModel<BehaviorSet>(strSql.ToString());
        }

    }
}
