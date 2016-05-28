using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 首页对象
    /// </summary>
   public partial  class PanelShowModel
    {
       /// <summary>
       /// 今天昨天对比
       /// </summary>
       public partial class SomedayDataCount
       {
           /// <summary>
           /// 添加索引器
           /// </summary>
           /// <param name="name"></param>
           /// <returns></returns>
           public int this[string name]
           {
               get
               {
                   int reNum = 0;
                   switch (name)
                   {
                       case "saleNum":
                           reNum = saleNum;
                           break;
                       case "userNum":
                           reNum = userNum;
                           break;
                       case "goodsNum":
                           reNum= goodsNum;
                           break;
                       case "smsNum":
                           reNum = smsNum;
                           break;
                       case "orderNum":
                           reNum = orderNum;
                           break;
                   }

                   return reNum;
               }
           }

           public SomedayDataCount()
           {
               regNum = 0;
               saleNum = 0;
               saleMoney = 0;
               userNum = 0;
               goodsNum = 0;
               smsNum = 0; 
               orderNum = 0;
               orderMoney = 0;
           }
           public SomedayDataCount(DateTime _nowTime)
           {
               nowTime = _nowTime;
               regNum = 0;
               saleNum = 0;
               saleMoney = 0;
               userNum = 0;
               goodsNum = 0;
               smsNum = 0;
               orderNum = 0;
               orderMoney = 0;
           }
           /// <summary>
           /// 日期时间
           /// </summary>
           public DateTime nowTime { get; set; }
           /// <summary>
           /// 日期
           /// </summary>
           public string nowTimeString { get; set; }
           /// <summary>
           /// 注册数
           /// </summary>
           public int regNum { get; set; }
           /// <summary>
           /// 销售数
           /// </summary>
           public int saleNum { get; set; }
           /// <summary>
           /// 销售金额
           /// </summary>
           public decimal saleMoney { get; set; }
           /// <summary>
           /// 会员数
           /// </summary>
           public int userNum { get; set; }
           /// <summary>
           /// 商品数
           /// </summary>
           public int goodsNum { get; set; }
           /// <summary>
           /// 短信数
           /// </summary>
           public int smsNum { get; set; }
           /// <summary>
           /// 订单数
           /// </summary>
           public int orderNum { get; set; }
           /// <summary>
           /// 订单金额
           /// </summary>
           public decimal orderMoney { get; set; }
       }

    }
}
