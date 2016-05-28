using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DAL
{
    public class T_Order_businessDAL
    {
        /// <summary>
        /// 产品订单分析
        /// </summary>
        /// <returns>{bus_name:名称,num:总数,quantity:数量,smsNum:短信数量,accNum:版本月数,money:金额,baifen:金额比,bus_mclass:类别}</returns>
        public OrderAnalysis GetBussinessOrderAnalyse(DateTime stDate, DateTime edDate)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("  select oid,busId,busQuantity,RealPayMoney,(case when busId=0 then remark else '' end ) busName,null smsCount,null banCount,0 bus_mclass  into #list  from T_OrderInfo   ");
            strSql.Append("     where  createDate<= @endTime and createDate >= @statTime and ");
            strSql.Append(" orderStatus=2 and (OrderTypeId=1 or OrderTypeId=4); ");


            strSql.Append(" update #list set smsCount=a.smsCount , banCount=a.banCount from( ");
            strSql.Append(" select oid,Sum(case when itemId=1 then itemQuantity else 0 end) smsCount,Sum(case when itemId=3 then (itemQuantity *2) when itemId=2 then itemQuantity else 0 end) banCount ");
            strSql.Append("  from T_Order_List where oid in(select oid from #list) group  by oid) a  where a.oid=#list.oid; ");
            strSql.Append(" if(EXISTS(select * from #list where smsCount is null)) ");
            strSql.Append(" begin ");
            strSql.Append(" 	update #list set smsCount=a.smsCount , banCount=a.banCount from( ");
            strSql.Append(" 	select busId,Sum(case when itemId=1 then itemQuantity else 0 end) smsCount,Sum(case when itemId=3 then (itemQuantity*2) when  ");
            strSql.Append(" 	itemId=2 then itemQuantity else 0 end) banCount from T_Order_Project_List where busId in(select busId from #list where smsCount is null) group by busId ");
            strSql.Append(" 	) a  where #list.smsCount is null and #list.banCount is null and #list.busId=a.busId ; ");
            strSql.Append(" end ");
            strSql.Append(" update #list set busName=T_Order_Project.displayName ,bus_mclass=projectType from T_Order_Project where T_Order_Project.busId=#list.busId ; ");
            strSql.Append(" select busName,bus_mclass,COUNT(oid) num,SUM(busQuantity) quantity,SUM(smsCount * busQuantity) smsNum,SUM(banCount * busQuantity) accNum, ");
            strSql.Append(" SUM(RealPayMoney) [money] from #list group by busName,bus_mclass ");

            #region 获取其他类型订单数据

            strSql.Append(" union all ");
            strSql.Append(" select mg.AliasName busName,5 bus_mclass,COUNT(oi.oid) num,COUNT(oi.oid) quantity, ");
            strSql.Append(" 0 smsNum,0 accNum,SUM(oi.RealPayMoney) money from i200.dbo.T_OrderInfo oi  ");
            strSql.Append(" left join [I200].[dbo].[T_MaterialGoods] mg on oi.busId=mg.GoodsId ");
            strSql.Append(" where oi.OrderTypeId=2 and orderStatus=1 and createDate<= @endTime and createDate >= @statTime ");
            strSql.Append(" group by mg.AliasName ");
            strSql.Append(" union all  ");
            strSql.Append(" select '京东日百供货' busName,6 bus_mclass,COUNT(oid) num,COUNT(oid) quantity,0 smsNum,0 accNum,SUM(RealPayMoney) money ");
            strSql.Append(" from i200.dbo.T_OrderInfo where OrderTypeId=3 and orderStatus=2 and createDate<= @endTime and createDate >= @statTime;  ");
            #endregion

            strSql.Append(" drop table #list; ");


            List<dynamic> ds =
                HelperForFrontend.Query<dynamic>(strSql.ToString(), new { endTime = edDate, statTime = stDate }).ToList();


            int allNum = 0;
            int allQuantity = 0;
            int allSmsNum = 0;
            int allAccNum = 0;
            int allMoney = 0;
            OrderAnalysis orderAM = new OrderAnalysis();
            if (ds.Count > 0)
            {
                foreach (dynamic dr in ds)
                {
                    if (dr.num != 0)
                    {
                        OrderAnalysisItem itemModel = new OrderAnalysisItem();

                        if (dr.busName != null && dr.busName != "")
                        {
                            itemModel.busName = dr.busName.ToString();
                        }
                        if (dr.bus_mclass != null && dr.bus_mclass != 0)
                        {
                            int bus_m = Convert.ToInt32(dr.bus_mclass);
                            if (bus_m == 1)
                            {
                                itemModel.bus_mclass = "短信";
                            }
                            else if (bus_m == 2)
                            {
                                itemModel.bus_mclass = "版本";
                            }
                            else if (bus_m == 3)
                            {
                                itemModel.bus_mclass = "产品";
                            }
                            else if (bus_m == 4)
                            {
                                itemModel.bus_mclass = "套餐";
                            }
                            else if (bus_m == 5)
                            {
                                itemModel.bus_mclass = "个";
                            }
                            else if (bus_m == 6)
                            {
                                itemModel.bus_mclass = "批";
                            }
                        }
                        if (dr.num != null && dr.num != 0)
                        {
                            itemModel.num = Convert.ToInt32(dr.num);
                        }
                        if (dr.quantity != null && dr.quantity != 0)
                        {
                            itemModel.quantity = Convert.ToInt32(dr.quantity);
                        }
                        if (dr.smsNum != null && dr.smsNum != 0)
                        {
                            itemModel.smsNum = Convert.ToInt32(dr.smsNum);
                        }
                        if (dr.accNum != null && dr.accNum != 0)
                        {
                            itemModel.accNum = Convert.ToInt32(dr.accNum) / 2;
                        }
                        if (dr.money != null && dr.money != 0)
                        {
                            itemModel.money = Convert.ToInt32(dr.money);
                        }

                        allNum += itemModel.num;
                        allQuantity += itemModel.quantity;
                        allSmsNum += itemModel.smsNum;
                        allAccNum += itemModel.accNum;
                        allMoney += itemModel.money;
                        orderAM.itemList.Add(itemModel);
                    }

                }
            }



            orderAM.num = allNum;
            orderAM.quantity = allQuantity;
            orderAM.smsNum = allSmsNum;
            orderAM.accNum = allAccNum;
            orderAM.money = allMoney;
            return orderAM;
        }

        /// <summary>
        /// 单个产品订单趋势
        /// </summary>
        /// <returns></returns>
        public List<dynamic> GetSingleBussinessOrder(DateTime statTime, DateTime endTime, string keyword)
        {
            //DataSet ds = new DataSet();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select accid,Sys_I200.dbo.SysRpt_ShopInfo.regTime,cast(T_Order_Project.createDate as date) as createDate,T_Order_Project.displayName,busSumMoney from i200.dbo.T_OrderInfo t left join Sys_I200.dbo.SysRpt_ShopInfo on accid=Sys_I200.dbo.SysRpt_ShopInfo.accountid");
            strSql.Append(" left join i200.dbo.T_Order_Project on t.busId=T_Order_Project.busId where T_Order_Project.createDate>@statTime and T_Order_Project.createDate<@endTime and displayName=@keyword ");

            List<dynamic> ds = DapperHelper.Query<dynamic>(strSql.ToString(), new { statTime = statTime, endTime = endTime, keyword = keyword }).ToList();

            return ds;
        }

        /// <summary>
        /// 根据业务Id计算相关产品的成本和利润
        /// </summary>
        /// <param name="busId"></param>
        /// <param name="accId"></param>
        /// <returns></returns>
        /// 所有打包产品都算Saas服务
        /// 单独的短信包和维客短信是第三方服务
        /// 话费充值和实物产品算他营产品
        public OrderItemProp GetListItemProps(int busId, int accId)
        {
            StringBuilder strSql = new StringBuilder();
            OrderItemProp propModel = new OrderItemProp();

            strSql.Append("select * from [i200].[dbo].[T_Order_Project_List] where busId=@busId;");

            try
            {

                List<dynamic> projectList = DapperHelper.Query<dynamic>(strSql.ToString(), new { busId = busId }).ToList();

                //独立产品的处理
                if (projectList.Count == 1)
                {
                    dynamic model = projectList[0];
                    //处理短信
                    if (model.itemId == 1)
                    {
                        propModel.Cost = Convert.ToInt32(model.itemQuantity) * 0.05;
                        //标记为第三方服务
                        propModel.PureSms = "1";
                    }
                    //处理维客短信
                    else if (model.itemName.ToString().IndexOf("维客") >= 0)
                    {
                        T_Sms_ListDAL dal = new T_Sms_ListDAL();
                        double smsCnt = dal.GetFreeSmsCnt(accId);
                        propModel.Cost = smsCnt * 0.05;
                        propModel.isFreeSms = 1;
                        //标记为第三方服务
                        propModel.PureSms = "1";
                    }
                    else if (model.itemName.ToString().IndexOf("充值100") >= 0)
                    {
                        propModel.Cost = 100;
                    }
                    else if (model.itemName.ToString().IndexOf("充值50") >= 0)
                    {
                        propModel.Cost = 50;
                    }
                    else if (model.itemName.ToString().IndexOf("充值30") >= 0)
                    {
                        propModel.Cost = 30;
                    }
                    else
                    {
                        propModel.Cost = 0;
                        propModel.SelfMark = "1";
                    }

                }
                //组合产品的处理
                else if (projectList.Count > 1)
                {
                    dynamic model = projectList[0];
                    //propModel.SelfMark = "1";

                    //处理维客类套餐
                    if (model.itemName.ToString().IndexOf("维客") >= 0)
                    {
                        T_Sms_ListDAL dal = new T_Sms_ListDAL();
                        double smsCnt = dal.GetFreeSmsCnt(accId);
                        propModel.Cost = smsCnt * 0.06;
                        propModel.isFreeSms = 1;
                        //标记为第三方服务
                        propModel.PureSms = "1";
                    }
                    else
                    {
                        foreach (var item in projectList)
                        {
                            if (item.itemId == 1)
                            {
                                propModel.Cost += Convert.ToInt32(model.itemQuantity) * 0.06;
                            }
                        }
                        propModel.SelfMark = "1";
                    }
                }
                //空列表
                else
                {

                }

                return propModel;
            }
            catch (Exception ex)
            {
                Logger.Error("获取用户详情订单摘要信息出错", ex);
                return propModel;
            }

        }
    }
}
