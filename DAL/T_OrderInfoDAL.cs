using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Model4View;
using Utility;

namespace DAL
{
    public class T_OrderInfoDAL : Base.T_OrderInfoBaseDAL
    {
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public List<OrderInfoModel> GetPage(int pageIndex, int pageSize, string Column, string strWhere)
        {
            List<OrderInfoModel> model = new List<OrderInfoModel>();

            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageSize = pageSize < 1 ? 20 : pageSize;

            //页数计算
            int bgNumber = ((pageIndex - 1) * pageSize) + 1;
            int edNumber = (pageIndex) * pageSize;

            StringBuilder strSql = new StringBuilder();

            strSql.Append("select * from (");
            strSql.Append("select ROW_NUMBER() over (order by oid desc) as rowNumber ");
            if (Column.Length > 0)
            {
                strSql.Append(" ," + Column + " ");
            }
            else
            {
                Column =
                    "oid,orderNo,dbo.GetAccountName(accId) accountName,accId,ta.UserRealName,datediff(dd,ta.LoginTimeLast,getdate()) as LoginLast,tb.aotjb as Edit,commuteIntegral,commuteIntegralMoney,dbo.GetOrderProjectName(busId) OrderProjectName,busId,orderPayType, busQuantity,busPrice,busSumMoney,couponMoney,couponId,confirmRemark,orderPayDesc, RealPayMoney,createDate, transactionDate, orderStatus, invoiceId,OrderTypeId,ReceivingAddressId,oFlag";
                strSql.Append(" ," + Column + " ");
            }
            strSql.Append(" from T_OrderInfo left join i200.dbo.T_Account ta on accId=ta.ID left join i200.dbo.T_Business tb on accId=tb.accountid ");

            if (strWhere.Length > 0)
            {
                strSql.Append(" where ");
            }
            strSql.Append(strWhere);
            strSql.Append(" ) t ");
            strSql.Append(" where t.rowNumber between @bgNumber and @edNumber; ");

            try
            {
                model = HelperForFrontend.Query<OrderInfoModel>(strSql.ToString(), new
                {
                    bgNumber = bgNumber,
                    edNumber = edNumber
                }).ToList();
            }
            catch (Exception ex)
            {
                model = null;
            }

            int count = 0;
            foreach (OrderInfoModel item in model)
            {
                if (item.orderStatus == 2)
                {
                    strSql.Clear();
                    strSql.Append(
                        "select count(*) from T_OrderInfo where accId=@accId and transactionDate<@date and orderStatus=2;");

                    try
                    {
                        count = HelperForFrontend.ExecuteScalar<int>(strSql.ToString(), new
                        {
                            accId = item.accId,
                            date = item.transactionDate
                        });
                    }
                    catch (Exception ex)
                    {
                        count = 1;
                    }

                    if (count == 0)
                    {
                        item.FirstFlag = "1";
                    }
                    else
                    {
                        item.FirstFlag = "0";
                    }
                }
                else
                {
                    item.FirstFlag = "0";
                }
            }

            return model;
        }

        /// <summary>
        /// 获取符合条件列表的总行数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int GetPageCount(string strWhere)
        {
            int count = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from T_OrderInfo ");

            if (strWhere.Length > 0)
            {
                strSql.Append(" where ");
            }
            strSql.Append(strWhere);

            try
            {
                count = HelperForFrontend.Query<int>(strSql.ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                count = 0;
            }

            return count;
        }

        /// <summary>
        /// 根据条件返回订单金额总和
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public decimal GetSumByCondition(string strWhere)
        {
            decimal count = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(RealPayMoney) from T_OrderInfo ");

            if (strWhere.Length > 0)
            {
                strSql.Append(" where ");
            }
            strSql.Append(strWhere);

            try
            {
                count = HelperForFrontend.Query<int>(strSql.ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                count = 0;
            }

            return count;
        }

        public decimal GetIntegralByCondition(string strWhere)
        {
            decimal count = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(commuteIntegralMoney) from T_OrderInfo ");

            if (strWhere.Length > 0)
            {
                strSql.Append(" where ");
            }
            strSql.Append(strWhere);

            try
            {
                count = HelperForFrontend.Query<int>(strSql.ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                count = 0;
            }

            return count;
        }

        public decimal GetIntegralSumByCondition(string strWhere)
        {
            decimal count = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(commuteIntegral) from T_OrderInfo ");

            if (strWhere.Length > 0)
            {
                strSql.Append(" where ");
            }
            strSql.Append(strWhere);

            try
            {
                count = HelperForFrontend.Query<int>(strSql.ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                count = 0;
            }

            return count;
        }

        public decimal GetCouponByCondition(string strWhere)
        {
            decimal count = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(couponMoney) from T_OrderInfo ");

            if (strWhere.Length > 0)
            {
                strSql.Append(" where ");
            }
            strSql.Append(strWhere);

            try
            {
                count = HelperForFrontend.Query<int>(strSql.ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                count = 0;
            }

            return count;
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
        public bool SetConfirmInfo(int oid, decimal confirmMoney, string confirmRemark, int confirmOpid, string confirmOpIp, DateTime confirmOpTime)
        {
            bool status = false;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_OrderInfo set " +
                          "confirmMoney=@confirmMoney," +
                          "confirmRemark=@confirmRemark," +
                          "confirmOpId=@confirmOpId," +
                          "confirmOpIp=@confirmOpIp," +
                          "confirmOpTime=@confirmOpTime," +
                          "orderStatus=1," +
                          "transactionDate=getdate() where oid=@oid");

            try
            {
                int result = HelperForFrontend.Execute(strSql.ToString(), new
                {
                    oid = oid,
                    confirmMoney = confirmMoney,
                    confirmRemark = confirmRemark,
                    confirmOpId = confirmOpid,
                    confirmOpIp = confirmOpIp,
                    confirmOpTime = confirmOpTime
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
        /// 查询数据列表，返回包括产品信息
        /// <para>列名，条件中请增加表前缀</para>
        /// </summary>
        /// <param name="Column">列名，请增加表名前缀 <para>例：T_OrderInfo.busId</para></param>
        /// <param name="strWhere">条件，请增加表名前缀 <para>例：T_OrderInfo.busId=10</para></param>
        /// <returns></returns>
        public List<dynamic> GetListContainOrderBusiness(string Column, string strWhere)
        {
            List<dynamic> dynamicList = new List<dynamic>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Column.Trim().Length > 0)
            {
                strSql.Append(" " + Column + " ");
            }
            else
            {
                strSql.Append(" * ");
            }
            strSql.Append(" FROM i200.dbo.T_OrderInfo ");
            strSql.Append(" inner join i200.dbo.T_Order_Project on T_OrderInfo.busId=T_Order_Project.busid ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            try
            {
                dynamicList = DapperHelper.Query<dynamic>(strSql.ToString()).ToList();
            }
            catch (Exception ex)
            {
                dynamicList = null;
            }

            return dynamicList;
        }
        /// <summary>
        /// 得到一个店铺的订单信息
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public List<dynamic> GetListByAccId(int accid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select p.displayName opName,b.busId , busSumMoney,transactionDate,RealPayMoney,0 LoginTimes,orderPayType,b.commuteIntegral,b.commuteIntegralMoney,b.couponMoney,orderTypeId,b.invoiceId,i.invoiceStatus  from ( ");
            strSql.Append(" select busid , busSumMoney,transactionDate,RealPayMoney,0 LoginTimes,orderPayType,commuteIntegral,commuteIntegralMoney,couponMoney,orderTypeId,invoiceId ");
            strSql.Append(" from i200.dbo.T_OrderInfo where  ");
            strSql.Append("  orderStatus=2 and accId=@accId ) b ");
            strSql.Append(" left outer join i200.dbo.T_Order_Project p on p.busId=b.busId " +
                          " left join i200.dbo.T_Order_Invoice i on b.invoiceId=i.id order by  transactionDate desc ");
            return HelperForFrontend.Query(strSql.ToString(), new { accId = accid }).ToList();

        }

        /// <summary>
        /// 获取用户的实物订单
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public List<dynamic> GetMaterialListByAccId(int accid)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select g.AliasName opName,b.busId , busSumMoney,transactionDate,RealPayMoney,0 LoginTimes,orderPayType,b.commuteIntegral,b.commuteIntegralMoney,b.couponMoney,orderTypeId,b.invoiceId,i.invoiceStatus from ( ");
            strSql.Append(" select busid , busSumMoney,transactionDate,RealPayMoney,0 LoginTimes,orderPayType,commuteIntegral,commuteIntegralMoney,couponMoney,orderTypeId,invoiceId ");
            strSql.Append(" from i200.dbo.T_OrderInfo where  ");
            strSql.Append("  orderStatus=1 and OrderTypeId=2 and accId=@accId ) b ");
            strSql.Append(" left outer join [i200].[dbo].[T_MaterialGoods] g on g.GoodsId=b.busId " +
                          " left join i200.dbo.T_Order_Invoice i on b.invoiceId=i.id order by  transactionDate desc ");
            return HelperForFrontend.Query(strSql.ToString(), new { accId = accid }).ToList();

        }

        /// <summary>
        /// 订单分析
        /// </summary>
        /// <param name="statTime"></param>
        /// <param name="endTime"></param>
        /// <param name="sourceList"></param>
        /// <returns></returns>
        public List<SourceAnalyzeModel> OrderSourceAnalyze(DateTime startTime, DateTime endTime, int[] sourceList)
        {

            string sourceStr = "";
            if (sourceList.Length > 0)
            {
                foreach (int id in sourceList)
                {
                    sourceStr += "," + id.ToString();
                }
            }
            if (sourceStr.Length > 0)
            {
                sourceStr = sourceStr.Trim(',');
            }
            else
            {
                sourceStr = "21,22,23,24";
            }

            StringBuilder strSql = new StringBuilder();

            strSql.Append(" create table #table (id int,purchaseTime datetime,sumMoney decimal(18,2),tag_id int) ");
            strSql.Append(" insert into #table(id,purchaseTime,sumMoney) ");
            strSql.Append(" select accId,CAST(transactionDate as date),RealPayMoney from i200.dbo.T_OrderInfo  ");
            strSql.Append(" where transactionDate>@statTime and transactionDate<@endTime and orderStatus=2   ");
            strSql.Append(" update #table set tag_id=a.tag_id from (select id,acc_id,tag_id from Sys_TagNexus where  ");
            strSql.Append(" tag_id in(" + sourceStr + ") and acc_id in(select ID from #table)) a where a.acc_id=#table.id; ");
            strSql.Append(" update #table set tag_id=24 where tag_id is null; ");
            strSql.Append(" select purchaseTime,tag_id,sum(sumMoney) countNum from #table group by purchaseTime,tag_id; ");
            strSql.Append(" drop table #table; ");

            List<dynamic> dataList = DapperHelper.Query(strSql.ToString(), new { statTime = startTime, endTime = endTime }).ToList();

            Dictionary<string, SourceAnalyzeModel> sourceModleList = new Dictionary<string, SourceAnalyzeModel>();
            foreach (dynamic item in sourceModleList)
            {
                string timeString = Convert.ToDateTime(item.purchaseTime).ToString("yyyy-MM-dd");
                if (!sourceModleList.ContainsKey(timeString))
                {
                    sourceModleList[timeString] = new SourceAnalyzeModel()
                    {
                        DateTime = Convert.ToDateTime(item.purchaseTime)
                    };
                }
                int sourceid = Convert.ToInt32(item.tag_id);

                SourceAnalyzeItemList sourceItemList = new SourceAnalyzeItemList();
                sourceItemList.SourceId = sourceid;
                sourceItemList.CountValue = Convert.ToDecimal(item.countNum);

                sourceModleList[timeString].ItemList.Add(sourceid.ToString(), sourceItemList);

                sourceModleList[timeString].CountValue += sourceItemList.CountValue;
                sourceModleList[timeString].count++;
            }
            List<SourceAnalyzeModel> modelList = new List<SourceAnalyzeModel>();

            foreach (KeyValuePair<string, SourceAnalyzeModel> keyValue in sourceModleList)
            {
                SourceAnalyzeModel sm = keyValue.Value;
                foreach (KeyValuePair<string, SourceAnalyzeItemList> il in sm.ItemList)
                {
                    il.Value.ValueScore = (il.Value.CountValue / sm.CountValue);
                    il.Value.weekend = ((int)sm.DateTime.DayOfWeek).ToString();
                }


                modelList.Add(sm);
            }


            return modelList;

        }

        public List<NewOrderProduct> GetNewUsrOrder(DateTime stDate, DateTime edDate)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select t.accid,t.busid,t.transactionDate into #List from i200.dbo.T_OrderInfo t " +
                          "left join i200.dbo.T_Account on t.accid=i200.dbo.T_Account.id " +
                          "where DATEDIFF(DAY,t.transactionDate,i200.dbo.T_Account.RegTime)=0;" +
                          "select #List.accId,i200.dbo.T_Order_Project.displayName from #List " +
                          "left join i200.dbo.T_Order_Project on #List.busId=i200.dbo.T_Order_Project.busId where #List.transactionDate between @stDate and @edDate and i200.dbo.T_Order_Project.displayName is not null;" +
                          "drop table #List;");

            List<NewOrderProduct> list =
                DapperHelper.Query<NewOrderProduct>(strSql.ToString(), new { stDate = stDate, edDate = edDate }).ToList();

            return list;
        }

        /// <summary>
        /// 更新订单发票信息
        /// </summary>
        /// <param name="accId"></param>
        /// <param name="orderId"></param>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public bool SetInvoiceId(int accId, int orderId, int invoiceId)
        {
            bool bResult = false;
            var strSql = new StringBuilder();
            strSql.Append(" update T_OrderInfo set invoiceId=@invoiceId");
            strSql.Append(" where accId=@accId and oid=@oid;");

            int iResult = HelperForFrontend.Execute(strSql.ToString(), new
            {
                accId = accId,
                oid = orderId,
                invoiceId = invoiceId
            });

            if (iResult > 0)
            {
                bResult = true;
            }

            return bResult;
        }

        /// <summary>
        /// 获取第三方充值订单列表补充信息
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public dynamic GetRechargeExtendInfo(string orderNo)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select top 1 * from i200.dbo.T_Orderinfo where orderNo=@orderNo;");

            try
            {
                dynamic item = DapperHelper.Query<dynamic>(strSql.ToString(), new { orderNo = orderNo }).ToList()[0];

                return item;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取实物名称
        /// </summary>
        /// <param name="busId"></param>
        /// <returns></returns>
        public string GetMaterialGoodsName(int busId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AliasName from [i200].[dbo].[T_MaterialGoods] where GoodsId=@id;");

            try
            {
                return DapperHelper.ExecuteScalar<string>(strSql.ToString(), new { id = busId });
            }
            catch (Exception ex)
            {
                Logger.Error("获取实物名称出错", ex);
                return "";
            }
        }

        public string GetBusIdByOid(int oid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BusId from [i200].[dbo].[T_OrderInfo] where oid=@id;");

            try
            {
                return DapperHelper.ExecuteScalar<string>(strSql.ToString(), new { id = oid });
            }
            catch (Exception ex)
            {
                Logger.Error("获取BusId出错", ex);
                return "";
            }
        }

        /// <summary>
        /// 获取实物商品的地址信息
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        public MaterialAddressInfo GetMaterialAddressInfo(int addressId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select ConsigneeName,ProviceName+' '+CityName+' '+CountryName+' '+AddressDetail Address,TelNumber,ExpressCode,ExpressCompany from [i200].[dbo].[T_Receiving_Address] " +
                "where AddressId=@addressId");

            try
            {
                return DapperHelper.Query<MaterialAddressInfo>(strSql.ToString(), new { addressId = addressId }).ToList()[0];
            }
            catch (Exception ex)
            {
                Logger.Error("获取实物商品地址出错", ex);
                return null;
            }

        }

        public int UpdateExpressAddress(int oid, int accId, string expressCompany, string expressCode)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select ReceivingAddressId from [i200].[dbo].[T_OrderInfo] where oid=@oid and accId=@accId;");
            int receiveId = DapperHelper.ExecuteScalar<int>(strSql.ToString(), new
            {
                oid = oid,
                accId = accId
            });

            strSql.Clear();
            strSql.Append(
                "update [i200].[dbo].[T_Receiving_Address] set ExpressCompany=@expressCompany,ExpressCode=@expressCode where AddressId=@addressId;");

            try
            {
                int reVal = HelperForFrontend.Execute(strSql.ToString(), new
                {
                    expressCompany = expressCompany,
                    expressCode = expressCode,
                    addressId = receiveId
                });

                return reVal;
            }
            catch (Exception ex)
            {
                Logger.Error("更新实物商品物流信息出错！", ex);
                return 0;
            }
        }

        public int DeleteOrder(int oid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from i200.dbo.T_Orderinfo where oid=@oid;");

            try
            {
                return HelperForFrontend.Execute(strSql.ToString(), new { oid = oid });
            }
            catch (Exception ex)
            {
                Logger.Error("删除订单出错", ex);
                return 0;
            }
        }

        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <param name="isMaterial">1-实物订单 3-京东订单</param>
        /// <returns></returns>
        public List<OrderInfoModel> GetOrderInfoByDate(DateTime stDate, DateTime edDate, int isMaterial)
        {
            StringBuilder strSql = new StringBuilder();

            if (isMaterial == 0)
            {
                strSql.Append("select * from i200.dbo.T_orderinfo where createDate between @stDate and @edDate and orderStatus=2 and (OrderTypeId=1 or OrderTypeId=4 or OrderTypeId is null);");
            }
            else if (isMaterial == 2)
            {
                strSql.Append(
                    "select * from i200.dbo.T_orderinfo where createDate between @stDate and @edDate and orderStatus=2 and OrderTypeId=3;");
            }
            else
            {
                strSql.Append(
                    "select * from i200.dbo.T_orderinfo where createDate between @stDate and @edDate and orderStatus=1 and OrderTypeId=2;");
            }

            return
                DapperHelper.Query<OrderInfoModel>(strSql.ToString(), new { stDate = stDate, edDate = edDate }).ToList();
        }

        //public OrderPartition GetOrderType(DateTime stDate, DateTime edDate)
        //{
        //    StringBuilder strSql = new StringBuilder();

        //    strSql.Append("")
        //}

        #region 退款相关操作
        /// <summary>
        /// 获取退款信息
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public DrawbackInfoModel GetDrawbackOrderInfo(int oid)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select oid,createDate,busId,orderTypeId,accid,RealPayMoney,busQuantity from i200.dbo.T_OrderInfo " +
                          "where oid=@oid;");

            try
            {
                return DapperHelper.GetModel<DrawbackInfoModel>(strSql.ToString(), new { oid = oid });
            }
            catch (Exception ex)
            {
                Logger.Error("获取退款订单信息出错！", ex);
                return null;
            }
        }

        /// <summary>
        /// 根据busId和订单类型获取产品名称
        /// </summary>
        /// <param name="busId"></param>
        /// <param name="orderTypeId">订单类型
        /// 1-Saas订单 
        /// 2-实物订单
        /// 3-京东订单
        /// 4-话费订单
        /// </param>
        /// <returns></returns>
        public string GetProductName(int busId, int orderTypeId)
        {
            StringBuilder strSql = new StringBuilder();

            switch (orderTypeId)
            {
                case 1:
                case 4:
                    strSql.Append("select displayName from [i200].[dbo].[T_Order_Project] where busId=@busId;");
                    break;
                case 2:
                    strSql.Append("select AliasName from [i200].[dbo].[T_MaterialGoods] where GoodsId=@busId;");
                    break;
                case 3:
                    return "京东订单";
            }

            try
            {
                return DapperHelper.ExecuteScalar<string>(strSql.ToString(), new { busId = busId });
            }
            catch (Exception ex)
            {
                Logger.Error("退货时获取产品名称出错！", ex);
                return "";
            }
        }

        /// <summary>
        /// 根据产品Id获取产品子项
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public List<OrderProjectItem> GetProductItemId(int oid)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select itemId,itemName,itemQuantity,oid from [i200].[dbo].[T_Order_List] where oid=@oid;");

            try
            {
                return DapperHelper.Query<OrderProjectItem>(strSql.ToString(), new { oid = oid }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("获取打包产品的子产品出错！", ex);
                throw;
            }
        }

        /// <summary>
        /// 退款通用处理逻辑
        /// </summary>
        /// <param name="model"></param>
        /// <param name="oper"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public bool GeneralDrawbackProc(int oper, string desc, DrawbackInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            bool mark = true;

            List<OrderProjectItem> itemList = new List<OrderProjectItem>();

            model.pName = GetProductName(model.busId, model.orderTypeId);

            //获取订单包含的业务
            if (model.orderTypeId == 1)
            {
                itemList = GetProductItemId(model.oid);
            }


            if (model.orderTypeId != 1)//实物、京东、手机订单只更新状态
            {
                AddDrawbackRec(model.oid, model.accid, model.pName, oper, 1, model.RealPayMoney, model.busQuantity,desc);
            }
            else
            {
                foreach (var item in itemList)
                {
                    if (item.itemId == 3)//高级版处理
                    {
                        mark = mark && AdvanceDrawback(model.accid, oper, item);
                    }
                    else if (item.itemId == 1)//短信处理
                    {
                        mark = mark && SmsDrawback(model.accid, oper, item);
                    }
                    else
                    {
                        mark = mark && AppDrawback(model.accid, oper, item);
                    }
                }
            }

            if (mark)
            {
                AddDrawbackRec(model.oid, model.accid, model.pName, oper, 1, model.RealPayMoney, model.busQuantity, desc);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 高级版退款处理
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="oper"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AdvanceDrawback(int accid, int oper, OrderProjectItem model)
        {
            //DateTime nowDate = DateTime.Now;
            bool status = false;

            StringBuilder strSql = new StringBuilder();

            strSql.Append("update [i200].[dbo].[T_Business] set endtime=DATEADD(MONTH,@quantity,endtime) where accountid=@accid;");

            try
            {
                int reVal = HelperForFrontend.Execute(strSql.ToString(), new { quantity = -model.itemQuantity, accid = accid });

                if (reVal > 0)
                {
                    AddDrawbackRec(model.oid, accid, model.itemName, oper, 2, 0, model.itemQuantity, "");
                    return true;//高级版退款成功
                }
                else
                {
                    Logger.Info("高级版退款没有找到对应记录！" + DateTime.Now + "-" + accid);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("高级版退款（更新Business表）出错！", ex);
                return false;
            }
        }

        /// <summary>
        /// 短信退款处理
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="oper"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SmsDrawback(int accid, int oper, OrderProjectItem model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("declare @dx int;" +
                          "set @dx=0;" +
                          "" +
                          "select @dx=dxunity from [i200].[dbo].[T_Business] where accountid=@accid; " +
                          "if @dx>=@quantity " +
                          "update [i200].[dbo].[T_Business] set dxunity=dxunity-@quantity where accountid=@accid " +
                          "else " +
                          "update [i200].[dbo].[T_Business] set dxunity=0 where accountid=@accid ;");

            try
            {
                int reVal = HelperForFrontend.Execute(strSql.ToString(), new { quantity = model.itemQuantity, accid = accid });

                if (reVal > 0)
                {
                    AddDrawbackRec(model.oid, accid, model.itemName, oper, 2, 0, model.itemQuantity, "");
                    return true;//短信条数重置成功
                }
                else
                {
                    Logger.Info("短信退款重置没有找到对应记录！" + DateTime.Now + "-" + accid);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("短信退款重置（更新Business表）出错！", ex);
                return false;
            }
        }

        /// <summary>
        /// App退款处理
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="oper"></param>
        /// <param name="model"></param>        
        /// <returns></returns>
        public bool AppDrawback(int accid, int oper, OrderProjectItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "update [i200].[dbo].[t_App_Au] set endtime=DATEADD(MONTH,@quantity,endtime) where accid=@accid ");

            switch (model.itemId)
            {
                case 4://手机橱窗
                    strSql.Append(" and appkey=1 and aa_Status=1;");
                    break;
                case 5://优惠券
                    strSql.Append(" and appkey=2 and aa_Status=1;");
                    break;
                case 8://暂结账（挂单）
                    strSql.Append(" and appkey=3;");
                    break;
                case 9://分店管理
                    strSql.Append(" and appkey=5;");
                    break;
                case 13://维客短信_会员生日
                    strSql.Append(" and appkey=11;");
                    break;
                case 14://维客短信_电子优惠券
                    strSql.Append(" and appkey=12;");
                    break;
                case 15://维客短信_充值提醒
                    strSql.Append(" and appkey=13;");
                    break;
                case 16://维客短信_店铺经营
                    strSql.Append(" and appkey=14;");
                    break;
                case 17://维客短信_电子账单
                    strSql.Append(" and appkey=15;");
                    break;
                case 18://维客短信_会员注册
                    strSql.Append(" and appkey=16;");
                    break;
                case 19://微信营销
                    strSql.Append(" and appkey=19;");
                    break;
                case 20://供货商
                    strSql.Append(" and appkey=20;");
                    break;
            }

            //如果是维客短信添加处理
            if (model.itemId== 13|| model.itemId == 14 || model.itemId == 15 
                || model.itemId == 16 || model.itemId == 17 || model.itemId == 18)
            {
                //判断购买前是否已经存在套餐
                if (GetSmsEndTime(model.itemId-2, accid)<DateTime.Now)
                {
                    strSql.Append(
                        "update [i200].[dbo].[T_Business] set smsbilling=null where accountid=@accid;");
                }                
            }

            try
            {
                int reVal = HelperForFrontend.Execute(strSql.ToString(), new
                {
                    quantity = -model.itemQuantity,
                    accid = accid
                });

                if (reVal > 0)
                {
                    AddDrawbackRec(model.oid, accid, model.itemName, oper, 2, 0, model.itemQuantity,"");
                    return true;//退款处理App表成功
                }
                else
                {
                    Logger.Info("App退款重置没有找到对应记录！" + DateTime.Now + "-" + accid);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("App退款（更新App表）出错！", ex);
                return false;
            }

        }

        /// <summary>
        /// 获取维客短信到期时间
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="accid"></param>
        /// <returns></returns>
        public DateTime GetSmsEndTime(int appKey,int accid)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select endtime from [i200].[dbo].[t_App_Au] where accid=@accid and appKey=@appKey;");

            return DapperHelper.ExecuteScalar<DateTime>(strSql.ToString(), new {accid = accid, appKey = appKey});
        }

        /// <summary>
        /// 退款日志
        /// </summary>
        /// <param name="oid">订单号</param>
        /// <param name="name">名称</param>
        /// <param name="oper">操作者</param>
        /// <param name="accid">店铺ID</param>
        /// <param name="type">
        /// 类型
        /// 1-完整订单
        /// 2-订单详情
        /// </param>
        /// <param name="realMoney">
        /// 退款金额
        /// </param>
        /// <param name="quantity">数量</param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public int AddDrawbackRec(int oid, int accid, string name, int oper, int type, decimal realMoney, int quantity,string desc)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("insert into Sys_DrawbackRec (Oid,AccId,Name,Oper,Type,ReturnMoney,Quantity,OperTime,Descrip) " +
                          "Values(@oid,@accid,@name,@oper,@type,@realMoney,@quantity,getdate(),@desc);");

            try
            {
                return DapperHelper.Execute(strSql.ToString(), new
                {
                    oid = oid,
                    accid = accid,
                    name = name,
                    oper = oper,
                    type = type,
                    realMoney = realMoney,
                    quantity = quantity,
                    desc= desc
                });
            }
            catch (Exception ex)
            {
                Logger.Error("增加退款日志记录出错！", ex);
                return 0;
            }

        }

        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="returnMoney"></param>
        /// <param name="oid"></param>
        /// <param name="accId"></param>
        /// <returns></returns>
        public bool UpdateOrderStatus(decimal returnMoney, int oid, int accId)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(
                "update [i200].[dbo].[T_OrderInfo] set orderStatus=-2,returnMoney=@money where oid=@oid and accId=@accId; ");

            try
            {
                int reVal = HelperForFrontend.Execute(strSql.ToString(), new
                {
                    money = returnMoney,
                    oid = oid,
                    accId = accId
                });

                if (reVal > 0)
                {
                    return true;
                }
                else
                {
                    Logger.Info("没有找到订单记录！" + oid);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("退款更新订单状态出错！", ex);
                return false;
            }

        }

        #endregion

        #region 话费补充日志
        /// <summary>
        /// 添加话费充值日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddRechargeLog(RechargeLogModel model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("insert into Sys_RechargeLog (Accid,Uid,Oid,CardNum,Status,Descrip,NewOrdNo) " +
                          "values (@Accid,@Uid,@Oid,@CardNum,@Status,@Descrip,@NewOrdNo);");

            try
            {
                return DapperHelper.Execute(strSql.ToString(), model);
            }
            catch (Exception ex)
            {
                Logger.Error("添加话费补充日志出错！", ex);
                return 0;
            }
        }

        #endregion
    }
}
