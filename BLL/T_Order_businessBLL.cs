using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;

namespace BLL
{
    public static class T_Order_businessBLL
    {
        /// <summary>
        /// 产品订单分析
        /// </summary>
        /// <returns>{bus_name:名称,num:总数,quantity:数量,smsNum:短信数量,accNum:版本月数,money:金额,baifen:金额比,bus_mclass:类别}</returns>
        public static OrderAnalysis GetBussinessOrderAnalyse(DateTime stDate, DateTime edDate)
        {
            T_Order_businessDAL dal = new T_Order_businessDAL();
            return dal.GetBussinessOrderAnalyse(stDate, edDate);
        }

        /// <summary>
        /// 单个产品订单趋势
        /// </summary>
        /// <returns></returns>
        public static List<dynamic> GetSingleBussinessOrder(DateTime statTime, DateTime endTime, string keyword)
        {
            T_Order_businessDAL dal = new T_Order_businessDAL();
            return dal.GetSingleBussinessOrder(statTime, endTime, keyword);
        }

        /// <summary>
        /// 获取业务相关产品的价格信息 
        /// </summary>
        /// <param name="busId"></param>
        /// <param name="accId"></param>
        /// <returns></returns>
        public static OrderItemProp GetListItemProps(int busId, int accId)
        {
            T_Order_businessDAL dal = new T_Order_businessDAL();
            return dal.GetListItemProps(busId, accId);
        }
    }
}
