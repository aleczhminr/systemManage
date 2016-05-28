using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ShopAlipayList
    {
        /// <summary>
        /// 销售日期
        /// </summary>
        public DateTime SaleDate { get; set; }
        /// <summary>
        /// 店铺支付宝销售列表
        /// </summary>
        public List<ShopAlipay> ShopList { get; set; }
    }
    public class ShopAlipay
    {
        /// <summary>
        /// 销售记录日期
        /// </summary>
        public DateTime DayDate { get; set; }
        /// <summary>
        /// 店铺Id
        /// </summary>
        public int AccId { get; set; }
        /// <summary>
        /// 销售笔数
        /// </summary>
        public int SaleCount { get; set; }
        /// <summary>
        /// 销售额
        /// </summary>
        public decimal SaleMoney { get; set; }        
    }

    /// <summary>
    /// 每日汇总支付宝交易信息
    /// </summary>
    public class DailyPayCount
    {
        /// <summary>
        /// 每日日期
        /// </summary>
        public DateTime DayDate { get; set; }
        /// <summary>
        /// 店铺数
        /// </summary>
        public int AccIdCount { get; set; }
        /// <summary>
        /// 销售笔数
        /// </summary>
        public int SaleCount { get; set; }
        /// <summary>
        /// 销售金额
        /// </summary>
        public decimal SaleMoney { get; set; }
    }

    /// <summary>
    /// 每日汇总信息列表
    /// </summary>
    //public class DailySumList
    //{
    //    public DateTime Date { get; set; }
    //    public List<DailyPayCount> DataList { get; set; }
    //}
}
