using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class MobileShowBLL
    {
        public static DxChartModel GetDataSource(DateTime stDate, DateTime edDate)
        {
            MobileShowDAL dal = new MobileShowDAL();
            return dal.GetDataSource(stDate, edDate);
        }

        public static DxChartModel GetDailyViewData(DateTime stDate, DateTime edDate)
        {
            MobileShowDAL dal = new MobileShowDAL();
            return dal.GetDailyViewData(stDate, edDate);
        }

        public static DxChartModel GetDailySumTrade(DateTime stDate, DateTime edDate)
        {
            MobileShowDAL dal = new MobileShowDAL();
            return dal.GetDailySumTrade(stDate, edDate);
        }

        public static DxChartModel GetDailyNewGoodsNum(DateTime stDate, DateTime edDate)
        {
            MobileShowDAL dal = new MobileShowDAL();
            return dal.GetDailyNewGoodsNum(stDate, edDate);
        }


        public static Dictionary<string, int> GetDataSourceSummary(DateTime stDate, DateTime edDate)
        {
            MobileShowDAL dal = new MobileShowDAL();
            return dal.GetDataSourceSummary(stDate, edDate);
        }

        public static Dictionary<string, decimal> GetDailySumTradeSummary(DateTime stDate, DateTime edDate)
        {
            MobileShowDAL dal = new MobileShowDAL();
            return dal.GetDailySumTradeSummary(stDate, edDate);
        }

        public static string GetDailyNewGoodsNumSummary(DateTime stDate, DateTime edDate)
        {
            MobileShowDAL dal = new MobileShowDAL();
            return dal.GetDailyNewGoodsNumSummary(stDate, edDate);
        }
    }
}
