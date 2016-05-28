using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Controls;
using Controls.MessageCenter;
using Controls.Order;
using DAL;
using Model;
using Model.Model4View;
using Utility;

namespace OperationPlatform.Controllers
{
    [OperationPlatform.App_Start.LoginAuthentication]
    public class OrderController : Controller
    {
        /// <summary>
        /// 订单列表控制器
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="Column"></param>
        /// <param name="strWhere"></param>
        /// <param name="orderStatus"></param>
        /// <param name="payType"></param>
        /// <param name="orderNo"></param>
        /// <param name="accId"></param>
        /// <param name="busName"></param>
        /// <param name="remark"></param>
        /// <param name="timePeriod"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public ActionResult Index()
        {
            #region Former Codes
            //ViewBag.Page = pageIndex;

            //if (pageIndex != 1 && TempData["strWhere"] != null && TempData["page"] != null)
            //{
            //    strWhere = TempData["strWhere"].ToString();
            //    ViewBag.AllPage = Convert.ToInt32(TempData["page"]);
            //}
            //else
            //{
            //    if (start != "")
            //    {
            //        DateTime stTime = Convert.ToDateTime(start);
            //        strWhere += " createDate >='" + stTime.ToString("yyyy-MM-dd") + "' and";
            //    }
            //    if (end != "")
            //    {
            //        DateTime edTime = Convert.ToDateTime(end);
            //        strWhere += " createDate <'" + edTime.AddDays(1).Date.ToString("yyyy-MM-dd") + "' and";
            //    }
                 

            //    if (orderStatus != -99)
            //    {
            //        strWhere += " orderStatus=" + orderStatus.ToString() + " and ";
            //    }
            //    if (payType != -99)
            //    {
            //        strWhere += " orderPayType=" + payType.ToString() + " and ";
            //    }
            //    if (orderNo != "")
            //    {
            //        strWhere += " orderNo=" + orderNo + " and ";
            //    }
            //    if (accId != "")
            //    {
            //        strWhere += " accId=" + accId + " and ";
            //    }
            //    if (busName != "")
            //    {
            //        strWhere += " dbo.GetOrderProjectName(busId) like '%" + busName + "%' and ";
            //    }
            //    if (remark != "")
            //    {
            //        strWhere += " remark like '%" + remark + "%' and ";
            //    }

            //    if (strWhere.Length > 0)
            //    {
            //        strWhere = strWhere.Substring(0, strWhere.LastIndexOf('a'));
            //    }

            //    int pageCount = OrderInfoList.GetPageCount(strWhere);
            //    ViewBag.AllPage = pageCount / pageSize + 1;

            //    TempData["strWhere"] = strWhere;
            //    TempData["page"] = ViewBag.AllPage;
            //}

            //List<OrderInfoModel> orderList = OrderInfoList.GetPage(pageIndex, pageSize, Column, strWhere);
            #endregion
            Model.ManageUserModel uM = (Model.ManageUserModel)Session["logUser"];

            if (uM!=null)
            {
                if (uM.PowerSession==0)
                {
                    ViewBag.AuthStatus = 1;
                }
                else
                {
                    ViewBag.AuthStatus = 0;
                }
            }
            else
            {
                ViewBag.AuthStatus = 0;
            }

            
            return View();
        }

        public ActionResult MobileRecharge()
        {
            return View();
        }

        public string GetOrderList(int pageIndex = 1, int pageSize = 15, string Column = "", string strWhere = "",
            int orderStatus = -99, int payType = -99, string orderNo = "", string accId = "", string busName = "", string remark = "", string start = "", string end = "", int zeroRemark = 0, int exchange = 0, int noRecharge=0)
        {
            Dictionary<string,string> dic=new Dictionary<string, string>()
            {
                {"RowCount",""},
                {"PageCount",""},
                {"list",""},
                {"sum",""},
                {"integral",""},
                {"coupon",""}
            };
            if ((!string.IsNullOrEmpty(start))&&(!string.IsNullOrEmpty(end)))
            {
                if (Convert.ToDateTime(end) > Convert.ToDateTime(start))
                {
                    if (start != "")
                    {
                        DateTime stTime = Convert.ToDateTime(start);
                        strWhere += " createDate >='" + stTime.ToString("yyyy-MM-dd") + "' and";
                    }
                    if (end != "")
                    {
                        DateTime edTime = Convert.ToDateTime(end);
                        strWhere += " createDate <'" + edTime.AddDays(1).Date.ToString("yyyy-MM-dd") + "' and";
                    }
                }
                else if ((Convert.ToDateTime(end) == Convert.ToDateTime(start)) && Convert.ToDateTime(start).ToShortDateString() != DateTime.Now.ToShortDateString())
                {
                    DateTime time = Convert.ToDateTime(start);
                    strWhere += " datediff(day,createDate,'" + time.Date.ToString("yyyy-MM-dd") + "')=0 and";
                }
                else if ((Convert.ToDateTime(end) == Convert.ToDateTime(start)) && Convert.ToDateTime(start).ToShortDateString() == DateTime.Now.ToShortDateString())
                {
                    DateTime time = Convert.ToDateTime(start);
                    strWhere += " datediff(day,createDate,getdate())=0 and ";
                }
            }
            
            
            if (orderStatus != -99)
            {
                strWhere += " orderStatus=" + orderStatus.ToString() + " and ";
            }
            if (payType != -99)
            {
                if (payType==7)//安卓内购处理
                {
                    strWhere += " oFlag=2 and ";
                }
                else
                {
                    strWhere += " orderPayType=" + payType.ToString() + " and ";
                }
                
            }
            if (orderNo != "")
            {
                strWhere += " orderNo=" + orderNo + " and ";
            }
            if (accId != "")
            {
                strWhere += " accId=" + accId + " and ";
            }
            if (busName != "")
            {
                //添加京东商品筛选逻辑
                strWhere += " ( ";
                if (busName.Contains("京东")|| busName.Contains("供货") || busName.Contains("日百"))
                {
                    strWhere += " orderTypeId=3 or ";
                }
                strWhere += " (dbo.GetOrderProjectName(busId) like '%" + busName + "%' and orderTypeId=1) or (dbo.GetMaterialProjectName(busId) like '%" + busName + "%' and orderTypeId=2)) and ";
            }
            if (remark != "")
            {
                strWhere += " remark like '%" + remark + "%' and ";
               
            }
            if (zeroRemark == 1)
            {
                strWhere += " (RealPayMoney>0 or commuteIntegral>0) and ";
            }

            if (exchange == 1)
            {
                strWhere += " commuteIntegral>0 and ";
            }
            if (noRecharge==1)
            {
                strWhere += " busId not in (56,58,59) and ";
            }

            if (strWhere.Length > 0)
            {
                strWhere = strWhere.Substring(0, strWhere.LastIndexOf('a'));
            }

            int pageCount = OrderInfoList.GetPageCount(strWhere);
            if (pageCount != 0)
            {
                dic["RowCount"] = pageCount.ToString();
            }
            else
            {
                dic["RowCount"] = "0";
            }
            if (pageCount > 0)
            {
                dic["PageCount"] = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(pageCount) / Convert.ToDecimal(pageSize))).ToString();
            }
            else
            {
                dic["PageCount"] = "0";
            }

            List<OrderInfoModel> orderList = OrderInfoList.GetPage(pageIndex, pageSize, Column, strWhere);

            foreach (var item in orderList)
            {
                if (item.oFlag==2)
                {
                    item.orderPayDesc = "安卓内购";
                }
            }

            decimal sum = OrderInfoList.GetSumByCondition(strWhere);
            decimal integral = OrderInfoList.GetIntegralByCondition(strWhere);
            decimal integralSum = OrderInfoList.GetIntegralSumByCondition(strWhere);
            decimal coupon = OrderInfoList.GetCouponByCondition(strWhere);

            dic["list"] = CommonLib.Helper.JsonSerializeObject(orderList, "yyyy-MM-dd HH:mm:ss");
            dic["sum"] = sum.ToString();
            dic["integral"] = integral.ToString();
            dic["coupon"] = coupon.ToString();
            dic["integralSum"] = integralSum.ToString();

            return CommonLib.Helper.JsonSerializeObject(dic);
        }

        public string GetRechargeList(int page, string phoneNum, string orderNo)
        {
            return OrderInfoList.GetRechargeOrderList(page, phoneNum, orderNo);
        }

        public ActionResult InvoiceList()
        {
            #region former codes
            //ViewBag.Page_2 = pageIndex_2;

            //if (pageIndex_2 != 1 && TempData["iStatus"] != null)
            //{
            //    strWhere = TempData["iStatus"].ToString();
            //}
            //else
            //{
            //    if (invoiceStatus != -99)
            //    {
            //        strWhere += " invoiceStatus=" + invoiceStatus;
            //    }
            //    if (AccountId != "")
            //    {
            //        int accid = 0;
            //        if (int.TryParse(AccountId, out accid))
            //        {
            //            strWhere += (strWhere.Length > 0 ? " and " : "") + " accId=" + accid.ToString();
            //        }
            //    }


            //    TempData["iStatus"] = strWhere;
            //}

            //OrderInvoiceList invoice = OrderInfoList.GetInovicePage(pageIndex_2, pageSize, strWhere, orderWhere);
            //ViewBag.AllPage_2 = invoice.RowCount / pageSize + 1;

            //List<OrderInvoiceModel> list = invoice.Data;
            //return View(list);
#endregion
            return View();
        }

        public string GetInvoiceList(DateTime stDate, DateTime edDate, int pageIndex = 1, int pageSize = 15, int invoiceStatus = -1, string accId = "")
        {
            string returnJson = "";
            string strWhere = "";

            if (invoiceStatus!=-1)
            {
                strWhere += " invoiceStatus=" + invoiceStatus;
            }

            if (accId != "")
            {
                int accid = 0;
                if (int.TryParse(accId, out accid))
                {
                    strWhere += (strWhere.Length > 0 ? " and " : "") + " o.accId=" + accid.ToString();
                }
            }

            if (stDate.ToShortDateString() == edDate.ToShortDateString() && edDate.ToShortDateString() == DateTime.Now.ToShortDateString())
            {
                edDate = DateTime.Now;
                stDate = edDate.AddMonths(-2);
            }

            strWhere += (strWhere.Length > 0 ? " and " : "") + " o.createDate between '" + stDate + "' and '" + edDate +
                        "'";

            OrderInvoiceList invoice = OrderInfoList.GetInovicePage(pageIndex, pageSize, strWhere, "createDate desc");
            //OrderInvoiceList invoiceForLoop = OrderInfoList.GetInovicePage(pageIndex, pageSize, strWhere, "createDate desc")

            returnJson = CommonLib.Helper.JsonSerializeObject(invoice,"yyyy-MM-dd");

            return returnJson;
        }

        //public ActionResult Invoice(int pageIndex = 1, int pageSize = 20, string strWhere = "", string orderWhere = "")
        //{
        //    ViewBag.Page = pageIndex;
        //    OrderInvoiceList invoice = OrderInfoList.GetInovicePage(pageIndex, pageSize, strWhere, orderWhere);
        //    ViewBag.AllPage = invoice.RowCount / pageSize + 1;

        //    List<OrderInvoiceModel> list = invoice.Data;

        //    return View(list);
        //}

        /// <summary>
        /// 获取单条发票信息
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public string GetInvoiceInfo(int id)
        {
            InvoiceSimple invoice = OrderInfoList.GetInvoiceInfo(id);
            if (invoice!=null)
            {
                return CommonLib.Helper.JsonSerializeObject(invoice);    
            }
            else
            {
                return "";
            }
            
        }

        public string TakeInvoice(string number = "", string remark = "", string express="",int inId = 0)
        {
            if (inId <= 0 || string.IsNullOrEmpty(number))
            {
                return "获取信息失败";
            }
            else
            {
                bool status = OrderInfoList.UpdateInvoiceNumber(inId, number, remark,express, Convert.ToInt32(Session["adminid"]));
                if (status)
                {
                    return "更新成功";
                }
                else
                {
                    return "更新发票信息失败"; 
                }
            }
            
        }

        public string ConfirmOfflineOrder(decimal confirmMoney, string confirmRemark, int oid)
        {
            ManageUserModel uM = (ManageUserModel)System.Web.HttpContext.Current.Session["logUser"];
            if (T_OrderInfoBLL.SetConfirmInfo(oid, confirmMoney, confirmRemark, Convert.ToInt32(uM.UserID), HttpContext.Request.UserHostAddress, DateTime.Now))
            {
                 
                //远程调用处理
                var orderInfo = T_OrderInfoBLL.GetModel(oid); 
                int iResult = T_OrderInfoBLL.RemoteConfirm(orderInfo.accId, orderInfo.orderNo, orderInfo.RealPayMoney.ToString());
                if (iResult == 1)
                {
                    return "1";
                }
                else
                {
                    return "2";
                }
            }
            else
            {
                return "2";
            }
        }

        /// <summary>
        /// 导出发票信息
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public ActionResult ExportOrder(DateTime stDate,DateTime edDate)
        {
            ManageUserModel uM = (ManageUserModel)Session["logUser"];
            int uid = uM.UserID;

            List<dynamic> ds = T_Order_InvoiceBLL.GetExportInvoice(stDate, edDate);

            Dictionary<string, string> colName = new Dictionary<string, string>();

            colName["ID"] = "店铺ID";
            colName["CompanyName"] = "店铺名称";
            colName["invoiceMoney"] = "发票金额";
            colName["transactionDate"] = "订单日期";
            colName["invoiceName"] = "发票抬头";
            colName["invoicePhone"] = "手机号码";
            colName["invoiceAddress"] = "发票地址";
            colName["UserRealName"] = "收件人姓名";
            colName["invoice_zip"] = "邮政编码";
            
            HelperEx.ExportExcel export = new HelperEx.ExportExcel("发票信息", "invoice", colName);
            string webPath = System.Web.HttpContext.Current.Server.MapPath("/");
            string strFileName = export.ExportFile(uM.UserName, ds, webPath);

            ViewBag.NavigateUrl = strFileName;

            Response.Redirect(strFileName);
            return View();
        }

        public string MakeUpInvoice(int accId, decimal invoiceMoney, int oid, string invoiceAddressee,
            string invoicePhone, string invoiceName, string invoiceAdress)
        {
            var invoiceModel = new T_Order_Invoice();
            invoiceModel.accId = accId;
            invoiceModel.oid = oid;
            invoiceModel.createDate = DateTime.Now;
            invoiceModel.invoiceMoney = invoiceMoney;
            invoiceModel.invoiceName = invoiceName;
            invoiceModel.invoiceAddressee = invoiceAddressee;
            invoiceModel.invoiceDesc = "信息服务费";
            invoiceModel.invoicePhone = invoicePhone;
            invoiceModel.invoiceAddress = invoiceAdress;
            invoiceModel.invoiceStatus = 0;
            invoiceModel.invoiceNo = "";

            int invoiceId = T_Order_InvoiceBLL.AddInvoice(invoiceModel);
            bool status = T_OrderInfoBLL.SetInvoiceId(accId, oid, invoiceId);

            if (status)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        public string DeleteOrder(int oid)
        {
            return T_OrderInfoBLL.DeleteOrder(oid).ToString();
        }

        public string AddExpressInfo(int oid, int accId, string expressCompany, string expressCode)
        {
            ManageUserModel uM = (ManageUserModel)Session["logUser"];
            int uid = uM.UserID;
            string uName = uM.Name;


            int reVal = T_OrderInfoBLL.AddExpressInfo(oid, accId, expressCompany, expressCode, uid, uName);

            //发货完成后推送消息
            if (reVal==1)
            {
                //获取实物商品名称
                T_OrderInfoDAL dal = new T_OrderInfoDAL();
                string goodsName = dal.GetMaterialGoodsName(Convert.ToInt32(dal.GetBusIdByOid(oid)));

                try
                {
                    #region Kafka Message 实物订单发货后

                    AfterExpress eModel = new AfterExpress();
                    eModel.EventId = 5;
                    eModel.AccId = accId;

                    eModel.GoodsName = goodsName;
                    eModel.ExpressName = expressCompany;
                    eModel.ExpressCode = expressCode;

                    string specModel = CommonLib.Helper.JsonSerializeObject(eModel);

                    KafkaMessage mSend = new KafkaMessage();
                    mSend.SendMsg(5, specModel);

                    #endregion
                }
                catch (Exception ex)
                {
                    Logger.Error("实物订单发货后推送模板消息错误", ex);
                }
            }

            return reVal.ToString();

        }

        /// <summary>
        /// 退款操作
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="accid"></param>
        /// <param name="dDesc"></param>
        /// <returns></returns>
        public string Drawback(int oid,int accid,string dDesc)
        {
            ManageUserModel uM = (ManageUserModel)Session["logUser"];
            int uid = uM.UserID;

            return T_OrderInfoBLL.DrawbackProcess(accid, oid, uid, dDesc) ? "1" : "0";
        }

        /// <summary>
        /// 后台话费补充
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="orderId"></param>        
        /// <returns></returns>
        public string RechargeMobile(int accid,int orderId)
        {
            ManageUserModel uM = (ManageUserModel)Session["logUser"];
            int uid = uM.UserID;

            return OrderInfoList.RechargeProc(accid, orderId, uid);
        }

        //public string ResetOrderStatus(int oid,int accId)
        //{

        //}

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.detail = 1;
            ViewBag.detailId = id;
            return RedirectToAction("Index");
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
