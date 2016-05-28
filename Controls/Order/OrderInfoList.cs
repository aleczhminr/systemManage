using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using BLL;
using Model.Model4View;
using Controls.MobileRechargeAPI;
using Microsoft.SqlServer.Server;
using Utility;

namespace Controls.Order
{
    public class OrderInfoList
    {
        /// <summary>
        /// 获取分页后的订单信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="Column"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public static List<OrderInfoModel> GetPage(int pageIndex, int pageSize, string Column, string strWhere)
        {
            return T_OrderInfoBLL.GetPage(pageIndex, pageSize, Column, strWhere);
        }

        /// <summary>
        /// 获取符合条件列表的行数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public static int GetPageCount(string strWhere)
        {
            return T_OrderInfoBLL.GetPageCount(strWhere);
        }

        public static decimal GetSumByCondition(string strWhere)
        {
            return T_OrderInfoBLL.GetSumByCondition(strWhere);
        }

        public static decimal GetIntegralByCondition(string strWhere)
        {
            return T_OrderInfoBLL.GetIntegralByCondition(strWhere);
        }

        public static decimal GetIntegralSumByCondition(string strWhere)
        {
            return T_OrderInfoBLL.GetIntegralSumByCondition(strWhere);
        }

        public static decimal GetCouponByCondition(string strWhere)
        {
            return T_OrderInfoBLL.GetCouponByCondition(strWhere);
        }

        /// <summary>
        /// 获取发票列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="strWhere"></param>
        /// <param name="orderWhere"></param>
        /// <returns></returns>
        public static OrderInvoiceList GetInovicePage(int pageIndex, int pageSize, string strWhere, string orderWhere)
        {
            return T_Order_InvoiceBLL.GetPage(pageIndex, pageSize, strWhere, orderWhere);
        }

        public static InvoiceSimple GetInvoiceInfo(int id)
        {
            return T_Order_InvoiceBLL.GetInvoiceInfo(id);
        }

        /// <summary>
        /// 更新发票信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="invoiceNum"></param>
        /// <param name="remark"></param>
        /// <param name="operId"></param>
        /// <returns></returns>
        public static bool UpdateInvoiceNumber(int id, string invoiceNum, string remark, string express, int operId)
        {
            return T_Order_InvoiceBLL.UpdateInvoiceNumber(id, invoiceNum, remark, express, operId);
        }

        /// <summary>
        /// 获取充值状态列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="phoneNum"></param>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public static string GetRechargeOrderList(int page, string phoneNum, string orderNo)
        {
            var model = MobileRecharge.GetHistoryOrder(page, 15, phoneNum, orderNo);
            if (model.error_code == 0)
            {
                if (model.result.data != null)
                {
                    foreach (var item in model.result.data)
                    {
                        //获取当前最后记录对应的订单ID
                        //int lastOid = Sys_RechargeRecordBLL.GetLastOid();                        

                        var tempModel = T_OrderInfoBLL.GetRechargeOrderListExtend(item.uorderid);

                        if (tempModel != null)
                        {
                            //更新的订单如果在记录表没有记录则插入信息
                            if (Sys_RechargeRecordBLL.CheckExist(tempModel.oid) == 0)
                            {
                                RechargeRecord recordModel = new RechargeRecord();
                                recordModel.AccId = tempModel.accId;
                                recordModel.AddTime = item.addtime;
                                recordModel.CardName = item.cardname;
                                recordModel.Oid = tempModel.oid;
                                recordModel.OrderNo = item.uorderid;
                                recordModel.State = item.game_state;
                                recordModel.CardNum = Convert.ToInt32(item.cardnum);
                                recordModel.RealNum = Convert.ToDecimal(item.uordercash);
                                recordModel.PaidNum = tempModel.RealPayMoney;
                                recordModel.GapNum = recordModel.RealNum - recordModel.PaidNum;

                                Sys_RechargeRecordBLL.AddNewRecord(recordModel);
                            }

                            item.accModel = T_AccountBLL.GetAccountBasic(tempModel.accId);

                        }

                    }

                    model.result.Summary = Sys_RechargeRecordBLL.GetRecordSum();

                    return CommonLib.Helper.JsonSerializeObject(model, "yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    return "";
                }
            }
            return CommonLib.Helper.JsonSerializeObject(model);
        }

        public static string GetJuheAccountBalance()
        {
            var model = MobileRecharge.GetBalance();
            string SendMobileRechargeAmounts = WeiXinDataMobileRechargeAPI.SendMobileRechargeAmounts(model.result.money);
            return CommonLib.Helper.JsonSerializeObject(model);
        }

        /// <summary>
        /// 补充话费并记录日志
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="oid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static string RechargeProc(int accid, int oid, int uid)
        {
            //获取订单相关信息
            T_OrderInfo mobileModel = T_OrderInfoBLL.GetModel(oid);
            string newOrdNum = mobileModel.orderNo.Substring(0, mobileModel.orderNo.Length - 2);

            Random rd = new Random();
            var randNum = newOrdNum + rd.Next(1, 100);

            while (randNum == mobileModel.orderNo)
            {
                randNum = newOrdNum + rd.Next(1, 100);
            }

            string cardNum = "";
            if (mobileModel.busPrice / 10 >= 9)
            {
                cardNum = "100";
            }
            else if (mobileModel.busPrice / 10 >= 4)
            {
                cardNum = "50";
            }
            else if (mobileModel.busPrice / 10 >= 2)
            {
                cardNum = "30";
            }
            
            //去掉手机号中的空格
            string phoneNum = mobileModel.remark.Replace(" ", "");            

            //调用话费补充接口
            var result = MobileRecharge.MobileCharge(phoneNum, cardNum, randNum);
            //var result = MobileRecharge.CheckPhoneNum(mobileModel.remark, cardNum);
            //string errInfo = "";

            if (result.error_code == 0)
            {
                //记录话费补充日志
                if (T_OrderInfoBLL.RechargeLog(accid, uid, oid, (int)mobileModel.busPrice, 0, result.reason, randNum))
                {
                    return "1";
                }
                else
                {
                    Logger.Info("补充提交成功，记录日志出错！");
                    return "补充提交成功，记录日志出错！";
                }

            }
            else
            {
                if (T_OrderInfoBLL.RechargeLog(accid, uid, oid, (int)mobileModel.busPrice, 1, result.reason, randNum))
                {
                    return "补充提交失败【" + result.reason + "】！";
                }
                else
                {
                    Logger.Info("补充提交失败，记录日志出错！");
                    return "补充提交失败【" + result.reason + "】，记录日志出错！";
                }
                //return "补充接口返回错误码！" + result.error_code;
            }

        }
    }
}
