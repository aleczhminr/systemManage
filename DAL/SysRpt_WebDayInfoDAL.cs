using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using CommonLib;
using Dapper;

namespace DAL
{
    public class SysRpt_WebDayInfoDAL
    {
        /// <summary>
        /// 得到当前系统今天的统计信息
        /// </summary>
        /// <param name="type">{新增店铺 "shop",新增会员 "Reg",新增商品 "goods",销售笔数和销售金额 "Sell",销售商品数 "saleGoods",当日支出 "Pay",当日短信 "Sms",当日订单 "Order",客户端登录 "kehuduan",登录 "loginNum"}</param>
        /// <returns></returns>
        public List<dynamic> GetIndexNowData(string type, DateTime nowDay)
        {
            #region 今天统计
            StringBuilder strSql = new StringBuilder();
            //店铺数,会员数,商品数,销售数,销售金额,支出数,支出金额,短信数,订单数,订单金额,微信,销售商品数,客户端,登录人数,IOS注册,安卓注册

            List<dynamic> partData = new List<dynamic>();
            strSql.Append(" declare  ");
            strSql.Append(" @bgTime datetime , ");// 开始时间
            strSql.Append(" @edTime datetime ; ");// 结束时间
            strSql.Append(" set @bgTime=CAST(GETDATE() as date) ; ");// 短信数
            strSql.Append(" set @edTime=GETDATE() ; ");// 订单数
            switch (type)
            {
                //新增店铺
                case "shop":
                    strSql.Append(" select DATEPART(HOUR,RegTime) as Period,COUNT(ID) as shop,sum(case when Remark='11' then 1 else 0 end) as android,sum(case when Remark='10' then 1 else 0 end) as ios ");
                    strSql.Append(" from I200.dbo.T_Account where  RegTime between @BgTime and @EdTime and [State]=1 group by DATEPART(HOUR,RegTime) ");
                    break;
                //新增会员
                case "Reg":
                    strSql.Append(" select DATEPART(HOUR,uRegTime) as Period,COUNT(uid) as Reg from I200.dbo.T_UserInfo where uRegTime between @BgTime and @EdTime group by DATEPART(HOUR,uRegTime) ");
                    break;
                //新增商品
                case "goods":
                    strSql.Append(" select DATEPART(HOUR,gAddTime) as Period,COUNT(*) as goods from I200.dbo.T_GoodsInfo where gAddTime between @BgTime and @EdTime group by DATEPART(HOUR,gAddTime) ");
                    break;
                //销售笔数和销售金额
                case "Sell":
                    strSql.Append(" select DATEPART(HOUR,insertTime) as Period,COUNT(saleID) as Sell,isnull(sum(RealMoney),0) as SellNum from I200.dbo.T_SaleInfo where insertTime between @BgTime and @EdTime and RealMoney<10000 and RealMoney>0  group by DATEPART(HOUR,insertTime) ");
                    break;
                //销售商品数
                case "saleGoods":
                    strSql.Append(" select DATEPART(HOUR,insertTime) as Period,count(DISTINCT GoodsName) as saleGoods from I200.dbo.T_Sale_List where insertTime between @BgTime and @EdTime group by DATEPART(HOUR,insertTime) ");
                    break;
                //当日支出
                case "Pay":
                    strSql.Append(" select DATEPART(HOUR,PayDate) as Period,COUNT(id) as Pay,isnull(SUM(PaySum),0) as PayNum from I200.dbo.T_PayRecord where PayDate between @BgTime and @EdTime group by DATEPART(HOUR,PayDate) ");
                    break;
                //当日短信
                case "Sms":
                    strSql.Append(" select DATEPART(HOUR,sendTime) as Period,isnull(sum(realCnt),0) as Sms from I200.dbo.T_Sms_List where sendTime between @BgTime and @EdTime group by DATEPART(HOUR,sendTime) ");
                    break;
                //当日订单
                case "Order":
                    strSql.Append(" select DATEPART(HOUR,transactionDate) as Period,COUNT(oid) as [Order],isnull(SUM(RealPayMoney),0) as OrderNum from I200.dbo.T_OrderInfo where transactionDate between @BgTime and @EdTime and (OrderStatus=2 or (OrderStatus=1 and OrderTypeId=2)) group by DATEPART(HOUR,transactionDate);");
                    break;
                //客户端登录
                case "kehuduan":
                    strSql.Append(" select DATEPART(HOUR,OperDate) as Period,COUNT(DISTINCT Accountid) as kehuduan from I200.dbo.T_LOG where LogMode like '3%' and OperDate between @BgTime and @EdTime group by DATEPART(HOUR,OperDate); ");
                    break;
                //登录
                case "loginNum":
                    strSql.Append(" select DATEPART(HOUR,OperDate) as Period,COUNT(DISTINCT Accountid) as loginNum from I200.dbo.T_LOG where OperDate between @BgTime and @EdTime group by DATEPART(HOUR,OperDate)");
                    break;

            }
            #endregion

            partData = HelperForFrontend.Query<dynamic>(strSql.ToString(), new { date = nowDay }).ToList();

            return partData;
        }



        /// <summary>
        /// 得到某一天数据
        /// </summary>
        /// <param name="webDate">日期</param>
        /// <returns></returns>
        public PanelShowModel.SomedayDataCount GetModelByDate(DateTime webDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 S_Date nowTime, NewAccNum regNum,saleNum saleNum,saleMoney saleMoney,userNum userNum,addGoodsNum goodsNum,smsNum smsNum,orderNum orderNum,orderMoney orderMoney from SysRpt_WebDayInfo where S_Date=@inserTime;");
            return DapperHelper.GetModel<PanelShowModel.SomedayDataCount>(strSql.ToString(), new { inserTime = webDate.Date });
        }
        /// <summary>
        /// 得到某日此时的数据
        /// </summary>
        /// <param name="webDate"></param>
        /// <returns></returns>
        public PanelShowModel.SomedayDataCount GetThisMomentData(DateTime webDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" declare  ");
            strSql.Append(" @regNum int , ");// 注册数
            strSql.Append(" @saleNum int , ");// 销售数
            strSql.Append(" @saleMoney decimal(18,2) , ");// 销售金额
            strSql.Append(" @userNum int , ");// 会员数
            strSql.Append(" @goodsNum int , ");// 商品数
            strSql.Append(" @smsNum int , ");// 短信数
            strSql.Append(" @orderNum int , ");// 订单数
            strSql.Append(" @orderMoney decimal(18,2); ");// 订单金额

            strSql.Append(" select @regNum=COUNT(ID) from i200.dbo.T_Account where DATEDIFF(day,RegTime,@insertTime)=0 and ");
            strSql.Append("  CAST(RegTime as time)<CAST(@insertTime as time) and State=1; ");

            strSql.Append(" select @saleNum=COUNT(saleID),@saleMoney=SUM(RealMoney) from i200.dbo.T_SaleInfo where DATEDIFF(DAY,insertTime,@insertTime)=0 and  ");
            strSql.Append(" CAST(insertTime as time)<CAST(@insertTime as time) and RealMoney<10000; ");

            strSql.Append(" select @userNum=COUNT(uid) from i200.dbo.T_UserInfo where DATEDIFF(day,uInsertTime,@insertTime)=0 ");
            strSql.Append("  and CAST(uInsertTime as time)<CAST(@insertTime as time); ");

            strSql.Append(" select @goodsNum=COUNT(gid) from i200.dbo.T_GoodsInfo where DATEDIFF(day,gAddTime,@insertTime)=0 ");
            strSql.Append("  and CAST(gAddTime as time)<CAST(@insertTime as time); ");

            strSql.Append(" select @smsNum=COUNT(id) from i200.dbo.T_Sms_List where DATEDIFF(day,sendTime,@insertTime)=0 and  ");
            strSql.Append(" CAST(sendTime as time)<CAST(@insertTime as time) and smsStatus=1; ");

            strSql.Append(" select @orderNum=COUNT(oid),@orderMoney=SUM(RealPayMoney) from i200.dbo.T_OrderInfo where orderStatus=2 and  ");
            strSql.Append(" DATEDIFF(day,transactionDate,@insertTime)=0 and  CAST(transactionDate as time)<CAST(@insertTime as time); ");


            strSql.Append("  select @insertTime nowTime, @regNum  regNum ,@saleNum  saleNum ,isnull(@saleMoney,0)  saleMoney ,");
            strSql.Append("  @userNum  userNum ,@goodsNum  goodsNum , @smsNum  smsNum ,@orderNum  orderNum ,isnull(@orderMoney,0)  orderMoney; ");
            return DapperHelper.GetModel<PanelShowModel.SomedayDataCount>(strSql.ToString(), new { insertTime = webDate });

        }

        /// <summary>
        /// 获取昨日同期对比数据
        /// </summary>
        /// <returns></returns>
        public PanelShowModel.SomedayDataCount GetYesterdayPeerData()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" declare  ");
            strSql.Append(" @regNum int , ");// 注册数
            strSql.Append(" @saleNum int , ");// 销售数
            strSql.Append(" @saleMoney decimal(18,2) , ");// 销售金额
            strSql.Append(" @userNum int , ");// 会员数
            strSql.Append(" @goodsNum int , ");// 商品数
            strSql.Append(" @smsNum int , ");// 短信数
            strSql.Append(" @orderNum int , ");// 订单数
            strSql.Append(" @bgTime datetime , ");// 开始时间
            strSql.Append(" @edTime datetime , ");// 结束时间
            strSql.Append(" @orderMoney decimal(18,2); ");// 订单金额
            strSql.Append(" set @bgTime=CAST(GETDATE()-1 as date) ; ");
            strSql.Append(" set @edTime=GETDATE()-1 ; ");

            strSql.Append(" select @regNum=COUNT(ID) from i200.dbo.T_Account where RegTime between @BgTime and @EdTime and ");
            strSql.Append("  State=1; ");

            strSql.Append(" select @saleNum=COUNT(saleID),@saleMoney=SUM(RealMoney) from i200.dbo.T_SaleInfo where insertTime between @BgTime and @EdTime  ");
            strSql.Append(" and RealMoney<10000; ");

            strSql.Append(" select @userNum=COUNT(uid) from i200.dbo.T_UserInfo where uInsertTime between @BgTime and @EdTime; ");

            strSql.Append(" select @goodsNum=COUNT(gid) from i200.dbo.T_GoodsInfo where gAddTime between @BgTime and @EdTime; ");

            strSql.Append(" select @smsNum=COUNT(id) from i200.dbo.T_Sms_List where sendTime between @BgTime and @EdTime  ");
            strSql.Append(" and smsStatus=1; ");

            strSql.Append(" select @orderNum=COUNT(oid),@orderMoney=SUM(RealPayMoney) from i200.dbo.T_OrderInfo where (OrderStatus=2 or (OrderStatus=1 and OrderTypeId=2)) and  ");
            strSql.Append(" transactionDate between @BgTime and @EdTime; ");


            strSql.Append("  select @regNum  regNum ,@saleNum  saleNum ,isnull(@saleMoney,0)  saleMoney ,");
            strSql.Append("  @userNum  userNum ,@goodsNum  goodsNum , @smsNum  smsNum ,@orderNum  orderNum ,isnull(@orderMoney,0)  orderMoney; ");
            return DapperHelper.GetModel<PanelShowModel.SomedayDataCount>(strSql.ToString());
        }

        /// <summary>
        /// 获取今日当前数据
        /// </summary>
        /// <returns></returns>
        public PanelShowModel.SomedayDataCount GetNowData()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" declare  ");
            strSql.Append(" @regNum int , ");// 注册数
            strSql.Append(" @saleNum int , ");// 销售数
            strSql.Append(" @saleMoney decimal(18,2) , ");// 销售金额
            strSql.Append(" @userNum int , ");// 会员数
            strSql.Append(" @goodsNum int , ");// 商品数
            strSql.Append(" @smsNum int , ");// 短信数
            strSql.Append(" @orderNum int , ");// 订单数
            strSql.Append(" @bgTime datetime , ");// 开始时间
            strSql.Append(" @edTime datetime , ");// 结束时间
            strSql.Append(" @orderMoney decimal(18,2); ");// 订单金额
            strSql.Append(" set @bgTime=CAST(GETDATE() as date) ; ");
            strSql.Append(" set @edTime=GETDATE() ; ");

            strSql.Append(" select @regNum=COUNT(ID) from i200.dbo.T_Account where RegTime between @bgTime and @edTime and ");
            strSql.Append("  State=1; ");

            strSql.Append(" select @saleNum=COUNT(saleID),@saleMoney=SUM(RealMoney) from i200.dbo.T_SaleInfo where insertTime between @bgTime and @edTime  ");
            strSql.Append(" and RealMoney<10000; ");

            strSql.Append(" select @userNum=COUNT(uid) from i200.dbo.T_UserInfo where uInsertTime between @bgTime and @edTime; ");

            strSql.Append(" select @goodsNum=COUNT(gid) from i200.dbo.T_GoodsInfo where gAddTime between @bgTime and @edTime; ");

            strSql.Append(" select @smsNum=COUNT(id) from i200.dbo.T_Sms_List where sendTime between @bgTime and @edTime  ");
            strSql.Append(" and smsStatus=1; ");

            strSql.Append(" select @orderNum=COUNT(oid),@orderMoney=SUM(RealPayMoney) from i200.dbo.T_OrderInfo where (OrderStatus=2 or (OrderStatus=1 and OrderTypeId=2)) and  ");
            strSql.Append(" transactionDate between @bgTime and @edTime; ");


            strSql.Append("  select @regNum  regNum ,@saleNum  saleNum ,isnull(@saleMoney,0)  saleMoney ,");
            strSql.Append("  @userNum  userNum ,@goodsNum  goodsNum , @smsNum  smsNum ,@orderNum  orderNum ,isnull(@orderMoney,0)  orderMoney; ");
            return DapperHelper.GetModel<PanelShowModel.SomedayDataCount>(strSql.ToString());
        }

        /// <summary>
        /// 得到某段时间内的平均数据
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public PanelShowModel.SomedayDataCount GetAverageData(DateTime startTime,DateTime endTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" declare @dateLeng int; ");
            strSql.Append(" set @dateLeng=DATEDIFF(DAY,@startTime,@endTime); ");
            strSql.Append(" if(isnull(@dateLeng,0)<1) ");
            strSql.Append(" begin ");
            strSql.Append("     set @dateLeng=1; ");
            strSql.Append(" end ");
            strSql.Append(" select SUM(NewAccNum)/@dateLeng regNum,SUM(saleNum)/@dateLeng saleNum,SUM(saleMoney)/@dateLeng saleMoney,SUM(userNum)/@dateLeng userNum, ");
            strSql.Append(" SUM(addGoodsNum)/@dateLeng goodsNum,SUM(smsNum)/@dateLeng smsNum,SUM(orderNum)/@dateLeng orderNum,SUM(orderMoney)/@dateLeng orderMoney  ");
            strSql.Append("  from SysRpt_WebDayInfo where S_Date>=@startTime and S_Date<@endTime; ");


            return DapperHelper.GetModel<PanelShowModel.SomedayDataCount>(strSql.ToString(), new { startTime = startTime, endTime = endTime });
        }

        /// <summary>
        /// 返回每月汇总增长数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<dynamic> GetMonthlyData(DateTime dt)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "declare @acc int;" +
                "select @acc=count(Id) from i200.dbo.T_Account where DATEDIFF(month,@month,regTime)=0 and state=1;" +
                "select @acc as Account,ISNULL(SUM(saleNum),0) as Sale,ISNULL(SUM(userNum),0) as Usr,ISNULL(SUM(addGoodsNum),0) as Goods,ISNULL(SUM(smsNum),0) as Sms,ISNULL(SUM(orderNum),0) as Ord,ISNULL(sum(orderMoney),0) as ordMon " +
                "from SysRpt_WebDayInfo where DATEDIFF(year,@year,S_Date)=0 and DATEDIFF(month,@month,S_Date)=0");

            List<dynamic> list =
                DapperHelper.Query<dynamic>(strSql.ToString(), new {year = dt, month = dt}).ToList();

            if (DateTime.Now.Month == dt.Month)
            {
                PanelShowModel.SomedayDataCount tData = GetNowData();

                //list[0].Account += tData.regNum;
                list[0].Sale += tData.saleNum;
                list[0].Usr += tData.userNum;
                list[0].Goods += tData.goodsNum;
                list[0].Sms += tData.smsNum;
                list[0].ordMon += tData.orderMoney;
            }
                    

            return list;
        }

        /// <summary>
        /// 返回平台所有数据
        /// </summary>
        /// <returns></returns>
        public dynamic GetAllData()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select SUM(NewAccNum) as Account,SUM(saleNum) as Sale,SUM(userNum) as Usr,SUM(addGoodsNum) as Goods,SUM(smsNum) as Sms,SUM(orderNum) as Ord,sum(orderMoney) as ordMon " +
                "from SysRpt_WebDayInfo;");

            dynamic list =
                DapperHelper.Query<dynamic>(strSql.ToString()).ToList()[0];

            return list;
        }

    }
}
