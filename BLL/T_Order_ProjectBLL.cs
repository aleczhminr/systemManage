using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public static class T_Order_ProjectBLL
    {
        /// <summary>
        /// 根据列名获得前几行数据
        /// </summary>
        /// <param name="Column">列名</param>
        public static List<Order_Project_Model> GetList(int Top, string Column, string strWhere, string filedOrder)
        {
            T_Order_ProjectDAL dal = new T_Order_ProjectDAL();
            return dal.GetList(Top, Column, strWhere, filedOrder);
        }
    }
}
