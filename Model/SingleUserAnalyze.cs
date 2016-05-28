using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SingleUserAnalyze
    {
        /// <summary>
        /// 店铺Id
        /// </summary>
        public int AccId { get; set; }
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string AccName { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegTime { get; set; }
        /// <summary>
        /// 登录次数
        /// </summary>
        public int LoginTime { get; set; }
        /// <summary>
        /// 注册来源
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 店铺活跃状态
        /// </summary>
        public int ActiveStatus { get; set; }
        /// <summary>
        /// 活跃状态描述
        /// </summary>
        public string ActiveDescription { get; set; }

        /// <summary>
        /// 商品总数
        /// </summary>
        public int GoodsNum { get; set; }
        /// <summary>
        /// 有条码的商品总数
        /// </summary>
        public int GoodsNumWithBarCode { get; set; }

        /// <summary>
        /// 商品录入方式
        /// </summary>
        public string GoodsInput { get; set; }
        /// <summary>
        /// 销售最高的时间字符串
        /// </summary>
        public string MaxSaleTimeStr { get; set; }

        /// <summary>
        /// 销售最多时间1
        /// </summary>
        public string MaxSaleTime1 { get; set; }
        /// <summary>
        /// 销售最多时间2
        /// </summary>
        public string MaxSaleTime2 { get; set; }
        /// <summary>
        /// 销售最多时间3
        /// </summary>
        public string MaxSaleTime3 { get; set; }
        /// <summary>
        /// 最大利润
        /// </summary>
        public decimal MaxProfit { get; set; }
        /// <summary>
        /// 最小利润
        /// </summary>
        public decimal MinProfit { get; set; }
        /// <summary>
        /// 产生利润最多的时间
        /// </summary>
        public DateTime MaxProfitTime { get; set; }
        /// <summary>
        /// 销售成交时间间隔
        /// </summary>
        public string Interval { get; set; }
        /// <summary>
        /// 成交笔数
        /// </summary>
        public string SuccessDeal { get; set; }

        /// <summary>
        /// 注册时间字符串
        /// </summary>
        public string RegTimeStr { get; set; }
    }

    public class MostSaleList
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GName { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal GPrice { get;set;}
        /// <summary>
        /// 单个商品利润
        /// </summary>
        public decimal GProfit { get; set; }
        /// <summary>
        /// 商品销售数量
        /// </summary>
        public int Quantity { get; set; }
    }

    public class TableList
    {
        public TableList()
        {
            tbList = new List<MostSaleList>();
        }

        public int ListCount { get; set; }
        public List<MostSaleList> tbList { get; set; }
    }
}
