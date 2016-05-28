using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Controls.Operation
{
    /// <summary>
    /// 运营数据
    /// </summary>
    public static class Operation
    {

        //Operation
        /// <summary>
        /// 平台趋势
        /// </summary>
        public static chartDataModel Tendency(DateTime startTime, DateTime endTime, string[] columns)
        {
            Dictionary<string, string> ColumnList = new Dictionary<string, string>();

            List<DapperWhere> dappWhere = new List<DapperWhere>();

            dappWhere.Add(new DapperWhere("startTime", startTime, " S_Date>=@startTime"));

            dappWhere.Add(new DapperWhere("endTime", endTime, " S_Date<=@endTime"));

            StringBuilder strColumn = new StringBuilder();

            #region  主键
            strColumn.Append("S_Date dayDate");

            strColumn.Append(",isnull(loginNum,0) loginNum");
            ColumnList.Add("loginNum", "登录用户");
            strColumn.Append(",(isnull(activeNum,0)+isnull(faithfulNum,0)) activeNum");
            ColumnList.Add("activeNum", "活跃用户");
            //strColumn.Append(",isnull(faithfulNum,0) faithfulNum");
            //ColumnList.Add("faithfulNum", "忠诚用户");

            if (columns.Contains("shop"))
            {
                strColumn.Append(",isnull(NewAccNum,0) NewAccNum");
                ColumnList.Add("NewAccNum", "店铺");
            }

            if (columns.Contains("reg"))
            {
                strColumn.Append(",isnull(userNum,0) userNum");
                ColumnList.Add("userNum", "会员");
            }
            if (columns.Contains("sell"))
            {
                strColumn.Append(",isnull(saleNum,0) saleNum");
                ColumnList.Add("saleNum", "销售");
            }
            if (columns.Contains("sms"))
            {
                strColumn.Append(",isnull(smsNum,0) smsNum");
                ColumnList.Add("smsNum", "短信");
            }
            if (columns.Contains("order"))
            {
                strColumn.Append(",isnull(orderMoney,0) orderMoney ");
                ColumnList.Add("orderMoney", "订单");
            }
            if (columns.Contains("pay"))
            {
                strColumn.Append(",isnull(outlayNum,0) outlayNum ");
                ColumnList.Add("outlayNum", "支出");
            }
            if (columns.Contains("client"))
            {
                strColumn.Append(",isnull(clientLogNum,0) clientLogNum ");
                ColumnList.Add("clientLogNum", "客户端");
            }
            if (columns.Contains("mood"))
            {
                strColumn.Append(",isnull(moodNum,0) moodNum");
                ColumnList.Add("moodNum", "心情");
            }
            if (columns.Contains("zhaixian"))
            {
                strColumn.Append(",isnull(acc_Rep,0)/isnull(loginNum,1) acc_Rep");
                ColumnList.Add("acc_Rep", "平均在线时长");
            }
            if (columns.Contains("sale"))
            {
                strColumn.Append(",isnull(saleMoney,0) saleMoney");
                ColumnList.Add("saleMoney", "销售额");
            }
            if (columns.Contains("goods"))
            {
                strColumn.Append(",isnull(addGoodsNum,0) addGoodsNum");
                ColumnList.Add("addGoodsNum", "商品");
            }
            #endregion

            List<dynamic> dataList = BLL.Base.SysRpt_WebDayInfoBaseBLL.GetList<dynamic>(0, strColumn.ToString(), dappWhere, " id desc");

            chartDataModel chartModel = new chartDataModel();
            return chartModel.SetDataAboutDataTime(startTime, endTime, dataList, ColumnList, "平台趋势");
        }

        /// <summary>
        /// 获取每日登录用户数和活跃用户数
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        //public static List<TrendAvgUsrModel> GetTrendAvgUsr(DateTime startTime, DateTime endTime)
        //{
        //    List<TrendAvgUsrModel> modelList = new List<TrendAvgUsrModel>();

        //    Dictionary<string, string> ColumnList = new Dictionary<string, string>();
        //    List<DapperWhere> dappWhere = new List<DapperWhere>();

        //    dappWhere.Add(new DapperWhere("startTime", startTime, " S_Date>=@startTime"));
        //    dappWhere.Add(new DapperWhere("endTime", endTime, " S_Date<=@endTime"));

        //    StringBuilder strColumn = new StringBuilder();
        //    strColumn.Append("S_Date dayDate");
        //    strColumn.Append(",isnull(loginNum,0) loginNum");

        //    List<dynamic> dataList = BLL.Base.SysRpt_WebDayInfoBaseBLL.GetList<dynamic>(0, strColumn.ToString(), dappWhere, " id desc");
        //    foreach (dynamic day in dataList)
        //    {
        //        modelList.Add(new TrendAvgUsrModel(day.dayDate, day.loginNum, 0));
        //    }



        //    return chartModel.SetDataAboutDataTime(startTime, endTime, dataList, ColumnList, "平台趋势"); 

        //}

        /// <summary>
        /// 数据对比
        /// </summary>
        /// <param name="timeType"></param>
        /// <param name="startMonth"></param>
        /// <param name="endMonth"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public static chartDataModel MonthComparison(int timeType, string startMonth, string endMonth, string dataType)
        {
            DateTime startTime = DateTime.Now.AddMonths(-3);
            DateTime endTime = DateTime.Now;
            if (timeType != 0)
            {
                startTime = DateTime.Now.AddMonths(0 - timeType);
            }
            else
            {
                startTime = Convert.ToDateTime(startMonth + "-1");
                endTime = Convert.ToDateTime(endMonth + "-1");
            }

            startTime = new DateTime(startTime.Year, startTime.Month, 1);
            endTime = new DateTime(endTime.Year, endTime.Month, 1);

            List<DapperWhere> allWhere = new List<DapperWhere>();
            List<DapperWhere> regWhere = new List<DapperWhere>();
            StringBuilder allColumn = new StringBuilder();
            StringBuilder regColumn = new StringBuilder();
            string captionTitle = "每月对比";
            allWhere.Add(new DapperWhere("statTime", startTime, "DATEDIFF(MONTH,@statTime,S_Date)>=0"));
            allWhere.Add(new DapperWhere("endTime", endTime, "DATEDIFF(month,S_Date,@endTime)>=0  group by left(CONVERT(varchar(100),S_Date, 23),7)"));

            regWhere.Add(new DapperWhere("statTime", startTime, "DATEDIFF(MONTH,@statTime,dayDate)>=0"));
            regWhere.Add(new DapperWhere("endTime", endTime, "DATEDIFF(month,dayDate,@endTime)>=0  and DATEDIFF(month,regTime,daydate)=0  group by left(CONVERT(varchar(100),dayDate, 23),7)"));




            allColumn.Append("left(CONVERT(varchar(100),S_Date, 23),7) dttime,");
            regColumn.Append("left(CONVERT(varchar(100),dayDate, 23),7) dttime,");
            #region 得到KEY
            if (dataType == "shop")
            {
                allColumn.Append("SUM(NewAccNum) cloumnNum,SUM(case when DAY(S_Date)<=DAY(getdate()) then NewAccNum else 0 end) cloumnNumDay,");
                regColumn.Append("0 cloumnNum,0 cloumnNumDay,");
                captionTitle = "注册店铺" + captionTitle;
            }
            else if (dataType == "reg")
            {
                allColumn.Append("SUM(userNum) cloumnNum,SUM(case when DAY(S_Date)<=DAY(getdate()) then userNum else 0 end) cloumnNumDay,");
                regColumn.Append("SUM(userNum) cloumnNum,SUM(case when DAY(dayDate)<=DAY(getdate()) then userNum else 0 end) cloumnNumDay,");
                captionTitle = "新增会员" + captionTitle;
            }
            else if (dataType == "sell")
            {
                allColumn.Append("SUM(saleNum) cloumnNum,SUM(case when DAY(S_Date)<=DAY(getdate()) then saleNum else 0 end) cloumnNumDay,");
                regColumn.Append("SUM(saleNum) cloumnNum,SUM(case when DAY(dayDate)<=DAY(getdate()) then saleNum else 0 end) cloumnNumDay,");
                captionTitle = "销售比笔" + captionTitle;
            }
            else if (dataType == "sms")
            {
                allColumn.Append("SUM(smsNum) cloumnNum,SUM(case when DAY(S_Date)<=DAY(getdate()) then smsNum else 0 end) cloumnNumDay,");
                regColumn.Append("SUM(smsNum) cloumnNum,SUM(case when DAY(dayDate)<=DAY(getdate()) then smsNum else 0 end) cloumnNumDay,");
                captionTitle = "发送短信" + captionTitle;
            }
            else if (dataType == "order")
            {
                allColumn.Append("SUM(orderMoney) cloumnNum,SUM(case when DAY(S_Date)<=DAY(getdate()) then orderMoney else 0 end) cloumnNumDay,");
                regColumn.Append("SUM(orderMoney) cloumnNum,SUM(case when DAY(dayDate)<=DAY(getdate()) then orderMoney else 0 end) cloumnNumDay,");
                captionTitle = "订单" + captionTitle;
            }
            else if (dataType == "pay")
            {
                allColumn.Append("SUM(outlayNum) cloumnNum ,SUM(case when DAY(S_Date)<=DAY(getdate()) then outlayNum else 0 end) cloumnNumDay,");
                regColumn.Append("SUM(outlayNum) cloumnNum ,SUM(case when DAY(dayDate)<=DAY(getdate()) then outlayNum else 0 end) cloumnNumDay,");
                captionTitle = "新增支出" + captionTitle;
            }
            else if (dataType == "goods")
            {
                allColumn.Append("SUM(addGoodsNum) cloumnNum ,SUM(case when DAY(S_Date)<=DAY(getdate()) then addGoodsNum else 0 end) cloumnNumDay,");
                regColumn.Append("SUM(goodsNum) cloumnNum ,SUM(case when DAY(dayDate)<=DAY(getdate()) then goodsNum else 0 end) cloumnNumDay,");
                captionTitle = "新增商品" + captionTitle;
            }
            #endregion

            List<dynamic> allList = BLL.Base.SysRpt_WebDayInfoBaseBLL.GetList<dynamic>(0, allColumn.ToString().Trim(','), allWhere, " dttime ");

            List<dynamic> regList = BLL.Base.SysRpt_ShopDayInfoBaseBLL.GetList<dynamic>(0, regColumn.ToString().Trim(','), regWhere, " dttime ");

            chartDataModel chartModel = new chartDataModel();
            chartModel.captionTitle = captionTitle;

            DateTime forDateTime = new DateTime(startTime.Year, startTime.Month, 1);
            string dayDateStr = DateTime.Now.ToString("yyyy-MM");
            while (forDateTime <= endTime)
            {
                #region 得到所有X轴
                charDataList charItemList = new charDataList();
                charItemList.XLable = forDateTime.ToString("yyyy-MM");
                charItemList.weekend = (int)forDateTime.DayOfWeek;

                charItemList.ItemList["all"] = new charDataItemList(0)
                {
                    series = "旧用户"
                };
                charItemList.ItemList["reg"] = new charDataItemList(0)
                {
                    series = "新用户"
                };
                if (dayDateStr == charItemList.XLable)
                {
                    charItemList.thisMonth = 1;
                }
                chartModel.DataList[charItemList.XLable] = charItemList;
                #endregion
                forDateTime = forDateTime.AddMonths(1);
            }
            foreach (dynamic item in allList)
            {
                string xLable = item.dttime.ToString();
                Dictionary<string, charDataItemList> dataitemList = chartModel.DataList[xLable].ItemList;
                dataitemList["all"].Values = item.cloumnNum;
            }

            foreach (dynamic item in regList)
            {
                string xLable = item.dttime.ToString();
                Dictionary<string, charDataItemList> dataitemList = chartModel.DataList[xLable].ItemList;
                dataitemList["reg"].Values = item.cloumnNum;
                dataitemList["all"].Values = Convert.ToDecimal(dataitemList["all"].Values) - Convert.ToDecimal(item.cloumnNum);

            }
            return chartModel;
        }


        /// <summary>
        /// 订单数据对比（PC、Android、IOS）
        /// </summary>
        /// <param name="timeType"></param>
        /// <param name="startMonth"></param>
        /// <param name="endMonth"></param>
        /// <param name="sourceType"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public static chartDataModel MonthOrderComparison(int timeType, string startMonth, string endMonth, string sourceType, string dataType)
        {
            DateTime startTime;
            var sourceTypes = sourceType.Split(',');
            var endTime = DateTime.Now;
            if (timeType != 0)
            {
                startTime = DateTime.Now.AddMonths(0 - timeType);
            }
            else
            {
                startTime = Convert.ToDateTime(startMonth + "-1");
                endTime = Convert.ToDateTime(endMonth + "-1");
            }
            startTime = new DateTime(startTime.Year, startTime.Month, 1);
            endTime = new DateTime(endTime.Year, endTime.Month, 1);
            List<DapperWhere> allWhere = new List<DapperWhere>();
            StringBuilder allColumn = new StringBuilder();
            var captionTitle = "订单每月对比";

            allWhere.Add(new DapperWhere("statTime", startTime, "createDate>=@statTime"));
            allWhere.Add(new DapperWhere("endTime", endTime, "createDate<=@endTime and ((orderStatus=1 and OrderTypeId=2) or orderStatus=2)  group by left(CONVERT(varchar(100),createDate, 23),7)"));
            allColumn.Append("left(CONVERT(varchar(100),createDate, 23),7) dttime,");
            allColumn.Append("count(accId) orderNum,");
            allColumn.Append("sum(case when oFlag = 1 then 1 else 0 end) iosNum, ");
            allColumn.Append("sum(case when oFlag = 0 then 1 else 0 end) pcNum, ");
            allColumn.Append("sum(case when oFlag = 2 then 1 else 0 end) androidNum ");
            var  orderList = BLL.Base.SysRpt_ShopDayInfoBaseBLL.GetOrderList<dynamic>(0, allColumn.ToString().Trim(','), allWhere, " dttime ");
            var chartModel = new chartDataModel { captionTitle = captionTitle };
            var forDateTime = new DateTime(startTime.Year, startTime.Month, 1);
            var dayDateStr = DateTime.Now.ToString("yyyy-MM");
            while (forDateTime <= endTime)
            {
                #region 得到所有X轴
                charDataList charItemList = new charDataList();
                charItemList.XLable = forDateTime.ToString("yyyy-MM");
                charItemList.weekend = (int)forDateTime.DayOfWeek;

                charItemList.ItemList["all"] = new charDataItemList(0)
                {
                    series = "网站"
                };
                charItemList.ItemList["iosNum"] = new charDataItemList(0)
                {
                    series = "iPhone"
                };
                charItemList.ItemList["androidNum"] = new charDataItemList(0)
                {
                    series = "Android"
                };
                if (dayDateStr == charItemList.XLable)
                {
                    charItemList.thisMonth = 1;
                }
                chartModel.DataList[charItemList.XLable] = charItemList;
                #endregion
                forDateTime = forDateTime.AddMonths(1);
            }

            foreach (dynamic item in orderList)
            {
                string xLable = item.dttime.ToString();
                Dictionary<string, charDataItemList> dataitemList = chartModel.DataList[xLable].ItemList;
                dataitemList["all"].Values = (Convert.ToDecimal(item.orderNum) - Convert.ToDecimal(item.iosNum) - Convert.ToDecimal(item.androidNum));
                dataitemList["iosNum"].Values = Convert.ToDecimal(item.iosNum);
                dataitemList["androidNum"].Values = Convert.ToDecimal(item.androidNum);
            }
            return chartModel;
        }


        /// <summary>
        /// 会员、销售、商品、短信、订单、支出分端数据对比
        /// </summary>
        /// <param name="timeType"></param>
        /// <param name="startMonth"></param>
        /// <param name="endMonth"></param>
        /// <param name="sourceType"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public static chartDataModel MonthSectionComparison(int timeType, string startMonth, string endMonth, string sourceType, string dataType)
        {
            DateTime startTime;
            var sourceTypes = sourceType.Split(',');
            var endTime = DateTime.Now;
            if (timeType != 0)
            {
                startTime = DateTime.Now.AddMonths(0 - timeType);
            }
            else
            {
                startTime = Convert.ToDateTime(startMonth + "-1");
                endTime = Convert.ToDateTime(endMonth + "-1");
            }
            startTime = new DateTime(startTime.Year, startTime.Month, 1);
            endTime = new DateTime(endTime.Year, endTime.Month, 1);
            List<DapperWhere> allWhere = new List<DapperWhere>();
            StringBuilder allColumn = new StringBuilder();
            var captionTitle = string.Empty;
            var dataList=  new List<dynamic>();

            if (dataType=="reg")
            {
                #region 会员分端数据
                captionTitle = "会员每月分端对比";
                allWhere.Add(new DapperWhere("statTime", startTime, "uRegTime>=@statTime"));
                allWhere.Add(new DapperWhere("endTime", endTime, "uRegTime<=@endTime  group by left(CONVERT(varchar(100),uRegTime, 23),7)"));
                allColumn.Append("left(CONVERT(varchar(100),uRegTime, 23),7) dttime,");
                allColumn.Append("count(DISTINCT uid) allNum,");
                allColumn.Append("sum(case when uFlag = 1 then 1 else 0 end) iosNum, ");
                allColumn.Append("sum(case when uFlag = 0 then 1 else 0 end) pcNum, ");
                allColumn.Append("sum(case when uFlag = 2 then 1 else 0 end) androidNum ");
                dataList = BLL.Base.SysRpt_ShopDayInfoBaseBLL.GetMonthSectionList<dynamic>(0, allColumn.ToString().Trim(','), allWhere, " dttime ", dataType);
                #endregion
            }
            else if (dataType=="sell")
            {
                #region 销售分端数据
                captionTitle = "销售每月分端对比";
                allWhere.Add(new DapperWhere("statTime", startTime, "saleTime>=@statTime"));
                allWhere.Add(new DapperWhere("endTime", endTime, "saleTime<=@endTime  group by left(CONVERT(varchar(100),saleTime, 23),7)"));
                allColumn.Append("left(CONVERT(varchar(100),saleTime, 23),7) dttime,");
                allColumn.Append("count(saleID) allNum,");
                allColumn.Append("sum(case when saleFlag = 1 then 1 else 0 end) iosNum, ");
                allColumn.Append("sum(case when saleFlag = 0 then 1 else 0 end) pcNum, ");
                allColumn.Append("sum(case when saleFlag = 2 then 1 else 0 end) androidNum ");
                dataList = BLL.Base.SysRpt_ShopDayInfoBaseBLL.GetMonthSectionList<dynamic>(0, allColumn.ToString().Trim(','), allWhere, " dttime ", dataType);

                #endregion    
            }

            else if (dataType=="goods")
            {
                #region 商品分端数据
                captionTitle = "商品每月分端对比";
                allWhere.Add(new DapperWhere("statTime", startTime, "gAddTime>=@statTime"));
                allWhere.Add(new DapperWhere("endTime", endTime, "gAddTime<=@endTime  group by left(CONVERT(varchar(100),gAddTime, 23),7)"));
                allColumn.Append("left(CONVERT(varchar(100),gAddTime, 23),7) dttime,");
                allColumn.Append("count(gid) allNum,");
                allColumn.Append("sum(case when gflag = 1 then 1 else 0 end) iosNum, ");
                allColumn.Append("sum(case when gflag = 0 then 1 else 0 end) pcNum, ");
                allColumn.Append("sum(case when gflag = 2 then 1 else 0 end) androidNum ");
                dataList = BLL.Base.SysRpt_ShopDayInfoBaseBLL.GetMonthSectionList<dynamic>(0, allColumn.ToString().Trim(','), allWhere, " dttime ", dataType);

                #endregion
            }
            else if (dataType == "order")
            {
                #region 订单分端数据

                captionTitle = "订单每月分端对比";
                allWhere.Add(new DapperWhere("statTime", startTime, "createDate>=@statTime"));
                allWhere.Add(new DapperWhere("endTime", endTime, "createDate<=@endTime and ((orderStatus=1 and OrderTypeId=2) or orderStatus=2)  group by left(CONVERT(varchar(100),createDate, 23),7)"));
                allColumn.Append("left(CONVERT(varchar(100),createDate, 23),7) dttime,");
                allColumn.Append("count(accId) allNum,");
                allColumn.Append("sum(case when oFlag = 1 then 1 else 0 end) iosNum, ");
                allColumn.Append("sum(case when oFlag = 0 then 1 else 0 end) pcNum, ");
                allColumn.Append("sum(case when oFlag = 2 then 1 else 0 end) androidNum ");
                dataList = BLL.Base.SysRpt_ShopDayInfoBaseBLL.GetMonthSectionList<dynamic>(0, allColumn.ToString().Trim(','), allWhere, " dttime ", dataType);

                #endregion
            }
            else if (dataType == "pay")
            {
                #region 支出分端数据
                captionTitle = "支出每月分端对比";
                allWhere.Add(new DapperWhere("statTime", startTime, "PayDate>=@statTime"));
                allWhere.Add(new DapperWhere("endTime", endTime, "PayDate<=@endTime  group by left(CONVERT(varchar(100),PayDate, 23),7)"));
                allColumn.Append("left(CONVERT(varchar(100),PayDate, 23),7) dttime,");
                allColumn.Append("count(ID) allNum,");
                allColumn.Append("sum(case when pFlag = 1 then 1 else 0 end) iosNum, ");
                allColumn.Append("sum(case when pFlag = 0 then 1 else 0 end) pcNum, ");
                allColumn.Append("sum(case when pFlag = 2 then 1 else 0 end) androidNum ");
                dataList = BLL.Base.SysRpt_ShopDayInfoBaseBLL.GetMonthSectionList<dynamic>(0, allColumn.ToString().Trim(','), allWhere, " dttime ", dataType);

                #endregion
            }

            var chartModel = new chartDataModel { captionTitle = captionTitle };
            var forDateTime = new DateTime(startTime.Year, startTime.Month, 1);
            var dayDateStr = DateTime.Now.ToString("yyyy-MM");
            while (forDateTime <= endTime)
            {
                #region 得到所有X轴
                charDataList charItemList = new charDataList();
                charItemList.XLable = forDateTime.ToString("yyyy-MM");
                charItemList.weekend = (int)forDateTime.DayOfWeek;

                charItemList.ItemList["pcNum"] = new charDataItemList(0)
                {
                    series = "网站"
                };
                charItemList.ItemList["iosNum"] = new charDataItemList(0)
                {
                    series = "iPhone"
                };
                charItemList.ItemList["androidNum"] = new charDataItemList(0)
                {
                    series = "Android"
                };
                if (dayDateStr == charItemList.XLable)
                {
                    charItemList.thisMonth = 1;
                }
                chartModel.DataList[charItemList.XLable] = charItemList;
                #endregion
                forDateTime = forDateTime.AddMonths(1);
            }

            foreach (dynamic item in dataList)
            {
                string xLable = item.dttime.ToString();
                Dictionary<string, charDataItemList> dataitemList = chartModel.DataList[xLable].ItemList;
                dataitemList["pcNum"].Values = Convert.ToDecimal(item.allNum)- Convert.ToDecimal(item.iosNum)-Convert.ToDecimal(item.androidNum);
                dataitemList["iosNum"].Values = Convert.ToDecimal(item.iosNum);
                dataitemList["androidNum"].Values = Convert.ToDecimal(item.androidNum);
            }
            return chartModel;
        }


        /// <summary>
        /// 获取数据对比摘要信息
        /// </summary>
        /// <param name="timeType"></param>
        /// <param name="startMonth"></param>
        /// <param name="endMonth"></param>
        /// <returns></returns>
        public static List<DataCompareModel> GetDataSummary(int timeType, string startMonth, string endMonth, string dataType)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strHtml = new StringBuilder();

            List<DataCompareModel> modelList = new List<DataCompareModel>();

            DateTime nowStartTime = DateTime.Now.AddMonths(-3);
            DateTime nowEndTime = DateTime.Now;

            DateTime startTime = new DateTime();
            DateTime endTime = new DateTime();

            if (timeType != 0)
            {
                startTime = DateTime.Now.AddMonths(-3);
                endTime = DateTime.Now.AddMonths(-3);

                startTime = startTime.AddMonths(0 - timeType);
                nowStartTime = DateTime.Now.AddMonths(0 - timeType);
            }
            else
            {
                int month = startTime.Year * 12 + startTime.Month - endTime.Year * 12 - endTime.Month;
                startTime = Convert.ToDateTime(startMonth + "-1").AddMonths(0 - month);
                if (endMonth.IndexOf("二月")>0 && endMonth.IndexOf("十") <= 0)
                {
                    endTime = Convert.ToDateTime(endMonth + "-28").AddMonths(0 - month);
                }
                else
                {
                    endTime = Convert.ToDateTime(endMonth + "-30").AddMonths(0 - month);
                }
                

                nowStartTime = Convert.ToDateTime(nowStartTime + "-1");
                nowEndTime = Convert.ToDateTime(nowEndTime + "-28");
            }

            startTime = new DateTime(startTime.Year, startTime.Month, 1);
            endTime = new DateTime(endTime.Year, endTime.Month, 1);

            nowStartTime = new DateTime(nowStartTime.Year, nowStartTime.Month, 1);
            nowEndTime = new DateTime(nowEndTime.Year, nowEndTime.Month, 1);

            if (dataType == "shop")
            {
                DataCompareModel allModel = new DataCompareModel("总注册");
                DataCompareModel accModel = new DataCompareModel("网站");
                DataCompareModel iosModel = new DataCompareModel("iPhone");
                DataCompareModel andModel = new DataCompareModel("ANDROID");
                DataCompareModel ipadModel = new DataCompareModel("iPad");

                List<DapperWhere> allWhere = new List<DapperWhere>();
                StringBuilder allColumn = new StringBuilder();

                allWhere.Add(new DapperWhere("statTime", startTime, " RegTime>=@statTime"));
                allWhere.Add(new DapperWhere("endTime", endTime, "RegTime<@endTime  and State=1  group by left(CONVERT(varchar(100),RegTime, 23),7)"));

                allColumn.Append("left(CONVERT(varchar(100),RegTime, 23),7) dttime,");
                allColumn.Append("COUNT(ID) accNum,");
                allColumn.Append("sum(case when Remark='10' then 1 else 0 end) iosNum, ");
                allColumn.Append("sum(case when Remark='13' then 1 else 0 end) ipadNum, ");
                allColumn.Append("sum(case when Remark='11' then 1 else 0 end) androidNum ");

                List<dynamic> accList = BLL.Base.T_AccountBaseBLL.GetList<dynamic>(0, allColumn.ToString(), allWhere, "dttime");
                foreach (dynamic item in accList)
                {
                    allModel.FormerData += item.accNum;
                    accModel.FormerData += item.accNum - item.iosNum - item.androidNum - item.ipadNum;
                    iosModel.FormerData += item.iosNum;
                    andModel.FormerData += item.androidNum;
                    ipadModel.FormerData += item.ipadNum;
                }

                allWhere.Clear();
                allColumn.Clear();

                allWhere.Add(new DapperWhere("statTime", nowStartTime, " RegTime>=@statTime"));
                allWhere.Add(new DapperWhere("endTime", nowEndTime, "RegTime<=@endTime  and State=1  group by left(CONVERT(varchar(100),RegTime, 23),7)"));

                allColumn.Append("left(CONVERT(varchar(100),RegTime, 23),7) dttime,");
                allColumn.Append("COUNT(ID) accNum,");
                allColumn.Append("sum(case when Remark='10' then 1 else 0 end) iosNum, ");
                allColumn.Append("sum(case when Remark='13' then 1 else 0 end) ipadNum, ");
                allColumn.Append("sum(case when Remark='11' then 1 else 0 end) androidNum ");

                List<dynamic> accListNow = BLL.Base.T_AccountBaseBLL.GetList<dynamic>(0, allColumn.ToString(), allWhere, "dttime");
                foreach (dynamic item in accListNow)
                {
                    allModel.AfterData += item.accNum;
                    accModel.AfterData += item.accNum - item.iosNum - item.androidNum - item.ipadNum;
                    iosModel.AfterData += item.iosNum;
                    andModel.AfterData += item.androidNum;
                    ipadModel.AfterData += item.ipadNum;
                }

                modelList.Add(allModel);
                modelList.Add(accModel);
                modelList.Add(iosModel);
                modelList.Add(andModel);
                //modelList.Add(ipadModel);

                return modelList;
            }
            else
            {
                DataCompareModel newModel = new DataCompareModel("");
                DataCompareModel oldModel = new DataCompareModel("");
                DataCompareModel allModel = new DataCompareModel("");

                List<DapperWhere> allWhere = new List<DapperWhere>();
                List<DapperWhere> regWhere = new List<DapperWhere>();
                StringBuilder allColumn = new StringBuilder();
                StringBuilder regColumn = new StringBuilder();

                #region 得到KEY
                if (dataType == "reg")
                {
                    allColumn.Append("SUM(userNum) cloumnNum,SUM(case when DAY(S_Date)<=DAY(getdate()) then userNum else 0 end) cloumnNumDay,");
                    regColumn.Append("SUM(userNum) cloumnNum,SUM(case when DAY(dayDate)<=DAY(getdate()) then userNum else 0 end) cloumnNumDay,");
                    newModel.Name = "新用户会员";
                    oldModel.Name = "旧用户会员";
                    allModel.Name = "总会员";
                }
                else if (dataType == "sell")
                {
                    allColumn.Append("SUM(saleNum) cloumnNum,SUM(case when DAY(S_Date)<=DAY(getdate()) then saleNum else 0 end) cloumnNumDay,");
                    regColumn.Append("SUM(saleNum) cloumnNum,SUM(case when DAY(dayDate)<=DAY(getdate()) then saleNum else 0 end) cloumnNumDay,");
                    newModel.Name = "新用户销售";
                    oldModel.Name = "旧用户销售";
                    allModel.Name = "总销售";
                }
                else if (dataType == "sms")
                {
                    allColumn.Append("SUM(smsNum) cloumnNum,SUM(case when DAY(S_Date)<=DAY(getdate()) then smsNum else 0 end) cloumnNumDay,");
                    regColumn.Append("SUM(smsNum) cloumnNum,SUM(case when DAY(dayDate)<=DAY(getdate()) then smsNum else 0 end) cloumnNumDay,");

                    newModel.Name = "新用户短信";
                    oldModel.Name = "旧用户短信";
                    allModel.Name = "总短信";
                }
                else if (dataType == "order")
                {
                    allColumn.Append("SUM(orderMoney) cloumnNum,SUM(case when DAY(S_Date)<=DAY(getdate()) then orderMoney else 0 end) cloumnNumDay,");
                    regColumn.Append("SUM(orderMoney) cloumnNum,SUM(case when DAY(dayDate)<=DAY(getdate()) then orderMoney else 0 end) cloumnNumDay,");

                    newModel.Name = "新用户订单";
                    oldModel.Name = "旧用户订单";
                    allModel.Name = "总订单";
                }
                else if (dataType == "pay")
                {
                    allColumn.Append("SUM(outlayNum) cloumnNum ,SUM(case when DAY(S_Date)<=DAY(getdate()) then outlayNum else 0 end) cloumnNumDay,");
                    regColumn.Append("SUM(outlayNum) cloumnNum ,SUM(case when DAY(dayDate)<=DAY(getdate()) then outlayNum else 0 end) cloumnNumDay,");

                    newModel.Name = "新用户支出";
                    oldModel.Name = "旧用户支出";
                    allModel.Name = "总支出";
                }
                else if (dataType == "goods")
                {
                    allColumn.Append("SUM(addGoodsNum) cloumnNum ,SUM(case when DAY(S_Date)<=DAY(getdate()) then addGoodsNum else 0 end) cloumnNumDay,");
                    regColumn.Append("SUM(goodsNum) cloumnNum ,SUM(case when DAY(dayDate)<=DAY(getdate()) then goodsNum else 0 end) cloumnNumDay,");

                    newModel.Name = "新用户商品";
                    oldModel.Name = "旧用户商品";
                    allModel.Name = "总商品";
                }

                allColumn.Append("left(CONVERT(varchar(100),S_Date, 23),7) dttime,");
                regColumn.Append("left(CONVERT(varchar(100),dayDate, 23),7) dttime,");
                #endregion

                allWhere.Add(new DapperWhere("statTime", startTime, " S_Date>=@statTime "));
                allWhere.Add(new DapperWhere("endTime", endTime, " @endTime>S_Date group by left(CONVERT(varchar(100),S_Date, 23),7)"));

                regWhere.Add(new DapperWhere("statTime", startTime, "DATEDIFF(MONTH,@statTime,dayDate)>=0"));
                regWhere.Add(new DapperWhere("endTime", endTime, "DATEDIFF(month,dayDate,@endTime)>=0  and DATEDIFF(month,regTime,daydate)=0  group by left(CONVERT(varchar(100),dayDate, 23),7)"));


                List<dynamic> allListBefore = BLL.Base.SysRpt_WebDayInfoBaseBLL.GetList<dynamic>(0, allColumn.ToString().Trim(','), allWhere, " dttime ");
                List<dynamic> regListBefore = BLL.Base.SysRpt_ShopDayInfoBaseBLL.GetList<dynamic>(0, regColumn.ToString().Trim(','), regWhere, " dttime ");
                foreach (dynamic item in allListBefore)
                {
                    oldModel.AfterData += item.cloumnNum;
                }
                foreach (dynamic item in regListBefore)
                {
                    newModel.AfterData += item.cloumnNum;
                }
                allModel.AfterData += oldModel.AfterData + newModel.AfterData;

                allWhere.Clear();
                regWhere.Clear();

                //allWhere.Add(new DapperWhere("statTime", nowStartTime, " S_Date>=@statTime "));
                //allWhere.Add(new DapperWhere("endTime", nowEndTime, " @endTime>=S_Date group by left(CONVERT(varchar(100),S_Date, 23),7)"));

                //regWhere.Add(new DapperWhere("statTime", nowStartTime, "DATEDIFF(MONTH,@statTime,dayDate)>=0"));
                //regWhere.Add(new DapperWhere("endTime", nowEndTime, "DATEDIFF(month,dayDate,@endTime)>=0  and DATEDIFF(month,regTime,daydate)=0  group by left(CONVERT(varchar(100),dayDate, 23),7)"));

                //List<dynamic> allList = BLL.Base.SysRpt_WebDayInfoBaseBLL.GetList<dynamic>(0, allColumn.ToString().Trim(','), allWhere, " dttime ");
                //List<dynamic> regList = BLL.Base.SysRpt_ShopDayInfoBaseBLL.GetList<dynamic>(0, regColumn.ToString().Trim(','), regWhere, " dttime ");
                //foreach (dynamic item in allList)
                //{
                //    oldModel.AfterData += item.cloumnNum;
                //}
                //foreach (dynamic item in regList)
                //{
                //    newModel.AfterData += item.cloumnNum;
                //}
                //allModel.AfterData += oldModel.AfterData + newModel.AfterData;

                modelList.Add(newModel);
                modelList.Add(oldModel);
                modelList.Add(allModel);

                return modelList;
            }
        }


        /// <summary>
        /// 获取会员、销售、商品、订单、支出数据对比摘要信息
        /// </summary>
        /// <param name="timeType"></param>
        /// <param name="startMonth"></param>
        /// <param name="endMonth"></param>
        /// <param name="dataType"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public static List<DataCompareModel> GetSectionDataSummary(int timeType, string startMonth, string endMonth, string dataType, string sourceType)
        {
            var modelList = new List<DataCompareModel>();
            var startTime = new DateTime();
            var endTime = new DateTime();
            if (timeType != 0)
            {
                startTime = DateTime.Now.AddMonths(0-timeType);
                endTime = DateTime.Now;
            }
            else
            {
                var month = startTime.Year * 12 + startTime.Month - endTime.Year * 12 - endTime.Month;
                startTime = Convert.ToDateTime(startMonth + "-1").AddMonths(0 - month);
                if (endMonth.IndexOf("二月", StringComparison.Ordinal) > 0 && endMonth.IndexOf("十", StringComparison.Ordinal) <= 0)
                {
                    endTime = Convert.ToDateTime(endMonth + "-28").AddMonths(0 - month);
                }
                else
                {
                    endTime = Convert.ToDateTime(endMonth + "-30").AddMonths(0 - month);
                }
            }

            startTime = new DateTime(startTime.Year, startTime.Month, 1);
            endTime = new DateTime(endTime.Year, endTime.Month, 1);


            var allModel = new DataCompareModel("总订单");
            var accModel = new DataCompareModel("网站");
            var iosModel = new DataCompareModel("iPhone");
            var andModel = new DataCompareModel("Android");

            var allWhere = new List<DapperWhere>();
            var allColumn = new StringBuilder();
            var dataList = new List<dynamic>();
            var captionTitle = string.Empty;

            if (dataType == "reg")
            {
                #region 会员分端数据
                captionTitle = "会员每月分端对比";
                allWhere.Add(new DapperWhere("statTime", startTime, "uRegTime>=@statTime"));
                allWhere.Add(new DapperWhere("endTime", endTime, "uRegTime<=@endTime  group by left(CONVERT(varchar(100),uRegTime, 23),7)"));
                allColumn.Append("left(CONVERT(varchar(100),uRegTime, 23),7) dttime,");
                allColumn.Append("count(DISTINCT uid) allNum,");
                allColumn.Append("sum(case when uFlag = 1 then 1 else 0 end) iosNum, ");
                allColumn.Append("sum(case when uFlag = 0 then 1 else 0 end) pcNum, ");
                allColumn.Append("sum(case when uFlag = 2 then 1 else 0 end) androidNum ");
                dataList = BLL.Base.SysRpt_ShopDayInfoBaseBLL.GetMonthSectionList<dynamic>(0, allColumn.ToString().Trim(','), allWhere, " dttime ", dataType);
                #endregion
            }
            else if (dataType == "sell")
            {
                #region 销售分端数据
                captionTitle = "销售每月分端对比";
                allWhere.Add(new DapperWhere("statTime", startTime, "saleTime>=@statTime"));
                allWhere.Add(new DapperWhere("endTime", endTime, "saleTime<=@endTime  group by left(CONVERT(varchar(100),saleTime, 23),7)"));
                allColumn.Append("left(CONVERT(varchar(100),saleTime, 23),7) dttime,");
                allColumn.Append("count(saleID) allNum,");
                allColumn.Append("sum(case when saleFlag = 1 then 1 else 0 end) iosNum, ");
                allColumn.Append("sum(case when saleFlag = 0 then 1 else 0 end) pcNum, ");
                allColumn.Append("sum(case when saleFlag = 2 then 1 else 0 end) androidNum ");
                dataList = BLL.Base.SysRpt_ShopDayInfoBaseBLL.GetMonthSectionList<dynamic>(0, allColumn.ToString().Trim(','), allWhere, " dttime ", dataType);

                #endregion
            }

            else if (dataType == "goods")
            {
                #region 商品分端数据
                captionTitle = "商品每月分端对比";
                allWhere.Add(new DapperWhere("statTime", startTime, "gAddTime>=@statTime"));
                allWhere.Add(new DapperWhere("endTime", endTime, "gAddTime<=@endTime  group by left(CONVERT(varchar(100),gAddTime, 23),7)"));
                allColumn.Append("left(CONVERT(varchar(100),gAddTime, 23),7) dttime,");
                allColumn.Append("count(gid) allNum,");
                allColumn.Append("sum(case when gflag = 1 then 1 else 0 end) iosNum, ");
                allColumn.Append("sum(case when gflag = 0 then 1 else 0 end) pcNum, ");
                allColumn.Append("sum(case when gflag = 2 then 1 else 0 end) androidNum ");
                dataList = BLL.Base.SysRpt_ShopDayInfoBaseBLL.GetMonthSectionList<dynamic>(0, allColumn.ToString().Trim(','), allWhere, " dttime ", dataType);

                #endregion
            }
            else if (dataType == "order")
            {
                #region 订单分端数据

                captionTitle = "订单每月分端对比";
                allWhere.Add(new DapperWhere("statTime", startTime, "createDate>=@statTime"));
                allWhere.Add(new DapperWhere("endTime", endTime, "createDate<=@endTime and ((orderStatus=1 and OrderTypeId=2) or orderStatus=2)  group by left(CONVERT(varchar(100),createDate, 23),7)"));
                allColumn.Append("left(CONVERT(varchar(100),createDate, 23),7) dttime,");
                allColumn.Append("count(accId) allNum,");
                allColumn.Append("sum(case when oFlag = 1 then 1 else 0 end) iosNum, ");
                allColumn.Append("sum(case when oFlag = 0 then 1 else 0 end) pcNum, ");
                allColumn.Append("sum(case when oFlag = 2 then 1 else 0 end) androidNum ");
                dataList = BLL.Base.SysRpt_ShopDayInfoBaseBLL.GetMonthSectionList<dynamic>(0, allColumn.ToString().Trim(','), allWhere, " dttime ", dataType);

                #endregion
            }
            else if (dataType == "pay")
            {
                #region 支出分端数据
                captionTitle = "支出每月分端对比";
                allWhere.Add(new DapperWhere("statTime", startTime, "PayDate>=@statTime"));
                allWhere.Add(new DapperWhere("endTime", endTime, "PayDate<=@endTime  group by left(CONVERT(varchar(100),PayDate, 23),7)"));
                allColumn.Append("left(CONVERT(varchar(100),PayDate, 23),7) dttime,");
                allColumn.Append("count(ID) allNum,");
                allColumn.Append("sum(case when pFlag = 1 then 1 else 0 end) iosNum, ");
                allColumn.Append("sum(case when pFlag = 0 then 1 else 0 end) pcNum, ");
                allColumn.Append("sum(case when pFlag = 2 then 1 else 0 end) androidNum ");
                dataList = BLL.Base.SysRpt_ShopDayInfoBaseBLL.GetMonthSectionList<dynamic>(0, allColumn.ToString().Trim(','), allWhere, " dttime ", dataType);

                #endregion
            }


            foreach (dynamic item in dataList)
            {
                allModel.FormerData += item.allNum;
                accModel.FormerData += item.allNum - item.iosNum - item.androidNum;
                iosModel.FormerData += item.iosNum;
                andModel.FormerData += item.androidNum;

                allModel.AfterData += item.allNum;
                accModel.AfterData += item.allNum - item.iosNum - item.androidNum;
                iosModel.AfterData += item.iosNum;
                andModel.AfterData += item.androidNum;
            }
            modelList.Add(allModel);
            modelList.Add(accModel);
            modelList.Add(iosModel);
            modelList.Add(andModel);
            return modelList;
        }


        public static string GetTendencySummary(DateTime startTime, DateTime endTime, string[] columns)
        {
            Dictionary<string, string> ColumnList = new Dictionary<string, string>();
            List<TendencySummaryModel> colDic = new List<TendencySummaryModel>();

            List<DapperWhere> dappWhere = new List<DapperWhere>();

            dappWhere.Add(new DapperWhere("startTime", startTime, " S_Date>=@startTime"));

            dappWhere.Add(new DapperWhere("endTime", endTime, " S_Date<=@endTime"));

            StringBuilder strColumn = new StringBuilder();

            #region  主键
            strColumn.Append("S_Date dayDate");

            strColumn.Append(",isnull(loginNum,0) loginNum");
            ColumnList.Add("loginNum", "登录用户");
            strColumn.Append(",(isnull(activeNum,0)+isnull(faithfulNum,0)) activeNum");
            ColumnList.Add("activeNum", "活跃用户");
            //strColumn.Append(",isnull(faithfulNum,0) faithfulNum");
            //ColumnList.Add("faithfulNum", "忠诚用户");

            if (columns.Contains("shop"))
            {
                strColumn.Append(",isnull(NewAccNum,0) NewAccNum");
                ColumnList.Add("NewAccNum", "店铺");
            }

            if (columns.Contains("reg"))
            {
                strColumn.Append(",isnull(userNum,0) userNum");
                ColumnList.Add("userNum", "会员");
            }
            if (columns.Contains("sell"))
            {
                strColumn.Append(",isnull(saleNum,0) saleNum");
                ColumnList.Add("saleNum", "销售");
            }
            if (columns.Contains("sms"))
            {
                strColumn.Append(",isnull(smsNum,0) smsNum");
                ColumnList.Add("smsNum", "短信");
            }
            if (columns.Contains("order"))
            {
                strColumn.Append(",isnull(orderMoney,0) orderMoney ");
                ColumnList.Add("orderMoney", "订单");
            }
            if (columns.Contains("pay"))
            {
                strColumn.Append(",isnull(outlayNum,0) outlayNum ");
                ColumnList.Add("outlayNum", "支出");
            }
            if (columns.Contains("client"))
            {
                strColumn.Append(",isnull(clientLogNum,0) clientLogNum ");
                ColumnList.Add("clientLogNum", "客户端");
            }
            if (columns.Contains("mood"))
            {
                strColumn.Append(",isnull(moodNum,0) moodNum");
                ColumnList.Add("moodNum", "心情");
            }
            if (columns.Contains("zhaixian"))
            {
                strColumn.Append(",isnull(acc_Rep,0) acc_Rep");
                ColumnList.Add("acc_Rep", "在线时长");
            }
            if (columns.Contains("sale"))
            {
                strColumn.Append(",isnull(saleMoney,0) saleMoney");
                ColumnList.Add("saleMoney", "销售额");
            }
            if (columns.Contains("goods"))
            {
                strColumn.Append(",isnull(addGoodsNum,0) addGoodsNum");
                ColumnList.Add("addGoodsNum", "商品");
            }
            #endregion

            List<SysRpt_WebDayInfo> dataList = BLL.Base.SysRpt_WebDayInfoBaseBLL.GetList<SysRpt_WebDayInfo>(0, strColumn.ToString(), dappWhere, " id desc");

            TimeSpan ts = endTime - startTime;
            DateTime comEdTime = startTime.AddDays(-1);
            DateTime comStTime = comEdTime.AddDays(-ts.TotalDays);

            dappWhere.Clear();
            dappWhere.Add(new DapperWhere("startTime", comStTime, " S_Date>=@startTime"));
            dappWhere.Add(new DapperWhere("endTime", comEdTime, " S_Date<=@endTime"));

            List<SysRpt_WebDayInfo> formerDataList = BLL.Base.SysRpt_WebDayInfoBaseBLL.GetList<SysRpt_WebDayInfo>(0, strColumn.ToString(), dappWhere, " id desc");

            foreach (string str in ColumnList.Keys)
            {
                TendencySummaryModel model = new TendencySummaryModel("总" + ColumnList[str]);
                foreach (SysRpt_WebDayInfo item in dataList)
                {
                    model.NowNum += Convert.ToInt32(item[str]);
                }

                int allCount = dataList.Count;
                int part = 0;

                if (allCount >= 14)
                {
                    part = allCount / 7;
                    decimal[] partData = new decimal[part];

                    for (int i = 0; i < part; i++)
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            partData[i] += dataList[i * 7 + j][str];
                        }
                    }


                    decimal[] ratioList = new decimal[part - 1];
                    for (int i = 0; i < part; i++)
                    {
                        if (i < part - 1)
                        {
                            if (partData[i] != 0)
                            {
                                ratioList[i] = (partData[i + 1] - partData[i]) * 100 / partData[i];
                            }
                            else
                            {
                                ratioList[i] = 0;
                            }
                        }
                    }

                    decimal allRatio = 0;
                    foreach (decimal item in ratioList)
                    {
                        allRatio += item;
                    }
                    model.Ratio = (allRatio / (part - 1)).ToString("f2");

                }
                else
                {
                    model.Ratio = "-999";
                }
                //foreach (SysRpt_WebDayInfo item in formerDataList)
                //{
                //    model.FormerNum += Convert.ToInt32(item[str]);
                //}

                //if (model.FormerNum!=0)
                //{
                //    model.Ratio = (model.NowNum - model.FormerNum)*100/model.FormerNum;
                //}

                colDic.Add(model);
            }

            return CommonLib.Helper.JsonSerializeObject(colDic);
        }

        public static string GetPeerMonthData(DateTime stDate, DateTime edDate)
        {
            stDate = new DateTime(stDate.Year, stDate.Month - 1, 1);

            List<DapperWhere> allWhere = new List<DapperWhere>();
            StringBuilder allColumn = new StringBuilder();

            allWhere.Add(new DapperWhere("statTime", stDate, " RegTime>=@statTime"));
            allWhere.Add(new DapperWhere("endTime", edDate, " @endTime>=RegTime   and State=1  group by left(CONVERT(varchar(100),RegTime, 23),7)"));

            allColumn.Append("left(CONVERT(varchar(100),RegTime, 23),7) dttime,");
            allColumn.Append("COUNT(ID) accNum,");
            allColumn.Append("sum(case when Remark='10' then 1 else 0 end) iosNum, ");
            allColumn.Append("sum(case when Remark='11' then 1 else 0 end) androidNum ");

            List<dynamic> accList = BLL.Base.T_AccountBaseBLL.GetList<dynamic>(0, allColumn.ToString(), allWhere, "dttime");

            return CommonLib.Helper.JsonSerializeObject(accList);
        }

        /// <summary>
        /// 数据对比  新注册店铺
        /// </summary>
        /// <param name="timeType"></param>
        /// <param name="startMonth"></param>
        /// <param name="endMonth"></param>
        /// <returns></returns>
        public static chartDataModel MonthComparisonAboutNewAccount(int timeType, string startMonth, string endMonth)
        {
            DateTime startTime = DateTime.Now.AddMonths(-3);
            DateTime endTime = DateTime.Now;
            if (timeType != 0)
            {
                startTime = DateTime.Now.AddMonths(0 - timeType);
            }
            else
            {
                startTime = Convert.ToDateTime(startMonth + "-1");
                endTime = Convert.ToDateTime(endMonth + "-1");
            }


            startTime = new DateTime(startTime.Year, startTime.Month, 1);
            endTime = new DateTime(endTime.Year, endTime.Month, 1);


            List<DapperWhere> allWhere = new List<DapperWhere>();
            StringBuilder allColumn = new StringBuilder();

            allWhere.Add(new DapperWhere("statTime", startTime, " DATEDIFF(MONTH,@statTime,RegTime)>=0"));
            allWhere.Add(new DapperWhere("endTime", endTime, "DATEDIFF(month,RegTime,@endTime)>=0   and State=1  group by left(CONVERT(varchar(100),RegTime, 23),7)"));

            allColumn.Append("left(CONVERT(varchar(100),RegTime, 23),7) dttime,");
            allColumn.Append("COUNT(ID) accNum,");
            allColumn.Append("sum(case when Remark='10' then 1 else 0 end) iosNum, ");
            allColumn.Append("sum(case when Remark='13' then 1 else 0 end) ipadNum, ");
            allColumn.Append("sum(case when Remark='11' then 1 else 0 end) androidNum ");

            List<dynamic> accList = BLL.Base.T_AccountBaseBLL.GetList<dynamic>(0, allColumn.ToString(), allWhere, "dttime");


            chartDataModel chartModel = new chartDataModel();
            chartModel.captionTitle = "新注册店铺每月对比";

            DateTime forDateTime = new DateTime(startTime.Year, startTime.Month, 1);
            string dayDateStr = DateTime.Now.ToString("yyyy-MM");
            while (forDateTime <= endTime)
            {
                #region 得到所有X轴
                charDataList charItemList = new charDataList();
                charItemList.XLable = forDateTime.ToString("yyyy-MM");
                charItemList.weekend = (int)forDateTime.DayOfWeek;

                charItemList.ItemList["all"] = new charDataItemList(0)
                {
                    series = "网站"
                };
                charItemList.ItemList["iosNum"] = new charDataItemList(0)
                {
                    series = "iPhone"
                };
                charItemList.ItemList["androidNum"] = new charDataItemList(0)
                {
                    series = "Android"
                };
                charItemList.ItemList["ipadNum"] = new charDataItemList(0)
                {
                    series = "iPad"
                };
                if (dayDateStr == charItemList.XLable)
                {
                    charItemList.thisMonth = 1;
                }
                chartModel.DataList[charItemList.XLable] = charItemList;
                #endregion
                forDateTime = forDateTime.AddMonths(1);
            }

            //dynamic peerData =
            //    CommonLib.Helper.JsonDeserializeObject<dynamic>(GetPeerMonthData(DateTime.Now,
            //        DateTime.Now.AddMonths(-1)));

            foreach (dynamic item in accList)
            {
                //if (item.dttime.Year == endTime.Year && item.dttime.Month == endTime.Month)
                //{
                //    string xLable = item.dttime.ToString();
                //    Dictionary<string, charDataItemList> dataitemList = chartModel.DataList[xLable].ItemList;
                //    dataitemList["all"].Values = (Convert.ToDecimal(item.accNum) - Convert.ToDecimal(item.iosNum) - Convert.ToDecimal(item.androidNum));
                //    dataitemList["iosNum"].Values = Convert.ToDecimal(item.iosNum);
                //    dataitemList["androidNum"].Values = Convert.ToDecimal(item.androidNum);
                //    dataitemList["all"].toolText = peerData.accNum - peerData.iosNum - peerData.androidNum;
                //    dataitemList["iosNum"].toolText = peerData.iosNum;
                //    dataitemList["androidNum"].toolText = peerData.androidNum;

                //}
                //else
                //{
                string xLable = item.dttime.ToString();
                Dictionary<string, charDataItemList> dataitemList = chartModel.DataList[xLable].ItemList;
                dataitemList["all"].Values = (Convert.ToDecimal(item.accNum) - Convert.ToDecimal(item.iosNum) - Convert.ToDecimal(item.androidNum) - Convert.ToDecimal(item.ipadNum));
                dataitemList["iosNum"].Values = Convert.ToDecimal(item.iosNum);
                dataitemList["androidNum"].Values = Convert.ToDecimal(item.androidNum);
                dataitemList["ipadNum"].Values = Convert.ToDecimal(item.ipadNum);
                //}

            }

            return chartModel;
        }

        /// <summary>
        /// 注册来源数据
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="source"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public static Dictionary<string, object> RegisterSource(DateTime startTime, DateTime endTime, string[] source, string dataType)
        {

            List<SourceAnalyzeModel> modelList = new List<SourceAnalyzeModel>();

            if (dataType == "regtime")
            {
                modelList = T_AccountBLL.RegSourceAnalyze(startTime, endTime, source);
            }
            else if (dataType == "detailSource")
            {
                modelList = T_AccountBLL.RegSourceAnalyze(startTime, endTime, source, "detail");
            }
            else if (dataType == "order")
            {
                modelList = T_OrderInfoBLL.OrderSourceAnalyze(startTime, endTime, source.Select(x => int.Parse(x)).ToArray());
            }
            else if (dataType == "log")
            {
                modelList = T_LOGBLL.LogSourceAnalyze(startTime, endTime, source.Select(x => int.Parse(x)).ToArray());
            }



            Dictionary<string, object> chart = new Dictionary<string, object>();

            List<object> obj = new List<object>();

            if (dataType == "detailSource")
            {
                obj = RegisterSourceListJson(startTime, endTime, modelList, source.ToList(), "detail");
            }
            else
            {
                obj = RegisterSourceListJson(startTime, endTime, modelList, source.ToList(), "");
            }

            obj.Reverse();
            chart["data"] = obj;
            chart["count"] = source.Count();
            return chart;
        }

        /// <summary>
        /// 注册来源数据 处理数据
        /// </summary>
        /// <returns></returns>
        public static List<object> RegisterSourceListJson(DateTime statTime, DateTime endTime, List<SourceAnalyzeModel> sourceanalyList, List<string> source, string detailMark = "")
        {
            List<object> data = new List<object>();

            for (int i = sourceanalyList.Count; i > 0; i--)
            {
                SourceAnalyzeModel model = sourceanalyList[i - 1];
                Dictionary<string, object> dataList = new Dictionary<string, object>();
                dataList["datetime"] = model.DateTime.ToString("yyyy-MM-dd");
                dataList["allCount"] = model.CountValue.ToString();
                Dictionary<string, object> dataItemList = new Dictionary<string, object>();
                foreach (var sid in source)
                {
                    Dictionary<string, string> itemList = new Dictionary<string, string>();
                    //判断是否周末
                    if (model.DateTime.DayOfWeek == DayOfWeek.Sunday || model.DateTime.DayOfWeek == DayOfWeek.Saturday)
                    {
                        itemList["weekend"] = "1";
                        //allItemList.Values = "{value:" + allVal + ",symbol:'star'}";
                    }
                    else
                        itemList["weekend"] = "0";

                    if (model.ItemList.ContainsKey(sid.ToString()))
                    {
                        itemList["CountValue"] = CommonLib.Helper.GetNumberFormats(model.ItemList[sid.ToString()].CountValue.ToString(), 2);
                        itemList["ValueScore"] = CommonLib.Helper.GetNumberFormats((model.ItemList[sid.ToString()].ValueScore * 100).ToString(), 2);
                    }
                    else if (model.ItemList.ContainsKey(sid.ToString().Trim('\'')))
                    {
                        itemList["CountValue"] = CommonLib.Helper.GetNumberFormats(model.ItemList[sid.ToString().Trim('\'')].CountValue.ToString(), 2);
                        itemList["ValueScore"] = CommonLib.Helper.GetNumberFormats((model.ItemList[sid.ToString().Trim('\'')].ValueScore * 100).ToString(), 2);
                    }
                    else
                    {
                        itemList["CountValue"] = "0";
                        itemList["ValueScore"] = "0";
                    }

                    if (string.IsNullOrEmpty(detailMark))
                    {
                        dataItemList[Model.Enum.AccountEnum.GetSourceName(Convert.ToInt32(sid))] = itemList;
                    }
                    else
                    {
                        dataItemList[sid] = itemList;
                    }

                }
                dataList["itemList"] = dataItemList;

                data.Add(dataList);
            }
            //List<object> data = new List<object>();

            //for (int i = sourceanalyList.Count; i > 0; i--)
            //{
            //    SourceAnalyzeModel model = sourceanalyList[i - 1];
            //    Dictionary<string, object> dataList = new Dictionary<string, object>();
            //    dataList["datetime"] = model.DateTime.ToString("yyyy-MM-dd");
            //    dataList["allCount"] = model.CountValue.ToString();
            //    Dictionary<string, object> dataItemList = new Dictionary<string, object>();
            //    foreach (int sid in source)
            //    {
            //        Dictionary<string, string> itemList = new Dictionary<string, string>();
            //        Dictionary<string, string> itemListEx = new Dictionary<string, string>();
            //        //判断是否周末
            //        if (model.DateTime.DayOfWeek == DayOfWeek.Sunday || model.DateTime.DayOfWeek == DayOfWeek.Saturday)
            //        {
            //            itemList["weekend"] = "1";
            //            itemListEx["weekend"] = "1";
            //            //allItemList.Values = "{value:" + allVal + ",symbol:'star'}";
            //        }
            //        else
            //        {
            //            itemList["weekend"] = "0";
            //            itemListEx["weekend"] = "0";
            //        }

            //        if (model.ItemList.ContainsKey(sid.ToString()))
            //        {
            //            itemList["CountValue"] = CommonLib.Helper.GetNumberFormats(model.ItemList[sid.ToString()].CountValue.ToString(), 2);
            //            itemList["ValueScore"] = CommonLib.Helper.GetNumberFormats((model.ItemList[sid.ToString()].ValueScore*100).ToString(), 2);
            //            dataItemList[Model.Enum.AccountEnum.GetSourceTagName(sid)] = itemList;
            //        }
            //        if (model.ItemList.ContainsKey("t" + Model.Enum.AccountEnum.GetSourceTagName(sid)))
            //        {
            //            itemListEx["CountValue"] = CommonLib.Helper.GetNumberFormats(model.ItemList["t" + Model.Enum.AccountEnum.GetSourceTagName(sid)].CountValue.ToString(), 2);
            //            dataItemList["t" + Model.Enum.AccountEnum.GetSourceTagName(sid)] = itemListEx;
            //        }
            //        else
            //        {
            //            itemList["CountValue"] = "0";
            //            itemList["ValueScore"] = "0";
            //            dataItemList[Model.Enum.AccountEnum.GetSourceTagName(sid)] = itemList;
            //        }

            //    }
            //    dataList["itemList"] = dataItemList;

            //    data.Add(dataList);
            //} 
            return data;
        }

        /// <summary>
        /// 活跃分析
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static chartDataModel ActiveAnalysisJson(DateTime startTime, DateTime endTime, string[] columns)
        {
            //List<DapperWhere> sqlWhere = new List<DapperWhere>();
            //sqlWhere.Add(new DapperWhere("statTime", startTime, " S_Date >= @statTime"));
            //sqlWhere.Add(new DapperWhere("endTime", endTime, " S_Date <= @endTime"));

            Dictionary<string, string> columnsList = new Dictionary<string, string>();

            #region 列主键

            if (columns.Contains("active"))
            {
                columnsList.Add("ActiveUsr", "活跃用户");
                //columnsList.Add("newAdd", "新增活跃用户");
            }
            if (columns.Contains("login"))
            {
                columnsList.Add("LoginUsr", "当天登录");
            }
            if (columns.Contains("faithful"))
            {
                columnsList.Add("FaithUsr", "忠实用户");
            }
            if (columns.Contains("dormancy"))
            {
                columnsList.Add("SleepUsr", "休眠用户");
            }
            if (columns.Contains("outflow"))
            {
                columnsList.Add("OutUsr", "流失用户");
            }
            //if (columns.Contains("stration"))
            //{
            //    columnsList.Add("registration", "签到用户");
            //}

            #endregion

            List<string> columnKey = columnsList.Keys.ToList();

            string columnStr = "ShowDate dayDate";
            foreach (string itemKey in columnKey)
            {
                columnStr += "," + itemKey;
            }

            List<dynamic> dataList = new List<dynamic>();
            try
            {
                dataList = GetActiveStatusList(startTime, endTime, columnStr);
            }
            catch (Exception ex)
            {
                Logger.Error("获取活跃率分析图表出错", ex);

            }

            //List<dynamic> dataList = BLL.Base.SysRpt_WebDayInfoBaseBLL.GetList<dynamic>(0, columnStr, sqlWhere, " S_Date asc");

            //if (columnKey.Contains("newAdd"))
            //{
            //    List<dynamic> newAcctive = SysRpt_ShopActiveBLL.GetNewActiveUser(startTime, endTime);
            //    foreach (dynamic dataItem in dataList)
            //    {
            //        foreach (dynamic newItem in newAcctive.Where(x => x.dayDate == dataItem.dayDate))
            //        {
            //            dataItem.newAdd = newItem.activeNum;
            //        }
            //    }
            //}
            chartDataModel charTmodel = new chartDataModel();
            try
            {
                return charTmodel.SetDataAboutDataTime(startTime, endTime, dataList, columnsList, "活跃度分析", "yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                Logger.Error("生成活跃度图表出错！", ex);
                return null;
            }

        }

        /// <summary>
        /// 活跃度分析摘要
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static string GetActiveSummary(DateTime startTime, DateTime endTime, string[] columns)
        {
            List<DapperWhere> sqlWhere = new List<DapperWhere>();
            sqlWhere.Add(new DapperWhere("statTime", startTime, " S_Date >= @statTime"));
            sqlWhere.Add(new DapperWhere("endTime", endTime, " S_Date <= @endTime"));

            List<TendencySummaryModel> colDic = new List<TendencySummaryModel>();
            Dictionary<string, string> columnsList = new Dictionary<string, string>();

            #region 列主键

            if (columns.Contains("active"))
            {
                columnsList.Add("activeNum", "活跃用户");
                columnsList.Add("newAdd", "新增活跃用户");
            }
            if (columns.Contains("login"))
            {
                columnsList.Add("loginNum", "当天登录");
            }
            if (columns.Contains("faithful"))
            {
                columnsList.Add("faithfulNum", "忠实用户");
            }
            if (columns.Contains("dormancy"))
            {
                columnsList.Add("dormancyNum", "休眠用户");
            }
            if (columns.Contains("outflow"))
            {
                columnsList.Add("outflowNum", "流失用户");
            }
            if (columns.Contains("stration"))
            {
                columnsList.Add("registration", "签到用户");
            }

            #endregion

            List<string> columnKey = columnsList.Keys.ToList();

            string columnStr = "S_Date";
            foreach (string itemKey in columnKey)
            {
                if (itemKey == "newAdd")
                {
                    columnStr += ",0 " + itemKey;
                }
                else
                {
                    columnStr += "," + itemKey;
                }
            }
            List<SysRpt_WebDayInfo> dataList = BLL.Base.SysRpt_WebDayInfoBaseBLL.GetList<SysRpt_WebDayInfo>(0, columnStr, sqlWhere, " S_Date asc");

            if (columnKey.Contains("newAdd"))
            {
                List<dynamic> newAcctive = SysRpt_ShopActiveBLL.GetNewActiveUser(startTime, endTime);
                foreach (SysRpt_WebDayInfo dataItem in dataList)
                {
                    foreach (dynamic newItem in newAcctive.Where(x => x.dayDate == dataItem.S_Date))
                    {
                        dataItem.newAdd = newItem.activeNum;
                    }
                }
            }

            #region abandon code
            //TimeSpan ts = endTime - startTime;
            //DateTime comEdTime = startTime.AddDays(-1);
            //DateTime comStTime = comEdTime.AddDays(-ts.TotalDays);

            //sqlWhere.Clear();
            //sqlWhere.Add(new DapperWhere("startTime", comStTime, " S_Date>=@startTime"));
            //sqlWhere.Add(new DapperWhere("endTime", comEdTime, " S_Date<=@endTime"));

            //List<SysRpt_WebDayInfo> formerDataList = BLL.Base.SysRpt_WebDayInfoBaseBLL.GetList<SysRpt_WebDayInfo>(0, columnStr, sqlWhere, " S_Date asc");
            #endregion

            foreach (string str in columnsList.Keys)
            {
                TendencySummaryModel model = new TendencySummaryModel(columnsList[str] + "平均周增长");
                foreach (SysRpt_WebDayInfo item in dataList)
                {
                    model.NowNum += Convert.ToInt32(item[str]);
                }
                int allCount = dataList.Count;
                int part = 0;

                if (str != "newAdd")
                {
                    if (allCount >= 7)
                    {
                        part = allCount / 7;
                        decimal[] partData = new decimal[part];

                        for (int i = 0; i < part; i++)
                        {
                            if (i == 0)
                            {
                                partData[i] = dataList[0][str];
                            }
                            else
                            {
                                partData[i] = dataList[i * 7 - 1][str];
                            }
                        }

                        if (part > 1)
                        {
                            decimal[] ratioList = new decimal[part - 1];
                            for (int i = 0; i < part; i++)
                            {
                                if (i < part - 1)
                                {
                                    if (partData[i] != 0)
                                    {
                                        ratioList[i] = (partData[i + 1] - partData[i]) * 100 / partData[i];
                                    }
                                    else
                                    {
                                        ratioList[i] = 0;
                                    }
                                }
                            }

                            decimal allRatio = 0;
                            foreach (decimal item in ratioList)
                            {
                                allRatio += item;
                            }
                            model.Ratio = (allRatio / (part - 1)).ToString("f2");
                        }
                        else
                        {
                            if (dataList[0][str] != 0)
                            {
                                model.Ratio = ((dataList[6][str] - dataList[0][str]) * 100 / dataList[0][str]).ToString("f2");
                            }
                            else
                            {
                                model.Ratio = "-999";
                            }

                        }

                    }
                    else
                    {
                        model.Ratio = "-999";
                    }
                }
                else
                {
                    if (allCount >= 14)
                    {
                        part = allCount / 7;
                        decimal[] partData = new decimal[part];

                        for (int i = 0; i < part; i++)
                        {
                            for (int j = 0; j < 7; j++)
                            {
                                partData[i] += dataList[i * 7 + j][str];
                            }
                        }


                        decimal[] ratioList = new decimal[part - 1];
                        for (int i = 0; i < part; i++)
                        {
                            if (i < part - 1)
                            {
                                if (partData[i] != 0)
                                {
                                    ratioList[i] = (partData[i + 1] - partData[i]) * 100 / partData[i];
                                }
                                else
                                {
                                    ratioList[i] = 0;
                                }
                            }
                        }

                        decimal allRatio = 0;
                        foreach (decimal item in ratioList)
                        {
                            allRatio += item;
                        }
                        model.Ratio = (allRatio / (part - 1)).ToString("f2");

                    }
                    else
                    {
                        model.Ratio = "-999";
                    }
                }


                colDic.Add(model);
            }

            return CommonLib.Helper.JsonSerializeObject(colDic);

        }

        /// <summary>
        /// 得到活跃分析的详情列表
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ActiveAnalysisList(DateTime startTime, DateTime endTime, int pageIndex = 1, int pageSize = 20)
        {

            Dictionary<string, object> list = new Dictionary<string, object>();

            List<DapperWhere> sqlWhere = new List<DapperWhere>();
            sqlWhere.Add(new DapperWhere("statTime", startTime, " S_Date >= @statTime"));
            sqlWhere.Add(new DapperWhere("endTime", endTime, " S_Date <= @endTime"));


            try
            {
                if (pageSize < 1)
                {
                    pageSize = 20;
                }

                int rowCount = 0;
                if (pageIndex == 1)
                {
                    rowCount = BLL.Base.SysRpt_WebDayInfoBaseBLL.GetCount(sqlWhere);
                }
                int maxPage = 0;
                if (rowCount > 0)
                {
                    maxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(rowCount) / pageSize));
                }

                list["rowCount"] = rowCount + 1;
                list["maxPage"] = maxPage;
                list["pageIndex"] = pageIndex;

                //循环处理每日的活跃信息，切换到实时的数据源ShopActive
                list["listData"] = GetDailyStatus(startTime, endTime, pageIndex);
            }
            catch (Exception ex)
            {
                Logger.Error("获取活跃度详情列表出错！", ex);
                return null;
            }




            //string Column = "S_Date [st],accountNum [an],NewAccNum [na],unknownNum [un],reg_Attention [ra],Attention [atten] ,loginNum [ln],registration [rs],activeNum [acn],faithfulNum [fn],dormancyNum [dn],outflowNum [on],CONVERT(varchar(100),S_Date, 23) [time]";

            //list["listData"] = BLL.Base.SysRpt_WebDayInfoBaseBLL.GetList<dynamic>(pageIndex, pageSize, Column, sqlWhere, "S_Date desc");

            return list;
        }

        /// <summary>
        /// 得到店铺状态列表信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="activeList"></param>
        /// <param name="isContinue"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ShopActiveList(int pageIndex, DateTime startTime, DateTime endTime, int[] activeList, int isContinue = 0, int pageSize = 20)
        {

            Dictionary<string, object> list = new Dictionary<string, object>();
            List<DapperWhere> sqlWhere = new List<DapperWhere>();


            if (isContinue == 1) //持续某一状态
            {
                sqlWhere.Add(new DapperWhere("startTime", startTime, "startTime<=@startTime"));
                sqlWhere.Add(new DapperWhere("endTime", endTime, "updatetime>=@endTime"));
            }
            else
            {
                sqlWhere.Add(new DapperWhere("startTime", startTime, "updatetime>=@startTime"));
                sqlWhere.Add(new DapperWhere("endTime", endTime, "startTime<=@endTime"));
            }


            string activeType = "";
            foreach (int t in activeList)
            {
                if (activeType.Length > 0)
                {
                    activeType += ',';
                }
                activeType += t.ToString();
            }
            if (activeType.Length > 0)
            {
                sqlWhere.Add(new DapperWhere("active", activeType, " active in(" + activeType + ") "));
            }



            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            if (pageSize < 1)
            {
                pageSize = 20;
            }


            var dataList = SysRpt_ShopActiveBLL.GetListContainSummary(pageIndex, pageSize, sqlWhere);

            int rowCount = dataList.rowCount;
            int maxPage = 0;
            if (rowCount > 0)
            {
                maxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(rowCount) / pageSize));
            }

            list["rowCount"] = rowCount;
            list["maxPage"] = maxPage;
            list["pageIndex"] = pageIndex;
            list["listData"] = dataList.shopList;

            return list;

        }
        /// <summary>
        /// 得到店铺状态列表信息
        /// </summary>
        public static Dictionary<string, object> GroupShopActiveList(int pageIndex, DateTime mainstart, DateTime mainend, DateTime followstart, DateTime followend, int[] mainactive, int[] followactive, int maincontinue = 0, int followcontinue = 0, int pageSize = 20)
        {

            Dictionary<string, object> list = new Dictionary<string, object>();
            #region 主要条件
            List<DapperWhere> mainSqlWhere = new List<DapperWhere>();

            if (maincontinue == 1) //持续某一状态
            {
                mainSqlWhere.Add(new DapperWhere("mainstart", mainstart, "startTime<=@mainstart"));
                mainSqlWhere.Add(new DapperWhere("mainend", mainend, "updatetime>=@mainend"));
            }
            else
            {
                mainSqlWhere.Add(new DapperWhere("mainstart", mainstart, "updatetime>=@mainstart"));
                mainSqlWhere.Add(new DapperWhere("mainend", mainend, "startTime<=@mainend"));
            }


            string activeType = "";
            foreach (int t in mainactive)
            {
                if (activeType.Length > 0)
                {
                    activeType += ',';
                }
                activeType += t.ToString();
            }
            if (activeType.Length > 0)
            {
                mainSqlWhere.Add(new DapperWhere("mainactive", activeType, " active in(" + activeType + ") "));
            }
            #endregion

            #region 次要条件
            List<DapperWhere> followSqlWhere = new List<DapperWhere>();


            if (followcontinue == 1) //持续某一状态
            {
                followSqlWhere.Add(new DapperWhere("followstart", followstart, "startTime<=@followstart"));
                followSqlWhere.Add(new DapperWhere("followend", followend, "updatetime>=@followend"));
            }
            else
            {
                followSqlWhere.Add(new DapperWhere("followstart", followstart, "updatetime>=@followstart"));
                followSqlWhere.Add(new DapperWhere("followend", followend, "startTime<=@followend"));
            }


            activeType = "";
            foreach (int t in followactive)
            {
                if (activeType.Length > 0)
                {
                    activeType += ',';
                }
                activeType += t.ToString();
            }
            if (activeType.Length > 0)
            {
                followSqlWhere.Add(new DapperWhere("followactive", activeType, " active in(" + activeType + ") "));
            }

            #endregion

            if (pageSize < 1)
            {
                pageSize = 20;
            }
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            var dataList = SysRpt_ShopActiveBLL.GetGroupListContainSummary(pageIndex, pageSize, mainSqlWhere, followSqlWhere);

            int rowCount = dataList.rowCount;
            int maxPage = 0;
            if (rowCount > 0)
            {
                maxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(rowCount) / pageSize));
            }

            list["rowCount"] = rowCount;
            list["maxPage"] = maxPage;
            list["pageIndex"] = pageIndex;
            list["listData"] = dataList.shopList;

            return list;
        }

        /// <summary>
        /// 得到不同来源的数据
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <param name="sourceType"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public static string GetDiffSourceData(DateTime stDate, DateTime edDate, string[] sourceType,
            string[] conditions)
        {
            return
                CommonLib.Helper.JsonSerializeObject(SourceDataBLL.GetSourceData(stDate, edDate, sourceType, conditions));
        }

        /// <summary>
        /// 根据起止时间获取店铺状态相关的数据
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static List<ActiveStatus> GetDailyStatus(DateTime stDate, DateTime edDate, int pageIndex)
        {
            List<ActiveStatus> list = new List<ActiveStatus>();
            List<DateTime> dtList = new List<DateTime>();
            List<DateTime> tmpList = new List<DateTime>();

            int bgNumber = ((pageIndex - 1) * 10);

            while (stDate <= edDate)
            {
                dtList.Add(stDate);
                stDate = stDate.AddDays(1);
            }
            dtList.Reverse();

            if (dtList.Count - bgNumber > 10)
            {
                tmpList = dtList.GetRange(bgNumber, 10);
            }
            else
            {
                tmpList = dtList.GetRange(bgNumber, dtList.Count - bgNumber);
            }

            foreach (var item in tmpList)
            {
                list.Add(SysRpt_ShopActiveBLL.GetdailyStatus(item));
            }

            return list;
        }

        /// <summary>
        /// 获取活跃状态图表数据
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public static List<dynamic> GetActiveStatusList(DateTime stDate, DateTime edDate, string col)
        {
            List<dynamic> list = new List<dynamic>();

            while (stDate <= edDate)
            {
                list.Add(SysRpt_ShopActiveBLL.GetActiveStatusList(stDate, col));
                stDate = stDate.AddDays(1);
            }

            return list;
        }
    }
}
