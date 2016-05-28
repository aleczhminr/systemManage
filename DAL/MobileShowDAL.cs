using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MobileShowDAL
    {
        /// <summary>
        /// 获取每日订单总数
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public DxChartModel GetDataSource(DateTime stDate, DateTime edDate)
        {
            StringBuilder strSql = new StringBuilder();
            List<dynamic> listGoods = new List<dynamic>();
            DxChartModel chartModel = new DxChartModel();
            DateTime iter = stDate;
            while (iter < edDate)
            {
                chartModel.DataList.Add(new DxChartData(iter.ToShortDateString()));
                iter = iter.AddDays(1);
            }
            strSql.Append(
                "select CAST(bInsertTime as date) opDate,COUNT(*) cnt from [i200].[dbo].[T_Goods_Booking] " +
                 "where bState in (1,2,3,4) and payType=1 and  bInsertTime between @stDate and @edDate group by CAST(bInsertTime as date);");
            listGoods = DapperHelper.Query<dynamic>(strSql.ToString(), new { stDate = stDate, edDate = edDate }).ToList();
            foreach (DxChartData day in chartModel.DataList)
            {
                List<dynamic> dayGoodsList = listGoods.FindAll(x => x.opDate.ToShortDateString() == day.Date);
                if (dayGoodsList.Count > 0)
                {
                    day.Data.Add("手机橱窗当日订单", (dayGoodsList.Exists(x => x.opDate.ToShortDateString() == day.Date) ? dayGoodsList.Find(x => x.opDate.ToShortDateString() == day.Date).cnt : 0));
                }
                else
                {
                    day.Data.Add("手机橱窗当日订单", 0);
                }
            }
            return chartModel;
        }

        /// <summary>
        /// 获取每日UV和PV
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public DxChartModel GetDailyViewData(DateTime stDate, DateTime edDate)
        {
            StringBuilder strSql = new StringBuilder();
            List<dynamic> uvList = new List<dynamic>();
            List<dynamic> pvList = new List<dynamic>();
            DxChartModel chartModel = new DxChartModel();
            DateTime iter = stDate;
            while (iter < edDate)
            {
                chartModel.DataList.Add(new DxChartData(iter.ToShortDateString()));
                iter = iter.AddDays(1);
            }
            strSql.Append(
                "select CAST(l_time as date) opDate,count(distinct l_ip) cnt from [I200_Log].[dbo].[HTML_Log] " +
                 "where l_time between @stDate and @edDate group by CAST(l_time as date);");
            uvList = DapperHelper.Query<dynamic>(strSql.ToString(), new { stDate = stDate, edDate = edDate }).ToList();

            strSql.Clear();

            strSql.Append(
                "select CAST(l_time as date) opDate,count(*) cnt from [I200_Log].[dbo].[HTML_Log] " +
                 "where l_time between @stDate and @edDate group by CAST(l_time as date);");
            pvList = DapperHelper.Query<dynamic>(strSql.ToString(), new { stDate = stDate, edDate = edDate }).ToList();

            foreach (DxChartData day in chartModel.DataList)
            {
                List<dynamic> dailyUv = uvList.FindAll(x => x.opDate.ToShortDateString() == day.Date);
                List<dynamic> dailyPv = pvList.FindAll(x => x.opDate.ToShortDateString() == day.Date);

                if (dailyUv.Count > 0)
                {
                    day.Data.Add("日UV", (dailyUv.Exists(x => x.opDate.ToShortDateString() == day.Date) ? dailyUv.Find(x => x.opDate.ToShortDateString() == day.Date).cnt : 0));
                }
                else
                {
                    day.Data.Add("日UV", 0);
                }

                if (dailyPv.Count > 0)
                {
                    day.Data.Add("日PV", (dailyPv.Exists(x => x.opDate.ToShortDateString() == day.Date) ? dailyPv.Find(x => x.opDate.ToShortDateString() == day.Date).cnt : 0));
                }
                else
                {
                    day.Data.Add("日PV", 0);
                }
            }
            return chartModel;
        }

        /// <summary>
        /// 获取日交易额
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public DxChartModel GetDailySumTrade(DateTime stDate, DateTime edDate)
        {
            StringBuilder strSql = new StringBuilder();
            List<dynamic> listGoods = new List<dynamic>();
            DxChartModel chartModel = new DxChartModel();
            DateTime iter = stDate;
            while (iter < edDate)
            {
                chartModel.DataList.Add(new DxChartData(iter.ToShortDateString()));
                iter = iter.AddDays(1);
            }
            strSql.Append(
                "select CAST(bInsertTime as date) opDate,sum(bRealMoney) cnt from [i200].[dbo].[T_Goods_Booking] " +
                 "where bState in (1,2,3,4) and payType=1 and  bInsertTime between @stDate and @edDate group by CAST(bInsertTime as date);");
            listGoods = DapperHelper.Query<dynamic>(strSql.ToString(), new { stDate = stDate, edDate = edDate }).ToList();
            foreach (DxChartData day in chartModel.DataList)
            {
                List<dynamic> dayGoodsList = listGoods.FindAll(x => x.opDate.ToShortDateString() == day.Date);
                if (dayGoodsList.Count > 0)
                {
                    day.Data.Add("当日交易额", (dayGoodsList.Exists(x => x.opDate.ToShortDateString() == day.Date) ? dayGoodsList.Find(x => x.opDate.ToShortDateString() == day.Date).cnt : 0));
                }
                else
                {
                    day.Data.Add("当日交易额", 0);
                }
            }
            return chartModel;
        }

        /// <summary>
        /// 获取每日手机橱窗新增商品
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public DxChartModel GetDailyNewGoodsNum(DateTime stDate, DateTime edDate)
        {
            StringBuilder strSql = new StringBuilder();
            List<dynamic> listGoods = new List<dynamic>();
            DxChartModel chartModel = new DxChartModel();
            DateTime iter = stDate;
            while (iter < edDate)
            {
                chartModel.DataList.Add(new DxChartData(iter.ToShortDateString()));
                iter = iter.AddDays(1);
            }
            strSql.Append(
                "select CAST(updateTime as date) opDate,count(*) cnt from [i200].[dbo].[t_GoodsExtend] " +
                 "where ge_stat=1 and  updateTime between @stDate and @edDate group by CAST(updateTime as date);");
            listGoods = DapperHelper.Query<dynamic>(strSql.ToString(), new { stDate = stDate, edDate = edDate }).ToList();
            foreach (DxChartData day in chartModel.DataList)
            {
                List<dynamic> dayGoodsList = listGoods.FindAll(x => x.opDate.ToShortDateString() == day.Date);
                if (dayGoodsList.Count > 0)
                {
                    day.Data.Add("当日新增商品", (dayGoodsList.Exists(x => x.opDate.ToShortDateString() == day.Date) ? dayGoodsList.Find(x => x.opDate.ToShortDateString() == day.Date).cnt : 0));
                }
                else
                {
                    day.Data.Add("当日新增商品", 0);
                }
            }
            return chartModel;
        }
        /// <summary>
        /// 获取订单总笔数
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public Dictionary<string, int> GetDataSourceSummary(DateTime stDate, DateTime edDate)
        {
            Dictionary<string, int> oneSummary = new Dictionary<string, int>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select isnull(payType,0) paytype,COUNT(*) cnt from [i200].[dbo].[T_Goods_Booking] " +
                 "where bState in (1,2,3,4) and  bInsertTime between @stDate and @edDate group by payType");
            List<dynamic> listitem = DapperHelper.Query<dynamic>(strSql.ToString(), new { stDate = stDate, edDate = edDate }).ToList();
            foreach (dynamic dr in listitem)
            {
                oneSummary.Add("paytype"+dr.paytype, dr.cnt);
            }
            return oneSummary;
        }
        /// <summary>
        /// 获取订单总交易额
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public Dictionary<string, decimal> GetDailySumTradeSummary(DateTime stDate, DateTime edDate)
        {
            Dictionary<string, decimal> oneSummary = new Dictionary<string, decimal>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select isnull(payType,0) paytype, sum(bRealMoney) cnt from [i200].[dbo].[T_Goods_Booking] " +
                 "where bState in (1,2,3,4) and  bInsertTime between @stDate and @edDate group by payType");
            List<dynamic> listitem = DapperHelper.Query<dynamic>(strSql.ToString(), new { stDate = stDate, edDate = edDate }).ToList();
            foreach (dynamic dr in listitem)
            {
                oneSummary.Add("paytype" + dr.paytype, dr.cnt);
            }
            return oneSummary;
        }
        /// <summary>
        /// 获取订单新增商品总数
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public string GetDailyNewGoodsNumSummary(DateTime stDate, DateTime edDate)
        {
            string iResult = string.Empty;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select count(*) cnt from [i200].[dbo].[t_GoodsExtend] " +
                 "where ge_stat=1 and  updateTime between @stDate and @edDate ");
            var sResult = HelperForFrontend.ExecuteScalar(strSql.ToString(), new { stDate = stDate, edDate = edDate });
            if (sResult != null && sResult != DBNull.Value)
            {
                iResult = sResult.ToString();
            }
            return iResult;
        }
    }
}
