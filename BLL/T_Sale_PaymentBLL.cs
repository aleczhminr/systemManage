using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public static class T_Sale_PaymentBLL
    {
        /// <summary>
        /// 按日期范围获取店铺支付宝收银的数据
        /// 详情数据方法
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public static ShopAlipayList GetPaymentInfo(DateTime stDate,DateTime edDate)
        {
            //ShopAlipayList 
            //T_Sale_PaymentDAL dal = new T_Sale_PaymentDAL();
            ////支付宝交易成功记录
            //List<ShopAlipay> list = dal.GetDailyShopAlipayList(stDate, edDate);

            //foreach (var item in list)
            //{
                
            //}
            return null;
        }

        /// <summary>
        /// 获取支付宝收款信息汇总
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public static List<dynamic> GetShopAlipaySumInfo(DateTime stDate,DateTime edDate)
        {
            T_Sale_PaymentDAL dal = new T_Sale_PaymentDAL();
            return dal.GetDailySummaryAboutAlipay(stDate, edDate);
        }
    }
}
