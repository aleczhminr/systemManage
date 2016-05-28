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
    /// 销售
    /// </summary>
    public static class T_SaleInfoBLL
    {
        /// <summary>
        /// 得到销售地图销售信息
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public static List<saleMapItemList> GetSaleMapSaleList(int oid)
        {
            T_SaleInfoDAL dal = new T_SaleInfoDAL();
            return dal.GetSaleMapSaleList(oid);
        }
    }
}
