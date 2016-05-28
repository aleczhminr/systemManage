using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Utility;

namespace DAL
{
    public class T_Sale_PaymentDAL
    {
        /// <summary>
        /// 获取每日店铺支付宝收单信息
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public List<ShopAlipay> GetDailyShopAlipayList(DateTime stDate,DateTime edDate)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(
                "select cast(CreateTime as date) DayDate,AccId,count(SalesId) SaleCount,sum(TotalMoney) SaleMoney " +
                "from [i200].[dbo].[T_Sale_Payment] " +
                "where PayStatus>=8 and CreateTime between @stDate and @edDate " +
                "group by AccId,cast(CreateTime as date)");

            try
            {
                return DapperHelper.Query<ShopAlipay>(strSql.ToString(), new
                {
                    stDate = stDate,
                    edDate = edDate
                }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取每日支付宝交易店铺汇总信息(附加微信数据)
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public List<dynamic> GetDailySummaryAboutAlipay(DateTime stDate,DateTime edDate)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(
                "select cast(CreateTime as date) dayDate,Count(distinct AccId) AccIdCount,count(SalesId) SaleCount,sum(TotalMoney) SaleMoney " +
                "into #Alipay from [i200].[dbo].[T_Sale_Payment] " +
                "where PayStatus>=8 and CreateTime between @stDate and @edDate " +
                "group by cast(CreateTime as date);" +
                "select cast(bInsertTime as date) dayDate,Count(distinct AccId) wAccIdCount,count(bid) wSaleCount,sum(bRealMoney) wSaleMoney " +
                "into #weixin from [I200].[dbo].[T_Goods_Booking] where bState in (1,2,3,4) and payType=1 " +
                "and bInsertTime between @stDate and @edDate " +
                "group by cast(bInsertTime as date);" +
                "select #Alipay.*,#weixin.wAccIdCount,#weixin.wSaleCount,#weixin.wSaleMoney " +
                "from #Alipay left join #weixin " +
                "on #Alipay.dayDate=#weixin.dayDate;"+
                "drop table #Alipay;"+
                "drop table #weixin;");
                //"select cast(CreateTime as date) dayDate,Count(distinct AccId) AccIdCount,count(SalesId) SaleCount,sum(TotalMoney) SaleMoney " +
                //"from [i200].[dbo].[T_Sale_Payment] " +
                //"where PayStatus>=8 and CreateTime between @stDate and @edDate " +
                //"group by cast(CreateTime as date)"

            try
            {
                return DapperHelper.Query<dynamic>(strSql.ToString(), new
                {
                    stDate = stDate,
                    edDate = edDate
                }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("获取店铺支付宝收款详情出错！", ex);
                return null;
            }
        }
    } 
}
