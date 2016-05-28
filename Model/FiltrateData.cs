using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 筛选器
    /// </summary>
   public partial  class FiltrateData
   {
       /// <summary>
       /// 数据中最大值
       /// </summary>
       public class AllMax
       {
           /// <summary>
           /// 最大会员数量
           /// </summary>
           public string MaxUserNum { get; set; }
           /// <summary>
           /// 最大销售数量
           /// </summary>
           public string MaxSaleNum { get; set; }
           /// <summary>
           /// 最大未登录的时间
           /// </summary>
           public string MaxLastLoginTime { get; set; }
           /// <summary>
           /// 最大订单金额
           /// </summary>
           public string MaxOrderMoney { get; set; }
           /// <summary>
           /// 最大订单数量
           /// </summary>
           public string MaxOrderNum { get; set; }
           /// <summary>
           /// 最大短信数量
           /// </summary>
           public string MaxSmsNum { get; set; }
           /// <summary>
           /// 最大商品数量
           /// </summary>
           public string MaxGoodsNum { get; set; }
           /// <summary>
           /// 最大支出金额
           /// </summary>
           public string MaxOutlayNum { get; set; }
           /// <summary>
           /// 最大登录数量
           /// </summary>
           public string MaxAllLoginNum { get; set; }
       }

    }
}
