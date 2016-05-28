using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public class T_Order_InvoiceBLL
    {
        /// <summary>
        /// 得到数据列表
        /// <para>返回 OrderInvoiceList</para>
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页行数</param>
        /// <param name="DBwhere">条件</param>
        /// <param name="orderWhere">排序</param>
        /// <returns></returns>
        public static OrderInvoiceList GetPage(int pageIndex, int pageSize, string strWhere, string orderWhere)
        {
            T_Order_InvoiceDAL dal = new T_Order_InvoiceDAL();
            return dal.GetPage(pageIndex, pageSize, strWhere, orderWhere);
        }
        /// <summary>
        /// 更新 发票信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="invoiceNum"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public static bool UpdateInvoiceNumber(int id, string invoiceNum, string remark, string express, int operId)
        {
            T_Order_InvoiceDAL dal = new T_Order_InvoiceDAL();
            return dal.UpdateInvoiceNumber(id, invoiceNum, remark,express, operId);
        }

        /// <summary>
        /// 获取单条发票信息
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public static InvoiceSimple GetInvoiceInfo(int id)
        {
            T_Order_InvoiceDAL dal = new T_Order_InvoiceDAL();
            return dal.GetInvoiceInfo(id);
        }

        /// <summary>
        /// 增加一条发票信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddInvoice(T_Order_Invoice model)
        {
            T_Order_InvoiceDAL dal = new T_Order_InvoiceDAL();
            return dal.AddInvoice(model);
        }

        public static List<dynamic> GetExportInvoice(DateTime stDate, DateTime edDate)
        {
            T_Order_InvoiceDAL dal = new T_Order_InvoiceDAL();
            return dal.GetInvoiceExportInfo(stDate, edDate);
        }
    }
}
