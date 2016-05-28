using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Model;
using Utility;

namespace DAL
{
    public class T_Order_InvoiceDAL
    {
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public List<OrderInvoiceModel> GetPage(int pageIndex, int pageSize, string Column, string strWhere, string orderWhere)
        {
            List<OrderInvoiceModel> models = new List<OrderInvoiceModel>();

            if (string.IsNullOrEmpty(orderWhere))
            {
                orderWhere = "createDate desc, id desc";
            }

            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageSize = pageSize < 1 ? 20 : pageSize;

            //页数计算
            int bgNumber = ((pageIndex - 1) * pageSize) + 1;
            int edNumber = (pageIndex) * pageSize;

            StringBuilder strSql = new StringBuilder();

            if (string.IsNullOrEmpty(Column))
            {
                Column = "*";
            }
            strSql.Append("select * from (");

            strSql.Append("select ROW_NUMBER() over (order by o." + orderWhere + ") as rowNumber, ");
            strSql.Append(" " + Column + " ");
            strSql.Append(" from T_Order_Invoice i left join T_OrderInfo o on i.oid=o.oid ");
            if (strWhere.Length > 0)
            {
                strSql.Append(" where " + strWhere + "  and o.orderStatus=2 ");
            }
            else
            {
                strSql.Append(" where o.orderStatus=2 ");
            }

            strSql.Append(") t ");
            strSql.Append(" where t.rowNumber between @bgNumber and @edNumber;");

            try
            {
                models = HelperForFrontend.Query<OrderInvoiceModel>(strSql.ToString(), new
                {
                    bgNumber = bgNumber,
                    edNumber = edNumber
                }).ToList();
            }
            catch (Exception ex)
            {
                models = null;
            }

            return models;
        }

        /// <summary>
        /// 获取单条发票信息
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public InvoiceSimple GetInvoiceInfo(int id)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(
                "select invoiceRemark,invoiceExpress,invoiceNo from [i200].[dbo].[T_Order_Invoice] where id=@id;");
            try
            {
                return DapperHelper.GetModel<InvoiceSimple>(strSql.ToString(), new { id = id });
            }
            catch (Exception ex)
            {

                return null;
            }
            
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public int GetPageCount(string strWhere)
        {
            int rouCount = 0;

            StringBuilder strSql = new StringBuilder();

            strSql.Append("select count(*) ");

            strSql.Append(" from T_Order_Invoice i left join T_OrderInfo o on i.oid=o.oid ");
            if (strWhere.Length > 0)
            {
                strSql.Append(" where " + strWhere + " and o.orderStatus=2");
            }
            else
            {
                strSql.Append(" where o.orderStatus=2");
            }

            try
            {
                rouCount = HelperForFrontend.Query<int>(strSql.ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                rouCount = 0;
            }

            return rouCount;
        }

        /// <summary>
        /// 得到数据列表
        /// <para>返回 OrderInvoiceList</para>
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页行数</param>
        /// <param name="DBwhere">条件</param>
        /// <param name="orderWhere">排序</param>
        /// <returns></returns>
        public OrderInvoiceList GetPage(int pageIndex, int pageSize, string strWhere, string orderWhere)
        {
            OrderInvoiceList dataList = new OrderInvoiceList();
            dataList.PageIndex = pageIndex;
            dataList.PageSize = pageSize;
            dataList.Data = new List<OrderInvoiceModel>();
            dataList.PageCount = 0;
            dataList.RowCount = 0;
            dataList.PageHtml = "";

            string oidFilter = string.Empty;
            string originOids = string.Empty;

            string Column = "id,invoiceName,invoiceAddress,invoicePhone,invoiceMoney,o.createDate,o.accId,o.oid,invoiceDesc,invoiceStatus,invoiceRemark,invoiceNo,invoiceOPeratorTime,invoiceOperatorId,invoiceAddressee,invoiceExpress";
            //List<OrderInvoiceModel> Invoiceorigin = GetPage(pageIndex, pageSize, Column, strWhere, orderWhere);

            //foreach (OrderInvoiceModel dr in Invoiceorigin)
            //{
            //    if (dr.oid != 0)
            //    {
            //        originOids += "," + dr.oid.ToString();
            //    }
            //}

            //T_OrderInfoDAL orderDalFilter = new T_OrderInfoDAL();
            //List<dynamic> orderFilter =
            //        orderDalFilter.GetListContainOrderBusiness(
            //            "T_OrderInfo.oid,T_OrderInfo.orderStatus,T_Order_Project.displayName bus_name",
            //            " T_OrderInfo.oid in(" + originOids.Trim(',') + ") and T_OrderInfo.orderStatus=2 ");
            //foreach (dynamic filter in orderFilter)
            //{
            //    oidFilter += "," + filter.oid.ToString();
            //}

            //strWhere += " oid in (" + oidFilter.Trim(',') + ") ";

            List<OrderInvoiceModel> InvoiceDs = GetPage(pageIndex, pageSize, Column, strWhere, orderWhere);

            if (InvoiceDs != null)
            { 
                string accids = string.Empty;
                string oids = string.Empty;
                string opids = string.Empty;
                foreach (OrderInvoiceModel dr in InvoiceDs)
                {
                    if (dr.accId != 0)
                    {
                        accids += "," + dr.accId.ToString();
                    }
                    if (dr.oid != 0)
                    {
                        oids += "," + dr.oid.ToString();
                    }
                    if (dr.invoiceOperatorId != 0)
                    {
                        opids += "," + dr.invoiceOperatorId.ToString();
                    }
                }

                #region 得到相关 店铺信息

                List<T_Account> AccountDs = new List<T_Account>();
                if (accids.Length > 0)
                {
                    T_AccountDAL accDal = new T_AccountDAL();

                    AccountDs = accDal.GetListByColumn("id,CompanyName,UserRealName", " id in(" + accids.Trim(',') + ") ");
                }
                #endregion
                #region 得到相关  订单信息

                List<dynamic> OrderDs = new List<dynamic>(); 
                if (oids.Length > 0)
                {
                    T_OrderInfoDAL orderDal = new T_OrderInfoDAL();
                    OrderDs = orderDal.GetListContainOrderBusiness("T_OrderInfo.oid,T_OrderInfo.orderStatus,T_Order_Project.displayName bus_name", " T_OrderInfo.oid in(" + oids.Trim(',') + ") ");
                }
                #endregion
                #region 得到相关 操作人员信息
                List<dynamic> OperDs = new List<dynamic>();
                if (opids.Length > 0)
                {
                    Sys_Manage_UserDAL ManageUserDal = new Sys_Manage_UserDAL();
                    OperDs = ManageUserDal.GetList("Id,UserName", " Id in(" + opids.Trim(',') + ") ");
                }
                #endregion
                foreach (OrderInvoiceModel dr in InvoiceDs)
                {
                    OrderInvoiceModel OIModel = new OrderInvoiceModel();
                    if (dr.id!=0)
                    {
                        OIModel.id = dr.id;
                    }
                    #region 店铺信息
                    if (dr.accId!=0)
                    {
                        OIModel.accId = dr.accId;

                        foreach (T_Account t in AccountDs.Where(x => x.ID == OIModel.accId))
                        {
                            OIModel.CompanyName = t.CompanyName;
                            OIModel.UserRealName = t.UserRealName;
                        }
                    }
                    #endregion
                    #region 订单信息
                    if (dr.oid!=0)
                    {
                        OIModel.oid = dr.oid;
                        try
                        {
                            foreach (dynamic busRow in OrderDs.Where(x => x.oid == OIModel.oid))
                            {
                                OIModel.bus_name = busRow.bus_name.ToString();
                                if (busRow.orderStatus != null && busRow.orderStatus.ToString() != "")
                                {
                                    OIModel.orderStat = int.Parse(busRow.orderStatus.ToString());
                                }
                                else
                                {
                                    OIModel.orderStat = 0;
                                }
                                OIModel.orderStatName = Enum.GetName(typeof(Model.Enum.OrderEnum.OrderStatus), OIModel.orderStat);
                            }
                        }
                        catch (Exception ex)
                        {
                                
                        }
                        
                    }
                    #endregion
                    if (dr.createDate != null)
                    {
                        OIModel.createDate = Convert.ToDateTime(dr.createDate);
                    }
                    if (dr.invoiceMoney != null)
                    {
                        OIModel.invoiceMoney = Convert.ToDecimal(dr.invoiceMoney);
                    }
                    OIModel.invoiceName = dr.invoiceName;
                    OIModel.invoiceDesc = dr.invoiceDesc;
                    OIModel.invoicePhone = dr.invoicePhone;
                    OIModel.invoiceAddress = dr.invoiceAddress;
                    OIModel.invoiceAddressee = dr.invoiceAddressee;
                    if (dr.invoiceStatus != null)
                    {
                        OIModel.invoiceStatus = dr.invoiceStatus;
                        if (OIModel.invoiceStatus == 1)
                        {
                            OIModel.invoiceStatusName = "已开发票";
                        }
                    }
                    OIModel.invoiceNo = dr.invoiceNo;
                    OIModel.invoiceRemark = dr.invoiceRemark;
                    if (dr.invoiceOperatorId != 0)
                    {
                        OIModel.invoiceOperatorId = dr.invoiceOperatorId;
                        if (OperDs != null && OperDs.Count>0)
                        {
                            foreach (dynamic opDr in OperDs.Where(x=>x.Id== OIModel.invoiceOperatorId))
                            {
                                OIModel.invoiceOperatorName = opDr.UserName;
                            }
                        }
                    }
                    if (dr.invoiceOPeratorTime != null)
                    {
                        OIModel.invoiceOPeratorTime = Convert.ToDateTime(dr.invoiceOPeratorTime);
                    }

                    dataList.Data.Add(OIModel);

                }
                //总行数
                int count = GetPageCount(strWhere);
                if (count!=0)
                {
                    dataList.RowCount = count;
                }
                if (dataList.RowCount > 0)
                {
                    dataList.PageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(dataList.RowCount) / Convert.ToDecimal(dataList.PageSize)));
                }
            }
            return dataList;
        }

        /// <summary>
        /// 更新 发票信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="invoiceNum"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public bool UpdateInvoiceNumber(int id, string invoiceNum, string remark, string express, int operId)
        {
            bool status = false;

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update T_Order_Invoice set ");
            strSql.Append(" invoiceNo=@invoiceNo,invoiceRemark=@invoiceRemark,invoiceOperatorId=@invoiceOperatorId,invoiceExpress=@express,invoiceOPeratorTime=GETDATE(),invoiceStatus=1 ");
            strSql.Append(" where id=@id");

            try
            {
                int result = HelperForFrontend.Execute(strSql.ToString(), new
                {
                    id = id,
                    invoiceNo = invoiceNum,
                    invoiceRemark = remark,
                    invoiceOperatorId = operId,
                    express = express
                });

                if (result > 0)
                {
                    status = true;
                }
            }
            catch (Exception ex)
            {
                status = false;
            }

            return status;
        }

        /// <summary>
        /// 增加一条发票信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddInvoice(T_Order_Invoice model)
        {
            int reVal = 0;

            var strSql = new StringBuilder();
            strSql.Append("insert into T_Order_Invoice(");
            strSql.Append("accId,oid,createDate,invoiceMoney,invoiceName,invoiceDesc,invoicePhone,invoiceAddress,invoiceStatus,invoiceNo,invoiceRemark,invoiceOperatorId,invoiceOPeratorTime,invoiceAddressee)");
            strSql.Append(" values (");
            strSql.Append("@accId,@oid,@createDate,@invoiceMoney,@invoiceName,@invoiceDesc,@invoicePhone,@invoiceAddress,@invoiceStatus,null,null,null,null,@invoiceAddressee)");
            strSql.Append(";select @@IDENTITY");

            try
            {
                reVal = HelperForFrontend.ExecuteScalar<int>(strSql.ToString(), new
                {
                    accId = model.accId,
                    oid = model.oid,
                    createDate = model.createDate,
                    invoiceMoney = model.invoiceMoney,
                    invoiceName = model.invoiceName,
                    invoiceDesc = model.invoiceDesc,
                    invoicePhone = model.invoicePhone,
                    invoiceAddress = model.invoiceAddress,
                    invoiceStatus = model.invoiceStatus,
                    invoiceAddressee = model.invoiceAddressee
                });
            }
            catch (Exception ex)
            {
                return 0;
            }
            
            return reVal;
        }

        //colName["ID"] = "店铺ID";
        //    colName["CompanyName"] = "店铺名称";
        //    colName["invoiceMoney"] = "发票金额";
        //    colName["PhoneNumber"] = "订单日期";
        //    colName["UserEmail"] = "发票抬头";
        //    colName["RegTime"] = "手机号码";
        //    colName["aotjb"] = "发票地址";
        //    colName["aotjbEndtime"] = "收件人姓名";
        
        public List<dynamic> GetInvoiceExportInfo(DateTime stDate, DateTime edDate)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(
                "select a.ID,a.CompanyName,o.invoiceMoney,o.invoiceName,ord.transactionDate,o.invoicePhone,o.invoiceAddress,a.UserRealName,invoice_zip from [i200].[dbo].[T_Order_Invoice] o left join i200.dbo.T_Account a on o.accId=a.ID left join i200.dbo.T_OrderInfo ord on ord.invoiceId=o.id " +
                "where o.createDate between @stDate and @edDate and ord.orderStatus=2;");

            try
            {
                return DapperHelper.Query<dynamic>(strSql.ToString(), new {stDate = stDate, edDate = edDate}).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("发票信息导出出错", ex);
                return null;
            }
        }
    }
}
