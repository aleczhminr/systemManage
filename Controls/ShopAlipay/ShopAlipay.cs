using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using Model;

namespace Controls.ShopAlipay
{
    public static class ShopAlipay
    {
        public static chartDataModel GetShopAlipay(DateTime startTime, DateTime endTime, string[] cs)
        {
            Dictionary<string, string> ColumnList = new Dictionary<string, string>();

            List<dynamic> list = T_Sale_PaymentBLL.GetShopAlipaySumInfo(startTime, endTime);

            if (cs.Contains("alipay"))
            {
                ColumnList.Add("AccIdCount", "支付宝店铺数");
                ColumnList.Add("SaleCount", "支付宝销售笔数");
                ColumnList.Add("SaleMoney", "支付宝销售金额");                
            }
            if (cs.Contains("weixin"))
            {
                ColumnList.Add("wAccIdCount", "微信店铺数");
                ColumnList.Add("wSaleCount", "微信销售笔数");
                ColumnList.Add("wSaleMoney", "微信销售金额");
            }

            //,Count(AccId) AccIdCount,count(SalesId) SaleCount,sum(TotalMoney) SaleMoney
            

            chartDataModel chartModel = new chartDataModel();
            return chartModel.SetDataAboutDataTime(startTime, endTime, list, ColumnList, "第三方收单");
        }
    }
}
