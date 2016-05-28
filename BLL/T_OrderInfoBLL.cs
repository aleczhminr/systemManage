using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib;
using DAL;
using Microsoft.SqlServer.Server;
using Model.Model4View;
using Model;
using Utility;

namespace BLL
{
    public class T_OrderInfoBLL
    {
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public static List<OrderInfoModel> GetPage(int pageIndex, int pageSize, string Column, string strWhere)
        {
            T_OrderInfoDAL dal = new T_OrderInfoDAL();
            var list = dal.GetPage(pageIndex, pageSize, Column, strWhere);

            foreach (OrderInfoModel item in list)
            {
                item.orderStatusName = Enum.GetName(typeof(Model.Enum.OrderEnum.OrderStatus), item.orderStatus);

                //京东订单处理
                if (item.OrderTypeId == 3)
                {
                    item.OrderProjectName = "京东日百供货";
                }

                if (item.OrderTypeId == 2)
                {
                    if (item.orderStatus == 1)
                    {
                        item.orderStatusName = "已付款（待发货）";
                    }
                    item.OrderProjectName = dal.GetMaterialGoodsName(item.busId);
                    try
                    {
                        var model = dal.GetMaterialAddressInfo(item.ReceivingAddressId);

                        if (model != null)
                        {
                            item.Address = model.Address;
                            item.ConsigneeName = model.ConsigneeName;
                            item.TelNumber = model.TelNumber;
                            if (!string.IsNullOrEmpty(model.ExpressCode) && item.orderStatus == 1)
                            {
                                item.orderStatusName = "已付款（已发货）";
                                item.ExpressCode = model.ExpressCode;
                                item.ExpressCompany = model.ExpressCompany;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("订单实物地址信息出错", ex);
                    }


                }
            }

            return list;
        }

        /// <summary>
        /// 更新线下订单
        /// </summary>
        /// <param name="oid">订单号</param>
        /// <param name="confirmMoney">支付金额</param>
        /// <param name="confirmRemark">备注</param>
        /// <param name="confirmOpid">操作ID</param>
        /// <param name="confirmOpIp">操作IP</param>
        /// <param name="confirmOpTime">操作时间</param>
        /// <returns></returns>
        public static bool SetConfirmInfo(int oid, decimal confirmMoney, string confirmRemark, int confirmOpid, string confirmOpIp, DateTime confirmOpTime)
        {
            T_OrderInfoDAL dal = new T_OrderInfoDAL();
            return dal.SetConfirmInfo(oid, confirmMoney, confirmRemark, confirmOpid, confirmOpIp, confirmOpTime);
        }

        /// <summary>
        /// 获取符合条件列表的行数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public static int GetPageCount(string strWhere)
        {
            T_OrderInfoDAL dal = new T_OrderInfoDAL();
            return dal.GetPageCount(strWhere);
        }

        public static decimal GetSumByCondition(string strWhere)
        {
            T_OrderInfoDAL dal = new T_OrderInfoDAL();
            return dal.GetSumByCondition(strWhere);
        }

        public static decimal GetIntegralByCondition(string strWhere)
        {
            T_OrderInfoDAL dal = new T_OrderInfoDAL();
            return dal.GetIntegralByCondition(strWhere);
        }

        public static decimal GetIntegralSumByCondition(string strWhere)
        {
            T_OrderInfoDAL dal = new T_OrderInfoDAL();
            return dal.GetIntegralSumByCondition(strWhere);
        }


        public static decimal GetCouponByCondition(string strWhere)
        {
            T_OrderInfoDAL dal = new T_OrderInfoDAL();
            return dal.GetCouponByCondition(strWhere);
        }

        /// <summary>
        /// 得到一个店铺的订单信息
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public static List<dynamic> GetListByAccId(int accid)
        {
            T_OrderInfoDAL dal = new T_OrderInfoDAL();

            List<dynamic> virtualProduct = dal.GetListByAccId(accid);
            List<dynamic> materialProduct = dal.GetMaterialListByAccId(accid);

            virtualProduct.AddRange(materialProduct);

            return virtualProduct.OrderByDescending(x => x.transactionDate).ToList();
        }

        /// <summary>
        /// 订单分析
        /// </summary>
        /// <param name="statTime"></param>
        /// <param name="endTime"></param>
        /// <param name="sourceList"></param>
        /// <returns></returns>
        public static List<SourceAnalyzeModel> OrderSourceAnalyze(DateTime startTime, DateTime endTime, int[] sourceList)
        {
            T_OrderInfoDAL dal = new T_OrderInfoDAL();
            return dal.OrderSourceAnalyze(startTime, endTime, sourceList);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static T_OrderInfo GetModel(int oid)
        {
            T_OrderInfoDAL dal = new T_OrderInfoDAL();
            return dal.GetModel(oid);
        }

        /// <summary>
        /// 提交线下订单确认信息（前台）
        /// </summary>
        /// <param name="accId">店铺id</param>
        /// <param name="orderNo">订单编码</param>
        /// <param name="orderMoney">订单金额</param>
        /// <returns></returns>
        public static int RemoteConfirm(int accId, string orderNo, string orderMoney)
        {
            string remoteUrl = "http://app.i200.cn/API/order/action.ashx";
            string authKey = "m5BNqiatNW0RvlPBkTWH";   //后台请求Key
            string strAuthCode = CommonLib.Helper.Md5Hash(accId.ToString() + orderNo + orderMoney + authKey).ToUpper();
            string strParam = string.Format("?accid={0}&orderno={1}&money={2}&authcode={3}", accId, orderNo, orderMoney,
                strAuthCode);

            Utility.Logger.Info(remoteUrl + strParam);

            string sResult = Helper.SendHttpGet(remoteUrl + strParam, null, null);
            return Convert.ToInt32(sResult);
        }

        public static List<NewOrderProduct> GetNewUsrOrder(DateTime stDate, DateTime edDate)
        {
            T_OrderInfoDAL dal = new T_OrderInfoDAL();
            return dal.GetNewUsrOrder(stDate, edDate);
        }

        /// <summary>
        /// 更新发票信息
        /// </summary>
        /// <param name="accId"></param>
        /// <param name="orderId"></param>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public static bool SetInvoiceId(int accId, int orderId, int invoiceId)
        {
            T_OrderInfoDAL dal = new T_OrderInfoDAL();
            return dal.SetInvoiceId(accId, orderId, invoiceId);
        }

        public static dynamic GetRechargeOrderListExtend(string orderNo)
        {
            T_OrderInfoDAL dal = new T_OrderInfoDAL();
            return dal.GetRechargeExtendInfo(orderNo);
        }

        public static int DeleteOrder(int oid)
        {
            T_OrderInfoDAL dal = new T_OrderInfoDAL();
            return dal.DeleteOrder(oid);
        }

        public static int AddExpressInfo(int oid, int accId, string expressCompany, string expressCode, int uid, string uName)
        {
            T_OrderInfoDAL dal = new T_OrderInfoDAL();
            int reVal = dal.UpdateExpressAddress(oid, accId, expressCompany, expressCode);

            //获取实物商品名称
            string goodsName = dal.GetMaterialGoodsName(Convert.ToInt32(dal.GetBusIdByOid(oid)));

            var model = T_AccountBLL.GetAccountBasic(accId);
            string companyName = string.IsNullOrEmpty(model.CompanyName) ? "用户" : "【" + model.CompanyName + "】";

            //发货完成后推送消息
            //if (reVal == 1)
            //{                
            //    //string msgTitle = "发货提醒";
            //    //string msgContent = "尊敬的" + companyName + "，您购买的【" + goodsName + "】已经发货，承运物流是【" + expressCompany + "】，单号是【" + expressCode + "】，您可以登录物流公司官网查询" +
            //    //                    "订单配送状态。如果遇到任何问题，请您拨打400-600-6815联系我们。";
            //    //int sendMessage = Utility.MessageCenter.PostMessage(accId.ToString(), msgTitle, msgContent, uid, uName, null);
            //    //int sendMobileMessage = Utility.MessageCenter.PostMobileMessage(accId.ToString(), msgTitle, msgContent, uid, uName, null);
            //}

            return reVal;
        }

        public static List<OrderInfoModel> GetOrderInfoByDate(DateTime stDate, DateTime edDate, int isMaterial)
        {
            T_OrderInfoDAL dal = new T_OrderInfoDAL();
            return dal.GetOrderInfoByDate(stDate, edDate, isMaterial);
        }

        //public static OrderPartition GetOrderType(DateTime stDate, DateTime edDate)
        //{
        //    T_OrderInfoDAL dal = new T_OrderInfoDAL();
        //    return dal.GetOrderType(stDate, edDate);
        //}
        #region 退款处理

        /// <summary>
        /// 处理退款逻辑
        /// </summary>
        /// <param name="accid"></param>        
        /// <param name="oper"></param>
        /// <param name="oid"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public static bool DrawbackProcess(int accid, int oid, int oper, string desc)
        {
            T_OrderInfoDAL dal = new T_OrderInfoDAL();
            //获取退款信息
            DrawbackInfoModel drawbackModel = dal.GetDrawbackOrderInfo(oid);

            if (drawbackModel == null)
            {
                return false;//获取订单信息出错
            }
            else
            {
                if (dal.GeneralDrawbackProc(oper, desc, drawbackModel))
                {
                    return dal.UpdateOrderStatus(drawbackModel.RealPayMoney, oid, accid);
                }
                else
                {
                    return false;
                }
            }

        }
        #endregion

        #region 话费补充
        /// <summary>
        /// 话费补充记录
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="uid"></param>
        /// <param name="oid"></param>
        /// <param name="cardNum"></param>
        /// <param name="status"></param>
        /// <param name="desc"></param>
        /// <param name="newOrdNo"></param>
        /// <returns></returns>
        public static bool RechargeLog(int accid,int uid,int oid,int cardNum,int status,string desc,string newOrdNo)
        {
            RechargeLogModel model = new RechargeLogModel();
            T_OrderInfoDAL dal = new T_OrderInfoDAL();

            model.Accid = accid;
            model.CardNum = cardNum;
            model.Descrip = desc;
            model.NewOrdNo = newOrdNo;
            model.Oid = oid;
            model.Status = status;
            model.Uid = uid;

            return dal.AddRechargeLog(model) == 1 ? true : false;
        }

        #endregion

    }
}
