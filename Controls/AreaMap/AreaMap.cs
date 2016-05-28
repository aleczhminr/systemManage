using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Model;
using BLL;
namespace Controls.AreaMap
{
    /// <summary>
    /// 地区
    /// </summary>
  public static  class AreaMap
    {

      /// <summary>
      /// 得到数据分布图表信息
      /// </summary>
      /// <param name="sKeyValue"></param>
      /// <param name="sClass"></param>
      /// <param name="showDate"></param>
      /// <param name="endDate"></param>
      /// <returns></returns>
      public static Sys_AreaDate4EchartsMapList GetDataDistributedChart(string sKeyValue, string sClass, DateTime showDate, DateTime endDate, string order)
      {
          Sys_AreaDate4EchartsMapList areaDateMapList = new Sys_AreaDate4EchartsMapList();
          areaDateMapList = Sys_SysAreaData4EchartsBLL.GetAreaDataMap(sKeyValue, sClass, showDate, endDate, order);

          return areaDateMapList;
      }
      public static Dictionary<string, object> GetDataDistributedList(int pageIndex, string areaname, DateTime bgdate, DateTime eddate, int pageSize)
      {
          return Sys_SysAreaData4EchartsBLL.GetAreaShopInfoEx(areaname, bgdate, eddate, pageIndex, pageSize);
      }

      public static List<saleMapItemList> GetMapSaleList(int oid)
      {
          return T_SaleInfoBLL.GetSaleMapSaleList(oid);
      }

      /// <summary>
      /// 获取商品地图
      /// </summary>
      /// <param name="gName"></param>
      /// <returns></returns>
      public static List<dynamic> GetGoodsList(string gName)
      {
          return T_GoodsInfoBLL.GetGoodsInfoMap(gName);
      }

      public static List<MobileMapModel> GetGoodsListEx(string gName)
      {
          return T_GoodsInfoBLL.GetGoodsInfoEx(gName);
      }

      public static List<ShopLocationModel> GetShopAround(string lng, string lat)
      {
          //List<MobileMapModel> list = 
          
          //listEx = listEx.Take(15).ToList();

          return T_GoodsInfoBLL.GetShopAround(lng,lat); 
      }

        public static List<ShopLocationModel> GetSpecShop(string shopName)
        {
            return T_GoodsInfoBLL.GetSpecShop(shopName); 
        }

    }

  
}
