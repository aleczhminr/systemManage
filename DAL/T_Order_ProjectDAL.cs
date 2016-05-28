using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    /// <summary>
    /// 订单产品
    /// </summary>
    public class T_Order_ProjectDAL:Base.T_Order_ProjectBaseDAL
    {
        /// <summary>
        /// 根据列名获得前几行数据
        /// </summary>
        /// <param name="Column">列名</param>
        public List<Order_Project_Model> GetList(int Top, string Column, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            Column = Column.Length > 0 ? Column : "*";
            strSql.Append(" " + Column + " ");
            strSql.Append(" FROM i200.dbo.T_Order_Project ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (filedOrder.Trim() != "")
            {
                strSql.Append(" order by " + filedOrder);
            }
            return DapperHelper.Query<Order_Project_Model>(strSql.ToString()).ToList();
        }

        /// <summary>
        /// 获取虚拟商品列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<Order_Project_Model> GetProjectProductName(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select busId,displayName,projectType from T_Order_Project ");
            strSql.Append(" where busId in ( " + where + " )");
            List<Order_Project_Model> listitem = new List<Order_Project_Model>();
            listitem = HelperForFrontend.Query<Order_Project_Model>(strSql.ToString()).ToList();
            return listitem;
        }
    }
}
