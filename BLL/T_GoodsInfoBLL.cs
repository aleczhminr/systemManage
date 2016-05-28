using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    /// <summary>
    /// 商品信息
    /// </summary>
    public static class T_GoodsInfoBLL
    {
        /// <summary>
        /// 得到基本列表信息
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">每页面显示数</param>
        /// <param name="sqlWhere">条件</param>
        /// <param name="filedOrder">排序</param>
        /// <returns></returns>
        public static List<T_GoodsInfoBasic> GetBaseList(int pageIndex, int pageSize, List<DapperWhere> sqlWhere, string filedOrder)
        {
            T_GoodsInfoDAL dal = new T_GoodsInfoDAL();
            return dal.GetBaseList(pageIndex, pageSize, sqlWhere, filedOrder);
        }

        public static List<dynamic> GetGoodsInfoMap(string gName)
        {
            T_GoodsInfoDAL dal = new T_GoodsInfoDAL();
            return dal.GetGoodsAccountAddressList(gName);
        }

        public static List<MobileMapModel> GetGoodsInfoEx(string gName)
        {
            T_GoodsInfoDAL dal = new T_GoodsInfoDAL();
            return dal.GetGoodsAccountAddressListEx(gName);
        }

        public static List<ShopLocationModel> GetShopAround(string lng, string lat)
        {
            T_GoodsInfoDAL dal = new T_GoodsInfoDAL();
            return dal.GetShopAround(lng, lat);
        }

        public static List<ShopLocationModel> GetSpecShop(string shopName)
        {
            T_GoodsInfoDAL dal = new T_GoodsInfoDAL();
            return dal.GetSpecShop(shopName);
        }
    }
}
