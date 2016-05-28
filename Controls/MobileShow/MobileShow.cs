using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.MobileShow
{
    public static class MobileShow
    {
        public static string GetDataSource(DateTime stDate, DateTime edDate,string dType)
        {
            switch (dType)
            {
                case "orderNum":
                    return CommonLib.Helper.JsonSerializeObject(MobileShowBLL.GetDataSource(stDate, edDate));
                    break;
                case "viewNum":
                    return CommonLib.Helper.JsonSerializeObject(MobileShowBLL.GetDailyViewData(stDate, edDate));
                    break;
                case "tradeNum":
                    return CommonLib.Helper.JsonSerializeObject(MobileShowBLL.GetDailySumTrade(stDate, edDate));
                    break;
                case "goodsNum":
                    return CommonLib.Helper.JsonSerializeObject(MobileShowBLL.GetDailyNewGoodsNum(stDate, edDate));
                    break;
                default:
                    break;
            }

            return "";        
        }

        public static string GetSourceSummary(DateTime stDate, DateTime edDate, string dType)
        {
            switch (dType)
            {
                case "orderNum":
                    return CommonLib.Helper.JsonSerializeObject(MobileShowBLL.GetDataSourceSummary(stDate, edDate));
                    break;
                case "viewNum":
                    return "";
                    break;
                case "tradeNum":
                    return CommonLib.Helper.JsonSerializeObject(MobileShowBLL.GetDailySumTradeSummary(stDate, edDate));
                    break;
                case "goodsNum":
                    return MobileShowBLL.GetDailyNewGoodsNumSummary(stDate, edDate);
                    break;
                default:
                    break;
            }

            return ""; 
        }
    }
}
